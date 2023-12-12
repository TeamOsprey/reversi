using NUnit.Framework;
using Reversi.Logic.Lobbies;
using Reversi.Logic.Rooms;
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
            lobby.AddRoom("Room1", userId);
            Assert.AreEqual(1, lobby.Rooms.Count);
            Assert.AreEqual(1, lobby.Rooms[0].Users.Count);
        }

        [Test]
        public void NewRoomContainsUserWhoCreatedIt()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.AddRoom("Room1", userId);
            Assert.AreEqual(userId, lobby.Rooms[0].Users[0]);
        }

        [Test]
        public void RoomNameSetCorrectly()
        {
            var lobby = new Lobby();
            var userId = "1";
            var room = lobby.AddRoom("Room1", userId);
            Assert.AreEqual("Room1", room.Name);
        }

        [Test]
        public void RoomNameDuplicatedReturnError()
        {
            var lobby = new Lobby();
            var userId = "1";
            var room1 = lobby.AddRoom("Room1", userId);
            Room room;

            var result = lobby.TryAddRoom("Room1", userId, out room);
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenSecondUserJoinsRoom_UserCountUpdatesCorrectly()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var room = lobby.AddRoom("Room1", userId1);
            var userId2 = "2";
            lobby.JoinRoom(userId2, room);
            Assert.AreEqual(2, room.Users.Count);
        }
    }
}
