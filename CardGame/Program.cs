using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            List<Card> test = new List<Card>();

            for (int i = 0; i < 10; ++i)
            {
                Card card = deck.TopCard();
                if (card != null)
                {
                    test.Add(card);
                }
            }

            foreach (Card card in test)
            {
                if (card != null)
                    Console.WriteLine(card.Rank);
            }
            Console.WriteLine("Number of items: " + test.Count);
            Console.WriteLine();

            test[0] = null;
            test[1] = null;


            test.RemoveAll(card => card == null);


            foreach (Card card in test)
            {
                if (card != null)
                    Console.WriteLine(card.Rank);
            }
            Console.WriteLine("Number of items: " + test.Count);


        }
    }
}
