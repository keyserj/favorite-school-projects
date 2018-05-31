using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SlidingPuzzleSolver
{
    class StateMove : FastPriorityQueueNode
    {
        public SlidingPuzzle puzzle;
        public Spot move;
        public List<Spot> takenMoves;

        public StateMove(SlidingPuzzle puzzle, Spot nextMove, List<Spot> takenMoves)
        {
            this.puzzle = puzzle;
            this.move = nextMove;
            this.takenMoves = takenMoves;
        }

        public StateMove(StateMove otherStateMove)
        {
            this.puzzle = new SlidingPuzzle(otherStateMove.puzzle);
            this.move = otherStateMove.move;
            this.takenMoves = new List<Spot>(otherStateMove.takenMoves);
        }

        public override bool Equals(object o)
        {
            StateMove stateMove = (StateMove)o;

            if ((object)o == null)
            {
                return false;
            }

            return this == (StateMove)stateMove;
        }

        public static bool operator ==(StateMove lhs, StateMove rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }
            return lhs.puzzle == rhs.puzzle;
        }

        public override int GetHashCode()
        {
            return puzzle.GetHashCode();
        }

        public static bool operator !=(StateMove lhs, StateMove rhs)
        {
            return !(lhs == rhs);
        }


    }
}
