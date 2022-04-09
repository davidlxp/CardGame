using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    public class ElevenChecker : Checker
    {

        /// <summary>
        /// Function checks if the cards user selected to replace have valid index.
        /// Valid index means none of them out of the range of index of available cards on the gameboard
        /// Output: if all indexes are valid, return TRUE. Otherwise, FALSE.
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
            bool boardEmpty = board.cardsOnBoard.Count == 0;            // variable indicates if the gameboard is empty
            bool removableExist = anyRemovableCards(ref board);         // variable indicates if any card can be removed from the gameboard

            if (boardEmpty)                                             // gameboard empty means Deck is empty as well => so the user win the game
                return "Win";
            else if (!boardEmpty && !removableExist)                    // gameboard is not empty but nothing can be removed => so the user lose the game
                return "Lose";
            else                                                        // gameboard is not empty and there are removable cards => the game can continue
                return "Ongoing";
        }


        /// <summary>
        /// Function checks if the cards user selected to replace are duplicated.
        /// Output: if cards are duplicated (any), return TRUE. Otherwise, FALSE.
        /// </summary>
        public bool cardsDuplicated(ref Gameboard board)
        {
            List<int> dedupReplaceIdx = board.cardsReplaceIdx.Distinct().ToList();
            return board.cardsReplaceIdx.Count != dedupReplaceIdx.Count;
        }


        /// <summary>
        /// Function checks if the cards user selected to replace follows pattern required.
        /// Output: if the cards follow the pattern, return TRUE. Otherwise, FALSE.
        /// </summary>
        public bool cardsPatternValid(ref Gameboard board)
        {
            int numReplace = board.cardsReplaceIdx.Count;

            /* For CardGame11, there are two ways to succesfully replace cards:
             * 1. Two cards selected and their ranks add up to eleven
             * 2. Three cards selected and they are J, Q, K
             */
            if (numReplace == 2)                                        // when user selected 2 cards
            {
                int sum = 0;

                foreach (int i in board.cardsReplaceIdx)                // sum the value of 2 cards user selected 
                    sum += board.cardsOnBoard[i - 1].Rank;

                return (sum == 11);                                     // if the sum of cards is 11, the selection is correct
            }
            else if (numReplace == 3)                                   // when user selected 3 cards
            {
                List<int> pattern = new List<int>() { 11, 12, 13 };

                foreach (int i in board.cardsReplaceIdx)
                {
                    int value = board.cardsOnBoard[i - 1].Rank;           // get the rank of one card
                    pattern.Remove(value);                              // try to remove the value from "pattern" list
                }

                return (pattern.Count == 0);                            // if everything removed => user selected cards follows J, Q, K pattern
            }
            else
                return false;

        }









        //public bool cardsPatternValid(ref Gameboard board)
        //{
        //    return true;
        //}

        //public bool anyRemovableCards(ref Gameboard board)
        //{
        //    return true;
        //}










        /// <summary>
        /// Function checks if the cards user selected to replace is valid.
        /// Output: if the cards are valid for replacement, return TRUE. Otherwise, FALSE.
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
            List<int> valueOfCards = new List<int>();   // a list store value of all the cards on gameboard
            foreach (Card c in board.cardsOnBoard)
                valueOfCards.Add(c.Rank);

            /* 1. Check if three cards "J, Q, K" exists */
            bool jqkAvail = valueOfCards.Contains(11) && valueOfCards.Contains(12) && valueOfCards.Contains(13);

            /* 2. Check if two cards sum up to 11 exists */
            bool pairElevenAvail = pairAvailable(valueOfCards, 11);

            return (jqkAvail || pairElevenAvail);
        }


        /// <summary>
        /// Function checks if two value sums up to "target" exists in a list of values
        /// </summary>
        /// <param name="values"> A list of values </param>
        /// <param name="targetSum"> The summation target </param>
        /// <returns> If found two cards which can sum up to "target", return TRUE. Otherwise, FALSE. </returns>
        private bool pairAvailable(List<int> valueList, int targetSum)
        {
            bool judge = false;

            for (int i = 0; i < valueList.Count; ++i)
            {
                int complement = 11 - valueList[i];

                for (int j = i + 1; j < valueList.Count; ++j)
                {
                    if (valueList[j] == complement)
                    {
                        judge = true;
                        break;
                    }
                }
            }
            return judge;
        }


    }
}
