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
        private List<int[]> directions = Direction.GetDirections();

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
                    switch (ReversiBoard.BoardPosition[row, col])
                    {
                        case 'B':
                            _blackCounters.Add(new Square(row, col));
                            break;
                        case 'W':
                            _whiteCounters.Add(new Square(row, col));
                            break;
                        case '.':
                            _blankSquares.Add(new Square(row, col));
                            break;
                    }
                }
            }
        }

        public bool PlaceCounter(Square selectedSquare)
        {
            if (!GetLegalPositions().Contains(selectedSquare))
                return false;

            ReversiBoard.BoardPosition[selectedSquare.Row, selectedSquare.Column] = TurnColor;
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

            Square currentSquare = UpdateSquare(selectedSquare, Direction.DOWN);
            while(ReversiBoard.BoardPosition[currentSquare.Row, currentSquare.Column] != TurnColor)
            {
                ReversiBoard.BoardPosition[currentSquare.Row, currentSquare.Column] = TurnColor;
                currentSquare = UpdateSquare(currentSquare, Direction.DOWN);
                // TODO: change it to this syntax currentSquare += Direction.DOWN;
            }
        }

        private Square UpdateSquare(Square originalSquare, int[] direction)
        {
            return new Square(originalSquare.Row + direction[0], originalSquare.Column + direction[1]);
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

        private bool IsNextPositionValid(Square startSquare, HashSet<Square> returnValue, int[] direction)
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

        private Square GetSquareInDirection(Square startSquare, int[] direction)
        {
            Square nextSquare = new Square(startSquare.Row + direction[0], startSquare.Column + direction[1]);
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