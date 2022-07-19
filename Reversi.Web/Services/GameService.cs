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

        public void AddPlayer(string connectionId)
        {
            Game.AddPlayer(connectionId);
        }

        public bool IsLastMoveValid()
        {
            return !Game.State.MoveInvalid;
        }

        public void PlaceCounter(int row, int col, string connectionId)
        {
            Game.PlaceCounter(row, col, connectionId);
        }

        public string[] GetOutput()
        {
            return Game.GetOutput();
        }

        public string GetCurrentPlayer()
        {
            return ConvertColourCharToString(Game.GetCurrentPlayerColour());
        }

        private string ConvertColourCharToString(char colour)
        {
            return colour switch
            {
                Constants.BLACK => "BLACK",
                Constants.WHITE => "WHITE",
                _ => "OBSERVER"
            };
        }

        public string GetPlayerColourString(string connectionId)
        {
            return ConvertColourCharToString(Game.GetRole(connectionId));
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
