﻿using Reversi.Logic;
using Reversi.Web.Services.Interfaces;
using System;
using System.Drawing;

namespace Reversi.Web.Services
{
    public class GameService : IGameService
    {
        public Game Game;

        public GameService(Game game)
        {
            Game = game;
        }

        public void AddPlayer(string userId)
        {
            Game.AddPlayer(userId);
        }

        public void RemovePlayer(string userId)
        {
            Game.RemovePlayer(userId);
        }

        public bool IsLastMoveValid()
        {
            return !(Game.State is MoveInvalid);
        }

        public void PlaceCounter(int row, int col, string userId)
        {
            Game.PlaceCounter(row, col, userId);
        }

        //public string[] GetOutputAsStringArray()
        //{
        //    return Game.GetOutputAsStringArray();
        //}
        public SquareDto[,] GetOutputAsSquares()
        {
            return ConvertToDto(Game.GetOutputAsSquares());
        }

        private SquareDto[,] ConvertToDto(Square[,] squares)
        {
            var dtoArray = new SquareDto[squares.GetLength(0), squares.GetLength(1)];
            for (int row = 0; row < squares.GetLength(0); row++)
            {
                for (int col = 0; col < squares.GetLength(1); col++)
                {
                    dtoArray[row, col] = new SquareDto(row, col, squares[row,col].Contents);
                }
            }

            return dtoArray;
        }

        public string GetCurrentPlayer()
        {
            return ConvertPlayerTypeToString(Game.Turn);
        }

        public string GetPlayerColourString(string userId)
        {
            return ConvertPlayerTypeToString(Game.GetPlayer(userId));
        }

        public string GetMessage()
        {
            return Game.State is { IsPersonal: false } ? Game.State.ErrorMessage : "";
        }

        public string GetPersonalMessage()
        {
            return Game.State is { IsPersonal: true } ? Game.State.ErrorMessage : "";
        }

        public int GetScoreByColor(char color)
        {
            return Game.GetNumberOfColor(color);
        }

        public bool HasAllPlayers()
        {
            return Game.Players.HasAllPlayers;
        }

        private string ConvertPlayerTypeToString(Player player)
        {
            return player != null ? player.ToString() : "Observer";

        }
    }
}
