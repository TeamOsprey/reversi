using Reversi.Logic;
using Reversi.Logic.Converters;

namespace Reversi.UnitTests
{
    internal static class Helper
    {
        internal static string[] GetOutputAsStringArray(Board reversiBoard)
        {
            var squares = reversiBoard.GetCurrentStateAsSquares();
            return BoardConverter.ConvertToStringArray(squares);
        }
    }
}
