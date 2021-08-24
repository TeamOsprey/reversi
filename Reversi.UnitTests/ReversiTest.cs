using NUnit.Framework;
using Reversi.Logic;

namespace Reversi.UnitTests
{

    [TestFixture]
    public class ReversiTest
    {
        [Test]
        public void initial_board_positions_with_black_turn()
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


            var reversi = new Game(board, 'B');
            var output = reversi.GetOutput();

            Assert.AreEqual('B', output[4][4]);
            Assert.AreEqual('B', output[4][5]);
            Assert.AreEqual('W', output[5][4]);
            Assert.AreEqual('W', output[5][5]);
            Assert.AreEqual('B', reversi.TurnColor);
        }

        [Test]
        public void return_the_only_legal_position()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....W...",
            "........",
            "........"};
            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....W...",
            "....0...",
            "........"};

            var reversi = new Game(board, 'B');
            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_postition_for_two_trapped_counters_Below()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....W...",
            "....W...",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....B...",
            "....W...",
            "....W...",
            "....0..."};


            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_position_for_two_trapped_counters_Above()
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

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "....0...",
            "....W...",
            "....W...",
            "....B..."};

            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_position_for_two_trapped_counters_Diagonal()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "...B....",
            "....W...",
            ".....W..",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "...B....",
            "....W...",
            ".....W..",
            "......0."};

            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_the_only_legal_position_for_two_trapped_counters_Left()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "...WWB..",
            "........",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..0WWB..",
            "........",
            "........",
            "........"};

            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void will_not_return_path_over_the_edge()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "WWB.....",
            "........",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "WWB.....",
            "........",
            "........",
            "........"};

            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void path_will_not_extend_past_own_counter()
        {
            var board = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..BWB...",
            "........",
            "........",
            "........"};

            var expected = new string[]{
            "........",
            "........",
            "........",
            "........",
            "..BWB...",
            "........",
            "........",
            "........"};

            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void return_multiple_legal_positions()
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

            var reversi = new Game(board, 'B');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }

        [Test]
        public void If_Next_Color_Is_White_Return_Expected_Legal_Positions()
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

            var reversi = new Game(board, 'W');

            CollectionAssert.AreEqual(expected, reversi.GetOutput());
        }
    }
}