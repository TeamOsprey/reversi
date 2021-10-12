using System.Collections.Generic;
using System;

namespace Reversi.Logic
{
    public class Game
    {
        public Board ReversiBoard { get; set; }
        public char TurnColour { get; private set; }
        private List<Vector> directions = Direction.GetDirections();

        public Game(string[] board, char turnColor)
        {
            ReversiBoard = new Board(board);

            TurnColour = turnColor;
        }


        public bool PlaceCounter(Square selectedSquare)
        {
            if (!GetLegalPositions().Contains(selectedSquare))
                return false;

            ReversiBoard.Positions[selectedSquare.Row, selectedSquare.Column].Colour = TurnColour;
            CaptureCounters(selectedSquare);
            return true;
        }

        private void CaptureCounters(Square selectedSquare)
        {
            foreach (var direction in directions)
            {
                Square currentSquare = ReversiBoard.Add(selectedSquare, direction);
                List<Square> currentLine = new List<Square>();
                while (SquareIsOpponentColour(currentSquare))
                {
                    currentLine.Add(currentSquare);
                    currentSquare = ReversiBoard.Add(currentSquare, direction);
                }
                if (SquareIsTurnColour(currentSquare))
                {
                    foreach (var square in currentLine)
                    {
                        square.Colour = TurnColour;
                    }
                }

            }
        }

        private bool SquareIsTurnColour(Square currentSquare)
        {
            return currentSquare != null && currentSquare.Colour == TurnColour;
        }

        private bool SquareIsOpponentColour(Square currentSquare)
        {
            return currentSquare != null && currentSquare.Colour != TurnColour && currentSquare.Colour != '.';
        }

        public string[] GetOutput()
        {
            ReversiBoard.SetLegalPositions(GetLegalPositions());

            return ReversiBoard.GetCurrentState();
        }

        private HashSet<Square> GetLegalPositions()
        {
         
            HashSet<Square> returnValue = new HashSet<Square>();
            if (ReversiBoard.GetBlankPositions().Count > 61)
            {
                returnValue.Add(new Square(3, 3));
                returnValue.Add(new Square(3, 4));
            }

            foreach (var startSquare in ReversiBoard.GetBlankPositions())
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

            while (SquareIsOpponentColour(nextSquare))
            {
                nextSquare = ReversiBoard.Add(nextSquare, direction);

                if (SquareIsTurnColour(nextSquare))
                {
                    returnValue.Add(startSquare);
                    result = true;
                }
            }
            return result;
        }
    }
}