using Reversi.Logic;
using Reversi.Web.Services.Interfaces;


namespace Reversi.Web.Services
{
    public class GameService : IGameService
    {
        public Game Game;

        public GameService(Game game)
        {
            Game = game;
        }
        public void PlaceCounter(int row, int col)
        {
            Game.PlaceCounter(row, col);
        }

        public string[] GetOutput()
        {
            return Game.GetOutput();
        }

        public string GetCurrentPlayer()
        {
            return (Game.GetCurrentPlayer() == Constants.BLACK) ? "BLACK" : "WHITE";
        }
    }
}
