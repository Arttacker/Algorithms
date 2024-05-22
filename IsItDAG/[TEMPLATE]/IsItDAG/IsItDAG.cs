using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    public static class IsItDAG
    {
        #region YOUR CODE IS HERE
        // Your Code is Here:
        // ==================
        public static bool IsDAG(string[] vertices, KeyValuePair<string, string>[] edges)
        {
            // Initialize color dictionary to keep track of vertex colors
            Dictionary<string, char> color = new Dictionary<string, char>();
            foreach (string ver in vertices)
            {
                color[ver] = 'W'; // 'W' denotes white color (unvisited)
            }

            // Initialize adjacency list to store neighbors of each vertex
            Dictionary<string, HashSet<string>> adj = new Dictionary<string, HashSet<string>>();
            foreach (string vertex in vertices)
            {
                adj[vertex] = new HashSet<string>(); // Initialize an empty set of neighbors for each vertex
            }

            // Build the adjacency list
            foreach (KeyValuePair<string, string> edge in edges)
            {
                string fromVertex = edge.Key;
                string toVertex = edge.Value;
                adj[fromVertex].Add(toVertex); // Add directed edge from 'fromVertex' to 'toVertex'
            }

            // Perform Depth First Search (DFS) on each vertex
            foreach (string vertex in vertices)
            {
                if (color[vertex] == 'W') // If the vertex is unvisited
                {
                    if (!DFS(adj, color, vertex))
                    {
                        // If a cycle is detected during DFS, return false (graph is not a DAG)
                        return false;
                    }
                }
            }

            // If no cycles are detected during DFS for any vertex, return true (graph is a DAG)
            return true;
        }

        // Depth First Search (DFS) function to detect cycles in the graph
        private static bool DFS(Dictionary<string, HashSet<string>> adj, Dictionary<string, char> color, string vertex)
        {
            // Mark the current vertex as visited (color it gray)
            color[vertex] = 'G';

            // Visit each neighbor of the current vertex
            foreach (string neighbor in adj[vertex])
            {
                if (color[neighbor] == 'G')
                {
                    // If a neighbor is already visited and colored gray, then a cycle exists
                    return false;
                }
                else if (color[neighbor] == 'W')
                {
                    // Recursively visit unvisited neighbors
                    if (!DFS(adj, color, neighbor))
                    {
                        // If a cycle is detected in any neighbor's subtree, return false
                        return false;
                    }
                }
            }

            // Mark the current vertex as processed (color it black)
            color[vertex] = 'B';

            // If no cycle is detected in the current DFS subtree, return true
            return true;
        }
        #endregion
    }
}
