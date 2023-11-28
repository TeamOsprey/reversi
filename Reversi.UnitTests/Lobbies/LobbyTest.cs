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
        public void Temp()
        { }
    }
}
