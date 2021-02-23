using Microsoft.AspNetCore.SignalR;
using Playground.SignalR.Stocks.Models;
using System;
using System.Threading.Tasks;

namespace Playground.SignalR.Stocks.Filters
{
    public class QuoteHubFilter : IHubFilter
    {
        public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext,
                                                          Func<HubInvocationContext,
                                                          ValueTask<object?>> next)
        {
            var request = invocationContext.HubMethodArguments[0] as QuoteRequest;

            try
            {
                if(request != null)
                    Console.WriteLine($"{invocationContext.Context.ConnectionId} has selected {request.NewSymbol}.");

                return await next(invocationContext);
            }
            catch (Exception exc)
            {
                if(request != null)
                    Console.WriteLine($"Error switching symbol to '{request.NewSymbol}': {exc}");

                throw;
            }
        }
    }
}