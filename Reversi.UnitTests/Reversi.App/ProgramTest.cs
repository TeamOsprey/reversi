using NUnit.Framework;
using Reversi.App;

namespace Reversi.UnitTests.Reversi.App
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void ConsoleAddsGuideToNewBoard()
        {
            var outputBoard = new string[]{
                     " 01234567",
                     "0........",
                     "1........",
                     "2........",
                     "3........",
                     "4........",
                     "5........",
                     "6........",
                     "7........"};
            var gameBoardUi = new GameBoardUi(false);

            Assert.AreEqual(outputBoard, gameBoardUi.guidedBoard);
        }
    }
}
