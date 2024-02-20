using NUnit.Framework;
using Reversi.Logic.Lobbies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.UnitTests.Lobbies
{
    [TestFixture]
    public class RoomNameTest
    {
        [Test]
        public void RoomNameAlwaysGetsTrimmed()
        {
            var roomNameResult = RoomName.Create("    test    ");
            Assert.IsTrue(roomNameResult.Success);
            Assert.AreEqual("test", roomNameResult.Value.Value);
        }

        [Test]
        public void ErrorWhenRoomNameIsAllWhiteSpace()
        {
            var result = RoomName.Create("  ");

            Assert.IsNull(result.Value);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Room name cannot be empty.", result.Error);
        }

    }
}
