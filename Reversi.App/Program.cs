using System;
using Reversi.Logic;

namespace Reversi.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "........",
            "....W...",
            "....W...",
            "....B..."};

            var reversi = new Game(board, 'B');
            Console.WriteLine(string.Join('\n', reversi.ReversiBoard.GetCurrentState()));
        }
    }
}
