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
                return new Result<Room>(false, "User already exists in a different room.", null);
            }

            if(string.IsNullOrWhiteSpace(name))
            {
                return new Result<Room>(false, "Room name cannot be empty.", null);
            }
            if (RoomNameExists(name))
            {
                return new Result<Room>(false, "Room name already exists.", null);
            }

            var room = new Room(name.Trim(), userId);
            Rooms.Add(room);
            return new Result<Room>(true, "", room);


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
            return Rooms.Any(x => x.Name.ToLower() == name.ToLower());
        }
    }

    public class Result<T>
    {
        public bool Success { get; }
        public string Error { get; }
        public T Value { get; }

        public Result(bool success, string error, T value)
        {
            Success = success;
            Error = error;
            Value = value;
        }
    }
}
