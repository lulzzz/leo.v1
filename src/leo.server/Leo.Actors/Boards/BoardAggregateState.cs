using Leo.Actors.Interfaces.Boards;
using Leo.Core.Orleans.EventSourcing;
using System.Collections.Generic;

namespace Leo.Actors.Boards
{
    public class BoardAggregateState : AggregateRoot
    {
        public string Name { get; private set; }

        public List<string> UserIds { get; private set; } = new List<string>();

        public IDictionary<string, Envelope> Envelopes { get; private set; } = new Dictionary<string, Envelope>();

        public IDictionary<string, Account> Accounts { get; private set; } = new Dictionary<string, Account>();

        public IDictionary<string, Bank> Banks { get; private set; } = new Dictionary<string, Bank>();

        public IDictionary<string, Transaction> Transactions { get; private set; } = new Dictionary<string, Transaction>();

        public IDictionary<string, Category> Categories { get; private set; } = new Dictionary<string, Category>();

        public void Apply(BoardCreated @event)
        {
            Name = @event.BoardName;
            UserIds.Add(@event.UserId);
        }

        public void Apply(AccountAdded e)
        {
            var balance = new AccountBalance(e.Available, e.Current, e.Limit);
            Accounts[e.AccountId] = new Account(e.AccountId, e.Name, e.OfficialName, e.Type, e.SubType, e.AccessToken, e.BankId, e.BankName, e.Timestamp, balance);
        }

        public void Apply(TransactionPulled e)
        {
            var coordinates = new Coordinates(e.Latitude, e.Longitude);
            var location = new Location(e.Address, e.City, coordinates, e.State, e.Zip);
            var meta = new PaymentMeta(e.Payee, e.PpdId, e.ReferenceNumber);
            Transactions[e.TransactionId] = new Transaction(e.TransactionId, e.AccountId, e.Amount, e.Date, e.TransactionType, e.CategoryId, e.Timestamp, location, meta);
        }

        public void Apply(TransactionEnvelopeAssigned e)
        {
            Transactions[e.TransactionId].AssignEnvelope(e.MatchedEnvelopeId);
        }

        public void Apply(EnvelopeAdded e)
        {
            Envelopes[e.Id] = new Envelope(e.Id, e.Name, e.Categories);
        }

        public void Apply(CategoryAdded e)
        {
            Categories[e.CategoryId] = new Category(e.CategoryId, e.Group, e.Hierarchy);
        }

        public void Apply(BankAdded e)
        {
            Banks[e.Id] = new Bank(e.Id, e.Name, e.AccessToken, e.Timestamp);
        }

        public void Apply(BankWebhookAdded e)
        {
            Banks[e.BankId].AddWebhook(e.Url);
        }
    }
}