using System.Collections.Generic;

namespace SlidingPuzzleSolver
{
    /// <summary>
    /// Interfaces with GUI
    /// </summary>
    class Solver
    {
        private const int PUZZLE_SIZE = 3;

        /// <summary>
        /// Returns null if algorithmType is not an implemented AlgorithmType.
        /// Otherwise returns the list of moves to solve the puzzle.
        /// </summary>
        /// <param name="puzzle"></param>
        /// <param name="algorithmType"></param>
        /// <returns></returns>
        public static List<int> SolvePuzzleWithAlgorithm(int[] puzzle, AlgorithmType algorithmType)
        {
            SlidingPuzzle slidingPuzzle = new SlidingPuzzle(PUZZLE_SIZE);
            slidingPuzzle.SetPuzzle(puzzle);

            // The stopwatch is currently counting the few ms this will take to run. Not sure if
            // we want to restructure this or not or just account for that later if needed.
            // We'd probably have to move the timer in here and restructure this method & output.
            if (!CanSolve(puzzle, slidingPuzzle.GetEmptySpot().row))
                return null;

            if (algorithmType == AlgorithmType.BFS)
                return (new BFSAlgorithm(slidingPuzzle)).Solve();
            else if (algorithmType == AlgorithmType.HEURISTIC1)
                return (new AStarAlgorithm(slidingPuzzle)).Solve();
            else if (algorithmType == AlgorithmType.HEURISTIC2)
                return (new GreedyBestFirstAlgorithm(slidingPuzzle)).Solve();
            return null;
        }

        /// <summary>
        /// Returns whether or not the given puzzle in its 1D form is solvable.
        /// 
        /// The puzzle is solvable if the number of inversions is even, when the dimension
        /// of the puzzle is odd.
        /// The puzzle is solvable if the number of inversions + the row (0-based) of the empty
        /// space is odd, when the dimension of the puzzle is even.
        /// 
        /// An inversion is any pair of blocks i and j where i is less than j, but i follows j,
        /// not counting the empty space.
        /// For example, consider: 1 2 3 4 0 6 8 5 7 --> 1 2 3 4 6 8 5 7
        /// The above has 3 inversions: 6 > 5, 8 > 5, 8 > 7. 3 is odd, so it is not solvable.
        /// </summary>
        /// <param name="puzzle">The puzzle in 1D form (row 1, followed by row 2, ...)</param>
        /// <param name="rowOfEmptySpot">The zero-based row that contains the empty spot.</param>
        /// <returns>True if the puzzle can be solved, false otherwise.</returns>
        private static bool CanSolve(int[] puzzle, int rowOfEmptySpot)
        {
            int inversions = 0;
            for (int i = 0; i < puzzle.Length - 1; i++)
            {
                for (int j = i + 1; j < puzzle.Length; j++)
                {
                    // Skip over the "empty" piece
                    if (puzzle[i] == 0 || puzzle[j] == 0)
                        continue;
                    if (puzzle[i] > puzzle[j])
                        inversions++;
                }
            }
            if (puzzle.Length % 2 == 1) // Odd dimension
                return inversions % 2 == 0;
            else
                return (rowOfEmptySpot + inversions) % 2 == 1;
        }

    }
}
