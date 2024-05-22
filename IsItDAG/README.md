

Certainly! Here's a README for the `IsItDAG` problem:

---

# Is It Directed Acyclic Graph (DAG)?

## Problem Description
Given a set of vertices and a set of directed edges between these vertices, determine whether the graph formed by these vertices and edges is a Directed Acyclic Graph (DAG).

##### See the more detailed description for the problem [here](https://github.com/Saalehh/Algorithms/tree/main/IsItDAG/Description.pdf)

### Input
- `vertices`: An array of strings representing the vertices in the graph.
- `edges`: An array of key-value pairs, where each pair represents a directed edge from one vertex to another.

### Output
- `true` if the graph is a Directed Acyclic Graph (DAG).
- `false` otherwise.

## Implementation Details
The implementation of the `IsItDAG` problem involves using Depth First Search (DFS) to detect cycles in the graph. Here's how the algorithm works:

1. Initialize a color dictionary to keep track of visited vertices. Initially, all vertices are marked as unvisited (`'W'` for white).
2. Build an adjacency list representing the directed edges between vertices.
3. Perform DFS on each vertex:
   - If the current vertex is unvisited, mark it as visited (color it gray) and recursively visit its neighbors.
   - While visiting neighbors, if a neighbor is already visited and colored gray, a cycle exists in the graph, and the function returns `false`.
   - If DFS completes for all vertices without detecting any cycles, the function returns `true`, indicating that the graph is a DAG.

## Usage
In the [TEMPLATE] directory you can find the implementation, To use the `IsItDAG` class, follow these steps:
1. Include the `Problem` namespace.
2. Call the `IsDAG` method with the appropriate input parameters.

```csharp
using System;
using System.Collections.Generic;
using Problem;

namespace YourNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] vertices = { "A", "B", "C", "D" };
            KeyValuePair<string, string>[] edges = {
                new KeyValuePair<string, string>("A", "B"),
                new KeyValuePair<string, string>("B", "C"),
                new KeyValuePair<string, string>("C", "D"),
                new KeyValuePair<string, string>("D", "A") // Creating a cycle
            };

            bool isDAG = IsItDAG.IsDAG(vertices, edges);
            Console.WriteLine($"Is it a Directed Acyclic Graph? {isDAG}");
        }
    }
}
```

## Complexity Analysis
- Time Complexity: O(V + E), where V is the number of vertices and E is the number of edges in the graph. The DFS algorithm traverses each vertex and edge exactly once.
- Space Complexity: O(V), where V is the number of vertices. Additional space is required for the color dictionary and adjacency list.