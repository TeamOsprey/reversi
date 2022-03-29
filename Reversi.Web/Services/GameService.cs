using Reversi.Logic;
using Reversi.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Web.Services
{
    public class GameService : IGameService
    {
        public void PlaceCounter(int row, int col)
        {
            Debug.WriteLine("You are in GameService: " + row + ", " + col);

            var game = new Game();

            //testing game
            game.PlaceCounter(row, col);
        }
    }
}
