namespace Reversi.Logic
{
    public class Player
    {
        public Player(char colour, string connectionId)
        {
            Colour = colour;
            ConnectionId = connectionId;
        }

        public char Colour { get; set; }
        public string ConnectionId { get; set; }
    }
}