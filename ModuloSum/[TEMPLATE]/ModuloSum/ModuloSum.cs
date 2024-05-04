using System;
using System.Collections.Generic;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class ModuloSum
    {
        #region YOUR CODE IS HERE    

        #region FUNCTION#1: Calculate the Value
        // Your Code is Here:
        // This function checks whether there's a subsequence of numbers in the given array
        // whose sum is divisible by M.
        /// <summary>
        /// Check whether there's a subsequence of numbers in the given array whose sum is divisible by M.
        /// </summary>
        /// <param name="items">Array of integers</param>
        /// <param name="N">Size of the array</param>
        /// <param name="M">Value to check against</param>
        /// <returns>True if there's a subsequence with a sum divisible by M, false otherwise</returns>
        static public bool SolveValue(int[] items, int N, int M)
        {
            // This array will store the remainder of each item in items, with the index as its reference.
            int[] Remainders = new int[N];

            for (int i = 0; i < N; i++)
            {
                int remainder = items[i] % M;

                // If there's an item in the array already divisible by M, return true.
                if (remainder == 0)
                {
                    return true;
                }
                else
                {
                    Remainders[i] = remainder;
                }
            }

            // Dynamic Programming - Bottom Up:
            // Check if there is a combination in the items array in which its sum is divisible by M.
            // This checks if there are items whose remainder sum equals M.
            if (HasCombinationSum(Remainders, M))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region FUNCTION#2: Construct the Solution
        // Your Code is Here:
        // This function constructs the solution, returning the numbers themselves if their sum is divisible by M.
        /// <summary>
        /// Construct the solution, returning the numbers themselves if their sum is divisible by M.
        /// </summary>
        /// <param name="items">Array of integers</param>
        /// <param name="N">Size of the array</param>
        /// <param name="M">Value to check against</param>
        /// <returns>If exists, return the numbers themselves whose sum is divisible by ‘M’ else, return null</returns>
        static public int[] ConstructSolution(int[] items, int N, int M)
        {
            int[] solution = null;
            List<int> solutionIndexes;

            // If there is already one item in the array divisible by M, return it in an array of size one.
            for (int i = 0; i < N; i++)
            {
                if ((items[i] % M) == 0)
                {
                    solution = new int[1];
                    solution[0] = items[i];
                    break;
                }
            }

            if (solution != null)
            {
                return solution;
            }

            // Dynamic Programming - Bottom Up:
            // Check if there is a combination in the items array in which its sum is divisible by M.
            // This checks if there are items whose remainder sum equals M.
            int[] Remainders = new int[N];
            for (int i = 0; i < N; i++)
            {
                Remainders[i] = items[i] % M;
            }

            solutionIndexes = FindCombinationIndexes(Remainders, M);

            if (solutionIndexes != null)
            {
                solution = new int[solutionIndexes.Count];

                for (int i = 0; i < solutionIndexes.Count; i++)
                {
                    solution[i] = items[solutionIndexes[i]];
                }
            }
            return solution;
        }
        #endregion
        #endregion

        // Function to check if there exists a combination sum of given numbers that equals the targetNumber.
        public static bool HasCombinationSum(int[] numbers, int targetNumber)
        {
            bool[] TABLE = new bool[targetNumber + 1];
            TABLE[0] = true;

            foreach (int number in numbers)
            {
                if (number < targetNumber)
                {
                    for (int i = targetNumber; i >= number; i--)
                    {
                        if (TABLE[i - number])
                        {
                            TABLE[i] = true;
                        }
                    }
                }
            }
            return TABLE[targetNumber];
        }

        // Function to find the indexes of the combination sum of given numbers that equals the targetNumber.
        public static List<int> FindCombinationIndexes(int[] numbers, int targetNumber)
        {
            int Length = numbers.Length;

            // Dictionary to store sums and their corresponding indexes.
            Dictionary<int, List<int>> TABLE = new Dictionary<int, List<int>>();
            TABLE[0] = new List<int>();

            for (int i = 0; i < Length; i++)
            {
                var currentCombinations = new List<int>(TABLE.Keys);
                foreach (int sum in currentCombinations)
                {
                    int newSum = sum + numbers[i];
                    if (newSum <= targetNumber)
                    {
                        List<int> newIndexes = new List<int>(TABLE[sum]);
                        newIndexes.Add(i);
                        TABLE[newSum] = newIndexes;
                    }
                }
            }

            // Return the indexes for the target sum if it exists, excluding indexes of zeroes.
            if (TABLE.ContainsKey(targetNumber))
            {
                List<int> indexes = TABLE[targetNumber];
                indexes.RemoveAll(index => numbers[index] == 0);
                if (indexes.Count > 0)
                {
                    return indexes;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                // No valid combination found.
                return null;
            }
        }
    }
}
