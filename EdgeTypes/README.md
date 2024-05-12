
# Edge Types Problem

This C# implementation solves the Edge Types problem, which involves detecting and counting the types of edges (backward, forward, and cross) in an undirected graph.

## Algorithm Explanation

The provided implementation consists of the following functions:

1. **DetectEdges**: This function detects and counts the edge types of the given undirected graph by applying complete Depth-First Search (DFS) on the entire graph. During the search, ties are broken by selecting vertices in ascending numeric order.

2. **CreateAdjacencyList**: This function creates an adjacency list representation of the graph based on the given vertices and edges. It initializes lists for each vertex and adds edges to the adjacency list (undirected graph).

3. **DFS**: This function performs Depth-First Search (DFS) on the graph starting from the given source vertex. It explores neighbors of each vertex, handles edge types (backward, forward) for visited neighbors, and keeps track of visited vertices and edge counts.

4. **Vertex Class**: This class represents a vertex in the graph, storing its name and parent in the DFS traversal.

##### See the more detailed description for the problem [here](https://github.com/Saalehh/Algorithms/tree/main/EdgeTypes/Description.pdf)

## Testing the Algorithm

In the [TEMPLATE] directory you can find the implementation, To test this algorithm, you can run the `Program.cs` file provided in this repository. It includes sample test cases to demonstrate the functionality of the Edge Types algorithm.

## Contributing

Contributions to this implementation or suggestions for improvements are welcome! If you have any ideas for optimizing the algorithm or adding new features, feel free to contribute by opening an issue or submitting a pull request.
