using System;
using System.Text;
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

        public void RemovePlayer(string connectionId)
        {
            Game.RemovePlayer(connectionId);
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
            var msg = new StringBuilder();
            Action<bool, string> appendLineIf = (state,message) =>
            {
                if (state) msg.AppendLine(message);
            };

            appendLineIf(Game.State.MoveInvalid, "Invalid Move!");
            appendLineIf(Game.State.PassOccured, "User had no possible moves. Turn passed!");
            appendLineIf(Game.State.GameOver, "Game Over!");
            appendLineIf(Game.State.InsufficientPlayers, "May not move until second player has joined the game!");

            return msg.ToString();
        }

        public int GetScoreByColor(char color)
        {
            return Game.GetNumberOfColor(color);
        }

        private string ConvertRoleCharToString(RoleEnum? role)
        {
            return role switch
            {
                RoleEnum.Black => RoleEnum.Black.ToString(),
                RoleEnum.White => RoleEnum.White.ToString(),
                _ => "Observer"
            };
        }
    }
}
