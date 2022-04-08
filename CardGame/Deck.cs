using System;
using System.Collections.Generic;

namespace CardGame
{
    public class Deck
    {
        List<Card> cards = new List<Card>();

        string[] suit = { "Hearts", "Diamonds", "Spades", "Clubs"};

        /* Constructor */
        public Deck()
        {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 13; ++j)
                    cards.Add(new Card(j + 1, suit[i]));
        }

        /* Get all the cards */
        public ref List<Card> Cards
        {
            get { return ref cards; }
        }

        /* Get the top card from deck (also remove the card from deck) */
        public Card TopCard()
        {
            if (cards.Count > 0) {
                int idxOfLast = cards.Count - 1;
                Card topCard = cards[idxOfLast];
                cards.RemoveAt(idxOfLast);
                return topCard;
            }
            return null;
        }

        /* Get how many cards in the deck */
        public int getCardsNum()
        {
            return cards.Count;
        }

        /* Shuffle the deck */
        public void shuffle()
        {
            Random rand = new Random();
            for (int i = cards.Count - 1; i > 0; i--)       // no need "i >= 0" b/c only one card left, no need to shuffle
            {
                int randomIdx = rand.Next(i+1);             // in "Next(X)", X is the maximum, all number will fall within [0,X)
                Card tempCard = cards[i];                   // hold the last card (dynamic)
                cards[i] = cards[randomIdx];                // put the random card to the last place 
                cards[randomIdx] = tempCard;                // put the last card to random index
            }
        }

        /* Print all the cards */
        public void print()
        {
            for (int i = 0; i < cards.Count; ++i)
                    Console.WriteLine(cards[i].Rank + " " + cards[i].Suit);
        }


    }
}
