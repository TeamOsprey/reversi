using System.Collections.Generic;
using Reversi.Logic.Lobbies;

namespace Reversi.Logic.Rooms
{
    public class Room
    {
        public List<string> Users { get; private set; }
        public RoomName Name { get; private set; }

        private Room(RoomName name, string userId)
        {
            Users = new List<string>();
            Users.Add(userId);
            Name = name;
        }

        public static Result<Room> Create(string name, string userId)
        {
            var roomNameResult = RoomName.Create(name);
            if (!roomNameResult.Success)
            {
                return Result<Room>.CreateFailedResult(roomNameResult.Error);
            }

            return Result<Room>.CreateSuccessfulResult(new Room(roomNameResult.Value, userId));
        }
        
        public void JoinRoom(string userId)
        {
            Users.Add(userId);
        }
    }
}