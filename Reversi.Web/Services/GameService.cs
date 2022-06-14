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

        public bool IsLastMoveValid()
        {
            return !Game.State.MoveInvalid;
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
            return (Game.GetCurrentPlayerColour() == Constants.BLACK) ? "BLACK" : "WHITE";
        }

        public string GetMessage()
        {
            string msg = "";
            
            if (Game.State.MoveInvalid)
                msg = "Invalid Move!";
            if (Game.State.PassOccured)
                msg += "User had no possible moves. Turn passed!";
            if (Game.State.GameOver)
                msg += "Game Over!";

            return msg;
        }

        public int GetScoreByColor(char color)
        {
            return Game.GetNumberOfColor(color);
        }
    }
}
