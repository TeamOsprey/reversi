using System.Collections.Generic;
using System;
using System.Linq;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        private char turnColour;
        private char OpponentColour => (turnColour == Constants.WHITE) ? Constants.BLACK : Constants.WHITE;
        private readonly List<Vector> directions = Direction.GetDirections();
        public State State = new State();
        private static readonly List<int[]> initialValues = new()
        {
                new[] { 3,3 },
                new[] { 3,4 },
                new[] { 4,4 },
                new[] { 4,3 },
        };

        private List<Player> playerList = new();

        #endregion
        #region constructors
        private Game(string[] board, char turnColor)
        {
            ReversiBoard = new Board(board);
            turnColour = turnColor;
        }
        public Game(bool placeInitialCounters = false)
        {
            ReversiBoard = new Board();
            turnColour = Constants.BLACK;
            State.InProgress = true;
            if (placeInitialCounters) RandomlyPlaceInitialCounters();
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
        public bool PlaceCounter(int row, int col, string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId) || !IsPlayersTurn(connectionId))
            {
                State.MoveInvalid = true;
                return false;
            }

            return PlaceCounter(new Square(row, col));
        }

        private bool IsPlayersTurn(string connectionId)
        {
            return playerList.Any(x => x.ConnectionId == connectionId && x.Colour == turnColour);
        }

        public char GetCurrentPlayerColour()
        {
            return turnColour;
        }
        public char GetPlayerColour(string connectionId)
        {
            var player = playerList.SingleOrDefault(x => x.ConnectionId == connectionId);
            return player?.Colour ?? Constants.OBSERVER;
        }
        public Player GetCurrentPlayer()
        {
            return playerList.Single(x => x.Colour == turnColour);
        }
        public string[] GetOutput()
        {
            ReversiBoard.SetLegalSquares(GetLegalSquares(turnColour));

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
        public int GetNumberOfColor(char color)
        {
            return ReversiBoard.GetNumberOfSquaresByColor(color);
        }

        public List<Player> GetPlayerList()
        {
            return playerList;
        }

        public void AddPlayer(string connectionId)
        {
            if (playerList.Any(x => x.ConnectionId == connectionId)) return;

            if(playerList.Count == 0)
                playerList.Add(new Player(Constants.BLACK, connectionId));
            else if (playerList.Any(x => x.Colour == Constants.BLACK) && playerList.Count == 1)
                playerList.Add(new Player(Constants.WHITE, connectionId));
        }

        #endregion
        #region private methods
        private bool PlaceInitialCounter(int row, int col)
        {
            return PlaceCounter(new Square(row, col));
        }

        private bool PlaceCounter(Square selectedSquare)
        {
            State = new State();
            if (!ConfirmLegalMove(selectedSquare)) return false;

            ReversiBoard.Squares[selectedSquare.Row, selectedSquare.Column].Colour = turnColour;
            CaptureCounters(selectedSquare);

            EndTurn();

            return true;
        }

        private bool ConfirmLegalMove(Square selectedSquare)
        {
            if (AllInitialTilesPlaced() && !ConfirmTwoPlayers()) return false;

            if (!ConfirmLegalPosition(selectedSquare)) return false;

            return true;
        }

        private bool ConfirmLegalPosition(Square selectedSquare)
        {
            if (!GetLegalSquares(turnColour).Contains(selectedSquare))
            {
                State.MoveInvalid = true;
                return false;
            }

            return true;
        }

        private bool ConfirmTwoPlayers()
        {
            if (playerList.Count < 2)
            {
                State.InsufficientPlayers = true;
                return false;
            }

            return true;
        }

        private void RandomlyPlaceInitialCounters()
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
            turnColour = turnColour == Constants.WHITE ? Constants.BLACK : Constants.WHITE;
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
            return GetLegalSquares(turnColour).Count < 1;
        }
        private bool IsGameOver()
        {
            return ReversiBoard.GetBlankSquares().Count == 0 || (GetLegalSquares(turnColour).Count < 1 && GetLegalSquares(OpponentColour).Count < 1);
        }
        private void CaptureCounters(Square selectedSquare)
        {
            foreach (var direction in directions)
            {
                Square currentSquare = ReversiBoard.Add(selectedSquare, direction);
                List<Square> currentLine = new List<Square>();
                while (SquareIsOtherColour(currentSquare, turnColour))
                {
                    currentLine.Add(currentSquare);
                    currentSquare = ReversiBoard.Add(currentSquare, direction);
                }
                if (SquareIsSameColour(currentSquare, turnColour))
                {
                    foreach (var square in currentLine)
                    {
                        square.Colour = turnColour;
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
        private HashSet<Square> GetLegalSquares(char color)
        {
            HashSet<Square> returnValue = new HashSet<Square>();
            if (!AllInitialTilesPlaced())
            {
                AddBlankCentreSquares(returnValue, ReversiBoard.GetBlankSquares());
            }
            else
            {
                AddLegalSquaresToSet(returnValue, color);
            }
            return returnValue;
        }

        private bool AllInitialTilesPlaced()
        {
            var blankSquares = ReversiBoard.GetBlankSquares();
            return blankSquares.Count <= 60;
        }

        private void AddLegalSquaresToSet(HashSet<Square> returnValue, char color)
        {
            foreach (var startSquare in ReversiBoard.GetBlankSquares())
            {
                foreach (var direction in directions)
                {
                    if (IsNextSquareValid(startSquare, returnValue, direction, color))
                        break;
                }
            }
        }
        private static void AddBlankCentreSquares(HashSet<Square> returnValue, List<Square> blankSquares)
        {
            foreach(var init in initialValues)
            {
                Square square = new Square(init[0], init[1]);
                if (blankSquares.Contains(square))
                    returnValue.Add(square);
            }
        }
        private bool IsNextSquareValid(Square startSquare, HashSet<Square> returnValue, Vector direction, char color)
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

        #endregion
    }
}