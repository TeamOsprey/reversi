using System.Collections.Generic;

namespace Reversi.Logic
{
    public class Direction
    {
        public static int[] UP =        new int[] { -1, 0 };
        public static int[] UPRIGHT  =  new int[] { -1, 1 };
        public static int[] RIGHT =     new int[] { 0, 1 };
        public static int[] DOWNRIGHT = new int[] { 1, 1 };
        public static int[] DOWN =      new int[] { 1, 0 };
        public static int[] DOWNLEFT =  new int[] { 1, -1 };
        public static int[] LEFT =      new int[] { 0, -1 };
        public static int[] UPLEFT =    new int[] { -1, -1 };

        public static List<int[]> GetDirections()
        {
            return new List<int[]> { UP, UPRIGHT, RIGHT, DOWNRIGHT, DOWN, DOWNLEFT, LEFT, UPLEFT };
        }
    }
}
