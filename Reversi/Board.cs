using Reversi.Logic.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Board
    {
        private HashSet<Square> _legalSquares;

        private readonly Square[,] _squares;

        const int Size = 8;

        public Board(string[] board)
        {
            _squares = BoardConverter.ConvertToSquares(board);
        }

        public static Board InitializeBoard()
        {
            return new Board(CreateInitialBoard());
        }

        public Square GetAdjacentSquare(Square originalSquare, Vector direction)
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
                if (item.Contents == SquareContents.None || item.Contents == SquareContents.Legal)
                    blankSquares.Add(item);
            }

            return blankSquares;
        }

        public int GetNumberOfSquaresByColor(char color)
        {
            int count = 0;

            foreach (Square item in _squares)
            {
                if (item.Contents == color)
                    count++;
            }

            return count;
        }

        public void SetLegalSquares(HashSet<Square> legalSquares)
        {
            _legalSquares = legalSquares;
            // Clear legal square characters first.
            foreach (Square square in _squares)
            {
                if (square.Contents == SquareContents.Legal)
                {
                    square.Contents = '.';
                }
            }
            // Update legal square characters
            foreach (Square square in _legalSquares)
            {
                _squares[square.Row, square.Column].Contents = SquareContents.Legal;
            }
        }

        public string[] GetCurrentStateAsStringArray()
        {
            return BoardConverter.ConvertToStringArray(_squares);
        }
        public Square[,] GetCurrentStateAsSquares()
        {
            return _squares;
        }

        public void ChangeSquareColour(int selectedSquareRow, int selectedSquareColumn, char turn)
        {
            _squares[selectedSquareRow, selectedSquareColumn].Contents = turn;
        }

        // this shuffle any Enumerable
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {

            Random random = new Random();
            return source.OrderBy(x => random.Next());
        }

        // this shuffle a string
        public static string Shuffle(string str)
        {

            char[] chars = str.ToCharArray();
            var shuffledChars = Shuffle(chars);
            return new string(shuffledChars.ToArray());
        }

        private static string[] CreateInitialBoard()
        {
            var squares = new Square[Size, Size];

            // initialize blank board
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    squares[row, col] = new Square(row, col, SquareContents.None);
                }
            }

            // Get middle four squares
            var middleFourSquares = new Square[4];
            middleFourSquares[0] = squares[3, 3];
            middleFourSquares[1] = squares[3, 4];
            middleFourSquares[2] = squares[4, 3];
            middleFourSquares[3] = squares[4, 4];

            // Shuffle middle four squares
            var shuffledCounters = Shuffle("BBWW");

            // Place shuffled counters in middle four squares
            for (int i = 0; i < 4; i++)
            {
                middleFourSquares[i].Contents = shuffledCounters[i];
            }

            return BoardConverter.ConvertToStringArray(squares);
        }
        public bool AllInitialTilesPlaced()
        {
            var blankSquares = GetBlankSquares();
            return blankSquares.Count <= 60;
        }
    }
}
