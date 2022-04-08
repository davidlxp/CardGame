using System;
using System.Collections.Generic;

namespace CardGame
{
    public class System
    {
        Deck deck = new Deck();                        // Init Deck of cards
        Gameboard gameboard = new Gameboard();         // Init Gameboard 
        ElevenChecker checker = new ElevenChecker();   // Init Checker for the game
        List<Player> players = new List<Player>();     // A list of players in the system

        int boardCardsNum;                             // Num of cards on board for this game

        /// <summary>
        /// Constructor of the system
        /// </summary>
        public System(int boardCardsNum1)
        {
            boardCardsNum = boardCardsNum1;
        }

        public void startGame()
        {

        }

        public void endGame()
        {

        }

        public void checkPlayer()
        {

        }

        private void initCardsOnBoard()
        {

        }

        public void collectReplaceRequest()
        {

        }

        public void handleReplaceRequest()
        {

        }

        void replaceCards()
        {

        }

        void alertInvalidRequest()
        {

        }

    }
}
