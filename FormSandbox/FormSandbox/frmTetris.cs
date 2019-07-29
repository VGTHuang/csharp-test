using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormSandbox
{
    public partial class frmTetris : Form
    {
        private Bitmap bm;

        public int size = 15;
        public int col = 12, row = 20;

        private Wall wall;

        public frmTetris()
        {
            InitializeComponent();
            this.bm = new Bitmap(col * size, row * size);
            this.wall = new Wall(this);
            wall.RandomizeNewBrick();
            drawAll();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();
        }

        private void drawAll()
        {
            using(Graphics gr = Graphics.FromImage(bm))
            {
                // background
                Rectangle rect = new Rectangle(0, 0, col * size, row * size);
                gr.FillRectangle(Brushes.LightGray, rect);
                this.wall.DrawWall(gr);
            }
            pbx.Image = bm;
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            drawAll();
        }

        private int iii = 0;
        public bool isGameOver = false;
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isGameOver == false)
            {
                Thread.Sleep(500);
                // move down
                if (!this.wall.BrickMoveDown())
                {
                    this.wall.AddBrickToWall();
                    if (!this.isGameOver)
                    {
                        this.wall.RandomizeNewBrick();
                    }
                }
                bgWorker.ReportProgress(iii++);
                iii = iii % 10;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.isGameOver)
            {
                bgWorker.ReportProgress(-1);
            }
        }

        private void frmTetris_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.isGameOver)
            {
                return;
            }
            switch (e.KeyValue)
            {
                case 65:  // left
                    this.wall.BrickMoveLeft();
                    break;
                case 83:  // drop
                    while (wall.BrickMoveDown());
                    this.wall.AddBrickToWall();
                    this.wall.RandomizeNewBrick();
                    break;
                case 68:  // right
                    this.wall.BrickMoveRight();
                    break;
                case 87:  // rotate
                    this.wall.BrickRotate();
                    break;
                default:
                    break;
            }
            this.drawAll();
        }

        #region wall

        private class Wall
        {
            public static Brush[] fills = new Brush[8]
            {
                Brushes.Black,
                Brushes.Red,
                Brushes.Blue,
                Brushes.Yellow,
                Brushes.Green,
                Brushes.Violet,
                Brushes.Olive,
                Brushes.Beige
            };

            public static Pen bBorder = Pens.DarkMagenta;


            private int[,] coords;
            private frmTetris tetris;
            private Brick brick;

            public Wall(frmTetris tetris)
            {
                coords = new int[tetris.row, tetris.col];
                this.tetris = tetris;
            }

            public void AddBrickToWall()
            {
                int[,] bCoords = this.brick.GetRotatedCoords();
                List<int> modifiedRows = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    if(bCoords[i, 1] < 0)
                    {
                        onGameOver(this.tetris);
                        return;
                    }
                    else
                    {
                        coords[bCoords[i, 1], bCoords[i, 0]] = this.brick.index;
                        if (!modifiedRows.Any((r) => r == bCoords[i, 1]))
                        {
                            modifiedRows.Add(bCoords[i, 0]);
                        }
                    }
                }
                foreach(int row in modifiedRows)
                {
                    int i = 0;
                    for(; i < this.coords.GetLength(1); i++)
                    {
                        if(coords[row, i] == 0)
                        {
                            break;
                        }
                    }
                    if(i == this.coords.GetLength(1))
                    {
                        // delete row
                        this.DeleteRow(row);
                    }
                }
            }

            public void DrawWall(Graphics gr)
            {
                for(int row = 0; row < this.tetris.row; row++)
                {
                    for (int col = 0; col < this.tetris.col; col++)
                    {
                        if (this.coords[row, col] != 0)
                        {
                            gr.FillRectangle(fills[this.coords[row, col]], col * this.tetris.size, row * this.tetris.size, this.tetris.size, this.tetris.size);
                            gr.DrawRectangle(bBorder, col * this.tetris.size, row * this.tetris.size, this.tetris.size, this.tetris.size);
                        }
                    }
                }
                int[,] currCoords = this.brick.GetRotatedCoords();
                for (int i = 0; i < 4; i++)
                {
                    gr.FillRectangle(fills[this.brick.index], currCoords[i, 0] * this.tetris.size, currCoords[i, 1] * this.tetris.size, this.tetris.size, this.tetris.size);
                    gr.DrawRectangle(bBorder, currCoords[i, 0] * this.tetris.size, currCoords[i, 1] * this.tetris.size, this.tetris.size, this.tetris.size);
                }
            }

            public void RandomizeNewBrick()
            {
                Random r = new Random();
                switch (r.Next(7) + 1)
                {
                    case 1:
                        this.brick = new BrickL();
                        break;
                    case 2:
                        this.brick = new BrickJ();
                        break;
                    case 3:
                        this.brick = new BrickS();
                        break;
                    case 4:
                        this.brick = new BrickZ();
                        break;
                    case 5:
                        this.brick = new BrickI();
                        break;
                    case 6:
                        this.brick = new BrickT();
                        break;
                    case 7:
                        this.brick = new BrickO();
                        break;
                    default:
                        this.brick = new BrickO();
                        break;
                }
                this.brick.position[0] = this.tetris.col / 2;
                this.brick.position[1] = -1;
            }

            #region move brick

            public bool BrickMoveDown()
            {
                this.brick.position[1]++;
                int[,] currCoord = this.brick.GetRotatedCoords();
                for (int i = 0; i < 4; i++)
                {
                    if (currCoord[i, 1] >= this.tetris.row || (currCoord[i, 0] >= 0 && currCoord[i, 1] >= 0 && this.coords[currCoord[i, 1], currCoord[i, 0]] > 0))
                    {
                        this.brick.position[1]--;
                        return false;
                    }
                }
                return true;
            }

            public void BrickMoveLeft()
            {
                this.brick.position[0]--;
                int[,] currCoord = this.brick.GetRotatedCoords();
                for (int i = 0; i < 4; i++)
                {
                    if (currCoord[i, 0] < 0 || (currCoord[i, 0] >= 0 && currCoord[i, 1] >= 0 && this.coords[currCoord[i, 1], currCoord[i, 0]] > 0))
                    {
                        this.brick.position[0]++;
                        break;
                    }
                }
            }

            public void BrickMoveRight()
            {
                this.brick.position[0]++;
                int[,] currCoord = this.brick.GetRotatedCoords();
                for (int i = 0; i < 4; i++)
                {
                    if (currCoord[i, 0] >= this.tetris.col || (currCoord[i, 0] >= 0 && currCoord[i, 1] >= 0 && this.coords[currCoord[i, 1], currCoord[i, 0]] > 0))
                    {
                        this.brick.position[0]--;
                        break;
                    }
                }
            }

            public void BrickRotate()
            {
                this.brick.rotRight();
                for (int i = 0; i < 4; i++)
                {
                    int[,] currCoord = this.brick.GetRotatedCoords();
                    if (currCoord[i, 0] >= 0 && currCoord[i, 1] >= 0 && currCoord[i, 0] < this.tetris.col && currCoord[i, 1] < this.tetris.row && this.coords[currCoord[i, 1], currCoord[i, 0]] > 0)
                    {
                        this.brick.rotLeft();
                        return;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    while (this.brick.GetRotatedCoords()[i, 0] < 0) // left wall
                    {
                        this.BrickMoveRight();
                    }
                    while (this.brick.GetRotatedCoords()[i, 0] >= this.tetris.col) // right wall
                    {
                        this.BrickMoveLeft();
                    }
                }
            }
            #endregion


            #region check brick status

            public bool CheckIfHitLeftWall(Brick b)
            {
                int[,] currCoords = b.GetRotatedCoords();
                for (int i = 0; i < currCoords.GetLength(0); i++)
                {
                    if (currCoords[i, 0] < 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            public bool CheckIfHitRightWall(Brick b)
            {
                int[,] currCoords = b.GetRotatedCoords();
                for (int i = 0; i < currCoords.GetLength(0); i++)
                {
                    if (currCoords[i, 0] >= this.tetris.col)
                    {
                        return true;
                    }
                }
                return false;
            }
            public bool CheckIfHitBottomWall(Brick b)
            {
                int[,] currCoords = b.GetRotatedCoords();
                for (int i = 0; i < currCoords.GetLength(0); i++)
                {
                    if (currCoords[i, 1] >= this.tetris.row)
                    {
                        return true;
                    }
                }
                return false;
            }

            #endregion

            private void DeleteRow(int rowIndex)
            {
                for(int row = rowIndex; row > 0; row--)
                {
                    for(int col = 0; col < this.tetris.col; col++)
                    {
                        this.coords[row, col] = this.coords[row - 1, col];
                    }
                }
            }

            private void onGameOver(frmTetris t)
            {
                MessageBox.Show("game over");
                t.isGameOver = true;
            }
        }
        
        private abstract class Brick
        {
            public int index;

            protected int[,] coords;
            public int[] position = new int[2];
            public int rot = 0;

            protected Brick() { }

            public virtual int[,] GetRotatedCoords()
            {
                int row = 4;
                int[,] ret = new int[row, 2];
                switch (rot)
                {
                    case 0:
                        for (int i = 0; i < row; i++)
                        {
                            ret[i, 0] = this.coords[i, 0] + this.position[0];
                            ret[i, 1] = this.coords[i, 1] + this.position[1];
                        }
                        break;
                    case 1:
                        for (int i = 0; i < row; i++)
                        {
                            ret[i, 0] = -this.coords[i, 1] + this.position[0];
                            ret[i, 1] = this.coords[i, 0] + this.position[1];
                        }
                        break;
                    case 2:
                        for (int i = 0; i < row; i++)
                        {
                            ret[i, 0] = -this.coords[i, 0] + this.position[0];
                            ret[i, 1] = -this.coords[i, 1] + this.position[1];
                        }
                        break;
                    case 3:
                        for (int i = 0; i < row; i++)
                        {
                            ret[i, 0] = this.coords[i, 1] + this.position[0];
                            ret[i, 1] = -this.coords[i, 0] + this.position[1];
                        }
                        break;
                    default:
                        return this.coords;
                }
                return ret;
            }

            public void rotLeft()
            {
                this.rot = (this.rot - 1) % 4;
            }

            public void rotRight()
            {
                this.rot = (this.rot + 1) % 4;
            }
        }

        private class BrickL : Brick
        {
            public BrickL() : base()
            {
                this.coords = new int[4, 2] { { 0, -2 }, { 0, -1 }, { 0, 0 }, { 1, 0 } };
                this.index = 1;
            }
        }
        private class BrickJ : Brick
        {
            public BrickJ() : base()
            {
                this.coords = new int[4, 2] { { 0, -2 }, { 0, -1 }, { 0, 0 }, { -1, 0 } };
                this.index = 2;
            }
        }
        private class BrickS : Brick
        {
            public BrickS() : base()
            {
                this.coords = new int[4, 2] { { 1, -1 }, { 0, -1 }, { 0, 0 }, { -1, 0 } };
                this.index = 3;
            }
        }
        private class BrickZ : Brick
        {
            public BrickZ() : base()
            {
                this.coords = new int[4, 2] { { -1, -1 }, { 0, -1 }, { 0, 0 }, { 1, 0 } };
                this.index = 4;
            }
        }
        private class BrickI : Brick
        {
            public BrickI() : base()
            {
                this.coords = new int[4, 2] { { 0, -2 }, { 0, -1 }, { 0, 0 }, { 0, 1 } };
                this.index = 5;
            }
        }
        private class BrickT : Brick
        {
            public BrickT() : base()
            {
                this.coords = new int[4, 2] { { 0, -1 }, { -1, 0 }, { 0, 0 }, { 1, 0 } };
                this.index = 6;
            }
        }

        private class BrickO : Brick
        {
            public BrickO() : base()
            {
                this.coords = new int[4, 2] { { 1, -1 }, { 1, 0 }, { 0, 0 }, { 0, -1 } };
                this.index = 7;
            }
            // does not rotate O brick
            public override int[,] GetRotatedCoords()
            {
                int[,] ret = new int[4, 2];
                for (int i = 0; i < 4; i++)
                {
                    ret[i, 0] = this.coords[i, 0] + this.position[0];
                    ret[i, 1] = this.coords[i, 1] + this.position[1];
                }
                return ret;
            }
        }

        #endregion

    }
}
