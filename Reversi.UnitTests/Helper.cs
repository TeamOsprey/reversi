using Reversi.Logic;
using Reversi.Logic.Converters;
using System.Linq;

namespace Reversi.UnitTests
{
    internal static class Helper
    {
        internal static string[] GetOutputAsStringArray(Board reversiBoard)
        {
            return BoardConverter.ConvertToStringArray(reversiBoard.Squares);
        }
        internal static string[] GetOutputAsStringArrayWithoutLegalSquares(Board reversiBoard)
        {
            var stringArray = GetOutputAsStringArray(reversiBoard);
            return RemoveLegalSquares(stringArray);
        }

        private static string[] RemoveLegalSquares(string[] board)
        {
            return board.Select(row => row.Replace("0", ".")).ToArray();
        }
    }
}
