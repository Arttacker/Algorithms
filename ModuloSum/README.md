# Modulo Sum Problem

This C# implementation solves the Modulo Sum problem, which involves finding whether there exists a subsequence of numbers in a given array whose sum is divisible by a specified value, M.

## Algorithm Explanation

The provided implementation consists of two main functions:

1. **SolveValue**: This function checks whether there's a subsequence of numbers in the given array whose sum is divisible by M. It utilizes a dynamic programming approach to efficiently compute the remainder of each item and then checks for combinations whose sum of remainders equals M.

2. **ConstructSolution**: This function constructs the solution by returning the numbers themselves if their sum is divisible by M. It first checks if there's already an item in the array divisible by M and returns it. If not, it uses a dynamic programming approach similar to SolveValue to find the combination of items whose sum of remainders equals M, and then returns those numbers.
##### See the more detailed description for the problem [here](https://github.com/Saalehh/Algorithms/tree/main/ModuloSum/Description.pdf)

## Testing the Algorithm

In the [TEMPLATE] directory you can find the implementation, To test this algorithm, you can run the `Program.cs` file provided in this repository. It includes sample test cases to demonstrate the functionality of the Modulo Sum algorithm.

## Contributing

Contributions to this implementation or suggestions for improvements are welcome! If you have any ideas for optimizing the algorithm

 or adding new features, feel free to contribute by opening an issue or submitting a pull request.
