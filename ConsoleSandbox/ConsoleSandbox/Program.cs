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

            Matrix.Test();

            Console.ReadKey();
        }

        private static void GetTableNameFromLayerAttributeByLayerId()
        {
            DbConnection cn = null;
            System.Data.Common.DbCommand com = null;
            try
            {
                DataSet dSet = new DataSet();
                string strConnection = "Data Source=192.168.66.151;Initial Catalog=PascalWebStdEnv_yamagata;Persist Security Info=True;User ID=sa;Password=Pascosa1";
                
                cn = new SqlConnection();
                cn.ConnectionString = strConnection;
                cn.Open();
                string strSQL = "SELECT DISTINCT [分布図ID] FROM [分布図一覧] WHERE 分布図名 LIKE 'gmap%'";

                com = new System.Data.SqlClient.SqlCommand(strSQL, (SqlConnection)cn);

                object result = com.ExecuteScalar();
                Console.WriteLine(Convert.ToString(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTableNameFromLayerAttributeByLayerId", ex.Message);
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
                if (cn != null)
                {
                    cn.Close();
                }
            }
        }
    }
    
}
