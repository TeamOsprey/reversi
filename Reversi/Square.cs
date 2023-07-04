using System;

namespace Reversi.Logic
{ 
    public class Square : IEquatable<Square>
    {
        public int Row { get; }
        public int Column { get; }
        public char Contents { get; set; }

        public Square(int row, int col, char colour = SquareContents.None)
        {
            Row = row;
            Column = col;
            Contents = colour;
        }

        public bool Equals(Square square)
        {
            return (Row == square.Row) && (Column == square.Column);
        }

        public override int GetHashCode() => (Row, Column).GetHashCode();
    }
}