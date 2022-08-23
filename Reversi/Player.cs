using System;

namespace Reversi.Logic
{
    public class Player
    {
        public Player(PlayerType type, string connectionId)
        {
            Type = type;
            ConnectionId = connectionId;
        }
        
        public PlayerType Type { get; set; }
        public string ConnectionId { get; set; }

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