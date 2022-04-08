using System;
using System.Collections.Generic;

namespace CardGame
{
    public class Player
    {
        // the cards a player holds
        List<Card> cardsOnHand = new List<Card>();

        // function to add a card to player
        public void addCard(Card card)
        {
            cardsOnHand.Add(card);
        }

        // function to return a list of cards on hand
        public ref List<Card> getCards() {
            return ref cardsOnHand;
        }
    }
}
