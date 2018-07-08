using Flurl;
using Flurl.Http;
using Leo.Core.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Vendors.Plaid.Client
{
    public class PlaidClient : IPlaidClient
    {
        private const string _settingsPrefix = "plaid";
        private readonly ISettingsProvider _settings;

        public PlaidClient(ISettingsProvider settings)
        {
            _settings = settings;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public Task<ExchangeTokenResponse> ExchangeToken(string publicToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(publicToken)) throw new ArgumentNullException(nameof(publicToken));

            var config = _settings.Get<PlaidConfig>(_settingsPrefix);

            ExchangeTokenRequest request = new ExchangeTokenRequest(config.ClientId, config.ClientSecret, publicToken);

            return config.Url.RequestJsonAsync<ExchangeTokenResponse>(request, cancellationToken);
        }

        public Task<GetCategoriesResponse> GetCategories(CancellationToken cancellationToken = default(CancellationToken))
        {
            var config = _settings.Get<PlaidConfig>(_settingsPrefix);
            var request = new GetCategoriesRequest();
            return config.Url.RequestJsonAsync<GetCategoriesResponse>(request, cancellationToken);
        }

        public Task<IEnumerable<Institution>> InstitutionSearch(string query, Products? product = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            var config = _settings.Get<PlaidConfig>(_settingsPrefix);

            var url = config.Url.AppendPathSegments("institutions", "search").SetQueryParam("q", query);
            if (product.HasValue)
                url = url.SetQueryParam("p", product.Value.ToString());

            return url.GetJsonAsync<IEnumerable<Institution>>(cancellationToken);
        }

        public Task<RetrieveAuthResponse> RetrieveAuth(string accessToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var config = _settings.Get<PlaidConfig>(_settingsPrefix);

            RetrieveAuthRequest request = new RetrieveAuthRequest(config.ClientId, config.ClientSecret, accessToken);

            return config.Url.RequestJsonAsync<RetrieveAuthResponse>(request, cancellationToken);
        }

        public Task<RetrieveBalancesResponse> RetrieveBalances(string accessToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var config = _settings.Get<PlaidConfig>(_settingsPrefix);

            RetrieveBalancesRequest request = new RetrieveBalancesRequest(config.ClientId, config.ClientSecret, accessToken);

            return config.Url.RequestJsonAsync<RetrieveBalancesResponse>(request, cancellationToken);
        }

        public async Task<RetrieveTransactionsResponse> RetrieveTransactions(string accessToken, DateTime start, DateTime end, IEnumerable<string> accountIds = null, ushort count = RetrieveTransactionsOptions.DefaultCount, uint offset = RetrieveTransactionsOptions.DefaultOffset, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var config = _settings.Get<PlaidConfig>(_settingsPrefix);

            RetrieveTransactionsRequest request = new RetrieveTransactionsRequest(config.ClientId, config.ClientSecret, accessToken, start, end, accountIds, count, offset);

            try
            {
                return await config.Url.RequestJsonAsync<RetrieveTransactionsResponse>(request, cancellationToken);
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var error = ex.GetResponseJson<ItemError>();
                    if (error != null && error.ErrorCode == ItemErrorCodes.PRODUCT_NOT_READY)
                    {
                        await Task.Delay(500, cancellationToken);
                        return await RetrieveTransactions(accessToken, start, end, accountIds, count, offset, cancellationToken);
                    }
                }

                throw;
            }
        }

        public Task<UpdateWebhookResponse> UpdateWebhook(string accessToken, string webhook, CancellationToken cancellationToken = default(CancellationToken))
        {
            var config = _settings.Get<PlaidConfig>(_settingsPrefix);
            var request = new UpdateWebhookRequest(config.ClientId, config.ClientSecret, accessToken, webhook);
            return config.Url.RequestJsonAsync<UpdateWebhookResponse>(request, cancellationToken);
        }
    }
}