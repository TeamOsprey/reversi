using System.Collections.Generic;

namespace Reversi.Logic.Rooms
{
    public class Room
    {
        public List<string> Users { get; set; }


        public Room(string userId)
        {
            Users.Add(userId);          
        }
    }
}