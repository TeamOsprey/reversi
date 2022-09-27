﻿using System;

namespace Reversi.Logic
{ 
    public class Square : IEquatable<Square>
    {
        public int Row { get; }
        public int Column { get; }
        public char Colour { get; set; }

        public Square(int row, int col, char colour = Counters.NONE)
        {
            Row = row;
            Column = col;
            Colour = colour;
        }

        public bool Equals(Square square)
        {
            return (Row == square.Row) && (Column == square.Column);
        }

        public override int GetHashCode() => (Row, Column).GetHashCode();
    }
}