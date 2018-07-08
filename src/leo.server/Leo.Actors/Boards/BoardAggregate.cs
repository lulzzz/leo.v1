using Leo.Actors.Interfaces.Boards;
using Leo.Core.Orleans.EventSourcing;
using System.Threading.Tasks;
using Orleans;
using Leo.Vendors.Plaid.Client;
using System.Threading;
using System;
using System.Linq;
using Leo.Core.Orleans;
using System.Collections.Generic;
using Orleans.Runtime;
using Leo.Core.Settings;

namespace Leo.Actors.Boards
{
    public class BoardAggregate : EventSourcedGrain<BoardAggregateState>, IBoardAggregate
    {
        private readonly IPlaidClient _plaid;
        private readonly ISettingsProvider _settings;        
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public BoardAggregate(IPlaidClient client, ISettingsProvider settings)
        {
            _plaid = client ?? throw new ArgumentNullException(nameof(client));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task CreateBoard(CreateBoard command)
        {
            List<IEvent> events = new List<IEvent>();
            events.Add(new BoardCreated(command.Name, command.BoardId, command.UserId));

            //var response = await _plaid.GetCategories(_tokenSource.Token);
            //var categoriesAdded = response.Categories.Select(c => new CategoryAdded(command.BoardId, c.CategoryId, c.Group, c.Hierarchy));
            //events.AddRange(categoriesAdded);

            //var systemEnvelopes = new List<EnvelopeAdded>()
            //{
            //    new EnvelopeAdded("sys-env-starting-balance", "Starting Balance", new string[]{ }, command.BoardId, command.UserId),
            //    new EnvelopeAdded("sys-env-funding", "Funding", new string[] { }, command.BoardId, command.UserId),
            //    new EnvelopeAdded("sys-env-income", "Income", new string[] { }, command.BoardId, command.UserId),
            //    new EnvelopeAdded("sys-env-rollover", "Rollover", new string[] { }, command.BoardId, command.UserId),
            //    new EnvelopeAdded("sys-env-credit-card-payments", "Credit Card Payments", new string[] { }, command.BoardId, command.UserId)
            //};
            //events.AddRange(systemEnvelopes);

            RaiseEvents(events);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Interfaces.Boards.Category>> SearchCategories(string search)
        {
            IEnumerable<Interfaces.Boards.Category> rvalues = State.Categories
                .Where(c => c.Value.Hierarchy.Any(h => h.ToLower().Contains(search.ToLower())))
                .Select(c => c.Value)
                .OrderBy(c => c.CategoryId)
                .Take(15)
                .ToList();

            return Task.FromResult(rvalues);
        }

        public async Task<bool> HasBoard(string userid)
        {
            await RefreshNow();
            return State.UserIds != null && State.UserIds.Contains(userid);
        }

        public Task AddEnvelope(AddEnvelope command)
        {
            var @event = new EnvelopeAdded(command.Id, command.Name, command.Categories, command.BoardId, command.UserId);
            RaiseEvent(@event);
            return Task.CompletedTask;
        }

        public async Task AddAccounts(AddAccounts command)
        {
            try
            {
                var exchangeTokenResponse = await _plaid.ExchangeToken(command.PublicToken, _tokenSource.Token);
                if (exchangeTokenResponse != null && !string.IsNullOrEmpty(exchangeTokenResponse.AccessToken))
                {
                    List<IEvent> events = new List<IEvent>();

                    var bankAddedEvent = new BankAdded(command.BankId, command.BankName, exchangeTokenResponse.AccessToken, command.BoardId);
                    events.Add(bankAddedEvent);

                    var config = _settings.Get<TransactionWebhookConfig>(TransactionWebhookConfig.SettingsPrefix);
                    var url = config.Url.Replace("{boardid}", command.BoardId);
                    var webhookResponse = await _plaid.UpdateWebhook(exchangeTokenResponse.AccessToken, url, _tokenSource.Token);
                    var bankWebhookAdded = new BankWebhookAdded(command.BankId, webhookResponse?.Item?.Webhook, command.BoardId);
                    events.Add(bankWebhookAdded);

                    var authResponse = await _plaid.RetrieveBalances(exchangeTokenResponse.AccessToken, _tokenSource.Token);
                    if (authResponse?.Accounts?.Any() ?? false)
                    {
                        var accountAddedEvents = authResponse.Accounts.Select(a =>
                            new AccountAdded(command.BoardId, exchangeTokenResponse.AccessToken, a.AccountId, command.BankName, command.BankId, a.Name, a.Type, a.OfficialName, a.SubType, a.Balances.Available, a.Balances.Current, a.Balances.Limit)
                        );
                        events.AddRange(accountAddedEvents);
                        RaiseEvents(events);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task PullTransactions(PullTransactions command)
        {
            ushort pageSize = 100;
            var lastPageSize = 0;
            var page = 0;

            var responses = new List<RetrieveTransactionsResponse>();
            do
            {
                var response = await _plaid.RetrieveTransactions(State.Banks[command.ItemId].AccessToken, command.Start, command.End, count: pageSize, offset: (ushort)(page * lastPageSize));
                responses.Add(response);
                lastPageSize = response.Transactions.Count();
                page++;
            } while (lastPageSize == pageSize);

            var transactionsPulled = responses.SelectMany(r => r.Transactions).Select(t => new TransactionPulled(t.TransactionId, command.BoardId, t.AccountId, 
                t.TransactionType, t.Amount, t.Date, t.CategoryId, t.Category, t.Location?.Coordinates?.Latitude, t.Location?.Coordinates?.Longitude, 
                t.Location?.Address, t.Location?.City, t.Location?.State, t.Location?.Zip, t.PaymentMeta?.Payee, t.PaymentMeta?.PpdId, t.PaymentMeta?.ReferenceNumber));

            RaiseEvents(transactionsPulled);
        }

        public override Task OnDeactivateAsync()
        {
            _tokenSource.Cancel();
            return base.OnDeactivateAsync();
        }

        public Task<IEnumerable<AccountGroup>> GetAccountGroupsAsync()
        {
            IEnumerable<AccountGroup> rvalues = State.Accounts
                .GroupBy(a => a.Value.Type)
                .Select(t => new AccountGroup
                {
                    Name = t.Key,
                    Accounts = t.Select(a => new AccountSummary
                    {
                        AccountId = a.Value.Id,
                        AccountName = a.Value.Name,
                        Current = a.Value.Balance.Current
                    }).ToArray(),
                    Total = t.Sum(a => a.Value.Balance.Current ?? 0)
                }).ToList();

            return Task.FromResult(rvalues);
        }

        public Task<Board> GetBoardAsync()
        {
            return Task.FromResult(new Board { Id = this.GetPrimaryKeyString(), Name = State.Name });
        }

        public Task<IEnumerable<TransactionGroup>> GetTransactionGroupsAsync()
        {
            IEnumerable<TransactionGroup> rvalues = State.Transactions
                .Select(t => t.Value)
                .GroupBy(t => t.TransactionDate)
                .Select(t => new TransactionGroup
                {
                    TransactionDate = t.Key,
                    Transactions = t.ToList()
                })
                .ToList();

            return Task.FromResult(rvalues);
        }

        public Task<IEnumerable<Envelope>> GetEnvelopesAsync()
        {
            IEnumerable<Envelope> rvalues = State.Envelopes.Select(e => e.Value).ToList();
            return Task.FromResult(rvalues);
        }
    }
}