//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;

///// <summary>
///// The cube controller class keeps references to all the cubes in the scene and gives a set of functions
///// to access and modify the cubes.  Using cube controller you can get a single cube, get the cubes surrounding
///// a cube, move a cube, move the cubes in a given sequence, and to suffle;
///// </summary>
//public class CubeController : MonoBehaviour
//{
//    public GameObject[] spaces;
//    public int emptyIndex = 9;

//    private bool isInputDisabled = false;
//    private bool isShuffling = false;

//    public void Shuffle()
//    {
//        if (isShuffling)
//        {
//            Debug.LogWarning("Cannot shuffle while shuffling.");
//            return;
//        }
      
//        isInputDisabled = true;
//        isShuffling = true;
//        StartCoroutine(ShuffleWithDelay());
//    }

//    public void ClickCubeAt(int index)
//    {
//        if (isInputDisabled)
//        {
//            Debug.LogWarning("Input is disabled.");
//            return;
//        }

//        MoveCubeAt(index);
//    }

//    public void BFSSolve()
//    {
//        if (isShuffling)
//        {
//            Debug.LogWarning("Cannot solve while shuffling.");
//            return;
//        }
//        int[] currentConfig = ParseCurrentConfiguration();
//        List<int> bfsSolution = SlidingPuzzleSolver.Solver.SolvePuzzleWithAlgorithm(currentConfig, SlidingPuzzleSolver.AlgorithmType.BFS);
//        StartCoroutine(MoveCubesInSequenceWithDelay(bfsSolution.ToArray(), 0.1f));
//    }



//    /// <summary>
//    /// Tries to move the cube at i to the empty space.  Will write an error in the console if
//    /// a cube cannot be moved.
//    /// </summary>
//    /// <param name="i">The index of the cube to move.</param>
//    private void MoveCubeAt(int i)
//    {
//        if (i < 0 || i > 8)
//        {
//            Debug.LogWarning(string.Format("Index {0} is outside of the puzzle.", i));
//            return;
//        }

//        GameObject emptySpace = spaces[emptyIndex];
//        Cube cube = spaces[i].GetComponent<Cube>();

//        Vector3 cubeOldPosition = cube.transform.position;
//        Vector3 emptySpaceOldPosition = emptySpace.transform.position;

//        if (!cube.canMove)
//        {
//            Debug.LogWarning(string.Format("Cube at {0}, cannot be moved to the empty space at {1}", i, emptyIndex));
//            return;
//        }

//        Debug.Log("Moving cube at " + i + " to the empty space.");
//        //Move the cube to the empty spot
//        cube.transform.position = emptySpaceOldPosition;
//        //Move the empty spot to the cube
//        emptySpace.transform.position = cubeOldPosition;

//        //Swap positions in the array.
//        spaces[i] = emptySpace;
//        spaces[emptyIndex] = cube.gameObject;

//        cube.currentIndex = emptyIndex;
//        emptyIndex = i;

//        SetAllCubesToCannotMove();

//        foreach (var adjacentCube in GetSurroundingCubes(emptyIndex))
//        {
//            if (adjacentCube != null)
//            {
//                adjacentCube.canMove = true;
//            }
//        }
//    }

//    /// <summary>
//    /// Given an index of a cube, returns the cubes around that cube.
//    /// </summary>
//    /// <returns>A list of the cubes surrounding the cube at index i.</returns>
//    /// <param name="index">The index to check around.</param>
//    private List<Cube> GetSurroundingCubes(int index)
//    {   
//        List<Cube> result = new List<Cube>();

//        foreach (int i in GetSurroundingIndexes(index))
//        {
//            result.Add(GetCubeSafely(i));
//        }

//        return result;
//    }

//    /// <summary>
//    /// Given an index of a cube, returns the indexes around that cube where -1 is an index
//    /// that is outside of the puzzle.
//    /// </summary>
//    /// <returns>A list of the cubes surrounding the cube at index i.</returns>
//    /// <param name="index">The index to check around.</param>
//    private List<int> GetSurroundingIndexes(int index)
//    {
//        List<int> result = new List<int>();
//        if (index < 0 || index > 8)
//            return result;

//        int col = index % 3;
//        int row = index / 3;

//        int left = index - 1;
//        if (left < row * 3)
//            left = -1;

//        int right = index + 1;
//        if (right >= ((row + 1) * 3))
//            right = -1;

//        int above = (row - 1) * 3 + col;
//        if (above < 0)
//            above = -1;

//        int below = (row + 1) * 3 + col;
//        if (below > 8)
//            below = -1;

//        if (left != -1)
//            result.Add(left);
//        if (right != -1)
//            result.Add(right);
//        if (above != -1)
//            result.Add(above);
//        if (below != -1)
//            result.Add(below);

//        return result;
//    }

//    /// <summary>
//    /// Given an index of a cube, returns the cube at that index or
//    /// null if there is no cube at that index.  All indexes can be handled.
//    /// </summary>
//    /// <returns>The cube at index i or null.</returns>
//    /// <param name="index">The index to access.</param>
//    private Cube GetCubeSafely(int index)
//    {
//        if (index >= 0 && index < spaces.Length)
//        {
//            var cubeAtLocation = spaces[index];
//            if (cubeAtLocation != null)
//                return cubeAtLocation.GetComponent<Cube>();
//        }
//        return null;
//    }

//    private void SetAllCubesToCannotMove()
//    {
//        foreach (var obj in spaces)
//        {
//            if (obj != null)
//            {
//                var temp = obj.GetComponent<Cube>();
//                if (temp != null)
//                {
//                    temp.canMove = false;
//                }
//            }
//        }
//    }

//    private IEnumerator MoveCubesInSequenceWithDelay(int[] sequence, float delay = 0.05f)
//    {
//        foreach (int i in sequence)
//        {
//            MoveCubeAt(i);
//            yield return new WaitForSeconds(delay);
//        }
//    }

//    /// <summary>
//    /// Shuffles the with delay.
//    /// </summary>
//    /// <returns>The with delay.</returns>
//    private IEnumerator ShuffleWithDelay(float delay = 0.0f)
//    {
//        for (int i = 0; i < 100; i++)
//        {
//            List<int> adjacentIndexes = GetSurroundingIndexes(emptyIndex);
//            int randomIndex = Random.Range(0, adjacentIndexes.Count);
//            MoveCubeAt(adjacentIndexes[randomIndex]);
//            yield return new WaitForSeconds(delay);
//        }
//        isInputDisabled = false;
//        isShuffling = false;
//    }

//    /// <summary>
//    /// Iterates through the indexes in the sequence and tries to move the cube at
//    /// that index.
//    /// </summary>
//    /// <param name="sequence">The sequence of indexes.</param>
//    private void RunSequence(int[] sequence)
//    {
//        StartCoroutine(MoveCubesInSequenceWithDelay(sequence));
//    }

//    /// <summary>
//    /// Parses the current configuration and outputs an array where the 0 index contains the
//    /// index of the cube in that space.
//    /// </summary>
//    /// <returns>The current configuration.</returns>
//    private int[] ParseCurrentConfiguration()
//    {
//        int[] result = new int[9];
//        for(int i = 0; i < spaces.Length; i++)
//        {
//            GameObject gameObj = spaces[i];
//            if(gameObj != null)
//            {
//                Cube cube = gameObj.GetComponent<Cube>();
//                if (cube != null)
//                {
//                    result[i] = cube.correctIndex;
//                }
//            }
//        }
//        return result;
//    }
//}