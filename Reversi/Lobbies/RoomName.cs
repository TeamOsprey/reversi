namespace Reversi.Logic.Lobbies
{
    public class RoomName
    {
        public string Value { get; set; }

        public RoomName(string name)
        {
            Value = name.Trim();
        }

        public static implicit operator RoomName(string s) => new RoomName(s);
    }
}