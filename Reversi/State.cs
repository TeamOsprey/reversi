using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic
{
    public class State // todo: make abstract
    {
        public bool MoveInvalid { get; set; }
        public bool PassOccurred { get; set; }
        public bool GameOver { get; set; }
        public bool InsufficientPlayers { get; set; }

        //public abstract string ErrorMessage { get; set; }
        //public abstract bool IsPersonal { get; set; }
    }

    //public class InvalidMoveState: State
    //{
    //    public override string ErrorMessage { get; set; }
    //    public override bool IsPersonal { get; set; }
    //}
}
