using Reversi.Logic;

namespace Reversi.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameBoardUi = new GameBoardUi();

            do
            {
                gameBoardUi.TakeTurn();
            } while (!(gameBoardUi.Reversi.State is GameOver));
        }
    }
}
