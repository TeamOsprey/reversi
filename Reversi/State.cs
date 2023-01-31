using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic
{
    public class State
    {
        public bool MoveInvalid { get; set; }
        public bool PassOccurred { get; set; }
        public bool GameOver { get; set; }
        public bool InsufficientPlayers { get; set; }

    }
}
