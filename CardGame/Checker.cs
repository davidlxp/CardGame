using System;
namespace CardGame
{
    public abstract class Checker
    {
        public abstract bool cardsIndexValid(ref Gameboard board);

        public abstract string checkGameStatus(ref Gameboard board);

    }
}
