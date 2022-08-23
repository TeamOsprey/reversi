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
            return ConvertPlayerTypeToString(Game.Turn());
        }

        public string GetPlayerColourString(string connectionId)
        {
            return ConvertPlayerTypeToString(Game.GetPlayerType(connectionId));
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

        private string ConvertPlayerTypeToString(PlayerType? type)
        {
            return type switch
            {
                PlayerType.Black => PlayerType.Black.ToString(),
                PlayerType.White => PlayerType.White.ToString(),
                _ => "Observer"
            };
        }
    }
}
