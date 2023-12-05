using System;
using System.Collections.Generic;

namespace Reversi.Logic.Rooms
{
    public class Room
    {
        public List<string> Users { get; set; }
        public string Name { get; set; }

        public Room(string name, string userId)
        {
            Users = new List<string>();
            Users.Add(userId);
            Name = name;
        }

        public void JoinRoom(string userId)
        {
            Users.Add(userId);
        }
    }
}