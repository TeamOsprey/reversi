using System.Runtime.CompilerServices;

namespace Reversi.Logic.Lobbies
{
    public class RoomName
    {
        private readonly string _name;

        public RoomName(string name)
        {
            this._name=name.Trim();
        }

        public static implicit operator RoomName(string s) => new RoomName(s);

        public override string ToString()
        {
            return this._name;
        }
    }
}