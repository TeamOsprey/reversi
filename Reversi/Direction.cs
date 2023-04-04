using System.Collections.Generic;

namespace Reversi.Logic
{
    public class Direction
    {
        public static Vector Up =        new(0, -1);
        public static Vector UpRight  =  new(1, -1);
        public static Vector Right =     new(1, 0);
        public static Vector DownRight = new(1, 1);
        public static Vector Down =      new(0, 1);
        public static Vector DownLeft =  new(-1, 1);
        public static Vector Left =      new(-1, 0);
        public static Vector UpLeft =    new(-1, -1);

        public static List<Vector> GetDirections()
        {
            return new List<Vector> { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft };
        }
    }
}
