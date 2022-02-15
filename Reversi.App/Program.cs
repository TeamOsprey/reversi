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
            var gameBoardUi = new GameBoardUi();

            do
            {
                gameBoardUi.SetupBoard(reversi);
                gameBoardUi.DisplayBoard(reversi);
                gameBoardUi.GetPlayerInput(reversi);
            } while (!reversi.State.GameOver);
        }


    }
}
