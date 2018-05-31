using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlidingPuzzleSolver
{
    /// <summary>
    /// Contains a row and a col that points to a specific
    /// location on the puzzle.
    /// </summary>
    public class Spot
    {
        public int row;
        public int col;

        public Spot(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public override bool Equals(object o)
        {
            Spot spot = (Spot)o;

            if ((object)o == null)
            {
                return false;
            }

            return this == (Spot)spot;
        }

        public static bool operator ==(Spot lhs, Spot rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }
            return lhs.row == rhs.row &&
                   lhs.col == rhs.col;
        }

        public static bool operator !=(Spot lhs, Spot rhs)
        {
            return !(lhs == rhs);
        }

    }
}
