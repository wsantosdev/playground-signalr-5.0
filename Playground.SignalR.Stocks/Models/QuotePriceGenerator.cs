using System;

namespace Playground.SignalR.Stocks.Models
{
    public class QuotePriceGenerator
    {
        private const int MinimumPrice = 10;
        private const int MaximumPrice = 30;
        private const int PriceTreshold = 35;
        private readonly Random _random = new Random();

        public decimal Generate(decimal previousPrice)
        {
            var modifier = (decimal)_random.NextDouble();

            if(previousPrice == 0)
                return _random.Next(MinimumPrice, MaximumPrice) + modifier;

            var updatedPrice = previousPrice + ((modifier > 0.6m ? modifier : modifier * -1) / 100);

            if(updatedPrice > PriceTreshold)
                return MaximumPrice + modifier;

            if(updatedPrice < MinimumPrice)
                return MinimumPrice + modifier;

            return updatedPrice;
        }
    }
}