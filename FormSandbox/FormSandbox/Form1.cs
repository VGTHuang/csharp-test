using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormSandbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LogManager l = new LogManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                Debug.WriteLine(openFileDialog1.FileName);
                string folderPath = Path.GetDirectoryName(openFileDialog1.FileName);
                foreach(string file in Directory.EnumerateFiles(folderPath))
                {
                    Debug.WriteLine(file);
                }
                FileSystemWatcher watcher = new FileSystemWatcher(folderPath, "*.jpg");
                watcher.Created += new FileSystemEventHandler(onChanged);
                watcher.EnableRaisingEvents = true;
            }
        }

        private static void onChanged(object source, FileSystemEventArgs e)
        {
            string destPath = @"E:\";
            Debug.WriteLine($"File: {e.FullPath} {e.ChangeType}");
            // copy to new path
            if(e.ChangeType == WatcherChangeTypes.Created)
            {
                if (!System.IO.Directory.Exists(destPath))
                {
                    System.IO.Directory.CreateDirectory(destPath);
                }
                System.IO.File.Copy(e.FullPath, Path.Combine(destPath, e.Name), true);
            }
        }
    }
}
