using System;

namespace Reversi.Logic
{
    public class Player
    {
        public Player(PlayerType type, string userId)
        {
            Type = type;
            UserId = userId;
        }
        
        public PlayerType Type { get; set; }
        public string UserId { get; set; }

        public char Counter => GetColourOfPlayer();

        private char GetColourOfPlayer()
        {
            return Type switch
            {
                PlayerType.Black => Counters.BLACK,
                PlayerType.White => Counters.WHITE,
                _ => throw new InvalidOperationException("This player Type has no valid counter.")
            };
        }

    }
}