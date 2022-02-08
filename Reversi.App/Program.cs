using System;
using System.Collections.Generic;
using System.Linq;
using Reversi.Logic;

namespace Reversi.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var reversi = new Game();

            do
            {
                var board = reversi.DisplayBoard();
                var currentPlayer = (reversi.GetCurrentPlayer() == Constants.BLACK) ? "BLACK" : "WHITE";
                var guidedBoard = PrependGuidesToStringArrays(board);

                Console.WriteLine(string.Join('\n', guidedBoard));
                Console.WriteLine();
                Console.WriteLine();

                if (reversi.State.MoveInvalid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Move");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("Current turn: " + currentPlayer);
                Console.Write("Enter coordinates (row,col): ");
                var coords = Console.ReadLine();
                var coordsSplit = coords.Split(',');
                reversi.PlaceCounter(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]));
                Console.Clear();
            } while (!reversi.State.GameOver);
        }

        public static IEnumerable<string> PrependGuidesToStringArrays(string[] Original)
        {
            string[] guidedArray = new string[8];
            int counter = 0;

            foreach(string S in Original)
            {
                guidedArray[counter] = counter+S;
                counter++;
            }
            var finalArray = guidedArray.Prepend(" 01234567");

            return finalArray;
        }

    }
}
