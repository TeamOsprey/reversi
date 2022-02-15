namespace Reversi.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameBoardUi = new GameBoardUi();

            do
            {
                gameBoardUi.TakeTurn();
            } while (!gameBoardUi.reversi.State.GameOver);
        }
    }
}
