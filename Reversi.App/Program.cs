using System;
using System.Collections.Generic;
using System.Linq;
using Reversi.Logic;

namespace Reversi.App
{
    public class Program : GameBoard
    {
        static IEnumerable<string> guidedBoard;
        static string currentPlayer;

        static void Main(string[] args)
        {
            var reversi = new Game();
            var gameBoard = new GameBoard();

            do
            {
                gameBoard.SetupBoard(reversi);
                DisplayBoard(reversi);

                GetPlayerInput(reversi);
            } while (!reversi.State.GameOver);
        }

        private static void GetPlayerInput(Game reversi)
        {
            var coords = Console.ReadLine();
            var coordsSplit = coords.Split(',');
            reversi.PlaceCounter(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]));
            Console.Clear();
        }

        private static void DisplayBoard(Game reversi)
        {
            currentPlayer = (reversi.GetCurrentPlayer() == Constants.BLACK) ? "BLACK" : "WHITE";

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

        private static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
