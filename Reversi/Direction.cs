using System.Collections.Generic;

namespace Reversi.Logic
{
    public class Direction
    {
        private static List<int[]> List = new List<int[]>();

        public static List<int[]> GetDirections()
        {
            List.Add(new int[] { -1, 0 }); //"Up"     
            List.Add(new int[] { -1, 1 }); //"UpRight"
            List.Add(new int[] { 0, 1 });  //"Right"
            List.Add(new int[] { 1, 1 });  //"DownRight"
            List.Add(new int[] { 1, 0 });  //"Down"
            List.Add(new int[] { 1, -1 }); //"DownLeft"
            List.Add(new int[] { 0, -1 }); //"Left"
            List.Add(new int[] { -1, -1 });//"UpLeft"

            return List;
        }
    }
}
