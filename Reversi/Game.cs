﻿using System.Collections.Generic;
using System;
using CSharpFunctionalExtensions;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        private char TurnColour { get; set; }
        private char OpponentColour { get { return TurnColour == Constants.WHITE ? Constants.BLACK : Constants.WHITE; } }
        private List<Vector> directions = Direction.GetDirections();
        public State State = new State();
        private static List<int[]> initialValues = new List<int[]>
            {
                new int[] { 3,3 },
                new int[] { 3,4 },
                new int[] { 4,4 },
                new int[] { 4,3 },
            };

        #endregion
        #region constructors
        private Game(string[] board, char turnColor)
        {
            ReversiBoard = new Board(board);
            TurnColour = turnColor;
        }
        public Game(bool randomizeStartingMoves = false)
        {
            ReversiBoard = new Board();
            TurnColour = Constants.BLACK;
            State.InProgress = true;
            if(randomizeStartingMoves) RandomizeStartingMoves();
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
        public bool PlaceCounter(int row, int col)
        {
            return PlaceCounter(new Square(row, col));
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
            var white = GetNumberOfColor(Constants.WHITE);
            var black = GetNumberOfColor(Constants.BLACK);

            return (white > black) ? Constants.WHITE : Constants.BLACK;
        }

        public string[] DisplayBoard()
        {
            return ReversiBoard.GetCurrentState();
        }


        #endregion
        #region private methods
        private bool PlaceCounter(Square selectedSquare)
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
        private void RandomizeStartingMoves()
        {
            var visited = new HashSet<int>();

            var random = new Random();
            do
            {
                var selected = random.Next(0, 4);
                if (!visited.Contains(selected))
                {
                    visited.Add(selected);
                    PlaceCounter(initialValues[selected][0], initialValues[selected][1]);
                }

            } while (visited.Count < 4);
        }
        private void EndTurn()
        {
            ChangeTurn();
            SetStatus();
            ActOnStatus();
            State.TurnComplete = true;
        }

        private void ActOnStatus()
        {
            if (State.PassOccured)
            {
                ChangeTurn();
                SetStatus();
            }

        }
        private void ChangeTurn()
        {
            TurnColour = TurnColour == Constants.WHITE ? Constants.BLACK : Constants.WHITE;
        }
        private void SetStatus()
        {
            if (IsGameOver())
                State.GameOver = true;
            else if (IsPass())
                State.PassOccured = true;
            else
                State.InProgress = true;
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
            return currentSquare != null && currentSquare.Colour != color && currentSquare.Colour != Constants.PERIOD;
        }
        private HashSet<Square> GetLegalPositions(char color)
        {
            HashSet<Square> returnValue = new HashSet<Square>();
            var blankPositions = ReversiBoard.GetBlankPositions();
            if (blankPositions.Count > 60)
            {
                AddCentreSquares(returnValue, ReversiBoard.GetBlankPositions());
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
        private static void AddCentreSquares(HashSet<Square> returnValue, List<Square> blankPositions)
        {
            foreach(var init in initialValues)
                if(blankPositions.Contains(new Square(init[0], init[1])))
                    returnValue.Add(new Square(init[0], init[1]));
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
        public int GetNumberOfColor(char color)
        {
            return ReversiBoard.GetNumberOfPositionsByColor(color);
        }
#endregion
    }
}