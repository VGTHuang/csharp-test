using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
            /*
            DataTable dt = new DataTable();
            string searchPath = @"D:\PasCAL";
            string savePath = @"E:\tree\";
            Methods.Node baseNode = new Methods.Node(new Methods.FileFolderPath(searchPath, true));
            Methods.DirSearch(baseNode);
            baseNode.Print(savePath);
            */
            /*
            XMLManager.CreatXmlBookshelf("admin1");
            XMLManager.readXmlFromFileWLinq();
            
            GetTableNameFromLayerAttributeByLayerId();
            */
            // GraphTest.Play();
            // MyStructure.TestMatrix();
            // MyStructure.MakeNN();

            BigNumber.Test();

            Console.ReadKey();
        }
    }
    
}
