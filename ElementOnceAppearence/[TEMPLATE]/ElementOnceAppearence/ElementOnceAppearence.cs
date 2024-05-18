using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class ElementOnceAppearence
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================

        /// <summary>
        /// Find the element that appears once in a sorted array where all other elements appear twice.
        /// </summary>
        /// <param name="X">Sorted array of elements where all elements appear twice except one</param>
        /// <param name="N">Number of elements in the array</param>
        /// <returns>The element that appears once</returns>
        public static int FindUniqueElement(int[] X, int N)
        {
            // Call a recursive function to find the unique element
            return RecurseOnThisHalf(X, 0, N - 1);
        }

        // Recursive function to find the unique element within a given range
        private static int RecurseOnThisHalf(int[] X, int start, int end)
        {
            int resultNumber = 0;

            // Calculate index of the middle element and its neighboring elements
            int indexOfMiddle = (start + end) / 2;
            int middileNumber = X[indexOfMiddle];
            int beforeMiddle = X[indexOfMiddle - 1];
            int afterMiddle = X[indexOfMiddle + 1];

            int lastNumber = X[end];
            int beforeLast = X[end - 1];

            int firstNumber = X[start];
            int afterFirst = X[start + 1];

            // Check conditions to determine if the unique element is at the first, middle, or end
            bool atTheFirst = (firstNumber != afterFirst);
            bool atTheMiddle = (middileNumber != beforeMiddle && middileNumber != afterMiddle);
            bool atTheEnd = (lastNumber != beforeLast);

            bool indexOfMiddleIsEven = (indexOfMiddle % 2 == 0);
            bool middleFirstOccurence = (middileNumber != beforeMiddle);

            // Check if the unique element is at the middle, first, or end
            if (atTheMiddle) { return middileNumber; }
            if (atTheFirst) { return firstNumber; }
            if (atTheEnd) { return lastNumber; }

            // Recurse on the appropriate half based on the position of the middle element
            if (indexOfMiddleIsEven && middleFirstOccurence)
            {
                resultNumber = RecurseOnThisHalf(X, indexOfMiddle, end);
            }
            else if (indexOfMiddleIsEven && !middleFirstOccurence)
            {
                resultNumber = RecurseOnThisHalf(X, start, indexOfMiddle);
            }
            else if (!indexOfMiddleIsEven && middleFirstOccurence)
            {
                resultNumber = RecurseOnThisHalf(X, start, indexOfMiddle - 1);
            }
            else
            {
                resultNumber = RecurseOnThisHalf(X, indexOfMiddle - 1, end);
            }

            return resultNumber;
        }

        /// <summary>
        /// Naive implementation to find the element that appears once in a sorted array.
        /// </summary>
        /// <param name="X">Sorted array of elements where all elements appear twice except one</param>
        /// <param name="N">Number of elements in the array</param>
        /// <returns>The element that appears once</returns>
        public static int NaiveFindUniqueElement(int[] X, int N)
        {
            // Iterate through the array and return the first element that appears once
            for (int i = 0; i < N; i += 2)
            {
                if (i == (N - 1))
                {
                    return X[i];
                }

                if (X[i] != X[i + 1])
                {
                    return X[i];
                }
            }
            return 0; // Default return value
        }
        #endregion
    }
}
