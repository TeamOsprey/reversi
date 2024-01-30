using NUnit.Framework;
using Reversi.Logic.Lobbies;
using Reversi.Logic.Rooms;
using System;
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
            var result = lobby.TryAddRoom("Room1", userId);
            Assert.AreEqual(1, lobby.Rooms.Count);
            Assert.AreEqual(1, result.Value.Users.Count);
        }

        [Test]
        public void NewRoomContainsUserWhoCreatedIt()
        {
            var lobby = new Lobby();
            var userId = "1";
            var result = lobby.TryAddRoom("Room1", userId);
            Assert.AreEqual(userId, result.Value.Users[0]);
        }

        [Test]
        public void RoomNameSetCorrectly()
        {
            var lobby = new Lobby();
            var userId = "1";
            var result = lobby.TryAddRoom("Room1", userId);
            Assert.AreEqual("Room1", result.Value.Name);
        }

        [Test]
        public void RoomNameDuplicatedWithDifferentCases()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            lobby.TryAddRoom("Room1", userId1);
            var result2 = lobby.TryAddRoom("ROOM1", userId1);
            Assert.IsFalse(result2.Success);
            Assert.AreEqual(1, lobby.Rooms.Count);
        }

        [Test]
        public void UniqueRoomNameAddedToLobby()
        {
            var lobby = new Lobby();
            var result1 = lobby.TryAddRoom("Room1", "User1");
            var result2 = lobby.TryAddRoom("Room2", "User2");
            Assert.IsTrue(result1.Success);
            Assert.IsTrue(result2.Success);

            Assert.AreEqual(2, lobby.Rooms.Count);
        }

        [Test]
        public void TrimRoomNamesWhenCreated()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var result = lobby.TryAddRoom(" Room ", userId1);

            Assert.AreEqual("Room", result.Value.Name);
        }

        [Test]
        public void DuplicateUserIdNotAllowedToJoinSameRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            var result1 = lobby.TryAddRoom("Room1", userId);
            var result2 = lobby.TryJoinRoom(result1.Value, userId);
            Assert.IsFalse(result2);
        }

        [Test]
        public void DuplicateUserIdNotAllowedToJoinSecondRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId);
            var result2 = lobby.TryAddRoom("Room2", userId);
            Assert.IsFalse(result2.Success);
        }

        [Test]
        public void WhenSecondUserJoinsRoom_UserCountUpdatesCorrectly()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var result = lobby.TryAddRoom("Room1", userId1);
            var userId2 = "2";
            lobby.TryJoinRoom(result.Value, userId2);
            Assert.AreEqual(2, result.Value.Users.Count);
        }

        [Test]
        public void ErrorWhenRoomNameIsAllWhiteSpace()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var result = lobby.TryAddRoom("  ", userId1);

            Assert.IsNull(result.Value);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Room name cannot be empty.", result.Error);
        }

        [Test]
        public void ErrorWhenSearchContainsWhiteSpace()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var userId2 = "2";
            var result = lobby.TryAddRoom("Room1", userId1);

            var result2 = lobby.TryAddRoom("Room1 ", userId2);

            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.Success);

            Assert.IsFalse(result2.Success);
            Assert.AreEqual("Room name already exists.", result2.Error);
        }

        [Test]
        public void ErrorRoomNameDuplicated()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var userId2 = "2";
            lobby.TryAddRoom("Room1", userId1);
            var result2 = lobby.TryAddRoom("Room1", userId2);
            Assert.IsFalse(result2.Success);
            Assert.AreEqual("Room name already exists.", result2.Error);
            Assert.AreEqual(1, lobby.Rooms.Count);
        }

        [Test]
        public void ErrorWhenCreatingRoomIfUserIsInOtherRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId);
            var result2 = lobby.TryAddRoom("Room2", userId);
            Assert.IsFalse(result2.Success);
            Assert.AreEqual("User already exists in a different room.", result2.Error);
            Assert.AreEqual(1, lobby.Rooms.Count);
        }

        [Test]
        public void RoomNameAlwaysGetsTrimmed()
        {
            var roomName = RoomName.Create("    test    ");
            Assert.AreEqual("test", roomName.Value);
        }
    }
}
