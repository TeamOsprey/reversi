using System;
using System.Linq;
using Reversi.Logic;

namespace Reversi.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var reversi = new Game();

            do
            {
                var board = reversi.DisplayBoard();
                var currentPlayer = (reversi.GetCurrentPlayer() == Constants.BLACK) ? "BLACK" : "WHITE";
                var GuidedBoard = board.Prepend(" 01234567");

                Console.WriteLine(string.Join('\n', GuidedBoard));
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Current turn: " + currentPlayer);
                Console.Write("Enter coordinates (row,col): ");
                var coords = Console.ReadLine();
                var coordsSplit = coords.Split(',');
                reversi.PlaceCounter(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]));
            } while (!reversi.State.GameOver);
        }
    }
}
