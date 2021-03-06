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
    public class ConsolTest
    {
        //public void After_Consol_File_Saved_To_AppData_Game_State()
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);

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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.WHITE);
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
        public void IfWhiteUserMoveIllegalDontPlaceCounter()
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

            var reversi = Game.Load(board, Constants.Roles.WHITE);

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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.WHITE);
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

            var reversi = Game.Load(board, Constants.Roles.WHITE);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);

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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.WHITE);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);

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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(5, 4, "1");

            Assert.AreEqual(Constants.Roles.WHITE, reversi.Turn());
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);

            reversi.PlaceCounter(7, 6, "1");

            Assert.AreEqual(Constants.Roles.BLACK, reversi.Turn());
        }

        [Test]
        public void InitiateGameWithTurnColourBlack()
        {
            var reversi = new Game();
            Assert.AreEqual(Constants.Roles.BLACK, reversi.Turn());
        }

        [Test]
        public void InitiateGameWithCorrectBoard()
        {
            var expected = new string[]{
                "........",
                "........",
                "........",
                "........",
                "........",
                "........",
                "........",
                "........"};

            var reversi = new Game(false);
            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());

        }

        [Test]
        public void InitiateGameWithStatusInProgress()
        {
            var reversi = new Game();
            Assert.IsTrue(reversi.State.InProgress);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            Assert.AreEqual(Constants.Roles.WHITE, reversi.Turn());
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            Assert.AreEqual(Constants.Roles.BLACK, reversi.GetWinner());
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            Assert.AreEqual(Constants.Roles.BLACK, reversi.GetWinner());
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(7, 6, "1");

            Assert.IsTrue(reversi.State.PassOccured);
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(4, 7, "1");

            Assert.IsTrue(reversi.State.GameOver);
        }

        [Test]
        public void IfPlayersMoveWorksProperlyFlagDeclareTurnOver()
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

            var reversi = Game.Load(board, Constants.Roles.BLACK);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            reversi.PlaceCounter(7, 6, "1");

            Assert.IsTrue(reversi.State.TurnComplete);
        }

        [Test]
        public void FirstPlayerIsAssignedBlack()
        {
            var game = new Game();
            game.AddPlayer("1");
            var players = game.GetPlayerList();
            Assert.IsTrue(players.Count == 1);
            Assert.AreEqual(players[0].Role, Constants.Roles.BLACK);
            Assert.AreEqual(players[0].ConnectionId, "1"); // TODO: refactor to compare all properties at once
        }

        [Test]
        public void SecondPlayerIsAssignedWhite()
        {
            var game = new Game();
            game.AddPlayer("1");
            game.AddPlayer("2");
            var players = game.GetPlayerList();
            Assert.IsTrue(players.Count == 2);
            Assert.AreEqual(players[0].Role, Constants.Roles.BLACK);
            Assert.AreEqual(players[1].Role, Constants.Roles.WHITE);
        }

        [Test]
        public void NewGameHasNoPlayers()
        {
            var game = new Game();
            var players = game.GetPlayerList();
            Assert.IsTrue(players.Count == 0);
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
            Assert.AreEqual(players[0].Role, Constants.Roles.BLACK);
            Assert.AreEqual(players[1].Role, Constants.Roles.WHITE);
        }

        [Test]
        public void AfterThirdPlayerJoinsOtherPlayersCanStillMove()
        {
            var game = new Game(false);
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.AddPlayer("3");
            game.PlaceCounter(4, 4, "1");
            game.PlaceCounter(3, 4, "2");
            Assert.IsTrue(game.State.TurnComplete);
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
            var game = new Game(false);
            game.AddPlayer("1");
            game.AddPlayer("2");
            game.PlaceCounter(4, 4, "1");
            Assert.IsTrue(game.State.TurnComplete);
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

    }
}
