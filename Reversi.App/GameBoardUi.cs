using System;
using System.Collections.Generic;
using System.Linq;
using Reversi.Logic;
using Reversi.Logic.Converters;

namespace Reversi.ConsoleApp
{
    public class GameBoardUi
    {
        public IEnumerable<string> GuidedBoard;
        public Game Reversi;

        public GameBoardUi()
        {
            Reversi = new Game();
            Reversi.AddPlayer("1");
            Reversi.AddPlayer("2");
            SetupBoard();
        }

        public void TakeTurn()
        {
            SetupBoard();
            DisplayBoard();
            GetPlayerInput();
        }

        private void SetupBoard()
        {
            var board = BoardConverter.ConvertToStringArray(Reversi.GetOutputAsSquares());
            GuidedBoard = PrependGuidesToStringArrays(board);
        }
 
        private IEnumerable<string> PrependGuidesToStringArrays(string[] original)
        {
            string[] guidedArray = new string[8];
            int counter = 0;

            foreach (string item in original)
            {
                guidedArray[counter] = counter + item;
                counter++;
            }
            var finalArray = guidedArray.Prepend(" 01234567");

            return finalArray;
        }
        private void DisplayBoard()
        {
            Console.WriteLine(string.Join('\n', GuidedBoard));
            Console.WriteLine();
            Console.WriteLine();


            if (Reversi.State is MoveInvalid)
                WriteErrorMessage("Invalid Move!");
            if (Reversi.State is PassOccurred)
                WriteErrorMessage("User had no possible moves. Turn passed!");
            if (Reversi.State is GameOver)
                WriteErrorMessage("Game Over!");

            Console.WriteLine("Current turn: " + Reversi.Turn);
            Console.Write("Enter coordinates (row,col): ");
        }
 
        private void GetPlayerInput()
        {
            var coords = Console.ReadLine();
            var coordsSplit = coords.Split(',');
            var userId = Reversi.Turn.UserId;
            Reversi.PlaceCounter(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]), userId);
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