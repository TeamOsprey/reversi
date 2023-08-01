namespace Reversi.Logic.Converters
{
    public static class BoardConverter
    {
        public static string[] GetOutputAsStringArray(Square[,] squares)
        {
            var size = squares.GetLength(0);
            string[] output = new string[size];
            char[] rowString = new char[size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    rowString[col] = squares[row, col].Contents;
                    if (squares[row, col].Contents == SquareContents.Legal)
                    {
                        rowString[col] = SquareContents.Legal;
                    }
                }
                output[row] = new string(rowString);
            }

            return output;
        }

    }
}
