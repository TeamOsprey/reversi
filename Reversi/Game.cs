using System.Collections.Generic;
using System;
using CSharpFunctionalExtensions;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        public char TurnColour { get; private set; }
        public char OpponentColour { get { return TurnColour == 'W' ? 'B' : 'W'; } }
        private List<Vector> directions = Direction.GetDirections();
        public Status Status { get; private set; }

        public State State = new State();

        #endregion
        #region constructors
        private Game(string[] board, char turnColor)
        {
            ReversiBoard = new Board(board);
            TurnColour = turnColor;
        }
        public Game()
        {
            ReversiBoard = new Board();
            TurnColour = 'B';
            Status = Status.INPROGESS;
        }
        public static Game Load(string[] board, char turnColour)
        {
            var game = new Game(board, turnColour);
            game.SetStatus();
            game.ActOnStatus();
            return game;
        }
        #endregion
        #region public methods
        public bool PlaceCounter(Square selectedSquare)
        {
            State = new State();
            if (!GetLegalPositions(TurnColour).Contains(selectedSquare))
            {
                State.MoveInvalid = true;
                return false;
            }

            ReversiBoard.Positions[selectedSquare.Row, selectedSquare.Column].Colour = TurnColour;
            CaptureCounters(selectedSquare);

            EndTurn();

            return true;
        }
        public char GetCurrentPlayer()
        {
            return TurnColour;
        }
        public string[] GetOutput()
        {
            ReversiBoard.SetLegalPositions(GetLegalPositions(TurnColour));

            return ReversiBoard.GetCurrentState();
        }
        public char GetWinner()
        {
            var white = GetNumberOfColor('W');
            var black = GetNumberOfColor('B');

            return (white > black) ? 'W' : 'B';
        }
        #endregion
        #region private methods
        private void EndTurn()
        {
            ChangeTurn();
            SetStatus();
            ActOnStatus();
            State.TurnComplete = true;
        }

        private void ActOnStatus()
        {
            if (Status == Status.PASS)
            {
                ChangeTurn();
                SetStatus();
            }

        }
        private void ChangeTurn()
        {
            TurnColour = TurnColour == 'W' ? 'B' : 'W';
        }
        private void SetStatus()
        {
            if (IsGameOver())
            {
                Status = Status.GAMEOVER;
                State.GameOver = true;
            }
            else if (IsPass())
            {
                Status = Status.PASS;
                State.PassOccured = true;
            }
            else
            {
                Status = Status.INPROGESS;
            }
        }
        private bool IsPass()
        {
            return GetLegalPositions(TurnColour).Count < 1;
        }
        private bool IsGameOver()
        {
            return ReversiBoard.GetBlankPositions().Count == 0 || (GetLegalPositions(TurnColour).Count < 1 && GetLegalPositions(OpponentColour).Count < 1);
        }
        private void CaptureCounters(Square selectedSquare)
        {
            foreach (var direction in directions)
            {
                Square currentSquare = ReversiBoard.Add(selectedSquare, direction);
                List<Square> currentLine = new List<Square>();
                while (SquareIsOtherColour(currentSquare, TurnColour))
                {
                    currentLine.Add(currentSquare);
                    currentSquare = ReversiBoard.Add(currentSquare, direction);
                }
                if (SquareIsSameColour(currentSquare, TurnColour))
                {
                    foreach (var square in currentLine)
                    {
                        square.Colour = TurnColour;
                    }
                }
            }
        }
        private bool SquareIsSameColour(Square currentSquare, char color)
        {
            return currentSquare != null && currentSquare.Colour == color;
        }
        private bool SquareIsOtherColour(Square currentSquare, char color)
        {
            return currentSquare != null && currentSquare.Colour != color && currentSquare.Colour != '.';
        }
        private HashSet<Square> GetLegalPositions(char color)
        {
            HashSet<Square> returnValue = new HashSet<Square>();
            if (ReversiBoard.GetBlankPositions().Count > 60)
            {
                AddCentreSquares(returnValue);
            }
            else
            {
                AddLegalPositionsToSet(returnValue, color);
            }
            return returnValue;
        }
        private void AddLegalPositionsToSet(HashSet<Square> returnValue, char color)
        {
            foreach (var startSquare in ReversiBoard.GetBlankPositions())
            {
                foreach (var direction in directions)
                {
                    if (IsNextPositionValid(startSquare, returnValue, direction, color))
                        break;
                }
            }
        }
        private static void AddCentreSquares(HashSet<Square> returnValue)
        {
            returnValue.Add(new Square(3, 3));
            returnValue.Add(new Square(3, 4));
            returnValue.Add(new Square(4, 4));
            returnValue.Add(new Square(4, 3));
        }
        private bool IsNextPositionValid(Square startSquare, HashSet<Square> returnValue, Vector direction, char color)
        {
            bool result = false;
            var nextSquare = ReversiBoard.Add(startSquare, direction);

            while (SquareIsOtherColour(nextSquare, color))
            {
                nextSquare = ReversiBoard.Add(nextSquare, direction);

                if (SquareIsSameColour(nextSquare, color))
                {
                    returnValue.Add(startSquare);
                    result = true;
                }
            }
            return result;
        }
        private int GetNumberOfColor(char color)
        {
            return ReversiBoard.GetNumberOfPositionsByColor(color);
        }
#endregion
    }
    #region enum
    public enum Status
    {
        INPROGESS,
        PASS,
        GAMEOVER
    }
#endregion
}