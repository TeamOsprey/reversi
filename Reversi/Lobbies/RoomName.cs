namespace Reversi.Logic.Lobbies
{
    public class RoomName
    {
        public string Value { get; set; }

        private RoomName(string name)
        {
            Value = name.Trim();
        }
        public static Result<RoomName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<RoomName>.CreateFailedResult("Room name cannot be empty.");
            }

            var newRoom = Result<RoomName>.CreateSuccessfulResult(new RoomName(name));

            return newRoom;
        }
    }
}