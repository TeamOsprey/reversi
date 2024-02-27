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
            var roomName = roomNameResult.Value;
            Assert.IsTrue(roomNameResult.Success);
            Assert.AreEqual("test", roomName.Value);
        }

        [Test]
        public void ErrorWhenRoomNameIsAllWhiteSpace()
        {
            var result = RoomName.Create("  ");

            Assert.IsNull(result.Value);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorConstants.RoomNameCannotBeEmpty, result.Error);
        }

    }
}
