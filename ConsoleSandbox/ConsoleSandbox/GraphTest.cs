using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class GraphTest
    {
        public static void Play()
        {
            Graph myGraph = new Graph();

            myGraph.MakeGraph();
            myGraph.printGraph();

            myGraph.clearSearch();
            myGraph.Dfs(0);
            myGraph.printRels();

        }

        private class Graph
        {
            List<Node> nodes;
            public Graph()
            {
                this.nodes = new List<Node>();
            }

            public void MakeGraph()
            {
                for (int i = 0; i < 7; i++)
                {
                    nodes.Add(new Node(i));
                }
                nodes[0].AddAdjacentNode(nodes[1], 3);
                //nodes[1].AddAdjacentNode(nodes[2], 4);
                nodes[2].AddAdjacentNode(nodes[3], 5);
                nodes[0].AddAdjacentNode(nodes[4], 1);
                nodes[1].AddAdjacentNode(nodes[4], 4);
                nodes[2].AddAdjacentNode(nodes[4], 7);
                nodes[3].AddAdjacentNode(nodes[4], 6);
                nodes[0].AddAdjacentNode(nodes[5], 2);
                nodes[4].AddAdjacentNode(nodes[5], 8);
                nodes[5].AddAdjacentNode(nodes[6], 3);
                nodes[3].AddAdjacentNode(nodes[6], 4);
                nodes[4].AddAdjacentNode(nodes[6], 3);
            }

            public void printGraph()
            {
                foreach (Node node in nodes)
                {
                    node.printStatus();
                }
            }

            public void clearSearch()
            {
                foreach (Node node in this.nodes)
                {
                    node.relIndices.Clear();
                    node.isVisited = false;
                }
            }

            public void Dfs(int fromIndex)
            {
                this.nodes[fromIndex].isVisited = true;
                Node tempNode = this.nodes[fromIndex];
                foreach(KeyValuePair<Node, int> pair in tempNode.Adj)
                {
                    if(pair.Key.isVisited == false)
                    {
                        tempNode.relIndices.Add(pair.Key.index);
                        this.Dfs(pair.Key.index);
                    }
                }
            }

            public void printRels()
            {
                foreach(Node node in this.nodes)
                {
                    foreach(int index in node.relIndices)
                    {
                        Console.Write(index + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        private class Node
        {
            public int index { get; }
            public Dictionary<Node, int> Adj { get; }
            public bool isVisited { get; set; }
            public List<int> relIndices;

            public Node(int index)
            {
                this.index = index;
                this.Adj = new Dictionary<Node, int>();
                this.relIndices = new List<int>();
            }

            public void printStatus()
            {
                foreach(KeyValuePair<Node, int> pair in this.Adj)
                {
                    Console.WriteLine(this.index + " " + pair.Key.index + " " + pair.Value);
                }
            }

            public void AddAdjacentNode(Node node, int dist)
            {
                try
                {
                    Adj.Add(node, dist);
                    node.Adj.Add(this, dist);
                }
                catch
                {

                }
            }

            public int getDistBetweenNodes(Node node)
            {
                int val = -1;
                if(this.Adj.TryGetValue(node, out val))
                {
                    return val;
                }
                else
                {
                    return -1;
                }
            }
        }


    }
}
