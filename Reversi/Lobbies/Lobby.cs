using System;
using System.Collections.Generic;
using System.Globalization;
using Reversi.Logic.Rooms;

namespace Reversi.Logic.Lobbies
{
    public class Lobby
    {
        public List<Room> Rooms { get; private set; }
        public Lobby()
        {
            Rooms = new List<Room>();
        }

        public Room AddRoom(string name, string userId)
        {
            var room = new Room(name, userId);
            Rooms.Add(room);
            return room;
        }

        public void JoinRoom(string userId, Room room)
        {
            room.JoinRoom(userId);
        }
    }
}
