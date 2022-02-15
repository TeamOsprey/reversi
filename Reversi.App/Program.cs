namespace Reversi.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameBoardUi = new GameBoardUi();

            gameBoardUi.SetupBoard();
            do
            {
                gameBoardUi.DisplayBoard();
                gameBoardUi.GetPlayerInput();
            } while (!gameBoardUi.reversi.State.GameOver);
        }
    }
}
