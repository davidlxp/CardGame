using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    public class ElevenChecker : Checker
    {

        /// <summary>
        /// Function checks if the cards user selected to replace have valid index.
        /// Valid index means none of them out of the range of index of available
        /// cards on the gameboard
        /// </summary>
        public override bool cardsIndexValid(ref Gameboard board)
        {
            bool res = true;

            /* If any of the index is out of the valid range */
            foreach (int i in board.cardsReplaceIdx)
            {
                if (i < 1 || i > board.getBoardCardsNum())
                {
                    res = false;
                    break;
                }
            }
            
            return res;
        }


        /// <summary>
        /// Function checks if user win, lose a game, or the game is still ongoing.
        /// 3 Outputs: Win, Lose, Ongoing
        /// </summary>
        public override string checkGameStatus(ref Gameboard board)
        {
            string status = "Ongoing";





            return status;
        }


        /// <summary>
        /// Function checks if the cards user selected to replace are duplicated
        /// </summary>
        public bool cardsDuplicated(ref Gameboard board)
        {
            List<int> dedupReplaceIdx = board.cardsReplaceIdx.Distinct().ToList();
            return board.cardsReplaceIdx.Count != dedupReplaceIdx.Count;
        }


        /// <summary>
        /// Function checks if the cards user selected to replace follows pattern required
        /// </summary>
        public bool cardsPatternValid(ref Gameboard board)
        {
            int numReplace = board.cardsReplaceIdx.Count;

            /* For CardGame11, there are two ways to succesfully replace cards:
             * 1. Two cards selected and their ranks add up to eleven
             * 2. Three cards selected and they are J, Q, K
             */
            if (numReplace == 2)
            {
                int sum = 0;

                foreach (int i in board.cardsReplaceIdx)
                    sum += board.cardsOnBoard[i].Rank;

                return (sum == 11);
            }
            else if (numReplace == 3)
            {
                List<int> pattern = new List<int>() { 11, 12, 13 };

                foreach (int i in board.cardsReplaceIdx)
                {
                    int value = board.cardsOnBoard[i].Rank;
                    if (pattern.Contains(value))
                        pattern.Remove(value);
                }

                return (pattern.Count == 0);   
            }
            else
                return false;
        }


        /// <summary>
        /// Function checks if the cards user selected to replace is valid.
        /// </summary>
        public bool replaceRequestValid(ref Gameboard board)
        {
            if (cardsIndexValid(ref board) && !cardsDuplicated(ref board) && cardsPatternValid(ref board))
                return true;
            else
                return false;
        }


        /// <summary>
        /// Function check if any cards are available to be removed from gameboard
        /// </summary>
        public bool anyRemovableCards(ref Gameboard board)
        {
            bool res = false;


            return res;
        }


    }
}
