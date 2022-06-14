using System.Collections.Generic;
using System;
using CSharpFunctionalExtensions;
using System.Linq;

namespace Reversi.Logic
{
    public class Player
    {
        public Player(char colour, string connectionId)
        {
            Colour = colour;
            ConnectionId = connectionId;
        }

        public char Colour { get; set; }
        public string ConnectionId { get; set; }
    }

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
        public List<Player> PlayerList = new List<Player>();

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
        public bool PlaceCounter(int row, int col, string connectionId)
        {
            if (!IsPlayersTurn(connectionId))
                return false;

            return PlaceCounter(new Square(row, col));
        }

        private bool IsPlayersTurn(string connectionId)
        {
            return PlayerList.Single(x => x.ConnectionId == connectionId).Colour == GetCurrentPlayer();
        }

        public char GetCurrentPlayer()
        {
            return TurnColour;
        }
        public string[] GetOutput()
        {
            ReversiBoard.SetLegalSquares(GetLegalSquares(TurnColour));

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
            return PlayerList;
        }

        public void AddPlayer(string connectionId)
        {
            if(PlayerList.Count == 0)
                PlayerList.Add(new Player(Constants.BLACK, connectionId));
            else if (PlayerList.Any(x => x.Colour == Constants.BLACK) && PlayerList.Count == 1)
                PlayerList.Add(new Player(Constants.WHITE, connectionId));
        }

        #endregion
        #region private methods
        private bool PlaceCounter(Square selectedSquare)
        {
            State = new State();
            if (!ConfirmLegalMove(selectedSquare)) return false;

            ReversiBoard.Squares[selectedSquare.Row, selectedSquare.Column].Colour = TurnColour;
            CaptureCounters(selectedSquare);

            EndTurn();

            return true;
        }

        private bool ConfirmLegalMove(Square selectedSquare)
        {
            if (!ConfirmTwoPlayers()) return false;

            if (!ConfirmLegalPosition(selectedSquare)) return false;

            return true;
        }

        private bool ConfirmLegalPosition(Square selectedSquare)
        {
            if (!GetLegalSquares(TurnColour).Contains(selectedSquare))
            {
                State.MoveInvalid = true;
                return false;
            }

            return true;
        }

        private bool ConfirmTwoPlayers()
        {
            if (PlayerList.Count < 2)
            {
                State.InsufficientPlayers = true;
                return false;
            }

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
            return GetLegalSquares(TurnColour).Count < 1;
        }
        private bool IsGameOver()
        {
            return ReversiBoard.GetBlankSquares().Count == 0 || (GetLegalSquares(TurnColour).Count < 1 && GetLegalSquares(OpponentColour).Count < 1);
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
        private HashSet<Square> GetLegalSquares(char color)
        {
            HashSet<Square> returnValue = new HashSet<Square>();
            var blankSquares = ReversiBoard.GetBlankSquares();
            if (blankSquares.Count > 60)
            {
                AddBlankCentreSquares(returnValue, ReversiBoard.GetBlankSquares());
            }
            else
            {
                AddLegalSquaresToSet(returnValue, color);
            }
            return returnValue;
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