using System;

namespace Reversi.Logic
{ 
    public class Vector : IEquatable<Vector>
    {
        public int Horizontal { get; private set; }
        public int Vertical { get; private set; }

        public Vector(int horizontal, int vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public bool Equals(Vector vector)
        {
            return (Horizontal == vector.Horizontal) && (Vertical == vector.Vertical);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Vector);
        }

        public override int GetHashCode() => (Horizontal, Vertical).GetHashCode();
    }
}