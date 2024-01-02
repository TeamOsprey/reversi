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
            lobby.TryAddRoom("Room1", userId, out Room room);
            Assert.AreEqual(1, lobby.Rooms.Count);
            Assert.AreEqual(1, lobby.Rooms[0].Users.Count);
        }

        [Test]
        public void NewRoomContainsUserWhoCreatedIt()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId, out Room room);
            Assert.AreEqual(userId, lobby.Rooms[0].Users[0]);
        }

        [Test]
        public void RoomNameSetCorrectly()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId, out Room room);
            Assert.AreEqual("Room1", room.Name);
        }

        [Test]
        public void RoomNameDuplicatedReturnError()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId, out Room room1);
            Room room;

            var result = lobby.TryAddRoom("Room1", userId, out room);
            Assert.IsFalse(result);
            Assert.AreEqual(1, lobby.Rooms.Count);
        }

        [Test]
        public void RoomNameDuplicatedWithDifferentCases()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId, out Room room1);
            Room room;

            var result = lobby.TryAddRoom("ROOM1", userId, out room);
            Assert.IsFalse(result);
            Assert.AreEqual(1, lobby.Rooms.Count);
        }


        [Test]
        public void UniqueRoomNameAddedToLobby()
        {
            var lobby = new Lobby();
            lobby.TryAddRoom("Room", "User1", out Room room1);
            Room room;

            var result = lobby.TryAddRoom("Room1", "User2", out room);
            Assert.IsTrue(result);
            Assert.AreEqual(2, lobby.Rooms.Count);
        }

        [Test]
        public void TrimRoomNamesWhenCreated()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom(" Room ", userId, out Room room1);

            Assert.AreEqual("Room", room1.Name);
        }

        [Test]
        public void DuplicateUserIdNotAllowedToJoinSameRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId, out Room room);
            var result = lobby.TryJoinRoom(room, userId);
            Assert.IsFalse(result);
        }

        [Test]
        public void DuplicateUserIdNotAllowedToJoinSecondRoom()
        {
            var lobby = new Lobby();
            var userId = "1";
            lobby.TryAddRoom("Room1", userId, out Room room1);
            var result = lobby.TryAddRoom("Room2", userId, out Room room2);
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenSecondUserJoinsRoom_UserCountUpdatesCorrectly()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            lobby.TryAddRoom("Room1", userId1, out Room room);
            var userId2 = "2";
            lobby.TryJoinRoom(room, userId2);
            Assert.AreEqual(2, room.Users.Count);
        }

        [Test]
        public void WhenRoomNameIsAllWhiteSpaceReturnError()
        {
            var lobby = new Lobby();
            var userId1 = "1";
            var result = lobby.TryAddRoom("  ", userId1, out Room room);

            Assert.IsNull(room);
            Assert.IsFalse(result);
        }
    }
}
