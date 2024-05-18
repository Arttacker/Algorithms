# Element Once Appearance Problem

This C# implementation provides an efficient algorithm to find the element that appears once in a sorted array where all other elements appear twice.

## Algorithm Explanation

The provided code implements two methods:

1. **FindUniqueElement**: This method uses a recursive approach to find the element that appears once. It recursively divides the array and checks if the unique element is at the first, middle, or end of the current range. Based on this analysis, it narrows down the search space until the unique element is found.
    
2. **NaiveFindUniqueElement**: This method provides a naive implementation using a linear search. It iterates through the array and returns the first element that appears only once.
##### See the more detailed description for the problem [here](https://github.com/Saalehh/Algorithms/tree/main/ElementOnceAppearance/Description.pdf)

## Usage

In the [TEMPLATE] directory you can find the implementation, To use this implementation:

1. Copy the provided code into your C# project.
2. Call the `FindUniqueElement` method, providing the sorted array and the number of elements.


```csharp
// Example usage 
int[] sortedArray = { 1, 1, 2, 2, 3, 4, 4 };
int uniqueElement = ElementOnceAppearence.FindUniqueElement(sortedArray, sortedArray.Length);
```

## Contributing

Contributions to this implementation or suggestions for improvements are welcome! If you have any ideas for optimizing the algorithm, adding new features, or enhancing the code structure, feel free to contribute by opening an issue or submitting a pull request.
