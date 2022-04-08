using System;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string hey = "6,  8 ,  9";
            hey = hey.Replace(" ", "");
            string[] split = hey.Split(",");

            for (int i = 0; i < split.Length; ++i)
            {
                Console.WriteLine(split[i]);
            }
        }
    }
}
