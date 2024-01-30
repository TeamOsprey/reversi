using System.Runtime.InteropServices;

namespace Reversi.Logic.Lobbies
{
    public class RoomName
    {
        public string Value { get; set; }

        private RoomName(string name)
        {
            Value = name.Trim();
        }

        public static RoomName Create(string name)
        {
            var newRoom = new RoomName(name);

            return newRoom;
        }

        public static implicit operator RoomName(string s) => new RoomName(s);
    }
}