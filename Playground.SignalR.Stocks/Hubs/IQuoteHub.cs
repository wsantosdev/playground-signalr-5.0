using System.Threading.Tasks;
using Playground.SignalR.Stocks.Models;

namespace Playground.SignalR.Stocks.Hubs
{
    public interface IQuoteHub
    {
        Task SendQuote(QuoteResponse quote);
    }
}