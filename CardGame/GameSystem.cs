using System;
using System.Collections.Generic;

namespace CardGame
{
    public class GameSystem
    {
        ElevenChecker checker = new ElevenChecker();                               // Init Checker for the game

        Dictionary<string, Player> playerInfo = new Dictionary<string, Player>();  // A dictionary store player name and their detailed information
        string activePlayer;                                                       // The username of players who logged-in to the system

        int numCardsOnBoard;                                                       // Number of cards on gameboard for this game


        /// <summary>
        /// Constructor of the system
        /// </summary>
        public GameSystem(int numCardsOnBoard1)
        {
            numCardsOnBoard = numCardsOnBoard1;
        }


        /// <summary>
        /// Function run the program of this game system
        /// </summary>
        public void runProgram()
        {
            bool loggedIn = false;              // variable indicates player loggedIn or not

            /* 1. Show user the login page */
            int choice = loginMenu();
            while (choice != 3)
            {
                switch (choice)
                {
                    case 1:
                        login(ref loggedIn);
                        break;
                    case 2:
                        register(ref loggedIn);
                        break;
                    default:
                        Console.WriteLine("Invalid selection, please reselect!");
                        break;
                }

                if (loggedIn)
                    /* 2. Show user the game page only when user logged-in */
                    runUserProgram(ref loggedIn);

                choice = loginMenu();
            }
        }


        /// <summary>
        /// Menu for the login page
        /// </summary>
        public int loginMenu()
        {
            string choiceStr;
            int choice = 0;

            Console.WriteLine("\n");
            Console.WriteLine("<*>------ Login to CardGame11 ------<*>");

            while (choice == 0)
            {
                Console.WriteLine("1. Log In");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit the Program");
                Console.Write("Please make a choice by typing its index: ");

                choiceStr = Console.ReadLine();
                Int32.TryParse(choiceStr, out choice);

                if (choice == 0)
                    Console.WriteLine("Invalid, please type an integer!\n");
            }
            return choice;

        }


        /// <summary>
        /// Function let an existing player to login
        /// </summary>
        private void login(ref bool loggedIn)
        {
            int attempt = 0;            // maximum attempts to login
            string username = "";         
            string password = "";

            while (!loggedIn && attempt < 3)
            {
                if (attempt == 0)
                    Console.WriteLine("\nPlease provide your credential below");
                else
                    Console.WriteLine("\nCredential wrong, please try again!\n");

                Console.Write("Username: ");
                username = Console.ReadLine();

                if (playerInfo.ContainsKey(username))
                {
                    Console.Write("Password: ");
                    password = Console.ReadLine();

                    loggedIn = playerInfo[username].Password == password;
                    attempt++;
                }
                else
                {
                    Console.WriteLine("The username does NOT exists, please register instead.");
                    break;
                }
            }

            if (loggedIn)
            {
                Console.Write($"Login Success! Welcome {username}!");

                if (activePlayer != username)
                    activePlayer = username;                                // set this player as active
            }
            else
                Console.Write("Login Failed!");
        }


        /// <summary>
        /// Function to register a new player
        /// </summary>
        private void register(ref bool loggedIn)
        {
            string username = "";
            string password = "";
            bool success = false;


            while (!success)
            {
                Console.Write("Create your username: ");
                username = Console.ReadLine();                     // get new username from user

                if (!playerInfo.ContainsKey(username))             // validate if username exists already
                {
                    Console.Write("Create your password: ");
                    password = Console.ReadLine();                 // get new password from user 

                    Player newPlayer = new Player();               // create a new player
                    newPlayer.Id = playerInfo.Count + 1;           // assign userId (auto-increment)
                    newPlayer.Name = username;                     // assign userName
                    newPlayer.Password = password;                 // assign password
                    playerInfo[username] = newPlayer;              // adding the newPlayer to system

                    success = true;

                    loggedIn = true;                               // automatically loggin the successfully registered user
                    if (activePlayer != username)
                        activePlayer = username;                   // set this player as active
                }
                else
                    Console.WriteLine("The username already exists, please try another name!\n");
            }
        }


        /// <summary>
        /// Run program which exclusively shows to the user who logged in
        /// </summary>
        public void runUserProgram(ref bool loggedIn)
        {
            int choice = mainMenu();
            while (choice != 4)
            {
                switch (choice)
                {
                    case 1:                                         // start game
                        startGame();
                        break;
                    case 2:                                         // see my historical score
                        Console.WriteLine();
                        playerInfo[activePlayer].showUserInfo();
                        break;
                    case 3:                                         // see the leaderboard
                        showLeaderboard();                          
                        break;
                }
                choice = mainMenu();
            }

            /* log off */
            if (choice == 4)
            {               
                loggedIn = false;
                activePlayer = "";
            }
        }


        /// <summary>
        /// Function takes user's choice of this program
        /// </summary>
        /// <returns> The choice which player made </returns>
        public int mainMenu()
        {
            string choiceStr;
            int choice = 0;

            Console.WriteLine("\n");
            Console.WriteLine("<*>------ CardGame11 ------<*>");

            while (choice == 0)
            {
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. See My Historical Score");
                Console.WriteLine("3. See Leaderboard");
                Console.WriteLine("4. Log Off");
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
                gameboard.showWonHistory();                                                 // showing "You've won x/y games"
                choice = continueMenu();                                                    // ask user if they want to have the next round of game

                if (choice == "y")
                    prepareNextRound(ref deck, ref gameboard);                              // prepare for the next round of game
                else
                {
                    // in the end, load the game info to user's database
                    playerInfo[activePlayer].updateGamePlayed(gameboard.getGamePlayed());   
                    playerInfo[activePlayer].updateGameWon(gameboard.getGameWon());
                    playerInfo[activePlayer].updateWonRatio();
                }
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


        /// <summary>
        /// Function to show the leaderboard
        /// </summary>
        void showLeaderboard()
        {
            List<string> gamesWon = new List<string>();
            List<string> gamesPlayed = new List<string>();

            for (int i = 0; i < 3; ++i)
            {
                /* Finding the top players */
                string wonName = "";
                int maxWon = 0;

                string playedName = "";
                int maxPlayed = 0;

                foreach (string name in playerInfo.Keys)
                {
                    int currWon = playerInfo[name].getGameWon();
                    int currPlayed = playerInfo[name].getGamePlayed();

                    if (!gamesWon.Contains(name) && currWon > maxWon)
                    {
                        maxWon = currWon;
                        wonName = name;
                    }

                    if (!gamesPlayed.Contains(name) && currPlayed > maxPlayed)
                    {
                        maxPlayed = currPlayed;
                        playedName = name;
                    }
                }

                /* Adding Top player's name to lists */
                if (wonName != "")
                    gamesWon.Add(wonName);

                if (playedName != "")
                    gamesPlayed.Add(playedName);
            }

            /* Print the top players */
            Console.WriteLine("\nTop3 Users by the Number of Games Won:");
            foreach (string name in gamesWon)
                playerInfo[name].showUserInfo();

            Console.WriteLine("\nTop3 Users by the Number of Games Played:");
            foreach (string name in gamesPlayed)
                playerInfo[name].showUserInfo();

        }

    }
}
