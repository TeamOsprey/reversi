namespace Reversi.Logic.Converters
{
    public static class BoardConverter
    {
        public static string[] ConvertToStringArray(Square[,] squares)
        {
            var size = squares.GetLength(0);

            string[] output = new string[size];
            char[] rowString = new char[size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                    rowString[col] = squares[row, col].Contents;

                output[row] = new string(rowString);
            }

            return output;
        }

        public static Square[,] ConvertToSquares(string[] board)
        {
            var size = board.Length;

            var squares = new Square[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    squares[row, col] = new Square(row, col, board[row][col]);
                }
            }

            return squares;
        }
    }
}
