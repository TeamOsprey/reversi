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
            new BlackPlayer(null),
            new WhitePlayer(null) 
        };

        public BlackPlayer BlackPlayer => Players[0] as BlackPlayer;
        public WhitePlayer WhitePlayer => Players[1] as WhitePlayer;


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

        public bool HasBlackPlayer()
        {
            return BlackPlayer.UserId != null;
        }

        public bool HasWhitePlayer()
        {
            return WhitePlayer.UserId != null;
        }
    }
}
