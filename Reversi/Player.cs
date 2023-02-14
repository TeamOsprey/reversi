using System;
using System.Reflection;

namespace Reversi.Logic
{
    public abstract class Player
    {
        private char counter;
        
        public Player(string userId)
        {
            UserId = userId;
        }
        
        public string UserId { get; set; }

        //public char Counter => GetColourOfPlayer();

        protected char GetColourOfPlayer()
        {
            return counter;
        }
    }

    public class BlackPlayer : Player
    {
        char counter = Counters.BLACK;
        public BlackPlayer(string userId) : base(userId)
        {
        }
    }

    public class WhitePlayer : Player
    {
        char counter = Counters.WHITE;
        public WhitePlayer(string userId) : base(userId)
        {
        }
    }
}