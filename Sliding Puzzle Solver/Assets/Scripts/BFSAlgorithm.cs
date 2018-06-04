using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SlidingPuzzleSolver
{
    public class BFSAlgorithm : Algorithm
    {
        public BFSAlgorithm(SlidingPuzzle puzzle) : base(puzzle)
        {

        }

        /// <summary>
        /// Returns the list of integers that refer to the location of the block
        /// that is moved during each step.
        /// </summary>
        /// <returns>List of steps</returns>
        public override List<int> Solve()
        {
            Stopwatch stopwatchdequeue = new Stopwatch();
            Stopwatch stopwatchenqueue = new Stopwatch();

            if (puzzle.IsSolved())
				return new List<int>();
			
            HashSet<StateMove> stateHistory = new HashSet<StateMove>();
            List<Spot> movableSteps = puzzle.GetMovableSpots();
            Queue<StateMove> bfsQueue = new Queue<StateMove>();

            stateHistory.Add(new StateMove(puzzle, null, null));  
            foreach (Spot move in movableSteps)
            {
                SlidingPuzzle nextPuzzle = new SlidingPuzzle(puzzle);
                StateMove stateMove = new StateMove(nextPuzzle, move, new List<Spot>());
                bfsQueue.Enqueue(stateMove);
            }

            // Should we check if the puzzle is solvable before ever executing this algorithm?
            while (bfsQueue.Count != 0)
            {
                // Get stateMove pair
                stopwatchdequeue.Start();
                StateMove stateMove = bfsQueue.Dequeue();
                stopwatchdequeue.Stop();
                Spot move = stateMove.move;
                SlidingPuzzle puzz = stateMove.puzzle;
                List<Spot> takenMoves = stateMove.takenMoves;

                // Make move on state
                try
                {
                    puzz.MoveSquare(move);
                }
                catch (Exception)
                {
                    continue;
                }

                stateMove.takenMoves.Add(move);
                //puzzle.SetPuzzle(new int[] { 1,2,3,4,5,6,7,8,0 });
                // Check if solved
                if (puzz.IsSolved())
                {
                    //Console.WriteLine("Remove took: {0}, Add took: {1}",
                    //    stopwatchdequeue.ElapsedMilliseconds,
                    //    stopwatchenqueue.ElapsedMilliseconds);
                    return ConvertMovesToInt(takenMoves, puzz.Size);
                }

                // Get next possible moves and add them to the queue
                movableSteps = puzz.GetMovableSpots();
                StateMove puzzleState = new StateMove(puzz, null, null);
                //bool contains = stateHistory.Contains(puzzleState);
                //int hash1 = puzzleState.GetHashCode();
                //int hash2 = stateHistory.First().GetHashCode();
                if (stateHistory.Add(puzzleState))
                {
                    foreach (Spot nextMove in movableSteps)
                    {
                        SlidingPuzzle nextState = new SlidingPuzzle(puzz);
                        List<Spot> nextTakenMoves = new List<Spot>(takenMoves);
                        StateMove nextStateMove = new StateMove(nextState, nextMove, nextTakenMoves);
                        stopwatchenqueue.Start();
                        bfsQueue.Enqueue(nextStateMove);
                        stopwatchenqueue.Stop();
                    }
                }
            }

            return new List<int>();
        }
    }
}
