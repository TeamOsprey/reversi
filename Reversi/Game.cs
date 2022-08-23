using System.Collections.Generic;
using System;
using System.Linq;

namespace Reversi.Logic
{
    public class Game
    {
        #region fields
        public Board ReversiBoard { get; set; }
        private PlayerType _turn;
        
        public PlayerType Turn() => _turn;

        private PlayerType _opponent => _turn == PlayerType.White ? PlayerType.Black : PlayerType.White;
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
        private Game(string[] board, PlayerType turn)
        {
            ReversiBoard = new Board(board);
            _turn = turn;
        }
        public Game(bool placeInitialCounters = true)
        {
            ReversiBoard = new Board();
            _turn = PlayerType.Black;
            State.InProgress = true;
            if (placeInitialCounters) RandomlyPlaceInitialCounters();
        }
        public static Game Load(string[] board, PlayerType turn)
        {
            var game = new Game(board, turn);
            game.SetStatus();
            game.ActOnStatus();
            return game;
        }
        #endregion
        #region public methods
        public bool PlaceCounter(int row, int col, string connectionId)
        {
            State = new State();
            if (string.IsNullOrEmpty(connectionId) || !IsPlayersTurn(connectionId))
            {
                State.MoveInvalid = true;
                return false;
            }

            return PlaceCounter(new Square(row, col));
        }

        private bool IsPlayersTurn(string connectionId)
        {
            return playerList.Any(x => x.ConnectionId == connectionId && x.Type == _turn);
        }

        public PlayerType? GetPlayerType(string connectionId)
        {
            var player = playerList.SingleOrDefault(x => x.ConnectionId == connectionId);
            return player?.Type;
        }
        public Player GetCurrentPlayer()
        {
            return playerList.Single(x => x.Type == _turn);
        }
        public string[] GetOutput()
        {
            ReversiBoard.SetLegalSquares(GetLegalSquares(_turn));

            return ReversiBoard.GetCurrentState();
        }
        public PlayerType GetWinner()
        {
            var white = GetNumberOfColor(Counters.WHITE);
            var black = GetNumberOfColor(Counters.BLACK);

            return (white > black) ? PlayerType.White : PlayerType.Black;
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

            Action<PlayerType> addPlayer = (type) =>
            {
                playerList.Add(new Player(type, connectionId));
            };

            if (!playerList.Any(x => x.Type == PlayerType.Black))
            {
                addPlayer(PlayerType.Black);
            }
            else if (!playerList.Any(x => x.Type == PlayerType.White))
            {
                addPlayer(PlayerType.White);
            }
        }
        public void RemovePlayer(string connectionId)
        {
            var player = playerList.SingleOrDefault(x => x.ConnectionId == connectionId);
            if (player != null) 
                playerList.Remove(player);
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
            _turn = _opponent;
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
        private HashSet<Square> GetLegalSquares(PlayerType type)
        {
            HashSet<Square> returnValue = new HashSet<Square>();
            if (!AllInitialTilesPlaced())
            {
                AddBlankCentreSquares(returnValue, ReversiBoard.GetBlankSquares());
            }
            else
            {
                //TODO: refactor this. Probably change _turn and _opponent to be Player. Then we pass Player instead of PlayerType to this method.
                var player = playerList.First(x => x.Type == type);
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