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
            new Player(Player.Black, null),
            new Player(Player.White, null) 
        };


        public bool IsGameFull => Players.All(x=>x.UserId != null);

        public bool DoesConnectionExist(string userId)
        {
            return Players.Any(x => x.UserId == userId);
        }

        public void AddPlayer(Player type, string userId)
        {
            Players.Find(x => x.Type == type).UserId = userId;
        }

        public void Remove(Player player)
        {
            player.UserId = null;
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return ((IEnumerable<Player>)Players).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Players).GetEnumerator();
        }

        public bool IsCurrentPlayer(string userId, Player turn)
        {
            return Players.Any(x => x.UserId == userId && x.Type == turn);
        }

        public bool HasPlayer(Player playerType)
        {
            return Players.Any(x => x.Type == playerType && x.UserId != null);
        }
    }
}
