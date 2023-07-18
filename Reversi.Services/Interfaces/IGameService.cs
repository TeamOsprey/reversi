using Reversi.Logic;

namespace Reversi.Services.Interfaces
{
    public interface IGameService
    {
        public void AddPlayer(string userId);
        public void RemovePlayer(string userId);
        public void PlaceCounter(int row, int col, string userId);
        public bool IsLastMoveValid();
        public SquareDto[,] GetOutputAsSquares();
        public string GetCurrentPlayer();
        public string GetPlayerColourString(string userId);
        public string GetMessage();
        public string GetPersonalMessage();
        public int GetScoreByColor(char color);
        public bool HasAllPlayers();
    }
}
