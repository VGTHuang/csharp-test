using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            string searchPath = @"C:\PascalWebSTD\Data";
            string savePath = @"E:\tree\";
            Methods.Node baseNode = new Methods.Node(new Methods.FileFolderPath(searchPath, true));
            Methods.DirSearch(baseNode);
            baseNode.Print(savePath);
            
            Console.ReadKey();
        }
    }
    
}
