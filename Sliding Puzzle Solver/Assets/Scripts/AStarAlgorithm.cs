using System;
using System.Collections.Generic;
using System.Diagnostics;
using Priority_Queue;

namespace SlidingPuzzleSolver
{

    public class DictionaryComparer<TKey> : IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            return x.CompareTo(y) == 0 ? 1 : x.CompareTo(y);
        }
    }

    public class AStarAlgorithm : Algorithm
    {
        public AStarAlgorithm(SlidingPuzzle puzzle) : base(puzzle)
        {

        }

        public override List<int> Solve()
        {
            Stopwatch stopwatchfirst = new Stopwatch();
            Stopwatch stopwatchremove = new Stopwatch();
            Stopwatch stopwatchadd = new Stopwatch();

            // Check if solved right away
            if (puzzle.IsSolved())
                return ConvertMovesToInt(new List<Spot>(), puzzle.Size);

            FastPriorityQueue<StateMove> closedSet = new FastPriorityQueue<StateMove>(MAX_NUM_STATES);
            HashSet<StateMove> stateHistory = new HashSet<StateMove>();
            List<Spot> movableSteps = puzzle.GetMovableSpots();

            int h = puzzle.CalcNumIncorrectSpotsHeuristic();
            int g = 0;
            int f = g + h;
            stateHistory.Add(new StateMove(puzzle, null, null));
            foreach (Spot move in movableSteps)
            {
                SlidingPuzzle nextState = new SlidingPuzzle(puzzle);
                List<Spot> nextTakenMoves = new List<Spot>();
                StateMove nextStateMove = new StateMove(nextState, null, nextTakenMoves);

                // Make move on state
                try
                {
                    nextState.MoveSquare(move);
                }
                catch (Exception)
                {
                    continue;
                }
                nextStateMove.takenMoves.Add(move);

                // Check if solved
                if (nextState.IsSolved())
                {
                    return ConvertMovesToInt(nextStateMove.takenMoves, nextState.Size);
                }

                h = nextState.CalcNumIncorrectSpotsHeuristic();
                g = nextStateMove.takenMoves.Count;
                f = g + h;

                stateHistory.Add(nextStateMove);
                closedSet.Enqueue(nextStateMove, f);
            }

            SlidingPuzzle currentPuzzle;
            while (closedSet.Count != 0)
            {
                // Get lowest cost move
                stopwatchfirst.Start();
                StateMove stateMove = closedSet.Dequeue();
                stopwatchfirst.Stop();

                Spot move = stateMove.move;
                currentPuzzle = stateMove.puzzle;
                List<Spot> takenMoves = stateMove.takenMoves;

                // Get next possible moves and add them to the queue
                movableSteps = currentPuzzle.GetMovableSpots();
                foreach (Spot nextMove in movableSteps)
                {
                    SlidingPuzzle nextState = new SlidingPuzzle(currentPuzzle);
                    List<Spot> nextTakenMoves = new List<Spot>(takenMoves);
                    StateMove nextStateMove = new StateMove(nextState, null, nextTakenMoves);

                    // Make move on state
                    try
                    {
                        nextState.MoveSquare(nextMove);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    nextStateMove.takenMoves.Add(nextMove);

                    // Check if solved
                    if (nextState.IsSolved())
                    {
                        //Console.WriteLine("First took: {0}, Remove took: {1}, Add took: {2}",
                        //    stopwatchfirst.ElapsedMilliseconds,
                        //    stopwatchremove.ElapsedMilliseconds,
                        //    stopwatchadd.ElapsedMilliseconds);
                        return ConvertMovesToInt(nextStateMove.takenMoves, nextState.Size);
                    }

                    // Only add nextState if it hasn't been seen before
                    if (stateHistory.Add(nextStateMove))
                    {
                        h = nextState.CalcNumIncorrectSpotsHeuristic();
                        g = nextStateMove.takenMoves.Count;
                        f = g + h;
                        stopwatchadd.Start();
                        closedSet.Enqueue(nextStateMove, f);
                        stopwatchadd.Stop();
                    }
                }
            }

            return new List<int>();
        }
    }
}
