using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        private Player _turn;
        
        public Player Turn() => _turn;

        private Player Opponent => _turn is WhitePlayer ? _players.BlackPlayer : _players.WhitePlayer;
        
        private readonly List<Vector> _directions = Direction.GetDirections();
        public State State;

        private readonly PlayerCollection _players = new();

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

            return ReversiBoard.GetCurrentState();
        }
        public Player GetWinner()
        {
            var white = GetNumberOfColor(Counters.White);
            var black = GetNumberOfColor(Counters.Black);

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

        public PlayerCollection Players => _players;

        public void AddPlayer(string userId)
        {
            lock (_players)
            {
                var addedPlayer = _players.TryAdd(userId);

                if (addedPlayer && _players.HasAllPlayers)
                {
                    SetStatus();
                    ActOnStatus();
                }
            }
        }

        public void RemovePlayer(string userId)
        {
            _players.Remove(userId);
        }

        #endregion
        #region private methods

        private bool PlaceCounter(Square selectedSquare)
        {
            if (!ConfirmLegalMove(selectedSquare)) return false;

            ReversiBoard.ChangeSquareColour(selectedSquare.Row, selectedSquare.Column, GetCurrentPlayer().Counter);
            var capturableSquares = GetCapturableSquares(selectedSquare);

            if (capturableSquares.Count() > 0)
            {
                foreach (var square in capturableSquares)
                {
                    square.Colour = GetCurrentPlayer().Counter;
                }
            }

            EndTurn();

            return true;
        }

        private bool ConfirmLegalMove(Square selectedSquare)
        {
            if (ReversiBoard.AllInitialTilesPlaced() && !ConfirmTwoPlayers()) return false;

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
            _turn = Opponent;
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
            return ReversiBoard.GetBlankSquares().Count == 0 || (GetLegalSquares(_turn).Count < 1 && GetLegalSquares(Opponent).Count < 1);
        }
        private HashSet<Square> GetCapturableSquares(Square selectedSquare)
        {
            var capturableSquares = new HashSet<Square>();
            var colourOfTurnPlayer = GetCurrentPlayer().Counter;

            foreach (var direction in _directions)
            {
                Square adjacentSquare = ReversiBoard.GetAdjacentSquare(selectedSquare, direction);
                var currentLine = new HashSet<Square>();
                
                while (SquareIsOtherColour(adjacentSquare, colourOfTurnPlayer))
                {
                    currentLine.Add(adjacentSquare);
                    adjacentSquare = ReversiBoard.GetAdjacentSquare(adjacentSquare, direction);
                }
                if (SquareIsSameColour(adjacentSquare, colourOfTurnPlayer))
                {
                    capturableSquares.UnionWith(currentLine);                    
                }
            }

            return capturableSquares;
        }

        private bool SquareIsSameColour(Square currentSquare, char color)
        {
            return currentSquare != null && currentSquare.Colour == color;
        }
        private bool SquareIsOtherColour(Square currentSquare, char color)
        {
            return currentSquare != null && currentSquare.Colour != color && currentSquare.Colour != Counters.None;
        }
        // todo: don't call this many times - just once there is a new turn
        private Dictionary<Square, HashSet<Square>> GetLegalSquares(Player player)
        {
            HashSet<Square> legalSquares = new HashSet<Square>();
            if (ReversiBoard.AllInitialTilesPlaced())
            {
                foreach (var square in ReversiBoard.GetBlankSquares())
                {
                    var capturableSquares = GetCapturableSquares(square);
                    legalSquares.Add(square);
                }
            }
            return legalSquares;
        }

        //private HashSet<Square> WouldMoveCauseCaptureInGivenDirection(Square startSquare, Vector direction, char color)
        //{
        //    var adjacentSquare = ReversiBoard.GetAdjacentSquare(startSquare, direction);
        //    var capturableSquares = new HashSet<Square>();

        //    while (SquareIsOtherColour(adjacentSquare, color))
        //    {
        //        adjacentSquare = ReversiBoard.GetAdjacentSquare(adjacentSquare, direction);

        //        if (SquareIsSameColour(adjacentSquare, color))
        //        {
        //            capturableSquares.Add();
        //        }
        //    }

        //    return false;
        //}

        #endregion
    }
}