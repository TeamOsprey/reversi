using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic
{
    public class Lobby
    {
        public List<Room> Rooms { get; set; }
        public Lobby()
        {
            Rooms = new List<Room>();
        }
    }
}
