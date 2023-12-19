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

        public bool TryJoinRoom(Room room, string userId)
        {
            if (UserAlreadyInRoom(room, userId))
                return false;
            room.JoinRoom(userId);

            return true;
        }

        private static bool UserAlreadyInRoom(Room room, string userId)
        {
            return room.Users.Exists(x => x == userId);
        }

        public bool TryAddRoom(string name, string userId, out Room room)
        {
            if (IsRoomNameValid(name))
            {
                room = new Room(name.Trim(), userId);
                Rooms.Add(room);
                return true;
            }
            
            room = null;
            return false;
        }

        private bool IsRoomNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || Rooms.Exists(x => x.Name.ToLower() == name.ToLower()))
            {
                return false;
            }

            return true;
        }
    }
}
