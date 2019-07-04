using System;
using System.Collections.Generic;
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
            string searchPath = @"E:\MyRef\work\FilingSystem\";
            string savePath = @"E:\tree\";
            Methods.Node baseNode = new Methods.Node(new Methods.FileFolderPath(searchPath, true));
            Methods.DirSearch(baseNode);
            baseNode.Print(savePath);
            
            Console.ReadKey();
        }
    }
    
}
