namespace Reversi.Logic
{
    public abstract class State
    {
        public abstract string ErrorMessage { get; }
        public abstract bool IsPersonal { get; }
    }

    public class MoveInvalid : State
    {
        public override string ErrorMessage => "Invalid Move!";
        public override bool IsPersonal => true;
    }
    public class PassOccurred : State
    {
        public override string ErrorMessage => "User had no possible moves. Turn passed!";
        public override bool IsPersonal => false;
    }
    public class GameOver : State
    {
        public override string ErrorMessage => "Game Over!";
        public override bool IsPersonal => false;
    }
    public class InsufficientPlayers : State
    {
        public override string ErrorMessage => "May not move until second player has joined the game!";
        public override bool IsPersonal => true;
    }

}
