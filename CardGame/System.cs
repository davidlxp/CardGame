using System;
using System.Collections.Generic;

namespace CardGame
{
    public class System
    {
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


        /// <summary>
        /// Function run the program of this game system
        /// </summary>
        public void runProgram()
        {
            int choice = mainMenu();

            while (choice != 2)
            {
                if (choice == 1)
                    startGame();
                else
                    Console.WriteLine("Invalid selection, please reselect!");

                choice = mainMenu();
            }

        }




        private void credentialCheck()
        {

        }


        /// <summary>
        /// Function takes user's choice of this program
        /// </summary>
        /// <returns> The choice which player made </returns>
        public int mainMenu()
        {
            string choiceStr;
            int choice = 0;

            Console.WriteLine();
            Console.WriteLine("<*>------ CardGame11 ------<*>");

            while (choice == 0)
            {
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Exit the Program");
                Console.Write("Please make a choice by typing its index: ");

                choiceStr = Console.ReadLine();
                Int32.TryParse(choiceStr, out choice);

                if (choice == 0)
                    Console.WriteLine("Invalid, please type an integer!\n");
            }
            return choice;
        }


        /// <summary>
        /// Function to initiaze and start the game
        /// </summary>
        private void startGame()
        {
            /* Initialize deck and gameboard for this game */
            Deck deck = new Deck();
            Gameboard gameboard = new Gameboard();

            /* Say hi and introduce the rule of game */
            welcomePlayer();

            /* Game start to run */
            string choice = "y";
            while (choice == "y")
            {
                gameboard.updateGamePlayed();                                    // adding one played game to gameboard

                deck.shuffle();                                                  // shuffle the cards in deck
                addCardsToBoard(numCardsOnBoard, ref deck, ref gameboard);       // initialize the gameboard by adding cards to gameboard from deck                       

                gameboard.showCardsOnBoard();                                    // show the cards on the gameboard
                string gameStatus = checker.checkGameStatus(ref gameboard);      // variable to track the status of game. Initialize it by check the current gameboard status


                /* if "checker" validated that the game status is "ongoing" */
                while (gameStatus == "Ongoing")                                  
                {
                    getReplaceRequest(ref gameboard);                            // get replacement request from user
                    handleReplaceRequest(ref deck, ref gameboard);               // handle this request ---> grant or reject

                    gameboard.showDeckCardsNum(deck.getCardsNum());              // show number of cards on the gameboard after each replacement request                  

                    gameStatus = checker.checkGameStatus(ref gameboard);         // recheck game status after handling each replacement request

                    gameboard.showCardsOnBoard();                                // show cards on gameboard for the next turn if game status is "onging"
                }


                /* handle "Won" and "Lose" scenarios */
                if (gameStatus == "Win")
                {
                    Console.WriteLine("Congratulation! You won the game.");
                    gameboard.updateGameWon();                                   // adding one winning record
                }
                else
                    Console.WriteLine("No more card can be removed. You lose the game!");

                /* wrap up */
                gameboard.showWonHistory();                                      // showing "You've won x/y games"
                choice = continueMenu();                                         // ask user if they want to have the next round of game

                if (choice == "y")
                    prepareNextRound(ref deck, ref gameboard);                   // prepare for the next round of game
            }

        }


        /// <summary>
        /// Function lets user choose to have another round of game or exit the game
        /// </summary>
        private string continueMenu()
        {
            string choice = "";

            while (choice != "y" && choice != "n")
            {
                Console.WriteLine();
                Console.Write("Play another round of game? (y/n): ");

                choice = Console.ReadLine().ToLower();

                if (choice != "y" && choice != "n")
                    Console.WriteLine("Invalid, please type y or n!\n");
            }
            return choice;
        }


        /// <summary>
        /// Function shows welcome message and rule of this game
        /// </summary>
        private void welcomePlayer()
        {
            Console.WriteLine("\n\nWelcome to the GameCard11!\n" +
                "In the game, you will select cards to replace.\n");

            Console.WriteLine(">>> Replacable Card Patterns\n" +
                "1. Two cards add up to 11\n" +
                "2. Three cards which are 11, 12, 13\n");

            Console.WriteLine(">>> Win/Lose Criteria\n" +
                "Win: if all cards on gameboard are removed.\n" +
                "Lose: if there's no more cards can be removed from the gameboard and gameboard is not empty.\n");

            Console.WriteLine("Enjoy the game!\n\n");
        }


        /// <summary>
        /// Function renew the deck of cards and clean the "cardsOnBoard" and "cardsReplaceIdx" on gameboard
        /// to prepare for the next round of game for the same player
        /// </summary>
        private void prepareNextRound(ref Deck deck, ref Gameboard gameboard)
        {
            deck = new Deck();              // get a new deck of cards
            gameboard.emptyGameboard();     // empty gameboard for the next round of game for same user
        }


        /// <summary>
        /// Function draw N cards from deck and place them on the gameboard
        /// </summary>
        /// <param name="num"> number of cards to be drawn </param>
        private void addCardsToBoard(int num, ref Deck deck, ref Gameboard gameboard)
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
        private void getReplaceRequest(ref Gameboard gameboard)
        {
            string replaceRequest;

            Console.Write("Select cards to replace by typing their indexes, and separate by comma (eg. 1,4,6): ");
            replaceRequest = Console.ReadLine();

            gameboard.setCardsReplaceIdx(replaceRequest);               // parse the request and store it
        }


        /// <summary>
        /// Function handle the replace request stored in gameboard
        /// </summary>
        private void handleReplaceRequest(ref Deck deck, ref Gameboard gameboard)
        {
            bool requestValid = checker.replaceRequestValid(ref gameboard);            // using checker to check if the request valid

            if (requestValid)
            {
                replaceCards(ref deck, ref gameboard);                                 // if request is valid, removed the cards and draw new cards from deck to add to gameboard 
                Console.WriteLine("Replaced Successfully!");
            }
            else
            {
                Console.WriteLine("INVALID cards selection! Please choose again!");    // if request is invalid, warn the user about this invalid replacement request
            }
                    
            gameboard.cleanCardsReplaceIdx();                                          // remove the "index" stored in the "cardsReplaceIdx" list
        }


        /// <summary>
        /// Function removes cards selected by player from gameboard, and draw same number of cards from deck and add
        /// back to the gameboard (If cards available in the deck)
        /// </summary>
        void replaceCards(ref Deck deck, ref Gameboard gameboard)
        {
            int numReplace = gameboard.cardsReplaceIdx.Count;           // track the number of cards to replace

            foreach (int i in gameboard.cardsReplaceIdx)                // set all the cards at index for replacing to NULL
                gameboard.cardsOnBoard[i-1] = null;

            gameboard.cardsOnBoard.RemoveAll(card => card == null);     // remove all the NULL records from "cardsOnBoard" list

            addCardsToBoard(numReplace, ref deck, ref gameboard);       // draw same number of cards from deck and add back to "cardsOnBoard" list
        }

    }
}
