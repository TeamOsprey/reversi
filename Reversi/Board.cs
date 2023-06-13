﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Board
    {
        private HashSet<Square> _legalSquares;

        private readonly Square[,] _squares;
        public int Size => 8;

        private Square[,] ConvertToSquares(string[] board)
        {
            var squares = new Square[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    squares[row, col] = new Square(row, col, board[row][col]);
                }
            }

            return squares;
        }

        private string[] ConvertToStringArray(Square[,] squares, HashSet<Square> legalSquares)
        {
            string[] output = new string[Size];
            char[] rowString = new char[Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    rowString[col] = squares[row, col].Colour;
                    if (legalSquares != null && legalSquares.Contains(new Square(row, col)))
                    {
                        rowString[col] = '0';
                    }
                }
                output[row] = new string(rowString);
            }

            return output;
        }

        public Board(string[] board)
        {
            _squares = ConvertToSquares(board);
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
            string[] output = new string[Size];
            char[] rowString = new char[Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
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
            var shuffledCounters = Shuffle("BBWW");

            var initRowOne = shuffledCounters.Substring(0, 2);
            var initRowTwo = shuffledCounters.Substring(2, 2);

            var board = new string[] {
                    "........",
                    "........",
                    "........",
                    "...--...",
                    "...--...",
                    "........",
                    "........",
                    "........"};

            board[3] = board[3].Replace("--",initRowOne);
            board[4] = board[4].Replace("--",initRowTwo);

            return board;
        }
        public bool AllInitialTilesPlaced()
        {
            var blankSquares = GetBlankSquares();
            return blankSquares.Count <= 60;
        }
    }
}
