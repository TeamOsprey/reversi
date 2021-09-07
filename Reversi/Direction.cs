using System.Collections.Generic;

namespace Reversi.Logic
{
    public class Direction
    {
        public static Vector UP =        new Vector(-1, 0);
        public static Vector UPRIGHT  =  new Vector(-1, 1);
        public static Vector RIGHT =     new Vector(0, 1 );
        public static Vector DOWNRIGHT = new Vector( 1, 1);
        public static Vector DOWN =      new Vector(1, 0);
        public static Vector DOWNLEFT =  new Vector(1, -1);
        public static Vector LEFT =      new Vector(0, -1);
        public static Vector UPLEFT =    new Vector(-1, -1);

        public static List<Vector> GetDirections()
        {
            return new List<Vector> { UP, UPRIGHT, RIGHT, DOWNRIGHT, DOWN, DOWNLEFT, LEFT, UPLEFT };
        }
    }
}
