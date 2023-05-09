using NUnit.Framework;
using Reversi.Logic;
using System.Linq;

namespace Reversi.UnitTests
{
    [TestFixture]
    public class ConsoleTest
    {
        //public void After_Console_File_Saved_To_AppData_Game_State()
        //{
        //    string path = @"c:\temp\test.txt";
        //    Console.WriteLine(File.Exists(path));
        //}

        [Test]
        public void DisplayCurrentBoard()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "........",
            "....W...",
            "....W...",
            "....B..."};

            var reversi = Game.Load(board, true);

            CollectionAssert.AreEqual(board, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IsUserMoveLegal()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "...BW...",
            "...BW...",
            "....W...",
            "....W...",
            "....B..."};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(2, 4, "1"));
        }

        [Test]
        public void IfWhiteUserMoveLegalPlaceCounter()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "...WB...",
            "...WB...",
            "....B...",
            "....B...",
            "....W..."};

            var reversi = Game.Load(board, false);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(2, 4, "2"));

            var expected = new string[]{
            "........",
            "........",
            "....W...",
            "...WW...",
            "...WW...",
            "....W...",
            "....W...",
            "....W..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfWhiteUserMoveIllegalDoNotPlaceCounter()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....W..."};

            var reversi = Game.Load(board, false);

            Assert.IsFalse(reversi.PlaceCounter(1, 1, "2"));

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....W..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfUserMoveLegalPlaceCounterAndFlipThreeCounters()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....W...",
            "....W...",
            "....W...",
            "....B..."};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(3, 4, "1"));

            var expected = new string[]{
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....B...",
            "....B...",
            "....B..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }



        [Test]
        public void IfUserMoveLegalPlaceCounterAndFlipThreeCounters_version2()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "....B...",
            "....W...",
            "....W...",
            "....W...",
            "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(7, 4, "1"));

            var expected = new string[]{
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....B...",
            "....B...",
            "....B..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfWhiteUserMoveLegalFlipCorrectLineOnly()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "........",
            "...BB...",
            "..B.B...",
            "....W..."};

            var reversi = Game.Load(board, false);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 4, "2"));

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....W...",
            "...BW...",
            "..B.W...",
            "....W..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfOnlyOneLineLegalOnlyFlipThatLine()
        {
            var board = new string[]{
                    "........",
                    "........",
                    "........",
                    "........",
                    "........",
                    "...BB...",
                    "..B.B...",
                    ".B..W..."};

            var reversi = Game.Load(board, false);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 4, "2"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "........",
                    "....W...",
                    "...BW...",
                    "..B.W...",
                    ".B..W..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfEmptyBoardDoNotAcceptCentreSquareForPlacement()
        {
            var board = new string[]{
                    "........",
                    "........",
                    "........",
                    "........",
                    "........",
                    "........",
                    "........",
                    "........"};

            var reversi = Game.Load(board, true);

            Assert.IsFalse(reversi.PlaceCounter(0, 3, "1"));
        }

        [Test]
        public void IfThreeCountersOnBoardDoNotAcceptOutOfCentreSquareForPlacement()
        {
            var board = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "....W...",
                    "........",
                    "........",
                    "........"};

            var reversi = Game.Load(board, true);

            Assert.IsFalse(reversi.PlaceCounter(5, 4, "1"));
        }

        [Test]
        public void IfFourCountersOnBoardAcceptOutOfCentreSquareForPlacement()
        {
            var board = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "...BW...",
                    "........",
                    "........",
                    "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(5, 4, "1"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "...BB...",
                    "....B...",
                    "........",
                    "........"};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());

        }

        [Test]
        public void IfPlayersMoveIsOverTurnSwitchesToNextPlayer()
        {
            var board = new string[]{
                "........",
                "........",
                "........",
                "...WB...",
                "...BW...",
                "........",
                "........",
                "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(5, 4, "1");

            Assert.IsTrue(reversi.Turn() is WhitePlayer);
        }


        [Test]
        public void IfPlayersMoveIsOverNextPlayerTurnsPass()
        {
            var board = new string[]{
                "BWWWWWWW",
                "BWBBW...",
                "BWBBW...",
                "BWWWW...",
                "BWWWW...",
                "BWWWW...",
                "BWWBW...",
                "BBBBBW.."};

            var reversi = Game.Load(board, true);

            reversi.PlaceCounter(7, 6, "1");

            Assert.IsTrue(reversi.Turn() is BlackPlayer);
        }

        [Test]
        public void InitiateGameWithTurnColourBlack()
        {
            var reversi = new Game();
            Assert.IsTrue(reversi.Turn() is BlackPlayer);
        }

        [Test]
        public void GameOverTestWhenNoLegalMovesForBothPlayers()
        {
            var board = new string[]{
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWW.",
                "WWWWWW..",
                "WWWWWW.B",
                "WWWWWWW.",
                "WWWWWWWW"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.State is GameOver);
        }

        [Test]
        public void GameOverWhenBoardIsFilled()
        {
            var board = new string[]{
                "BWWWWWWW",
                "BWBBWBBB",
                "BWBBWBBB",
                "BWWWWBBB",
                "BWWWWBBB",
                "BWWWWBBB",
                "BWWBWBBB",
                "BBBBBBBW"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.State is GameOver);
        }

        [Test]
        public void GameLoadChangesTurnWhenBoardPassedInIsInPassedState()
        {
            var board = new string[]{
                "BWWWWWWW",
                "BWBBWBBB",
                "BWBBWBBB",
                "BWWWWBBB",
                "BWWWWBB.",
                "BWWWWBBB",
                "BWWBWBBB",
                "BBBBBBBW"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.Turn() is WhitePlayer);
        }
        [Test]
        public void WhenStatusIsGameOverDeclareWinner()
        {
            var board = new string[]{
                "BWWWWWWW",
                "BWBBWBBB",
                "BWBBWBBB",
                "BWWWWBBB",
                "BWWWWBBB",
                "BWWWWBBB",
                "BWWBWBBB",
                "BBBBBBBW"};

            var reversi = Game.Load(board, true);
            Assert.IsTrue(reversi.GetWinner() is BlackPlayer);
        }

        [Test]
        public void WhenLoadingGameOverBoardDeclareWinner()
        {
            var board = new string[]{
                "BWWWWWWW",
                "BWBBWBBB",
                "BWBBWBBB",
                "BWWWWBBB",
                "BWWWWBBB",
                "BWWWWBBB",
                "BWWBWBBB",
                "BBBBBBBW"};

            var reversi = Game.Load(board, true);
            Assert.IsTrue(reversi.GetWinner() is BlackPlayer);
        }
        [Test]
        public void WhenPlacingIllegalCounterFlagWarnsUs()
        {
            var board = new string[]{
                     "........",
                     "........",
                     "........",
                     "........",
                     "........",
                     "........",
                     "........",
                     "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(1, 1, "1");

            Assert.IsTrue(reversi.State is MoveInvalid);
        }

        [Test]
        public void IfPlayersMoveIsOverNextPlayerTurnsPassAndFlagDeclarePass()
        {
            var board = new string[]{
                 "BWWWWWWW",
                 "BWBBW...",
                 "BWBBW...",
                 "BWWWW...",
                 "BWWWW...",
                 "BWWWW...",
                 "BWWBW...",
                 "BBBBBW.."};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(7, 6, "1");

            Assert.IsTrue(reversi.State is PassOccurred);
        }

        [Test]
        public void IfPlayersMoveCausesGameOverFlagDeclareGameOver()
        {
            var board = new string[]{
                "BWWWWWWW",
                "BWBBWBBB",
                "BWBBWBBB",
                "BWWWWBBB",
                "BWWWWBW.",
                "BWWWWBBB",
                "BWWBWBBB",
                "BBBBBBBW"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(4, 7, "1");

            Assert.IsTrue(reversi.State is GameOver);
        }

        [Test]
        public void IfPlayersMoveWorksProperlyStateIsNotInvalid()
        {
            var board = new string[]{
                 "BWWWWWWW",
                 "BWBBW...",
                 "BWBBW...",
                 "BWWWW...",
                 "BWWWW...",
                 "BWWWW...",
                 "BWWBW...",
                 "BBBBBW.."};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(7, 6, "1");

            Assert.IsFalse(reversi.State is MoveInvalid);
        }

        [Test]
        public void FirstPlayerIsAssignedBlack()
        {
            var game = new Game();
            game.AddPlayer("1");
            var players = game.Players;
            Assert.Multiple(()=>
            {
                Assert.IsTrue(players.HasBlackPlayer);
                Assert.IsFalse(players.HasWhitePlayer);
                Assert.AreEqual(players.BlackPlayer.UserId, "1"); 
                Assert.AreEqual(players.WhitePlayer.UserId, null);
            });

        }

        [Test]
        public void SecondPlayerIsAssignedWhite()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            var players = game.Players;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(players.HasAllPlayers);
                Assert.IsTrue(players.BlackPlayer.UserId == "1");
                Assert.IsTrue(players.WhitePlayer.UserId == "2");
            });
        }

        [Test]
        public void NewGameHasNoPlayers()
        {
            var game = new Game();
            var players = game.Players;
            Assert.IsTrue(players.All(x=>x.UserId == null));
        }

        [Test]
        public void ThirdPlayerToJoinDoesNotGetAssigned()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.AddPlayer("3");
            var players = game.Players;
            Assert.IsTrue(players.HasAllPlayers);
            Assert.IsTrue(players.BlackPlayer.UserId == "1");
            Assert.IsTrue(players.WhitePlayer.UserId == "2");
        }

        [Test]
        public void AfterThirdPlayerJoinsOtherPlayersCanStillMove()
        {
            var board = new string[]{
                "........",
                "........",
                "........",
                "...WW...",
                "...BB...",
                "........",
                "........",
                "........"};

            var game = Game.Load(board, true);
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.AddPlayer("3");
            game.PlaceCounter(2, 5, "1");
            game.PlaceCounter(5, 5, "2");
            Assert.IsFalse(game.State is InsufficientPlayers);
        }

        [Test]
        public void TwoPlayersRequiredToMakeAMove()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.PlaceCounter(4, 4, "1");
            Assert.IsTrue(game.State is InsufficientPlayers);
        }
        [Test]
        public void WithTwoPlayersAllowPlaceCounter()
        {
            var board = new string[]{
                "........",
                "........",
                "........",
                "...WW...",
                "...BB...",
                "........",
                "........",
                "........"};

            var game = Game.Load(board, true);
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.PlaceCounter(2, 5, "1");
            Assert.IsFalse(game.State is InsufficientPlayers);
        }

        [Test]
        public void CannotPlaceCounterWhenNotPlayersTurn()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.PlaceCounter(4, 4, "2");
            Assert.IsTrue(game.State is MoveInvalid);
        }

        [Test]
        public void StartGameWithInitialBoard()
        {
            var game = new Game();

            var blackCounters = game.ReversiBoard.GetNumberOfSquaresByColor(Counters.Black);
            var whiteCounters = game.ReversiBoard.GetNumberOfSquaresByColor(Counters.White);

            Assert.IsTrue((blackCounters == whiteCounters && blackCounters == 2));            
        }
    }
}
