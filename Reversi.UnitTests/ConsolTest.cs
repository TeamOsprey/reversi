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

            var reversi = new Game(board, 'B');

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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'W');
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

            var reversi = new Game(board, 'W');
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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'W');
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

            var reversi = new Game(board, 'W');
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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'B');
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

            var reversi = new Game(board, 'B');
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
    }
}
