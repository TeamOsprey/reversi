using Reversi.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Reversi.App
{
    public class GameBoard
    {

        public void SetupBoard(Game reversi)
        {
            var board = reversi.DisplayBoard();
            guidedBoard = PrependGuidesToStringArrays(board);
        }
 
        public IEnumerable<string> PrependGuidesToStringArrays(string[] Original)
        {
            string[] guidedArray = new string[8];
            int counter = 0;

            foreach (string S in Original)
            {
                guidedArray[counter] = counter + S;
                counter++;
            }
            var finalArray = guidedArray.Prepend(" 01234567");

            return finalArray;
        }

    }
}