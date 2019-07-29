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
    public partial class frmColorMatch : Form
    {
        private Random rand = new Random();
        private int rb = 0;
        private int gb = 0;
        private int bb = 0;
        private int rf = 0;
        private int gf = 0;
        private int bf = 0;

        StreamWriter sw;

        public frmColorMatch()
        {
            InitializeComponent();
            changeColor();
            sw = new StreamWriter(@"resources/record.txt", true);
        }
        
        private void saveChoice(int level)
        {
            double[] hslb = rgbToHsl(this.rb, this.gb, this.bb);
            double[] hslf = rgbToHsl(this.rf, this.gf, this.bf);
            string rec = hslb[0] + " " + hslb[1] + " " + hslb[2] + " " + hslf[0] + " " + hslf[1] + " " + hslf[2] + " " + level;
            sw.WriteLine(rec);
            changeColor();
        }
        
        private void changeColor()
        {
            rb = rand.Next(256);
            gb = rand.Next(256);
            bb = rand.Next(256);
            rf = rand.Next(256);
            gf = rand.Next(256);
            bf = rand.Next(256);
            lbColor.BackColor = Color.FromArgb(rb, gb, bb);
            lbColor.ForeColor = Color.FromArgb(rf, gf, bf);
            lbColor.Text = (char)(rand.Next(26) + 65) + "";
        }

        private void analyze()
        {
            sw.Close();
            using (StreamReader sr = new StreamReader(@"resources/record.txt"))
            {
                int validRecCount = 0;
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    if (line.Count() == 7)
                    {
                        validRecCount++;
                    }
                }
                MessageBox.Show(validRecCount.ToString());
            }
            sw = new StreamWriter(@"resources/record.txt", true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveChoice(1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            saveChoice(2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            saveChoice(3);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            saveChoice(4);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            saveChoice(5);
        }

        public double[] rgbToHsl(int R, int G, int B)
        {
            double _R = R / 255f;
            double _G = G / 255f;
            double _B = B / 255f;

            double _Min = Math.Min(Math.Min(_R, _G), _B);
            double _Max = Math.Max(Math.Max(_R, _G), _B);
            double _Delta = _Max - _Min;

            double H = 0;
            double S = 0;
            double L = (_Max + _Min) / 2.0f;

            if (_Delta != 0)
            {
                if (L < 0.5f)
                {
                    S = (_Delta / (_Max + _Min));
                }
                else
                {
                    S = (_Delta / (2.0f - _Max - _Min));
                }


                if (_R == _Max)
                {
                    H = (_G - _B) / _Delta;
                }
                else if (_G == _Max)
                {
                    H = 2f + (_B - _R) / _Delta;
                }
                else if (_B == _Max)
                {
                    H = 4f + (_R - _G) / _Delta;
                }
            }

            H = H * 60f;
            if (H < 0) H += 360;

            double[] hsl = new double[3];
            hsl[0] = H;
            hsl[1] = S;
            hsl[2] = L;
            return hsl;
        }

        private void Form1_OnClosing(object sender, EventArgs e)
        {
            sw.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            analyze();
        }
    }
}
