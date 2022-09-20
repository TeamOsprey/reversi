using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic
{
    public class PlayerList : IEnumerable<Player>
    {
        public List<Player> Players { get; set; } = new();
        
        public bool IsGameFull => Players.Count == 2;

        public bool DoesConnectionExist(string connectionId)
        {
            return Players.Any(x => x.ConnectionId == connectionId);
        }

        public void AddPlayer(PlayerType type, string connectionId) 
        {
            Players.Add(new Player(type, connectionId));
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return ((IEnumerable<Player>)Players).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Players).GetEnumerator();
        }

        public void Remove(Player player)
        {
            Players.Remove(player);
        }
    }
}
