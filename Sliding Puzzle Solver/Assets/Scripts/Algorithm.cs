using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlidingPuzzleSolver
{
    public abstract class Algorithm
    {
        public const int MAX_NUM_STATES = 181440;
        protected SlidingPuzzle puzzle;

        public Algorithm(SlidingPuzzle puzzle)
        {
            this.puzzle = puzzle;
        }

        protected List<int> ConvertMovesToInt(List<Spot> moves, int size)
        {
            List<int> movesAsInts = new List<int>();
            foreach (Spot spot in moves)
            {
                movesAsInts.Add(spot.row * size + spot.col);
            }
            return movesAsInts;
        }

        public abstract List<int> Solve();
    }
}
