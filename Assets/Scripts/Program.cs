using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SlidingPuzzleSolver
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      static void Main()
      {
         Stopwatch stopwatch = new Stopwatch();
         long totalTime = 0;

         //--------------------------Testing a bunch of puzzles at once-----------------------//
         //List<int[]> puzzles = new List<int[]>();
         //puzzles.Add(new int[] { 4, 3, 5, 0, 7, 1, 6, 8, 2 });
         //puzzles.Add(new int[] { 0, 6, 8, 1, 3, 2, 5, 7, 4 });
         //puzzles.Add(new int[] { 5, 2, 3, 6, 7, 0, 4, 8, 1 });
         //puzzles.Add(new int[] { 8, 6, 7, 2, 5, 4, 3, 0, 1 });

         //foreach (int[] puzzle in puzzles)
         //{
         //    stopwatch.Start();
         //    List<int> solution = Solver.SolvePuzzleWithAlgorithm(puzzle, AlgorithmType.BFS);
         //    if (null == solution)
         //    {
         //        Console.WriteLine("The puzzle cannot be solved from the given state.");
         //        return;
         //    }
         //    stopwatch.Stop();
         //    totalTime += stopwatch.ElapsedMilliseconds;
         //    Console.WriteLine("This solution completed in " +
         //                       stopwatch.ElapsedMilliseconds + " milliseconds:");
         //    solution.ForEach(i => Console.Write("{0} ", i));
         //    Console.WriteLine();
         //    stopwatch.Reset();
         //}

         //Console.WriteLine("Solved all puzzles in: " + totalTime +
         //                  " milliseconds.");

         //--------------------------Solving one puzzle many times-----------------------//
         //int[] puzzle = { 1, 2, 3, 4, 0, 6, 7, 5, 8 };// 2 steps
         //int[] puzzle = { 4, 3, 5, 0, 7, 1, 6, 8, 2 };
         //int[] puzzle = { 5, 2, 4, 1, 3, 8, 7, 0, 6 };
         //int[] puzzle = { 0, 6, 8, 1, 3, 2, 5, 7, 4 }; // 24 steps
         //int[] puzzle = { 5, 2, 3 ,6, 7, 0, 4, 8, 1 }; // 17 steps
         int[] puzzle = { 8, 6, 7, 2, 5, 4, 3, 0, 1 }; // 31 steps
         //int[] puzzle = { 8, 1, 2, 0, 4, 3, 7, 6, 5 }; // Unsolvable
         //int[] puzzle = { 6, 4, 7, 8, 5, 0, 3, 2, 1 }; // Other 31 steps

         const int ITERATIONS = 5;
         for (int i = 0; i < ITERATIONS; i++)
         {
            stopwatch.Start();
            List<int> solution = Solver.SolvePuzzleWithAlgorithm(puzzle, AlgorithmType.BFS);
            if (null == solution)
            {
               Console.WriteLine("The puzzle cannot be solved from the given state.");
               return;
            }
            stopwatch.Stop();
            totalTime += stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Solution {0} completed in {1} milliseconds using {2} steps",
               i + 1, stopwatch.ElapsedMilliseconds, solution.Count);
            solution.ForEach(step => Console.Write("{0} ", step));
            Console.WriteLine();
            stopwatch.Reset();
         }

         Console.WriteLine("The average for the " + ITERATIONS + " iterations was: " +
                          (totalTime / (double)ITERATIONS) + " milliseconds.");
      }
   }
}
