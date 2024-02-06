using Reversi.Logic.Rooms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Reversi.Logic.Lobbies
{
    public class Lobby
    {
        public ConcurrentBag<Room> Rooms { get; private set; }
        public Lobby()
        {
            Rooms = new ConcurrentBag<Room>();
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

        public Result<Room> TryAddRoom(string name, string userId)
        {
            if (UserAlreadyInAnyRoom(userId))
            {
                return Result<Room>.CreateFailedResult("User already exists in a different room.");
            }

            var room = Room.Create(name, userId);

            if (RoomNameExists(room.Value.Name.Value)) //todo: not clear
            {
                return Result<Room>.CreateFailedResult("Room name already exists.");
            }

            Rooms.Add(room.Value);
            return Result<Room>.CreateSuccessfulResult(room.Value);

        }

        private bool UserAlreadyInAnyRoom(string userId)
        {
            foreach (var room in Rooms)
            {
                if (UserAlreadyInRoom(room, userId))
                    return true;
            }
            return false;
        }

        private bool RoomNameExists(string name)
        {
            return Rooms.Any(x => x.Name.Value.ToLower() == name.ToLower());
        }
    }
}
