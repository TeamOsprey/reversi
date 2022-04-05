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
        public Game Game;
        public GameService()
        {
            Game = new Game();
        }
        public void PlaceCounter(int row, int col)
        {
            Game.PlaceCounter(row, col);
        }

        public string[] GetOutput()
        {
            return Game.GetOutput();
        }
    }
}
