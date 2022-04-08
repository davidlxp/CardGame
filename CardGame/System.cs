using System;
using System.Collections.Generic;

namespace CardGame
{
    public class System
    {
        Deck deck = new Deck();                                                    // Init Deck of cards
        Gameboard gameboard = new Gameboard();                                     // Init Gameboard 
        ElevenChecker checker = new ElevenChecker();                               // Init Checker for the game
        Dictionary<string, string> credential = new Dictionary<string, string>();  // A dictionary store player name and their password
        Dictionary<string, Player> playerInfo = new Dictionary<string, Player>();  // A dictionary store player name and their detailed information

        int numCardsOnBoard;                                                       // Number of cards on gameboard for this game


        /// <summary>
        /// Constructor of the system
        /// </summary>
        public System(int numCardsOnBoard1)
        {
            numCardsOnBoard = numCardsOnBoard1;
        }

        public void startGame()
        {
            /* Login the existing player or register the new players */
            checkPlayer();

            /* Say hi to the players */
            welcomePlayer();

            /* Initialize cards on gameboard */
            initCardsOnBoard();




        }

        public void endGame()
        {
            /* When game end, reinitialize all the utilities in the game 
             * so the data will removed before next player using the system */
            deck = new Deck();
            gameboard = new Gameboard();


        }

        public void checkPlayer()
        {

        }

        public void welcomePlayer()
        {

        }


        /// <summary>
        /// In the beginning, draw N cards (N = Max number of cards on borad for this game) from deck
        /// and place them on the gameboard based on the requirement
        /// </summary>
        private void initCardsOnBoard()
        {
            addCardsToBoard(numCardsOnBoard);
        }

        /// <summary>
        /// Function draw N cards from deck and place them on the gameboard
        /// </summary>
        /// <param name="num"> number of cards to be drawn </param>
        private void addCardsToBoard(int num)
        {
            for (int i = 0; i < num; ++i)
            {
                Card card = deck.TopCard();
                if (card != null)
                    gameboard.addCardToBoard(card);
            }
        }


        /// <summary>
        /// Function collects replacement request (a string) provided by the player, and pass
        /// into gameboard to parse and store it.
        /// </summary>
        /// <param name="replaceRequest"></param>
        public void handleReplaceRequest(string replaceRequest)
        {
            gameboard.setCardsReplaceIdx(replaceRequest);
        }


        /// <summary>
        /// Function removes cards selected by player from gameboard, and draw same number of cards from deck and add
        /// back to the gameboard (If cards available in the deck)
        /// </summary>
        void replaceCards()
        {
            int numReplace = gameboard.cardsReplaceIdx.Count;           // track the number of cards to replace

            foreach (int i in gameboard.cardsReplaceIdx)                // set all the cards at index for replacing to NULL
                gameboard.cardsOnBoard[i] = null;

            gameboard.cardsOnBoard.RemoveAll(card => card == null);     // remove all the NULL records from "cardsOnBoard" list

            addCardsToBoard(numReplace);                                // draw same number of cards from deck and add back to "cardsOnBoard" list
        }


        /// <summary>
        /// Function alert the player about invalid replacing request
        /// </summary>
        void alertInvalidRequest()
        {
            Console.WriteLine("The cards you selected for replacing are INVALID! Please choose again!");
        }

    }
}
