using NUnit.Framework;
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

            var reversi = Game.Load(board, 'B');

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(4, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));
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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(4, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'W');
            Square selectedSquare = new Square(4, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'W');
            Square selectedSquare = new Square(1, 1);

            Assert.IsFalse(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(3, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(7, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'W');
            Square selectedSquare = new Square(4, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'W');
            Square selectedSquare = new Square(4, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(3, 3);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(0, 3);

            Assert.IsFalse(reversi.PlaceCounter(selectedSquare));
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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(3, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(3, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'W');
            Square selectedSquare = new Square(4,4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(4, 3);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(5, 4);

            Assert.IsFalse(reversi.PlaceCounter(selectedSquare));
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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(5, 4);

            Assert.IsTrue(reversi.PlaceCounter(selectedSquare));

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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(5, 4);

            reversi.PlaceCounter(selectedSquare);

            Assert.AreEqual('W', reversi.GetCurrentPlayer());
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

            var reversi = Game.Load(board, 'B');
            Square selectedSquare = new Square(7, 6);

            reversi.PlaceCounter(selectedSquare);

            Assert.AreEqual('B', reversi.GetCurrentPlayer());
        }

        [Test]
        public void InitiateGameWithTurnColourBlack()
        {
            var reversi = new Game();
            Assert.AreEqual('B', reversi.GetCurrentPlayer());
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

            var reversi = new Game();
            CollectionAssert.AreEqual(expected, reversi.ReversiBoard.GetCurrentState());

        }

        [Test]
        public void InitiateGameWithStatusInProgress()
        {
            var reversi = new Game();
            Assert.AreEqual(Status.INPROGESS, reversi.Status);
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

            var reversi = Game.Load(board, 'B');
            Assert.AreEqual(Status.GAMEOVER, reversi.Status);
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

            var reversi = Game.Load(board, 'B');
            Assert.AreEqual(Status.GAMEOVER, reversi.Status);
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

            var reversi = Game.Load(board, 'B');
            Assert.AreEqual('W', reversi.TurnColour);
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

            var reversi = Game.Load(board, 'B');
            Assert.AreEqual('B', reversi.GetWinner());
        }

        //[Test]
        //public void WhenLoadingGameOverBoardDeclareWinner()
        //{
        //    var board = new string[]{
        //        "BWWWWWWW",
        //        "BWBBWBBB",
        //        "BWBBWBBB",
        //        "BWWWWBBB",
        //        "BWWWWBBB",
        //        "BWWWWBBB",
        //        "BWWBWBBB",
        //        "BBBBBBBW"};

        //    var reversi = Game.Load(board, 'B');
        //    Assert.AreEqual('B', reversi.GetWinner());
        //}
        // [Test]
        // public void WhenPlacingIllegalCounterMessageWarnsUs()
        // {
        //     var board = new string[]{
        //             "........",
        //             "........",
        //             "........",
        //             "........",
        //             "........",
        //             "........",
        //             "........",
        //             "........"};
        //
        //     var reversi = Game.Load(board, 'B');
        //     Square selectedSquare = new Square(1, 1);
        //
        //     Assert.AreEqual("Position is not legal", reversi.PlaceCounter(selectedSquare).Error);
        // }
        //
        // [Test]
        // public void IfPlayersMoveIsOverNextPlayerTurnsPassAndMessageDeclarePass()
        // {
        //     var board = new string[]{
        //         "BWWWWWWW",
        //         "BWBBW...",
        //         "BWBBW...",
        //         "BWWWW...",
        //         "BWWWW...",
        //         "BWWWW...",
        //         "BWWBW...",
        //         "BBBBBW.."};
        //
        //     var reversi = Game.Load(board, 'B');
        //     Square selectedSquare = new Square(7, 6);
        //
        //     Assert.AreEqual("It is now B's turn.", reversi.PlaceCounter(selectedSquare).Value);
        // }
    }
}
