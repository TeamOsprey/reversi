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
        public List<Player> Players { get; set; } = new()
        {
            new Player(PlayerType.Black, null),
            new Player(PlayerType.White, null) 
        };


        public bool IsGameFull => Players.All(x=>x.ConnectionId != null);

        public bool DoesConnectionExist(string connectionId)
        {
            return Players.Any(x => x.ConnectionId == connectionId);
        }

        public void AddPlayer(PlayerType type, string connectionId)
        {
            Players.Find(x => x.Type == type).ConnectionId = connectionId;
        }

        public void Remove(Player player)
        {
            player.ConnectionId = null;
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return ((IEnumerable<Player>)Players).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Players).GetEnumerator();
        }

        public bool IsCurrentPlayer(string connectionId, PlayerType turn)
        {
            return Players.Any(x => x.ConnectionId == connectionId && x.Type == turn);
        }

        public bool HasPlayer(PlayerType playerType)
        {
            return Players.Any(x => x.Type == playerType && x.ConnectionId != null);
        }
    }
}
