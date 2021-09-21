using System.Collections.Generic;
using System;

namespace Reversi.Logic
{
    public class Game
    {
        public Board ReversiBoard { get; set; }
        public char TurnColor { get; private set; }
        private List<Square> _blankSquares = new List<Square>();
        private List<Vector> directions = Direction.GetDirections();

        public Game(string[] board, char turnColor)
        {
            ReversiBoard = new Board(board);

            TurnColor = turnColor;
            SetCounter();
        }

        private void SetCounter()
        {
            for (int row = 0; row < ReversiBoard.Size; row++)
            {
                for (int col = 0; col < ReversiBoard.Size; col++)
                {
                    var currentSquare = ReversiBoard.Positions[row, col];
                    switch (currentSquare.Colour)
                    {
                        case '.':
                            _blankSquares.Add(currentSquare);
                            break;
                    }
                }
            }
        }

        public bool PlaceCounter(Square selectedSquare)
        {
            if (!GetLegalPositions().Contains(selectedSquare))
                return false;

            ReversiBoard.Positions[selectedSquare.Row, selectedSquare.Column].Colour = TurnColor;
            CaptureCounters(selectedSquare);
            return true;
        }

        private void CaptureCounters(Square selectedSquare)
        {
            
            foreach (var direction in directions)
            {
                //if (direction == directions)
                    break;
            }

            if (selectedSquare.Row != 7)
            {
                Square currentSquare = ReversiBoard.Add(selectedSquare, Direction.DOWN);
                while (currentSquare.Colour != TurnColor)
                {
                    currentSquare.Colour = TurnColor;
                    currentSquare = ReversiBoard.Add(currentSquare, Direction.DOWN);
                }
            }
            else
            {
                Square currentSquare = ReversiBoard.Add(selectedSquare, Direction.UP);
                while (currentSquare.Colour != TurnColor)
                {
                    currentSquare.Colour = TurnColor;
                    currentSquare = ReversiBoard.Add(currentSquare, Direction.UP);
                }

            }
        }

        public string[] GetOutput()
        {
            ReversiBoard.SetLegalPositions(GetLegalPositions());

            return ReversiBoard.GetCurrentState();
        }

        private HashSet<Square> GetLegalPositions()
        {
            HashSet<Square> returnValue = new HashSet<Square>();

            foreach (var startSquare in _blankSquares)
            {
                foreach (var direction in directions)
                {
                    if (IsNextPositionValid(startSquare, returnValue, direction))
                        break;
                }
            }

            return returnValue;
        }

        private bool IsNextPositionValid(Square startSquare, HashSet<Square> returnValue, Vector direction)
        {
            bool result = false;
            var nextSquare = ReversiBoard.Add(startSquare, direction);

            while (nextSquare != null && nextSquare.Colour != TurnColor && nextSquare.Colour != '.')
            {
                nextSquare = ReversiBoard.Add(nextSquare, direction);

                if (nextSquare != null && nextSquare.Colour == TurnColor)
                {
                    returnValue.Add(startSquare);
                    result = true;
                }
            }
            return result;
        }
    }

}