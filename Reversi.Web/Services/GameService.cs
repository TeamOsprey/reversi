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

        public string[] GetOutput()
        {
            return Game.GetOutput();
        }

        public string GetCurrentPlayer()
        {
            return ConvertPlayerTypeToString(Game.Turn());
        }

        public string GetPlayerColourString(string userId)
        {
            return ConvertPlayerTypeToString(Game.GetPlayerType(userId));
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
