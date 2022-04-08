using System;
using System.Collections.Generic;

namespace CardGame
{
    public class Gameboard
    {
        int gamePlayed;                                    // round of games played between "start game" and "end game"
        int gameWon;                                       // round of games won

        public List<Player> playersInGame = new List<Player>();    // all players participated in the game when game is active
        public List<Card> cardsOnBoard = new List<Card>();         // the cards which on the gameboard
        public List<int> cardsReplaceIdx = new List<int>();        // the index of cards selected for replacement


        public Gameboard()
        {
            gameWon = 0;
            gamePlayed = 0;
        }

        /// <summary>
        /// Function adds 1 new Card to the gameboard
        /// </summary>
        public void addCardToBoard(Card card)
        {
            cardsOnBoard.Add(card);
        }


        /// <summary>
        /// Function prints all the cards on gameboard to the console
        /// </summary>
        public void showCardsOnBoard()
        {
            for (int i = 0; i < getBoardCardsNum(); ++i)
            {
                Card card = cardsOnBoard[i];
                string suit = card.Suit;
                int rank = card.Rank;

                Console.WriteLine((i + 1) + ". " + suit + " " + rank);
            }
            Console.WriteLine();
        }


        /// <summary>
        /// Function gets number of cards on the gameboard
        /// </summary>
        public int getBoardCardsNum()
        {
            return cardsOnBoard.Count;
        }


        /// <summary>
        /// Function shows the player how many cards still remains in the deck
        /// </summary>
        public void showDeckCardsNum(int deckCardsNum)
        {
            Console.WriteLine($"{deckCardsNum} undealt cards remain in the Deck.");
        }


        /// <summary>
        /// Function transform a string of user selected cards to replace into
        /// integers (index of cards), and store in the "cardsReplaceIdx" list
        /// </summary>
        public void setCardsReplaceIdx(string request)
        {
            string rmSpace = request.Replace(" ", "");      // remove any white space
            string[] split = request.Split(",");            // split to get number

            foreach (string s in split)
            {
                int idx = -1;
                Int32.TryParse(s, out idx);                 // try to parse string to int

                cardsReplaceIdx.Add(idx);                   // add the index of cards to replace to list
            }
        }


        /// <summary>
        /// Function clears the list for storing indexes of cards for removing
        /// </summary>
        public void cleanCardsReplaceIdx()
        {
            cardsReplaceIdx.Clear();
        }


        /// <summary>
        /// Function to increment gameWon variable
        /// </summary>
        public void updateGameWon()
        {
            gameWon++;
        }


        /// <summary>
        /// Function to return number of game won
        /// </summary>
        public int getGameWon()
        {
            return gameWon;
        }


        /// <summary>
        /// Function to increment gamePlayed variable
        /// </summary>
        void updateGamePlayed()
        {
            gamePlayed++;
        }


        /// <summary>
        /// Function to return number of game played
        /// </summary>
        int getGamePlayed()
        {
            return gamePlayed;
        }


        /// <summary>
        /// Function to show x/y game won
        /// </summary>
        void showWonHistory()
        {
            Console.WriteLine($"You've won {gameWon} out of {gamePlayed} games.");
        }

    }
}
