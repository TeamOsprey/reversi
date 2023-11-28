using NUnit.Framework;
using Reversi.Logic.Lobbies;

namespace Reversi.UnitTests.Lobbies
{
    [TestFixture]
    public class LobbyTest
    {
        [Test]
        public void NewLobbyHasNoRooms()
        {
            var lobby = new Lobby();
            Assert.AreEqual(0, lobby.Rooms.Count);
        }

        [Test]
        public void UserEntersNewRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.AddRoom(userId);
            Assert.AreEqual(1, lobby.Rooms.Count);
            Assert.AreEqual(1, lobby.Rooms[0].Users.Count);
        }
    }
}
