using NUnit.Framework;
using Reversi.App;
using Reversi.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var reversi = Game.Load(board, PlayerType.Black);

            CollectionAssert.AreEqual(board, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IsUserMoveLegal()
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 4, "1"));
        }

        [Test]
        public void IfUserMoveLegalPlaceCounter()
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 4, "1"));

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....B...",
            "....B..."};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfWhiteUserMoveLegalPlaceCounter()
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

            var reversi = Game.Load(board, PlayerType.White);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 4, "2"));

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....W...",
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

            var reversi = Game.Load(board, PlayerType.White);

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

            var reversi = Game.Load(board, PlayerType.Black);
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

            var reversi = Game.Load(board, PlayerType.Black);
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

            var reversi = Game.Load(board, PlayerType.White);
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

            var reversi = Game.Load(board, PlayerType.White);
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
        public void IfEmptyBoardAcceptCentreSquareForPlacement()
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(3, 3, "1"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "...B....",
                    "........",
                    "........",
                    "........",
                    "........"};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfEmptyBoardOnlyAcceptCentreSquareForPlacement()
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

            var reversi = Game.Load(board, PlayerType.Black);

            Assert.IsFalse(reversi.PlaceCounter(0, 3, "1"));
        }

        [Test]
        public void IfEmptyBoardAcceptCentreSquareForPlacement1()
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(3, 4, "1"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "....B...",
                    "........",
                    "........",
                    "........",
                    "........"};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfOneCounterOnBoardAcceptCentreSquareForPlacement()
        {
            var board = new string[]{
                    "........",
                    "........",
                    "........",
                    "...W....",
                    "........",
                    "........",
                    "........",
                    "........"};

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(3, 4, "1"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "........",
                    "........",
                    "........",
                    "........"};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfTwoCountersOnBoardAcceptCentreSquareForPlacement()
        {
            var board = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "........",
                    "........",
                    "........",
                    "........"};

            var reversi = Game.Load(board, PlayerType.White);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 4, "2"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "....W...",
                    "........",
                    "........",
                    "........"};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
        }

        [Test]
        public void IfThreeCountersOnBoardAcceptCentreSquareForPlacement()
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.PlaceCounter(4, 3, "1"));

            var expected = new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "...BW...",
                    "........",
                    "........",
                    "........"};

            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());
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

            var reversi = Game.Load(board, PlayerType.Black);

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

            var reversi = Game.Load(board, PlayerType.Black);
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(5, 4, "1");

            Assert.AreEqual(PlayerType.White, reversi.Turn());
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

            var reversi = Game.Load(board, PlayerType.Black);

            reversi.PlaceCounter(7, 6, "1");

            Assert.AreEqual(PlayerType.Black, reversi.Turn());
        }

        [Test]
        public void InitiateGameWithTurnColourBlack()
        {
            var reversi = new Game();
            Assert.AreEqual(PlayerType.Black, reversi.Turn());
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.State.GameOver);
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.IsTrue(reversi.State.GameOver);
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            Assert.AreEqual(PlayerType.White, reversi.Turn());
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

            var reversi = Game.Load(board, PlayerType.Black);
            Assert.AreEqual(PlayerType.Black, reversi.GetWinner());
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

            var reversi = Game.Load(board, PlayerType.Black);
            Assert.AreEqual(PlayerType.Black, reversi.GetWinner());
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(1, 1, "1");

            Assert.IsTrue(reversi.State.MoveInvalid);
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(7, 6, "1");

            Assert.IsTrue(reversi.State.PassOccurred);
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(4, 7, "1");

            Assert.IsTrue(reversi.State.GameOver);
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

            var reversi = Game.Load(board, PlayerType.Black);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(7, 6, "1");

            Assert.IsFalse(reversi.State.MoveInvalid);
        }

        [Test]
        public void FirstPlayerIsAssignedBlack()
        {
            var game = new Game();
            game.AddPlayer("1");
            var players = game.GetPlayerList();
            Assert.Multiple(()=>
            {
                Assert.AreEqual(players[0].Type, PlayerType.Black);
                Assert.AreEqual(players[0].UserId, "1"); 
                Assert.AreEqual(players[1].UserId, null);
            });

        }

        [Test]
        public void SecondPlayerIsAssignedWhite()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            var players = game.GetPlayerList();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(players.Count == 2);
                Assert.AreEqual(players[0].Type, PlayerType.Black);
                Assert.AreEqual(players[1].Type, PlayerType.White);
            });
        }

        [Test]
        public void NewGameHasNoPlayers()
        {
            var game = new Game();
            var players = game.GetPlayerList();
            Assert.IsTrue(players.All(x=>x.UserId == null));
        }

        [Test]
        public void ThirdPlayerToJoinDoesNotGetAssigned()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.AddPlayer("3");
            var players = game.GetPlayerList();
            Assert.IsTrue(players.Count == 2);
            Assert.AreEqual(players[0].Type, PlayerType.Black);
            Assert.AreEqual(players[1].Type, PlayerType.White);
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

            var game = Game.Load(board, PlayerType.Black);
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.AddPlayer("3");
            game.PlaceCounter(2, 5, "1");
            game.PlaceCounter(5, 5, "2");
            Assert.IsFalse(game.State.InsufficientPlayers);
        }

        [Test]
        public void TwoPlayersRequiredToMakeAMove()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.PlaceCounter(4, 4, "1");
            Assert.IsTrue(game.State.InsufficientPlayers);
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

            var game = Game.Load(board, PlayerType.Black);
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.PlaceCounter(2, 5, "1");
            Assert.IsFalse(game.State.InsufficientPlayers);
        }

        [Test]
        public void CannotPlaceCounterWhenNotPlayersTurn()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.PlaceCounter(4, 4, "2");
            Assert.IsTrue(game.State.MoveInvalid);
        }

        [Test]
        public void StartGameWithInitialBoard()
        {
            var game = new Game();

            var blackCounters = game.ReversiBoard.GetNumberOfSquaresByColor(Counters.BLACK);
            var whiteCounters = game.ReversiBoard.GetNumberOfSquaresByColor(Counters.WHITE);

            Assert.IsTrue((blackCounters == whiteCounters && blackCounters == 2));            
        }
    }
}
