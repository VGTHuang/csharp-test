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
        public class FileFolderPath
        {
            public string fullPath { get; }
            public string Path
            {
                get { return this.fullPath.Substring(this.fullPath.LastIndexOf('\\')+1);//.Substring(0, this.fullPath.IndexOf('\\')); 
                }
            }
            public bool isFolder { get; }
            public FileFolderPath(string path, bool isFolder)
            {
                this.fullPath = path;
                this.isFolder = isFolder;
            }
        }

        public class Node : IComparable
        {
            private FileFolderPath nodePath { set; get; }
            public List<Node> children { set; get; }

            public string fullPath
            {
                get { return this.nodePath.fullPath; }
            }
            public string displayNodeName
            {
                get { return (this.nodePath.isFolder ? "【" : "") + this.nodePath.Path + (this.nodePath.isFolder?"】":""); }
            }

            public Node(FileFolderPath path)
            {
                this.nodePath = path;
                this.children = new List<Node>();
            }

            public void addChild(Node childNode)
            {
                this.children.Add(childNode);
            }
            
            public void sortChildren()
            {
                this.children.Sort();
            }

            public int CompareTo(object obj)
            {
                if(!(obj is Node))
                {
                    return -1;
                }
                else if(this.nodePath.isFolder && !(obj as Node).nodePath.isFolder)
                {
                    return -1;
                }
                else if (!this.nodePath.isFolder && (obj as Node).nodePath.isFolder)
                {
                    return 1;
                }
                else
                {
                    int i = (obj as Node).nodePath.Path.CompareTo(this.nodePath.Path);
                    return i == 0 ? 1 : -i;
                }
            }

            public void Print(string path)
            {
                string stackStr = "";
                bool isLast = true;
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(path + @"tree" + System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".txt", true))
                    {
                        _recPrint(stackStr, isLast, file);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            private string _recPrint(string stackStr, bool isLast, System.IO.StreamWriter file)
            {
                if(this.children.Count > 0)
                {
                    if (isLast)
                    {
                        file.WriteLine(stackStr + "┗+ " + this.displayNodeName);
                        stackStr += "　　";
                    }
                    else
                    {
                        file.WriteLine(stackStr + "┣+ " + this.displayNodeName);
                        stackStr += "┃　";
                    }
                }
                else
                {
                    if (isLast)
                    {
                        file.WriteLine(stackStr + "┗　" + this.displayNodeName);
                    }
                    else
                    {
                        file.WriteLine(stackStr + "┣　" + this.displayNodeName);
                    }
                }
                for (int i = 0; i < this.children.Count; i++)
                {
                    if (i == this.children.Count - 1)
                    {
                        stackStr = this.children[i]._recPrint(stackStr, true, file);
                    }
                    else
                    {
                        stackStr = this.children[i]._recPrint(stackStr, false, file);
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

        public static void DirSearch(Node parent)
        {
            try
            {
                foreach (string f in Directory.GetFiles(parent.fullPath))
                {
                    Console.WriteLine("ffff   "+f);
                    FileFolderPath tempPath = new FileFolderPath(f, false);
                    parent.addChild(new Node(tempPath));
                }
                foreach (string d in Directory.GetDirectories(parent.fullPath))
                {
                    Console.WriteLine("dddd   " + d);
                    FileFolderPath tempPath = new FileFolderPath(d, true);

                    Node newDirectoryNode = new Node(tempPath);
                    DirSearch(newDirectoryNode);
                    
                    parent.addChild(newDirectoryNode);
                }
                parent.sortChildren();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
