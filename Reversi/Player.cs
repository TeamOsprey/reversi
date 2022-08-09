namespace Reversi.Logic
{
    public class Player
    {
        public Player(RoleEnum role, string connectionId)
        {
            Role = role;
            ConnectionId = connectionId;
        }
        
        public RoleEnum Role { get; set; }
        public string ConnectionId { get; set; }
    }
}