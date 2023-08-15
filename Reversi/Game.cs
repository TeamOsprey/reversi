using Reversi.Logic.Converters;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        private Player _turn;
        private Dictionary<Square, HashSet<Square>> _whiteLegalSquareDictionary;
        private Dictionary<Square, HashSet<Square>> _blackLegalSquareDictionary;

        public Player Turn => _turn;

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
            SetLegalSquareDictionaries();
        }

        public Game()
        {
            ReversiBoard = Board.InitializeBoard();
            _turn = _players.BlackPlayer;
            SetLegalSquareDictionaries();
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

        public Player GetPlayer(string userId)
        {
            return _players.SingleOrDefault(x => x.UserId == userId);
        }

        public Square[,] GetOutputAsSquares()
        {
            return ReversiBoard.GetCurrentStateAsSquares();
        }

        public Player GetWinner()
        {
            var white = GetNumberOfColor(SquareContents.White);
            var black = GetNumberOfColor(SquareContents.Black);

            return (white > black) ? _players.WhitePlayer : _players.BlackPlayer;
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
        private bool IsPlayersTurn(string userId)
        {
            return _turn.UserId == userId;
        }

        private void SetLegalSquareDictionaries()
        {
            _whiteLegalSquareDictionary = GetLegalSquares(_players.WhitePlayer);
            _blackLegalSquareDictionary = GetLegalSquares(_players.BlackPlayer);
            ReversiBoard.SetLegalSquares(GetPlayerMoveDictionary(_turn).Keys.ToHashSet());
        }

        private Dictionary<Square, HashSet<Square>> GetPlayerMoveDictionary(Player player)
        {
            if (player is WhitePlayer)
                return _whiteLegalSquareDictionary;
            else
                return _blackLegalSquareDictionary;
        }

        private bool PlaceCounter(Square selectedSquare)
        {
            if (!ConfirmLegalMove(selectedSquare)) return false;

            ReversiBoard.ChangeSquareColour(selectedSquare.Row, selectedSquare.Column, _turn.Counter);
            var capturableSquares = GetCapturableSquares(selectedSquare, _turn);

            if (capturableSquares.Count() > 0)
            {
                foreach (var square in capturableSquares)
                {
                    square.Contents = _turn.Counter;
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
            if (!GetPlayerMoveDictionary(_turn).ContainsKey(selectedSquare))
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
            SetLegalSquareDictionaries();
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
            return GetPlayerMoveDictionary(_turn).Count < 1;
        }
        private bool IsGameOver()
        {
            return ReversiBoard.GetBlankSquares().Count == 0 || (_whiteLegalSquareDictionary.Count < 1 && _blackLegalSquareDictionary.Count < 1);
        }
        private HashSet<Square> GetCapturableSquares(Square selectedSquare, Player player)
        {
            var capturableSquares = new HashSet<Square>();
            var counterColour = player.Counter;

            foreach (var direction in _directions)
            {
                Square adjacentSquare = ReversiBoard.GetAdjacentSquare(selectedSquare, direction);
                var currentLine = new HashSet<Square>();

                while (SquareIsOtherColour(adjacentSquare, counterColour))
                {
                    currentLine.Add(adjacentSquare);
                    adjacentSquare = ReversiBoard.GetAdjacentSquare(adjacentSquare, direction);
                }
                if (SquareIsSameColour(adjacentSquare, counterColour))
                {
                    capturableSquares.UnionWith(currentLine);
                }
            }

            return capturableSquares;
        }

        private bool SquareIsSameColour(Square currentSquare, char color)
        {
            return currentSquare != null && currentSquare.Contents == color;
        }
        private bool SquareIsOtherColour(Square currentSquare, char color)
        {
            return currentSquare != null && currentSquare.Contents != color && currentSquare.Contents != SquareContents.None && currentSquare.Contents != SquareContents.Legal;
        }
        private Dictionary<Square, HashSet<Square>> GetLegalSquares(Player player)
        {
            var legalSquareDictionary = new Dictionary<Square, HashSet<Square>>();

            if (ReversiBoard.AllInitialTilesPlaced())
            {
                foreach (var square in ReversiBoard.GetBlankSquares())
                {
                    var capturableSquares = GetCapturableSquares(square, player);
                    if (capturableSquares.Count() > 0)
                    {
                        legalSquareDictionary.Add(square, capturableSquares);
                    }
                }
            }
            return legalSquareDictionary;
        }
        #endregion
    }
}