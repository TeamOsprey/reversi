namespace Reversi.Services
{
    public class SquareDto
    {
        public SquareDto(int row, int column, char colour)
        {
            Row = row;
            Column = column;
            Colour = colour;
        }

        public int Row { get; }
        public int Column { get; }
        public char Colour { get; }
    }
}