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

        public void AddPlayer(string userId)
        {
            Game.AddPlayer(userId);
        }

        public void RemovePlayer(string userId)
        {
            Game.RemovePlayer(userId);
        }

        public bool IsLastMoveValid()
        {
            return !(Game.State is MoveInvalid);
        }

        public void PlaceCounter(int row, int col, string userId)
        {
            Game.PlaceCounter(row, col, userId);
        }

        public string[] GetOutputAsStringArray()
        {
            return Game.GetOutputAsStringArray();
        }
        public Square[,] GetOutputAsSquares()
        {
            return Game.GetOutputAsSquares();
        }

        public string GetCurrentPlayer()
        {
            return ConvertPlayerTypeToString(Game.Turn);
        }

        public string GetPlayerColourString(string userId)
        {
            return ConvertPlayerTypeToString(Game.GetPlayer(userId));
        }

        public string GetMessage()
        {
            return Game.State is { IsPersonal: false } ? Game.State.ErrorMessage : "";
        }

        public string GetPersonalMessage()
        {
            return Game.State is { IsPersonal: true } ? Game.State.ErrorMessage : "";
        }

        public int GetScoreByColor(char color)
        {
            return Game.GetNumberOfColor(color);
        }

        public bool HasAllPlayers()
        {
            return Game.Players.HasAllPlayers;
        }

        private string ConvertPlayerTypeToString(Player player)
        {
            return player != null ? player.ToString() : "Observer";

        }

        public string[] GetOutput()
        {
            throw new System.NotImplementedException();
        }

        string[] IGameService.GetOutputAsSquares()
        {
            throw new System.NotImplementedException();
        }
    }
}
