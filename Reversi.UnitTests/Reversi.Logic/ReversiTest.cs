using NUnit.Framework;
using Reversi.Logic;

namespace Reversi.UnitTests
{

    [TestFixture]
    public class ReversiTest
    {
        [Test]
        public void initial_board_squares_with_black_turn()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....BB..",
            "....WW..",
            "........",
            "........"};


            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            var output = reversi.GetOutput();

            Assert.AreEqual(Counters.BLACK, output[4][4]);
            Assert.AreEqual(Counters.BLACK, output[4][5]);
            Assert.AreEqual(Counters.WHITE, output[5][4]);
            Assert.AreEqual(Counters.WHITE, output[5][5]);
            Assert.IsTrue(reversi.Turn() is BlackPlayer);
        }
     
        [Test]
        public void return_the_only_legal_square()
        {
            var board = new string[]{
            "........",
            "........",
            "....B...",
            "....B...",
            "....B...",
            "....W...",
            "........",
            "........"};
            var expected = new string[]{
            "........",
            "........",
            "....B...",
            "....B...",
            "....B...",
            "....W...",
            "....0...",
            "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");
            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_position_for_two_trapped_counters_Below()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....W...",
            "....W...",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "....B...",
            "....B...",
            "....W...",
            "....W...",
            "....0..."};


            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_square_for_two_trapped_counters_Above()
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

            var expected = new string[]{
            "........",
            "........",
            "........",
            "....0...",
            "....W...",
            "....W...",
            "....W...",
            "....B..."};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_square_for_two_trapped_counters_Diagonal()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "..B.....",
            "...W....",
            "....W...",
            ".....W..",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "..B.....",
            "...W....",
            "....W...",
            ".....W..",
            "......0."};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_square_for_two_trapped_counters_Left()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "...WWBB.",
            "........",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..0WWBB.",
            "........",
            "........",
            "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void path_over_the_edge_passes_to_other_colour()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "WWBB....",
            "........",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "WWBB0...",
            "........",
            "........",
            "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void path_extending_past_own_counter_passes()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..BWWB..",
            "........",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            ".0BWWB0.",
            "........",
            "........",
            "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_multiple_legal_squares()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..BBB...",
            "...W....",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..BBB...",
            "...W....",
            "..000...",
            "........"};

            var reversi = Game.Load(board, true);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void If_Next_Color_Is_White_Return_Expected_Legal_Squares()
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

            var expected = new string[]{
            "........",
            "........",
            "....0...",
            "...WB0..",
            "..0BW...",
            "...0....",
            "........",
            "........"};

            var reversi = Game.Load(board, false);
            reversi.AddPlayer("1");
            reversi.AddPlayer("2");

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }
    }
}