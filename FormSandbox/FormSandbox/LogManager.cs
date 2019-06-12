using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormSandbox
{
    class LogManager
    {
        private string path;
        public LogManager()
        {
            DateTime dateTime = DateTime.Now;
            path = Environment.CurrentDirectory + "\\log";
            try
            {
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                this.path = this.path + "\\" + dateTime.ToString("yyyy-MM-dd") + ".txt";
            }
            catch
            {
                throw new Exception();
            }
        }

        public void Log()
        {
            using (StreamWriter sw = new StreamWriter(this.path, true, System.Text.ASCIIEncoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "[INFO]");
            }
        }

        public void Log(string info)
        {
            using (StreamWriter sw = new StreamWriter(this.path, true, System.Text.ASCIIEncoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " [INFO]  - " + info);
            }
        }

        public void Log(string info, bool isCorrect)
        {
            using (StreamWriter sw = new StreamWriter(this.path, true, System.Text.ASCIIEncoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + (isCorrect?" [INFO]  - ":" [ERROR] - ") + info);
            }
        }
    }
}
