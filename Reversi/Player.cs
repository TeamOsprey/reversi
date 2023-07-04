namespace Reversi.Logic
{
    public abstract class Player
    {

        protected Player(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        //Counter represents a game piece, not a score counter.
        public abstract char Counter { get; }
    }

    public class BlackPlayer : Player
    {
        public BlackPlayer(string userId) : base(userId)
        {
        }
        
        public override char Counter => SquareContents.Black;
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
        public override char Counter => SquareContents.White;
        public override string ToString()
        {
            return "White";
        }
    }
}