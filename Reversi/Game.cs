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

        private Player _opponent => _turn is WhitePlayer ? Player.Black : Player.White;
        private readonly List<Vector> directions = Direction.GetDirections();
        public State State;
        private static readonly List<int[]> initialValues = new()
        {
                new[] { 3,3 },
                new[] { 3,4 },
                new[] { 4,4 },
                new[] { 4,3 },
        };

        private PlayerList playerList = new();
        private bool _setInitialStatus;

        #endregion
        #region constructors
        private Game(string[] board, Player turn)
        {
            ReversiBoard = new Board(board);
            _turn = turn;
        }
        public Game()
        {
            ReversiBoard = Board.InitializeBoard();
            _turn = Player.Black;
        }
        public static Game Load(string[] board, Player turn)
        {
            var game = new Game(board, turn);
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
            return playerList.IsCurrentPlayer(userId, _turn);
        }

        public string GetPlayerType(string userId)
        {
            var player = playerList.SingleOrDefault(x => x.UserId == userId);
            return player?.ToString();
        }
        public Player GetCurrentPlayer()
        {
            return playerList.Single(x => x.Type == _turn);
        }
        public string[] GetOutput()
        {
            if(playerList.IsGameFull)
                ReversiBoard.SetLegalSquares(GetLegalSquares(_turn)); // todo: consider renaming

            var showLegalMoves = true; // maybe use this.IsPlayersTurn(userId)
            return ReversiBoard.GetCurrentState(showLegalMoves);
        }
        public Player GetWinner()
        {
            var white = GetNumberOfColor(Counters.WHITE);
            var black = GetNumberOfColor(Counters.BLACK);

            return (white > black) ? Player.White : Player.Black;
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
            return playerList.Players;
        }

        public void AddPlayer(string userId)
        {
            lock (playerList)
            {
                if (playerList.IsGameFull)
                    return;

                if (playerList.DoesConnectionExist(userId)) return;

                if (!playerList.HasPlayer(Player.Black))
                {
                    playerList.AddPlayer(Player.Black, userId);
                }
                else if (!playerList.HasPlayer(Player.White))
                {
                    playerList.AddPlayer(Player.White, userId);
                }

                if (!_setInitialStatus && playerList.IsGameFull)
                {
                    SetStatus();
                    ActOnStatus();
                    _setInitialStatus = true;
                }
            }
        }

        public void RemovePlayer(string userId)
        {
            var player = playerList.SingleOrDefault(x => x.UserId == userId);
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
                State = new MoveInvalid();
                return false;
            }

            return true;
        }

        private bool ConfirmTwoPlayers()
        {
            if (!playerList.IsGameFull)
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
        private HashSet<Square> GetLegalSquares(Player type)
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