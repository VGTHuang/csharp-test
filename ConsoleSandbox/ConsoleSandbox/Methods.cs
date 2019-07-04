using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Methods
    {
        public class Node : IComparable
        {
            public string nodeName { set; get; }
            public List<Node> children { set; get; }
            public List<string> childPaths { set; get; }

            public Node(string name)
            {
                this.nodeName = name;
                this.children = new List<Node>();
                this.childPaths = new List<string>();
            }

            public void addChild(Node childNode)
            {
                this.children.Add(childNode);
            }

            public void addChildPath(string path)
            {
                if(!string.IsNullOrEmpty(path))
                    this.childPaths.Add(path);
            }

            public Node addChild(string childName)
            {
                Node child = new Node(childName);
                this.addChild(child);
                return child;
            }

            public void sortChildren()
            {
                this.children.Sort();
            }

            public int CompareTo(object obj)
            {
                if(obj is Node)
                {
                    return -1;
                }
                else
                {
                    return (obj as Node).nodeName.CompareTo(this.nodeName);
                }
            }

            public string Print(string stackStr, bool isLast)
            {
                if (isLast)
                {
                    Console.WriteLine(stackStr + "┗━" + this.nodeName);
                }
                else
                {
                    Console.WriteLine(stackStr + "┣━" + this.nodeName);
                }
                if(this.children.Count > 0)
                {
                    if (isLast)
                    {
                        stackStr += "　　";
                    }
                    else
                    {
                        stackStr += "┃　";
                    }
                }
                for (int i = 0; i < this.children.Count; i++)
                {
                    if (i == this.children.Count - 1)
                    {
                        stackStr = this.children[i].Print(stackStr, true);
                    }
                    else
                    {
                        stackStr = this.children[i].Print(stackStr, false);
                    }
                }
                if (this.children.Count > 0)
                {
                    return stackStr.Substring(0, stackStr.Length - 2);
                    
                }
                else
                {
                    return stackStr;
                }
            }
        }

        private static char separator = '\\';

        public static void MakeTree(Node node)
        {
            Dictionary<string, Node> nodeDic = new Dictionary<string, Node>();
            foreach(string item in node.childPaths)
            {
                string tempPrefix = string.Empty;
                if(item.IndexOf(separator) < 0)
                {
                    tempPrefix = item;
                    Node tempNode = new Node(tempPrefix);
                    if (!nodeDic.ContainsKey(tempPrefix))
                    {
                        nodeDic.Add(tempPrefix, tempNode);
                        node.addChild(tempNode);
                    }
                }
                else
                {
                    tempPrefix = item.Substring(0, item.IndexOf(separator));
                    if (nodeDic.ContainsKey(tempPrefix))
                    {
                        Node tempNode = null;
                        nodeDic.TryGetValue(tempPrefix, out tempNode);
                        tempNode.addChildPath(item.Substring(item.IndexOf(separator) + 1));
                    }
                    else
                    {
                        Node tempNode = new Node(tempPrefix);
                        tempNode.addChildPath(item.Substring(item.IndexOf(separator) + 1));
                        nodeDic.Add(tempPrefix, tempNode);
                        node.addChild(tempNode);
                    }
                }
            }

            node.sortChildren();

            foreach(Node childNode in node.children)
            {
                MakeTree(childNode);
            }
        }
    }
}
