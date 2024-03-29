﻿using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class Game
    {
        private Board Board { get; }
        public Player Turn => _turn;
        public State State { get; private set; }
        public Square[,] Squares => Board.Squares;
        public PlayerCollection Players => _players;

        #region fields
        private Player _turn;
        private Dictionary<Square, HashSet<Square>> _whiteLegalSquareDictionary;
        private Dictionary<Square, HashSet<Square>> _blackLegalSquareDictionary;

        private Player Opponent => _turn is WhitePlayer ? _players.BlackPlayer : _players.WhitePlayer;

        private readonly List<Vector> _directions = Direction.GetDirections();

        private readonly PlayerCollection _players = new();

        #endregion
        #region constructors
        private Game(string[] board, bool isTurnBlack)
        {
            Board = new Board(board);
            _turn = isTurnBlack ? _players.BlackPlayer : _players.WhitePlayer;
            SetLegalSquareDictionaries();
        }

        public Game()
        {
            Board = Board.InitializeBoard();
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

        public Player GetWinner()
        {
            var white = GetScoreByColour(SquareContents.White);
            var black = GetScoreByColour(SquareContents.Black);

            return (white > black) ? _players.WhitePlayer : _players.BlackPlayer;
        }

        public int GetScoreByColour(char color)
        {
            return Board.GetNumberOfSquaresByColor(color);
        }

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
            Board.SetLegalSquares(GetPlayerMoveDictionary(_turn).Keys.ToHashSet());
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

            Board.ChangeSquareColour(selectedSquare.Row, selectedSquare.Column, _turn.Counter);
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
            if (Board.AllInitialTilesPlaced() && !ConfirmTwoPlayers()) return false;

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
            return Board.GetBlankSquares().Count == 0 || (_whiteLegalSquareDictionary.Count < 1 && _blackLegalSquareDictionary.Count < 1);
        }
        private HashSet<Square> GetCapturableSquares(Square selectedSquare, Player player)
        {
            var capturableSquares = new HashSet<Square>();
            var counterColour = player.Counter;

            foreach (var direction in _directions)
            {
                Square adjacentSquare = Board.GetAdjacentSquare(selectedSquare, direction);
                var currentLine = new HashSet<Square>();

                while (SquareIsOtherColour(adjacentSquare, counterColour))
                {
                    currentLine.Add(adjacentSquare);
                    adjacentSquare = Board.GetAdjacentSquare(adjacentSquare, direction);
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
            return currentSquare != null && currentSquare.Contents != color && currentSquare.Contents != SquareContents.BlankAndNotLegal && currentSquare.Contents != SquareContents.BlankAndLegal;
        }
        private Dictionary<Square, HashSet<Square>> GetLegalSquares(Player player)
        {
            var legalSquareDictionary = new Dictionary<Square, HashSet<Square>>();

            if (Board.AllInitialTilesPlaced())
            {
                foreach (var square in Board.GetBlankSquares())
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