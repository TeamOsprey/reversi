using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic
{
    public class PlayerList
    {
        public List<Player> Players {get; set; }

        public bool IsGameFull => Players.Count == 2;

        public bool DoesConnectionExist(string connectionId)
        {
            return Players.Any(x => x.ConnectionId == connectionId);
        }

        public void AddPlayer(PlayerType type, string connectionId) 
        {
            Players.Add(new Player(type, connectionId);
        }
    }
}
