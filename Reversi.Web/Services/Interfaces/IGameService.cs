namespace Reversi.Web.Services.Interfaces
{
    public interface IGameService
    {
        public void AddPlayer(string connectionId);
        public void RemovePlayer(string connectionId);
        public void PlaceCounter(int row, int col, string connectionId);
        public bool IsLastMoveValid();
        public string[] GetOutput();
        public string GetCurrentPlayer();
        public string GetPlayerColourString(string connectionId);
        public string GetMessage();
        public int GetScoreByColor(char color);
    }
}
