using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlidingPuzzleSolver
{
   /// <summary>
   /// Stores each number of the sliding puzzle and has methods for
   /// creating and performing an action on the puzzle.
   /// </summary>
   public class SlidingPuzzle
   {
      public int[,] Puzzle { get; set; }
      // private static int[,] finalState; // used as a singleton
      public int Size { get; set; }

      /// <summary>
      /// Initializes a random puzzle of size: size x size.
      /// </summary>
      /// <param name="size">Number of integers lengthwise in the puzzle.</param>
      public SlidingPuzzle(int size)
      {
         this.Size = size;
         Puzzle = new int[this.Size, this.Size];
      }

      /// <summary>
      /// Copy constructor.
      /// </summary>
      /// <param name="slidingPuzzleToCopy"></param>
      public SlidingPuzzle(SlidingPuzzle slidingPuzzleToCopy)
      {
         this.Size = slidingPuzzleToCopy.Size;
         this.Puzzle = new int[this.Size, this.Size];

         for (int i = 0; i < Size; i++)
         {
            for (int j = 0; j < Size; j++)
            {
               this.Puzzle[i, j] = slidingPuzzleToCopy.Puzzle[i, j];
            }
         }

      }

      /// <summary>
      /// Sets the current values of the puzzle to the input values of
      /// the newPuzzleSetup.
      /// </summary>
      /// <param name="newPuzzleSetup"></param>
      public void SetPuzzle(int[] newPuzzleSetup)
      {
         // Populate 2D puzzle with input 1D puzzle
         for (int row = 0; row < Size; row++)
         {
            for (int col = 0; col < Size; col++)
            {
               Puzzle[row, col] = newPuzzleSetup[row * Size + col];
            }
         }
      }

      /// <summary>
      /// Returns possible if it's possible for a square from the
      /// specified spotFrom to the specified spotTo.
      /// </summary>
      /// <param name="spotFrom">Spot with square to move.</param>
      /// <param name="spotTo">Spot to move square to.</param>
      /// <returns></returns>
      private bool CanMoveBetween(Spot spotFrom, Spot spotTo)
      {
         bool movingToEmptySpot = (Puzzle[spotTo.row, spotTo.col] == 0);
         bool spotsAreAdjacent =
            ((Math.Abs(spotFrom.row - spotTo.row) == 1) && (spotFrom.col - spotTo.col == 0) ||
             (Math.Abs(spotFrom.col - spotTo.col) == 1) && (spotFrom.row - spotTo.row == 0));
         bool spotsAreWithinSize =
            ((spotFrom.row < Size) && (spotFrom.col < Size) &&
             (spotTo.row < Size) && (spotTo.col < Size));

         return (movingToEmptySpot && spotsAreAdjacent && spotsAreWithinSize);
      }

      /// <summary>
      /// Finds the empty spot on the puzzle and returns it.
      /// </summary>
      /// <returns>Spot on puzzle that is empty.</returns>
      public Spot GetEmptySpot()
      {
         for (int row = 0; row < Size; row++)
         {
            for (int col = 0; col < Size; col++)
            {
               if (Puzzle[row, col] == 0)
                  return new Spot(row, col);
            }
         }

         throw new Exception("No empty spot found in puzzle.");
      }

      //private void SetFinalState()
      //{
      //   int[,] calculatedFinalState = new int[this.Size, this.Size];
      //   int pieceNumber = 1;

      //   for (int row = 0; row < Size; row++)
      //      for (int col = 0; col < Size; col++)
      //         calculatedFinalState[row, col] = pieceNumber++;
      //   calculatedFinalState[this.Size - 1, this.Size - 1] = 0;

      //   finalState = calculatedFinalState;
      //}

      /// <summary>
      /// Attempts to move the square at the specified spot
      /// of the puzzle into the empty square.
      /// </summary>
      /// <param name="spotToMove">spot of square to move.</param>
      public void MoveSquare(Spot spotToMove)
      {
         Spot emptySpot = GetEmptySpot();
         if (CanMoveBetween(spotToMove, emptySpot))
         {
            Puzzle[emptySpot.row, emptySpot.col] = Puzzle[spotToMove.row, spotToMove.col];
            Puzzle[spotToMove.row, spotToMove.col] = 0;
         }
         else
         {
            throw new Exception("Can't move that square.");
         }
      }

      /// <summary>
      /// Returns true if the puzzle is solved, false otherwise.
      /// </summary>
      /// <returns>Whether or not puzzle is solved.</returns>
      public bool IsSolved()
      {
         for (int row = 0; row < Size; row++)
         {
            for (int col = 0; col < Size; col++)
            {
               if (row == Size - 1 && col == Size - 1)
                  return true;

               if (Puzzle[row, col] != (row * Size + col + 1))
                  return false;
            }
         }

         return false;
      }

      /// <summary>
      /// Returns all movable spots that can be legally moved given the
      /// input puzzle.
      /// </summary>
      /// <param name="puzzle">Puzzle to check which spots can be moved.</param>
      /// <returns>List of Spots that can be moved.</returns>
      public List<Spot> GetMovableSpots()
      {
         List<Spot> movableSpots = new List<Spot>();
         Spot emptySpot = GetEmptySpot();
         int spotRow = emptySpot.row;
         int spotCol = emptySpot.col;

         if (spotCol - 1 > -1)
            movableSpots.Add(new Spot(spotRow, spotCol - 1));
         if (spotRow - 1 > -1)
            movableSpots.Add(new Spot(spotRow - 1, spotCol));
         if (spotCol + 1 < Size)
            movableSpots.Add(new Spot(spotRow, spotCol + 1));
         if (spotRow + 1 < Size)
            movableSpots.Add(new Spot(spotRow + 1, spotCol));

         return movableSpots;
      }

      public static bool operator ==(SlidingPuzzle lhs, SlidingPuzzle rhs)
      {
         if (System.Object.ReferenceEquals(lhs, rhs))
         {
            return true;
         }
         if (((object)lhs == null) || ((object)rhs == null))
         {
            return false;
         }
         if (lhs.Size != rhs.Size)
            return false;
         for (int i = 0; i < lhs.Size; i++)
         {
            for (int j = 0; j < lhs.Size; j++)
            {
               if (lhs.Puzzle[i, j] != rhs.Puzzle[i, j])
                  return false;
            }
         }
         return true;
      }

      public override int GetHashCode()
      {
         int result = 0;
         for (int i = 0; i < Size; i++)
         {
            for (int j = 0; j < Size; j++)
            {
               result += (int)Math.Pow(Puzzle[i, j], i * Size + j + 1);
            }
         }
         return result;
      }

      public static bool operator !=(SlidingPuzzle lhs, SlidingPuzzle rhs)
      {
         return !(lhs == rhs);
      }

      /// <summary>
      /// Returns the number of 
      /// </summary>
      /// <returns>The correct spots.</returns>
      public int CalcNumIncorrectSpotsHeuristic()
      {
         int incorrectSpots = 0;
         for (int row = 0; row < Size; row++)
         {
            for (int col = 0; col < Size; col++)
            {
               if ((row == Size - 1 && col == Size - 1) && Puzzle[row, col] == 0)
                  incorrectSpots++;

               if (Puzzle[row, col] == (row * Size + col + 1))
                  incorrectSpots++;
            }
         }
         return 9 - incorrectSpots;
      }


      
      /// <summary>
      ///    Calculates the sum of manhattan distances for each tile from its
      ///    solved location.
      ///    Manhattan distance is defined as the distance using only vertical
      ///    or horizontal moves.
      ///    http://codereview.stackexchange.com/questions/86597/optimizing-manhattan-distance-method-for-n-by-n-puzzles
      /// </summary>
      /// <returns>Sum of manhattan distances</returns>
      public int CalcManhattanHeuristic()
      {
         int distance = 0;
         int currentPiece, correctRowForCurrentPiece, correctColForCurrentPiece;
         int colDistance;
         int rowDistance;

         // finalState is a "singleton member". This is the same same thing as
         // if (null == finalState) return finalState else { ... }
         // This means it will only be calculated once per puzzle, when needed.
         // if (null == finalState || finalState.GetLength(0) != this.Size)
            // SetFinalState();

         for (int row = 0; row < Size; row++)
         {
            for (int col = 0; col < Size; col++)
            {
               currentPiece = Puzzle[row, col];
               
               if (currentPiece == 0)
               {
                  correctRowForCurrentPiece = Size - 1;
                  correctColForCurrentPiece = Size - 1;
               }
               else
               {
                  correctRowForCurrentPiece = (currentPiece - 1) / Size;
                  correctColForCurrentPiece = (currentPiece - 1) % Size;
               }

               colDistance = Math.Abs(col - correctColForCurrentPiece);
               rowDistance = Math.Abs(row - correctRowForCurrentPiece);
               distance += colDistance + rowDistance;
            }
         }

         return distance;
      }
   }
}
