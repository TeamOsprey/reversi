using Reversi.Logic.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Reversi.Logic.Lobbies
{
    public class Lobby
    {
        public List<Room> Rooms { get; private set; }
        public Lobby()
        {
            Rooms = new List<Room>();
        }

        public void JoinRoom(string userId, Room room)
        {
            room.JoinRoom(userId);
        }

        public bool TryAddRoom(string name, string userId, out Room room)
        {
            room = new Room(name, userId);
            if (!Rooms.Exists(x => x.Name == name))
            {
                Rooms.Add(room);
                return true;
            }
            return false;
        }
    }
}
