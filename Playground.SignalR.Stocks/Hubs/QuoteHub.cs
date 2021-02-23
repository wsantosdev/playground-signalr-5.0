using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Playground.SignalR.Stocks.Models;

namespace Playground.SignalR.Stocks.Hubs
{
    public class QuoteHub : Hub<IQuoteHub>
    {
        public async Task ChangeSubscription(QuoteRequest request)
        {
            if(!string.IsNullOrEmpty(request.CurrentSymbol))
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, request.CurrentSymbol);

            await Groups.AddToGroupAsync(Context.ConnectionId, request.NewSymbol);
        }
    }
}