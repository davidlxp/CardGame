using System;
using System.Collections.Generic;

namespace CardGame
{
    public class Player
    {
        int id;
        string name;
        string password;
        int totalGameWon;
        int totalGamePlayed;
        double wonRatio;


        /// <summary>
        /// Setter and getter of username
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        /// <summary>
        /// Setter and getter of username
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// Setter and getter of username
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        /// <summary>
        /// Adding more game won
        /// </summary>
        public void updateGameWon(int wonGames)
        {
            totalGameWon += wonGames;
        }


        /// <summary>
        /// Get the number of game won
        /// </summary>
        public int getGameWon()
        {
            return totalGameWon;
        }


        /// <summary>
        /// Adding more game played 
        /// </summary>
        public void updateGamePlayed(int playedGames)
        {
            totalGamePlayed += playedGames;
        }


        /// <summary>
        /// Get the number of game played
        /// </summary>
        public int getGamePlayed()
        {
            return totalGamePlayed;
        }


        /// <summary>
        /// Updates the "wonRatio" after renew the "gamePlayed" and "gameWon"
        /// </summary>
        public void updateWonRatio()
        {
            float ratio = (float)totalGameWon / (float)totalGamePlayed;
            wonRatio = Math.Round(ratio, 2);
        }


        /// <summary>
        /// Function to show user's info
        /// </summary>
        public void showUserInfo()
        {
            Console.WriteLine($"{name} (ID {id}) || Game Played {totalGamePlayed} || Game Won {totalGameWon} || Won Ratio {wonRatio}");
        }

    }
}
