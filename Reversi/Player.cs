namespace Reversi.Logic
{
    public class Player
    {
        public Player(char colour, string connectionId)
        {
            Role = colour;
            ConnectionId = connectionId;
        }

        public char Role { get; set; }
        public string ConnectionId { get; set; }
    }
}