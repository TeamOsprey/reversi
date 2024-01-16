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

            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Room>.CreateFailedResult("Room name cannot be empty.");
            }

            if (RoomNameExists(name))
            {
                return Result<Room>.CreateFailedResult("Room name already exists.");
            }

            var room = new Room(name.Trim(), userId);
            Rooms.Add(room);
            return Result<Room>.CreateSuccessfulResult(room);

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

    public class Result<T> where T : class
    {
        public bool Success { get; }
        public string Error { get; }
        public T Value { get; }

        private Result(bool success, string error, T value)
        {
            Success = success;
            Error = error;
            Value = value;
        }

        public static Result<T> CreateSuccessfulResult(T value)
        {
            return new Result<T>(true, "", value);
        }
        public static Result<T> CreateFailedResult(string error)
        {
            return new Result<T>(false, error, null);
        }
    }
}
