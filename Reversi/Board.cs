using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Board
    {
        public HashSet<Square> LegalPositions { get; set; }

        public Square[,] Positions { get; set; }
        public int Size { get; }

        public Board(string[] board)
        {
            Size = 8;
            Positions = new Square[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Positions[row, col] = new Square(row, col, board[row][col]);
                }
            }
        }

        public Square Add(Square originalSquare, Vector direction)
        {
            int row = originalSquare.Row + direction.Vertical;
            int column = originalSquare.Column + direction.Horizontal;

            if (IsRowInBounds(row) && IsColumnInBounds(column))
                return Positions[row, column];
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

        public List<Square> GetBlankPositions()
        {
            List<Square> blankPositions = new List<Square>();

            foreach (Square item in Positions)
            {
                if (item.Colour == '.')
                    blankPositions.Add(item);
            }

            return blankPositions;
        }

        public void SetLegalPositions(HashSet<Square> legalPositions)
        {
            LegalPositions = legalPositions;
        }

        public string[] GetCurrentState()
        {
            string[] output = new string[8];
            char[] rowString = new char[8];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    rowString[col] = Positions[row, col].Colour;
                    if (LegalPositions != null && LegalPositions.Contains(new Square(row, col)))
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
