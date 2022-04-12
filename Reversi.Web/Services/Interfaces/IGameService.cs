namespace Reversi.Web.Services.Interfaces
{
    public interface IGameService
    {
        public void PlaceCounter(int row, int col);
        public string[] GetOutput();
        public string GetCurrentPlayer();
        public string GetMessage();
    }
}
