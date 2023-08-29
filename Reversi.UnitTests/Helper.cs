using Reversi.Logic;
using Reversi.Logic.Converters;
using System.Linq;

namespace Reversi.UnitTests
{
    internal static class Helper
    {
        internal static string[] GetOutputAsStringArray(Square[,] squares)
        {
            return BoardConverter.ConvertToStringArray(squares);
        }
        internal static string[] GetOutputAsStringArrayWithoutLegalSquares(Square[,] squares)
        {
            var stringArray = GetOutputAsStringArray(squares);
            return RemoveLegalSquares(stringArray);
        }

        private static string[] RemoveLegalSquares(string[] board)
        {
            return board.Select(row => row.Replace("0", ".")).ToArray();
        }
    }
}
