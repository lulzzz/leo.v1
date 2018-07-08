using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Vendors.Plaid.Client
{
    public interface IPlaidClient
    {
        Task<ExchangeTokenResponse> ExchangeToken(string publicToken, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Institution>> InstitutionSearch(string query, Products? product = default(Products?), CancellationToken cancellationToken = default(CancellationToken));

        Task<RetrieveAuthResponse> RetrieveAuth(string accessToken, CancellationToken cancellationToken = default(CancellationToken));

        Task<RetrieveTransactionsResponse> RetrieveTransactions(string accessToken, DateTime start, DateTime end, IEnumerable<string> accountIds = null, ushort count = RetrieveTransactionsOptions.DefaultCount, uint offset = RetrieveTransactionsOptions.DefaultOffset, CancellationToken cancellationToken = default(CancellationToken));
    }
}