using MessagePack;
using System;

namespace Playground.SignalR.Stocks.Models
{
    [MessagePackObject(true)]
    public record QuoteRequest(string CurrentSymbol, string NewSymbol);

    [MessagePackObject(true)]
    public record QuoteResponse(string Symbol, decimal Price, DateTime Time);
}