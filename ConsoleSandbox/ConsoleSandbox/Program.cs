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
            List<string> list = new List<string>
            {
                @"D:\Installations\a5m2_2.14.1_x6D:\Installations\a5m2_2.14.1_x64\A5M2.ENG",
                @"D:\Installations\a5m2_2.14.1_x64\A5M2.ENU",
                @"D:\Installations\a5m2_2.14.1_x64\A5M2.exe",
                @"D:\Installations\a5m2_2.14.1_x64\concrt140.dll",
                @"D:\Installations\a5m2_2.14.1_x64\history.txt",
                @"D:\Installations\a5m2_2.14.1_x64\libbson - 1.0.dll",
                @"D:\Installations\a5m2_2.14.1_x64\libmongoc - 1.0.dll",
                @"D:\Installations\a5m2_2.14.1_x64\license.txt",
                @"D:\Installations\a5m2_2.14.1_x64\license_en.txt",
                @"D:\Installations\a5m2_2.14.1_x64\msvcp140.dll",
                @"D:\Installations\a5m2_2.14.1_x64\readme.txt",
                @"D:\Installations\a5m2_2.14.1_x64\readme_en.txt",
                @"D:\Installations\a5m2_2.14.1_x64\sqlite3.dll",
                @"D:\Installations\a5m2_2.14.1_x64\vcruntime140.dll",
                @"D:\Installations\a5m2_2.14.1_x64\VirusCheck.txt",
                @"D:\Installations\a5m2_2.14.1_x64\Portable",
                @"D:\Installations\a5m2_2.14.1_x64\Portable\Setting.cini",
                @"D:\Installations\a5m2_2.14.1_x64\Portable\logs",
                @"D:\Installations\a5m2_2.14.1_x64\Portable\logs\20190510.log",
                @"D:\Installations\a5m2_2.14.1_x64\Portable\logs\20190513.log",
                @"D:\Installations\a5m2_2.14.1_x64\Portable\temp",
                @"D:\Installations\a5m2_2.14.1_x64\sample",
                @"D:\Installations\a5m2_2.14.1_x64\sample\CreateTableDefinition.xls",
                @"D:\Installations\a5m2_2.14.1_x64\sampledb",
                @"D:\Installations\a5m2_2.14.1_x64\sampledb\ShoppingSite.a5er",
                @"D:\Installations\a5m2_2.14.1_x64\sampledb\ShoppingSite.mdb",
                @"D:\Installations\a5m2_2.14.1_x64\scripts",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\Tool",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\Tool\SqlEmbededStr.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB\FavoritesExport.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB\FavoritesImport.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB\OpenSchemaTable.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB\oracle_procedureSources.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB\oracle_viewSources.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeDB\reccount_query.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeTB",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeTB\CsvCopy.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeTB\InsertStatements.dms",
                @"D:\Installations\a5m2_2.14.1_x64\scripts\TreeTB\TableInfo.dms"
            };

            Methods.Node baseNode = new Methods.Node("base");
            baseNode.childPaths = list;
            Methods.MakeTree(baseNode);

            baseNode.Print("", true);

            //Methods.DirSearch(@"D:\Installations\a5m2_2.14.1_x64\");

            Console.ReadKey();
        }
    }
    
}
