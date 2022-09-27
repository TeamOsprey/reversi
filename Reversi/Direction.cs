using System.Collections.Generic;

namespace Reversi.Logic
{
    public class Direction
    {
        public static Vector UP =        new(0, -1);
        public static Vector UPRIGHT  =  new(1, -1);
        public static Vector RIGHT =     new(1, 0);
        public static Vector DOWNRIGHT = new(1, 1);
        public static Vector DOWN =      new(0, 1);
        public static Vector DOWNLEFT =  new(-1, 1);
        public static Vector LEFT =      new(-1, 0);
        public static Vector UPLEFT =    new(-1, -1);

        public static List<Vector> GetDirections()
        {
            return new List<Vector> { UP, UPRIGHT, RIGHT, DOWNRIGHT, DOWN, DOWNLEFT, LEFT, UPLEFT };
        }
    }
}
