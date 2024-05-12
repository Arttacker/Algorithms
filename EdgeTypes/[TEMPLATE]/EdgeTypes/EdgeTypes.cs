using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    public static class EdgeTypes
    {
        #region YOUR CODE IS HERE
        // Your Code is Here:
        // This function detects and counts the edge types of the given undirected graph by applying complete Depth-First Search (DFS) on the entire graph.
        // Note: During search, ties are broken by selecting vertices in ascending numeric order.
        /// <summary>
        /// Detect and count the edge types of the given undirected graph by applying complete DFS on the entire graph.
        /// Note: During search, break ties (if any) by selecting the vertices in ascending numeric order.
        /// </summary>
        /// <param name="vertices">Array of vertices in the graph (named from 0 to |V| - 1)</param>
        /// <param name="edges">Array of edges in the graph</param>
        /// <returns>Return an array of 3 numbers: [0] number of backward edges, [1] number of forward edges, [2] number of cross edges</returns>
        public static int[] DetectEdges(int[] vertices, KeyValuePair<int, int>[] edges)
        {
            int[] BFC = new int[3]; // Initialize the array to store edge counts: [0] backward edges, [1] forward edges, [2] cross edges
            HashSet<int> VISITED = new HashSet<int>(); // HashSet to keep track of visited vertices
            List<List<int>> graph = CreateAdjacencyList(vertices.Length, edges); // Create adjacency list representation of the graph

            (VISITED, BFC) = DFS(graph, vertices[0]); // Perform DFS starting from the first vertex

            // Continue DFS from unvisited vertices until all vertices are visited
            while (VISITED.Count != vertices.Length)
            {
                int[] bfc = new int[3];
                HashSet<int> visited = new HashSet<int>();

                // Find an unvisited vertex
                int newNode = vertices.Except(VISITED).First();

                // Perform DFS starting from the unvisited vertex
                (visited, bfc) = DFS(graph, newNode);

                // Merge the results of DFS into the main result array BFC
                VISITED.UnionWith(visited);
                for (int i = 0; i < 2; i++)
                {
                    BFC[i] += bfc[i];
                }
            }

            return BFC; // Return the final result array
        }

        // Function to create an adjacency list representation of the graph.
        private static List<List<int>> CreateAdjacencyList(int numVertices, KeyValuePair<int, int>[] edges)
        {
            List<List<int>> graph = new List<List<int>>(numVertices); // Initialize the adjacency list

            // Initialize lists for each vertex
            for (int i = 0; i < numVertices; i++)
            {
                graph.Add(new List<int>());
            }

            // Add edges to the adjacency list (undirected graph)
            foreach (KeyValuePair<int, int> edge in edges)
            {
                graph[edge.Key].Add(edge.Value);
                graph[edge.Value].Add(edge.Key);
            }

            return graph; // Return the adjacency list
        }

        // Function to perform Depth-First Search (DFS) on the graph.
        public static (HashSet<int>, int[]) DFS(List<List<int>> graph, int source)
        {
            HashSet<int> visited = new HashSet<int>(); // HashSet to keep track of visited vertices
            int[] BFC = new int[3]; // Array to store edge counts: [0] backward edges, [1] forward edges, [2] cross edges
            int counter = 0; // Counter to keep track of the order of discovery of vertices
            Dictionary<int, int> discoveredOrder = new Dictionary<int, int>(); // Dictionary to store the order of discovery of vertices

            Stack<Vertex> stack = new Stack<Vertex>(); // Stack for DFS traversal
            stack.Push(new Vertex(source)); // Push the source vertex onto the stack

            // Perform DFS traversal
            while (stack.Count > 0)
            {
                Vertex current = stack.Pop(); // Pop the current vertex from the stack

                // If the current vertex is not visited, mark it as visited and record its discovery order
                if (!visited.Contains(current.name))
                {
                    visited.Add(current.name);
                    discoveredOrder[current.name] = counter++;
                }
                else // If the current vertex is already visited, handle edge types (backward/forward/cross)
                {
                    int parentDiscoveredOrder = discoveredOrder[current.parent];
                    int childDiscoveredOrder = discoveredOrder[current.name];

                    if (childDiscoveredOrder > parentDiscoveredOrder)
                    {
                        BFC[0]++; // Increment count of forward edges
                    }
                    else
                    {
                        BFC[1]++; // Increment count of backward edges
                    }

                    continue;
                }

                // Explore neighbors of the current vertex
                foreach (int child in graph[current.name])
                {
                    // Skip if the neighbor is the parent of the current vertex
                    if (child == current.parent)
                    {
                        continue;
                    }

                    // Handle edge types (backward/forward) for visited neighbors
                    if (visited.Contains(child))
                    {
                        int parentDiscoveredOrder = discoveredOrder[current.name];
                        int childDiscoveredOrder = discoveredOrder[child];

                        if (childDiscoveredOrder > parentDiscoveredOrder)
                        {
                            BFC[0]++; // Increment count of forward edges
                        }
                        else
                        {
                            BFC[1]++; // Increment count of backward edges
                        }

                        continue;
                    }

                    // Push unvisited neighbors onto the stack for traversal
                    stack.Push(new Vertex(child, current.name));
                }
            }

            return (visited, BFC); // Return visited vertices and edge counts
        }

        // Class representing a vertex in the graph.
        public class Vertex
        {
            public int name; // Name of the vertex
            public int parent; // Parent of the vertex in the DFS traversal

            public Vertex()
            {

            }

            public Vertex(int name)
            {
                this.name = name;
            }

            public Vertex(int name, int parent)
            {
                this.name = name;
                this.parent = parent;
            }
        }
        #endregion
    }
}
