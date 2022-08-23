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
    }
}