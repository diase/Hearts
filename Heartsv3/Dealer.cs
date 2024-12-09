using System;
using System.Collections.Generic;

namespace Heartsv3
{
    public class Dealer
    {
        private List<int> Deck;

        public Dealer()
        {
            Deck = new List<int>();
            for (int i = 0; i < 52; i++)
            {
                Deck.Add(i);
            }
        }

        public List<int> Shuffle()
        {
            Random rng = new Random();
            int n = Deck.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1); // Get a random index between 0 and i
                // Swap elements
                int temp = Deck[i];
                Deck[i] = Deck[j];
                Deck[j] = temp;
            }
            return Deck;
        }
    }
}