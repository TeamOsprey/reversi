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
            return ConvertRoleCharToString(Game.Turn());
        }

        public string GetPlayerColourString(string connectionId)
        {
            return ConvertRoleCharToString(Game.GetRole(connectionId));
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

        private string ConvertRoleCharToString(char colour)
        {
            return colour switch
            {
                Constants.Roles.BLACK => Constants.Roles.BLACK_STRING,
                Constants.Roles.WHITE => Constants.Roles.WHITE_STRING,
                _ => Constants.Roles.OBSERVER_STRING
            };
        }
    }
}
