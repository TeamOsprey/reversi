using System.Collections.Generic;
using System;
using System.Linq;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        private Player _turn;
        
        public Player Turn() => _turn;

        private Player _opponent => _turn is WhitePlayer ? _players.BlackPlayer : _players.WhitePlayer;
        
        private readonly List<Vector> directions = Direction.GetDirections();
        public State State;
        private static readonly List<int[]> initialValues = new()
        {
                new[] { 3,3 },
                new[] { 3,4 },
                new[] { 4,4 },
                new[] { 4,3 },
        };

        private PlayerCollection _players = new();
        private bool _setInitialStatus;

        #endregion
        #region constructors
        private Game(string[] board, bool isTurnBlack)
        {
            ReversiBoard = new Board(board);
            _turn = isTurnBlack ? _players.BlackPlayer : _players.WhitePlayer;
        }
        public Game()
        {
            ReversiBoard = Board.InitializeBoard();
            _turn = _players.BlackPlayer;
        }
        public static Game Load(string[] board, bool isTurnBlack)
        {
            var game = new Game(board, isTurnBlack);
            return game;
        }
        #endregion
        #region public methods
        public bool PlaceCounter(int row, int col, string userId)
        {
            State = null;
            if (string.IsNullOrEmpty(userId) || !IsPlayersTurn(userId))
            {
                State = new MoveInvalid();
                return false;
            }

            return PlaceCounter(new Square(row, col));
        }

        private bool IsPlayersTurn(string userId)
        {
            return _turn.UserId == userId;
        }

        public Player GetPlayer(string userId)
        {
            return _players.SingleOrDefault(x => x.UserId == userId);
        }
        public Player GetCurrentPlayer()
        {
            return _turn;
        }
        public string[] GetOutput()
        {
            if(_players.HasAllPlayers)
                ReversiBoard.SetLegalSquares(GetLegalSquares(_turn)); // todo: consider renaming

            var showLegalMoves = true; // maybe use this.IsPlayersTurn(userId)
            return ReversiBoard.GetCurrentState(showLegalMoves);
        }
        public Player GetWinner()
        {
            var white = GetNumberOfColor(Counters.WHITE);
            var black = GetNumberOfColor(Counters.BLACK);

            return (white > black) ? _players.WhitePlayer : _players.BlackPlayer;
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
            return _players.Players;
        }

        public void AddPlayer(string userId)
        {
            lock (_players)
            {
                if (_players.HasAllPlayers || _players.Contains(userId))
                    return;

                _players.Add(userId);

                if (!_setInitialStatus && _players.HasAllPlayers)
                {
                    SetStatus();
                    ActOnStatus();
                    _setInitialStatus = true;
                }
            }
        }

        public void RemovePlayer(string userId)
        {
            _players.Remove(userId);
        }

        #endregion
        #region private methods
        private bool PlaceInitialCounter(int row, int col)
        {
            return PlaceCounter(new Square(row, col));
        }

        private bool PlaceCounter(Square selectedSquare)
        {
            if (!ConfirmLegalMove(selectedSquare)) return false;

            ReversiBoard.ChangeSquareColour(selectedSquare.Row, selectedSquare.Column, GetCurrentPlayer().Counter);
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
            if (!GetLegalSquares(_turn).Contains(selectedSquare))
            {
                State = new MoveInvalid();
                return false;
            }

            return true;
        }

        private bool ConfirmTwoPlayers()
        {
            if (!_players.HasAllPlayers)
            {
                State = new InsufficientPlayers();
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
        }

        private void ActOnStatus()
        {
            if (State is PassOccurred)
            {
                ChangeTurn();
                SetStatus();
            }

        }
        private void ChangeTurn()
        {
            _turn = _opponent;
        }
        private void SetStatus()
        {
            ConfirmTwoPlayers();

            if (IsGameOver())
                State = new GameOver();
            else if (IsPass())
                State = new PassOccurred();
        }
        private bool IsPass()
        {
            return GetLegalSquares(_turn).Count < 1;
        }
        private bool IsGameOver()
        {
            return ReversiBoard.GetBlankSquares().Count == 0 || (GetLegalSquares(_turn).Count < 1 && GetLegalSquares(_opponent).Count < 1);
        }
        private void CaptureCounters(Square selectedSquare)
        {
            foreach (var direction in directions)
            {
                Square currentSquare = ReversiBoard.Add(selectedSquare, direction);
                List<Square> currentLine = new List<Square>();
                var colourOfTurnPlayer = GetCurrentPlayer().Counter;
                while (SquareIsOtherColour(currentSquare, colourOfTurnPlayer))
                {
                    currentLine.Add(currentSquare);
                    currentSquare = ReversiBoard.Add(currentSquare, direction);
                }
                if (SquareIsSameColour(currentSquare, colourOfTurnPlayer))
                {
                    foreach (var square in currentLine)
                    {
                        square.Colour = colourOfTurnPlayer;
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
            return currentSquare != null && currentSquare.Colour != color && currentSquare.Colour != Counters.NONE;
        }
        private HashSet<Square> GetLegalSquares(Player player)
        {
            HashSet<Square> returnValue = new HashSet<Square>();
            if (!AllInitialTilesPlaced())
            {
                AddBlankCentreSquares(returnValue, ReversiBoard.GetBlankSquares());
            }
            else
            {
                AddLegalSquaresToSet(returnValue, player.Counter);
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