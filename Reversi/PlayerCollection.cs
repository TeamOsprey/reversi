using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.Logic
{
    public class PlayerCollection : IEnumerable<Player>
    {
        private List<Player> Players { get; set; } = new()
        {
            new BlackPlayer(null),
            new WhitePlayer(null) 
        };

        public BlackPlayer BlackPlayer => Players[0] as BlackPlayer;
        public WhitePlayer WhitePlayer => Players[1] as WhitePlayer;


        public bool HasAllPlayers => Players.All(x=>x.UserId != null);


        public bool HasBlackPlayer => BlackPlayer.UserId != null;


        public bool HasWhitePlayer => WhitePlayer.UserId != null;



        public bool Contains(string userId)
        {
            return Players.Any(x => x.UserId == userId);
        }

        public void Add(string userId)
        {
            if (!HasBlackPlayer)
            {
                AddBlackPlayer(userId);
            }
            else if (!HasWhitePlayer)
            {
                AddWhitePlayer(userId);
            }
        }


        private void AddBlackPlayer(string userId)
        {
            BlackPlayer.UserId = userId;
        }

        private void AddWhitePlayer(string userId)
        {
            WhitePlayer.UserId = userId;
        }

        public void Remove(string userId)
        {
            var player = Players.SingleOrDefault(x => x.UserId == userId);
            if (player != null)            
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

    }
}
