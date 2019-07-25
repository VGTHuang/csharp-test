using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormSandbox
{
    public partial class frmMinesweeper : Form
    {
        public frmMinesweeper()
        {
            InitializeComponent();
            initializeField(10, 10, 0.12);
        }
        private int pixelSize = 30;
        private int msCol, msRow;
        private Pixel[,] pixels;

        private void initializeField(int row, int col, double density)
        {
            this.msCol = col;
            this.msRow = row;
            int tabIndex = 5;
            Random rand = new Random();
            this.pixels = new Pixel[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Label tempLabel = new Label();
                    tempLabel.BackColor = System.Drawing.SystemColors.ControlLight;
                    tempLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    tempLabel.Font = new System.Drawing.Font("Calibri", (float)(pixelSize * 0.6), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tempLabel.Location = new System.Drawing.Point(10 + pixelSize * i, 10 + pixelSize * j);
                    tempLabel.Name = "label_" + i + "_" + j;
                    tempLabel.Size = new System.Drawing.Size(pixelSize, pixelSize);
                    tempLabel.TabIndex = tabIndex++;
                    tempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    tempLabel.MouseDown += new MouseEventHandler(this.pixel_MouseDown);
                    this.pixels[i, j] = new Pixel(tempLabel, rand.NextDouble() < density ? -1 : 0);
                    this.panel1.Controls.Add(pixels[i, j].Label);
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if(pixels[i, j].Value != -1)
                    {
                        int adjacentMineCount = 0;
                        if (i > 0) // i - 1
                        {
                            if (j > 0)
                            {
                                if (pixels[i - 1, j - 1].Value == -1)
                                {
                                    adjacentMineCount++;
                                }
                            }
                            if (pixels[i - 1, j].Value == -1)
                            {
                                adjacentMineCount++;
                            }
                            if (j < col - 1)
                            {
                                if (pixels[i - 1, j + 1].Value == -1)
                                {
                                    adjacentMineCount++;
                                }
                            }
                        }
                        // i
                        if (j > 0)
                        {
                            if (pixels[i, j - 1].Value == -1)
                            {
                                adjacentMineCount++;
                            }
                        }
                        if (j < col - 1)
                        {
                            if (pixels[i, j + 1].Value == -1)
                            {
                                adjacentMineCount++;
                            }
                        }
                        if (i < row - 1) // i + 1
                        {
                            if (j > 0)
                            {
                                if (pixels[i + 1, j - 1].Value == -1)
                                {
                                    adjacentMineCount++;
                                }
                            }
                            if (pixels[i + 1, j].Value == -1)
                            {
                                adjacentMineCount++;
                            }
                            if (j < col - 1)
                            {
                                if (pixels[i + 1, j + 1].Value == -1)
                                {
                                    adjacentMineCount++;
                                }
                            }
                        }
                        pixels[i, j].Value = adjacentMineCount;
                        pixels[i, j].Label.Text = adjacentMineCount.ToString();
                    }
                }
            }
        }

        private void pixel_MouseDown(object sender, MouseEventArgs e)
        {
            int pRow = 0, pCol = 0;
            string[] indicesStrArr = ((Label)sender).Name.Split('_');
            pRow = Convert.ToInt16(indicesStrArr[1]);
            pCol = Convert.ToInt16(indicesStrArr[2]);
            Pixel currentPixel = pixels[pRow, pCol];
            // fixed pixel, can't operate
            if (currentPixel.Operate == 1)
            {
                return;
            }
            // is unflagging
            else if (currentPixel.Operate == -1 && e.Button == MouseButtons.Right)
            {
                currentPixel.Label.Text = "";
                currentPixel.Operate = 0;
                return;
            }
            if (currentPixel.Value == -1) // is mine
            {
                // if right key, label as flagged
                // if left key, game over
                if(e.Button == MouseButtons.Right)
                {
                    // flagged
                    flagPixel(currentPixel, pRow, pCol, true);
                }
                else
                {
                    // game over
                    MessageBox.Show("game over");
                }
            }
            else if(currentPixel.Value == 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    // flagged
                    flagPixel(currentPixel, pRow, pCol, true);
                }
                else
                {
                    // expand
                    recExpand(currentPixel, pRow, pCol);
                }
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    // flagged
                    flagPixel(currentPixel, pRow, pCol, false);
                }
                else
                {
                    // value > 0, show adjacent mines count
                    currentPixel.Label.Text = Convert.ToString(currentPixel.Value);
                    changeColor(currentPixel.Label, currentPixel.Value);
                    currentPixel.Operate = 1; // clicked
                }
            }
        }

        private void recExpand(Pixel pixel, int row, int col)
        {
            if(pixel.Operate == 1)
            {
                return; // already operated
            }
            pixel.Operate = 1;
            if(pixel.Value == 0)
            {
                // paint grey
                pixel.Label.Text = "";
                pixel.Label.BackColor = Color.Yellow;
                if (row > 0) recExpand(pixels[row - 1, col], row - 1, col);
                if (col > 0) recExpand(pixels[row, col - 1], row, col - 1);
                if (row < msRow - 1) recExpand(pixels[row + 1, col], row + 1, col);
                if (col < msCol - 1) recExpand(pixels[row, col + 1], row, col + 1);
            }
            else
            {
                pixel.Label.Text = Convert.ToString(pixel.Value);
                changeColor(pixel.Label, pixel.Value);
            }
        }

        private void flagPixel(Pixel pixel, int row, int col, bool isMine)
        {
            pixel.Operate = -1;
            pixel.Label.Text = "p";
            pixel.Label.ForeColor = Color.Purple;
            if (isMine)
            {
                // check all pixels
                for (int i = 0; i < msRow; i++)
                {
                    for (int j = 0; j < msCol; j++)
                    {
                        if(!pixels[i, j].checkFlagIsCorrect())
                        {
                            return;
                        }
                    }
                }
                // all flags are correct
                MessageBox.Show("finished!!");
            }
        }

        private void changeColor(Label label, int n)
        {
            switch (n)
            {
                case 1:
                    label.ForeColor = Color.Blue;
                    break;
                case 2:
                    label.ForeColor = Color.DodgerBlue;
                    break;
                case 3:
                    label.ForeColor = Color.LightSeaGreen;
                    break;
                case 4:
                    label.ForeColor = Color.LimeGreen;
                    break;
                case 5:
                    label.ForeColor = Color.YellowGreen;
                    break;
                case 6:
                    label.ForeColor = Color.DarkOrange;
                    break;
                case 7:
                    label.ForeColor = Color.OrangeRed;
                    break;
                case 8:
                    label.ForeColor = Color.Red;
                    break;
                default:
                    break;
            }
        }


        private class Pixel
        {
            /// <summary>
            /// -1: mine 0: no adjacent mine >0: number of adjacent mines
            /// </summary>
            public int Value = 0;
            /// <summary>
            /// -1: flagged 0: operatable 1: operated, cannot modify
            /// </summary>
            public int Operate = 0;
            public Label Label;
            public Pixel(Label label, int value)
            {
                this.Label = label;
                this.Value = value;
            }
            public bool checkFlagIsCorrect()
            {
                if(this.Value == -1 && this.Operate != -1)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
