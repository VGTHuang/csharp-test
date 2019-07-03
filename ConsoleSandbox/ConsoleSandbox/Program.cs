using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            List<string> list = new List<string>
            {
                @"C:\WINDOWS\AppPatch\MUI\040C",
                @"D:\WINDOWS\Microsoft.NET\Framework\v2.0.50727",
                @"E:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\MUI",
                @"C:\WINDOWS\addins",
                @"C:\WINDOWS\AppPatch",
                @"C:\WINDOWS\AppPatch\MUI",
                @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\MUI\0409"
            };

            Methods.Node baseNode = new Methods.Node("base");
            baseNode.childPaths = list;
            Methods.MakeTree(baseNode);

            baseNode.Print("%");

            Console.ReadKey();
        }
    }
    
}
