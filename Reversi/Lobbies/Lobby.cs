using System.Collections.Generic;
using Reversi.Logic.Rooms;

namespace Reversi.Logic.Lobbies
{
    public class Lobby
    {
        public List<Room> Rooms { get; set; }
        public Lobby()
        {
            Rooms = new List<Room>();
        }
    }
}
