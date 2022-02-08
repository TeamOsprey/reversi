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
                var guidedBoard = PrependGuidesToStringArrays(board);
                var finalBoard = guidedBoard.Prepend(" 01234567");

                Console.WriteLine(string.Join('\n', finalBoard));
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Current turn: " + currentPlayer);
                Console.Write("Enter coordinates (row,col): ");
                var coords = Console.ReadLine();
                var coordsSplit = coords.Split(',');
                reversi.PlaceCounter(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]));
                Console.Clear();
            } while (!reversi.State.GameOver);
        }

        private static string[] PrependGuidesToStringArrays(string[] Original)
        {
            string[] guidedArray = new string[8];
            int counter = 0;

            foreach(string S in Original)
            {
                guidedArray[counter] = counter+S;
                counter++;
            }
            return guidedArray;
        }

    }
}
