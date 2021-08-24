﻿using System;

namespace Reversi.Logic
{ 
    public class Square : IEquatable<Square>
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public char Colour { get; set; }

        public Square(int row, int col, char colour = '.')
        {
            Row = row;
            Column = col;
            Colour = colour;
        }

        public bool Equals(Square square)
        {
            return (Row == square.Row) && (Column == square.Column);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Square);
        }

        public override int GetHashCode() => (Row, Column).GetHashCode();
    }
}