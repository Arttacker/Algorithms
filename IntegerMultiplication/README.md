# Integer Multiplication Problem

This C# implementation solves the Integer Multiplication problem efficiently using Karatsuba's Method.

## Algorithm Explanation

The provided code implements the `IntegerMultiply` method which multiplies two large integers of N digits in an efficient way using Karatsuba's Method. The algorithm follows these steps:

1. **Padding**: Ensure both input integers have the same length by padding with zeros if necessary.
2. **Base Case**: If the number of digits (N) is 1, perform simple multiplication.
3. **Divide**: Split the input integers into two halves.
4. **Conquer**: Recursively multiply the halves and combine the results.
5. **Combine**: Subtract intermediate results, pad, and combine to get the final result.

The code also includes helper methods for addition, subtraction, multiplication, and padding.
##### See the more detailed description for the problem [here](https://github.com/Saalehh/Algorithms/tree/main/IntegerMultiplication/Description.pdf)

## Usage

In the [TEMPLATE] directory you can find the implementation, To use this implementation:

1. Copy the provided code into your C# project.
2. Call the `IntegerMultiply` method, providing the two integers to be multiplied and the number of digits (N).

```csharp
// Example usage
byte[] result = IntegerMultiplication.IntegerMultiply(X, Y, N);
```

## Contributing

Contributions to this implementation or suggestions for improvements are welcome! If you have any ideas for optimizing the algorithm, adding new features, or enhancing the code structure, feel free to contribute by opening an issue or submitting a pull request.
