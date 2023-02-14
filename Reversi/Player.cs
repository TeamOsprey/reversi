﻿namespace Reversi.Logic
{
    public abstract class Player
    {

        protected Player(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public abstract char Counter { get; }
    }

    public class BlackPlayer : Player
    {
        public BlackPlayer(string userId) : base(userId)
        {
        }
        
        public override char Counter => Counters.BLACK;
        public override string ToString()
        {
            return "Black";
        }
    }

    public class WhitePlayer : Player
    {
        public WhitePlayer(string userId) : base(userId)
        {
        }
        public override char Counter => Counters.WHITE;
        public override string ToString()
        {
            return "White";
        }
    }
}