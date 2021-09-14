using System.Collections.Generic;
using System;

namespace Reversi.Logic
{
    public class Game
    {
        public Board ReversiBoard { get; set; }
        public char TurnColor { get; private set; }
        private List<Square> _blackCounters = new List<Square>();
        private List<Square> _whiteCounters = new List<Square>();
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
                        case 'B':
                            _blackCounters.Add(currentSquare);
                            break;
                        case 'W':
                            _whiteCounters.Add(currentSquare);
                            break;
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

            if (selectedSquare.Column != 7)
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
            var nextSquare = GetSquareInDirection(startSquare, direction);
            while (PositionsWithOpponentColor().Contains(nextSquare))
            {
                nextSquare = GetSquareInDirection(nextSquare, direction);
                if (PositionsWithTurnColor().Contains(nextSquare))
                {
                    returnValue.Add(startSquare);
                    result = true;
                }
            }
            return result;
        }

        private Square GetSquareInDirection(Square startSquare, Vector direction)
        {
            Square nextSquare = new Square(startSquare.Row + direction.Horizontal, startSquare.Column + direction.Vertical);
            return nextSquare;
        }

        private List<Square> PositionsWithTurnColor()
        {
            return (TurnColor == 'W' ? _whiteCounters : _blackCounters);
        }

        private List<Square> PositionsWithOpponentColor()
        {
            return (TurnColor == 'B' ? _whiteCounters : _blackCounters);
        }


        private bool IsBlank(Square square)
        {
            if (_whiteCounters == null && _blackCounters == null)
                return true;

            return !_whiteCounters.Contains(square) && !_blackCounters.Contains(square);
        }
    }

}