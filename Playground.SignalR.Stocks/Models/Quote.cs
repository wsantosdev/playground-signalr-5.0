using System;

namespace Playground.SignalR.Stocks.Models
{
    public class Quote
    {
        public string Symbol { get; }
        public decimal Price { get; private set; }
        public DateTime Time { get; private set; }

        private Quote(string symbol) =>
            Symbol = symbol;

        public static Quote Create(string symbol) =>
            new Quote(symbol);

        public void Update(decimal price)
        {
            Price = price;
            Time = DateTime.Now;
        }
    }
}