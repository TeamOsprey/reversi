using System;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Board
    {
        private HashSet<Square> _legalSquares;

        private readonly Square[,] _squares;
        public int Size { get; }

        public Board(string[] board)
        {
            Size = 8;
            _squares = new Square[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    _squares[row, col] = new Square(row, col, board[row][col]);
                }
            }
        }


        public static Board InitializeBoard()
        {
            return new Board(CreateInitialBoard());
        }



        public Square Add(Square originalSquare, Vector direction)
        {
            int row = originalSquare.Row + direction.Vertical;
            int column = originalSquare.Column + direction.Horizontal;

            if (IsRowInBounds(row) && IsColumnInBounds(column))
                return _squares[row, column];
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

            foreach (Square item in _squares)
            {
                if (item.Colour == Counters.NONE)
                    blankSquares.Add(item);
            }

            return blankSquares;
        }

        public int GetNumberOfSquaresByColor(char color)
        {
            int count = 0;

            foreach (Square item in _squares)
            {
                if (item.Colour == color)
                    count++;
            }

            return count;
        }

        public void SetLegalSquares(HashSet<Square> legalSquares)
        {
            _legalSquares = legalSquares;
        }

        public string[] GetCurrentState()
        {
            string[] output = new string[8];
            char[] rowString = new char[8];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    rowString[col] = _squares[row, col].Colour;
                    if (_legalSquares != null && _legalSquares.Contains(new Square(row, col)))
                    {
                        rowString[col] = '0';
                    }
                }
                output[row] = new string(rowString);
            }
            
            return output;
        }

        public void ChangeSquareColour(int selectedSquareRow, int selectedSquareColumn, char turn)
        {
            _squares[selectedSquareRow, selectedSquareColumn].Colour = turn;
        }

        private static string[] CreateInitialBoard()
        {
            var initialBoards = new List<string[]>();
            initialBoards.Add(new string[]{
                "........",
                "........",
                "........",
                "...WW...",
                "...BB...",
                "........",
                "........",
                "........"});

            initialBoards.Add(new string[]{
                "........",
                "........",
                "........",
                "...WB...",
                "...WB...",
                "........",
                "........",
                "........"});
            initialBoards.Add(new string[]{
                "........",
                "........",
                "........",
                "...WB...",
                "...BW...",
                "........",
                "........",
                "........"});
            initialBoards.Add(new string[]{
                "........",
                "........",
                "........",
                "...BW...",
                "...WB...",
                "........",
                "........",
                "........"});
            initialBoards.Add(new string[]{
                "........",
                "........",
                "........",
                "...BB...",
                "...WW...",
                "........",
                "........",
                "........"});

            initialBoards.Add(new string[]{
                "........",
                "........",
                "........",
                "...BW...",
                "...BW...",
                "........",
                "........",
                "........"});
            
            return ChooseInitialLayout(initialBoards);
        }

        private static string[] ChooseInitialLayout(List<string[]> initialBoards)
        {
            var random = new Random();
            var selected = random.Next(0, 6);
            return initialBoards[selected];
        }
    }
}
