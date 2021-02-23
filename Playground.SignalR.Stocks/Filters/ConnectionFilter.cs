using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Playground.SignalR.Stocks.Filters
{
    public class ConnectionFilter : IHubFilter
    {
        public Task OnConnectedAsync(HubLifetimeContext context,
                                     Func<HubLifetimeContext, Task> next)
        {
            Console.WriteLine($"New client connected. Connection ID: {context.Context.ConnectionId}");

            return next(context);
        }

        public Task OnDisconnectedAsync(HubLifetimeContext context,
                                        Exception? exc,
                                        Func<HubLifetimeContext, Exception?, Task> next)
        {
            Console.WriteLine($"Client with connection ID {context.Context.ConnectionId} has disconnected.");
            if (exc != null)
                Console.WriteLine($"Disconnection exception: {exc}");

            return next(context, exc);
        }
    }
}