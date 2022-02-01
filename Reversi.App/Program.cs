using System;
using Reversi.Logic;

namespace Reversi.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var reversi = new Game();

            var board = reversi.DisplayBoard();
            var currentPlayer = reversi.GetCurrentPlayer();
            Console.WriteLine(string.Join('\n', board));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
