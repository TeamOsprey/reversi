using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Board
    {
        public HashSet<Square> LegalSquares { get; set; }

        public Square[,] Squares { get; set; }
        public int Size { get; }

        public Board(string[] board)
        {
            Size = 8;
            Squares = new Square[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Squares[row, col] = new Square(row, col, board[row][col]);
                }
            }
        }

        public Board()
        {

            Size = 8;
            Squares = new Square[Size, Size];
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Squares[row, col] = new Square(row, col, Counters.NONE);
                }
            }
        }

        public Square Add(Square originalSquare, Vector direction)
        {
            int row = originalSquare.Row + direction.Vertical;
            int column = originalSquare.Column + direction.Horizontal;

            if (IsRowInBounds(row) && IsColumnInBounds(column))
                return Squares[row, column];
            else
                return null;
        }

        private bool IsRowInBounds(int row)
        {
            return 0 <= row && row < Size;
        }
        private bool IsColumnInBounds(int column)
        {
            return 0 <= column && column < Size;
        }

        public List<Square> GetBlankSquares()
        {
            List<Square> blankSquares = new List<Square>();

            foreach (Square item in Squares)
            {
                if (item.Colour == Counters.NONE)
                    blankSquares.Add(item);
            }

            return blankSquares;
        }

        public int GetNumberOfSquaresByColor(char color)
        {
            int count = 0;

            foreach (Square item in Squares)
            {
                if (item.Colour == color)
                    count++;
            }

            return count;
        }

        public void SetLegalSquares(HashSet<Square> legalSquares)
        {
            LegalSquares = legalSquares;
        }

        public string[] GetCurrentState()
        {
            string[] output = new string[8];
            char[] rowString = new char[8];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    rowString[col] = Squares[row, col].Colour;
                    if (LegalSquares != null && LegalSquares.Contains(new Square(row, col)))
                    {
                        rowString[col] = '0';
                    }
                }
                output[row] = new string(rowString);
            }

            return output;
        }
    }
}
