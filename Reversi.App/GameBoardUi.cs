using System;
using Reversi.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.App
{
    public class GameBoardUi
    {
        IEnumerable<string> guidedBoard;
        public Game reversi;

        public void SetupBoard()
        {
            reversi = new Game();

            var board = reversi.DisplayBoard();
            guidedBoard = PrependGuidesToStringArrays(board);
        }
 
        public IEnumerable<string> PrependGuidesToStringArrays(string[] Original)
        {
            string[] guidedArray = new string[8];
            int counter = 0;

            foreach (string S in Original)
            {
                guidedArray[counter] = counter + S;
                counter++;
            }
            var finalArray = guidedArray.Prepend(" 01234567");

            return finalArray;
        }
        public void DisplayBoard()
        {
            var currentPlayer = (reversi.GetCurrentPlayer() == Constants.BLACK) ? "BLACK" : "WHITE";

            Console.WriteLine(string.Join('\n', guidedBoard));
            Console.WriteLine();
            Console.WriteLine();


            if (reversi.State.MoveInvalid)
                WriteErrorMessage("Invalid Move!");
            if (reversi.State.PassOccured)
                WriteErrorMessage("User had no possible moves. Turn passed!");
            if (reversi.State.GameOver)
                WriteErrorMessage("Game Over!");

            Console.WriteLine("Current turn: " + currentPlayer);
            Console.Write("Enter coordinates (row,col): ");
        }
        public void GetPlayerInput()
        {
            var coords = Console.ReadLine();
            var coordsSplit = coords.Split(',');
            reversi.PlaceCounter(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]));
            Console.Clear();
        }
        private void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}