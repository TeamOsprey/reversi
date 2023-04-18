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
                if (item.Colour == Counters.None)
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

        // this shuffle any Enumerable
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            
            Random random = new Random();
            return source.OrderBy(x => random.Next());
        }

        // this shuffle a string
        public static string Shuffle(this string str)
        {
 
            char[] chars = str.ToCharArray();
            var shuffledChars = Shuffle(chars);
            return new string(shuffledChars.ToArray());
        }

        private static string[] CreateInitialBoard()
        {
            var myString = "BBWW";
            var shuffledString = Shuffle(myString);

            var board = new string[] {
                "........",
                    "........",
                    "........",
                    "...--...",
                    "...--...",
                    "........",
                    "........",
                    "........"};

            var initialBoards = new List<string[]>
            {
                
                new string[]{
                    "........",
                    "........",
                    "........",
                    "...WW...",
                    "...BB...",
                    "........",
                    "........",
                    "........"},
                new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "...WB...",
                    "........",
                    "........",
                    "........"},
                new string[]{
                    "........",
                    "........",
                    "........",
                    "...WB...",
                    "...BW...",
                    "........",
                    "........",
                    "........"},
                new string[]{
                    "........",
                    "........",
                    "........",
                    "...BW...",
                    "...WB...",
                    "........",
                    "........",
                    "........"},
                new string[]{
                    "........",
                    "........",
                    "........",
                    "...BB...",
                    "...WW...",
                    "........",
                    "........",
                    "........"},
                new string[]{
                    "........",
                    "........",
                    "........",
                    "...BW...",
                    "...BW...",
                    "........",
                    "........",
                    "........"}
            };

            return ChooseInitialLayout(initialBoards);
        }



        private static string[] ChooseInitialLayout(List<string[]> initialBoards)
        {
            var random = new Random();
            var selected = random.Next(0, 6);
            return initialBoards[selected];
        }

        private char[] RandomlyPlaceInitialCounters()
        {
            var visited = new HashSet<int>();

            var random = new Random();
            do
            {
                var selected = random.Next(0, 4);
                if (!visited.Contains(selected))
                {
                    visited.Add(selected);
                    PlaceInitialCounter(initialValues[selected][0], initialValues[selected][1]);
                }

            } while (visited.Count < 4);
        }
    }
}
