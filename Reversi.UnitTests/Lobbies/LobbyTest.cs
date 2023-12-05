using NUnit.Framework;
using Reversi.Logic.Lobbies;
using System.Linq;

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
        public void UserCreatesNewRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.AddRoom(userId);
            Assert.AreEqual(1, lobby.Rooms.Count);
            Assert.AreEqual(1, lobby.Rooms[0].Users.Count);
        }

        [Test]
        public void NewRoomContainsUserWhoCreatedIt()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.AddRoom(userId);
            Assert.AreEqual(userId, lobby.Rooms[0].Users[0]);
        }

        [Test]
        public void NextUserSeesExistingRooms_MaybeRoomNamesAndOrUsersInTheRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.AddRoom(userId);
            Assert.AreEqual("Room:1", string.Join(',', lobby.Rooms));
        }

        [Test]
        public void WhenSecondUserJoinsRoom_UserCountUpdatesCorrectly()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            lobby.AddRoom(userId1);
            var userId2 = "2";
            lobby.JoinRoom(userId2, 0);
            Assert.AreEqual(2, lobby.Rooms[0].Users.Count);
        }
    }
}
