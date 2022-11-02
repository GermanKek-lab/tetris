using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        /*public Block[,] screen = {{new Block(92, 0), new Block(126, 0), new Block(160, 0), new Block(194, 0)},
                                       {new Block(92, 0), new Block(126, 0), new Block(160, 0), new Block(160, 32)},
                                       {new Block(92, 0), new Block(126, 0), new Block(160, 0), new Block(92, 32)},
                                       {new Block(92, 0), new Block(126, 0), new Block(160, 0), new Block(126, 32)},
                                       {new Block(92, 0), new Block(126, 0), new Block(126, 32), new Block(160, 32)},
                                       {new Block(92, 32), new Block(126, 32), new Block(126, 0), new Block(160, 0)},
                                       {new Block(126, 0), new Block(160, 0), new Block(126, 32), new Block(160, 32)}};*/
        public bool blockstastus = false;
        public int[,] screen = new int[25, 10];
        public Block[,] gameField = new Block[20, 10];

        public int[,] tetramin1 = { {0, 0, 0, 0},
                                    {1, 1, 1, 1},
                                    {0, 0, 0, 0},
                                    {0, 0, 0, 0}};

        public int[,] tetramin2 = { {0, 0, 0, 0},
                                    {1, 1, 0, 0},
                                    {1, 0, 0, 0},
                                    {1, 0, 0, 0}};

        public int[,] tetramin3 = { {0, 0, 0, 0},
                                    {0, 0, 1, 1},
                                    {0, 0, 0, 1},
                                    {0, 0, 0, 1}};

        public int[,] tetramin4 = { {0, 0, 0, 0},
                                    {0, 1, 0, 0},
                                    {1, 1, 1, 0},
                                    {0, 0, 0, 0}};

        public int[,] tetramin5 = { {0, 0, 0, 0},
                                    {0, 1, 0, 0},
                                    {0, 1, 1, 0},
                                    {0, 0, 1, 0}};

        public int[,] tetramin6 = { {0, 0, 0, 0},
                                    {0, 0, 1, 0},
                                    {0, 1, 1, 0},
                                    {0, 1, 0, 0}};

        public int[,] tetramin7 = { {0, 0, 0, 0},
                                    {0, 1, 1, 0},
                                    {0, 1, 1, 0},
                                    {0, 0, 0, 0}};


        public bool flag_go = true;
        public bool flag_right = true;
        public bool flag_left = true;
        public bool flag_move_left = true;
        public bool flag_move_right = true;

        public Form1()
        {
            InitializeComponent();
        }

        public class Block : PictureBox
        {
            public Block(int x1, int y1)
            {
                this.BackColor = Color.Black;
                this.Size = new Size(32, 32);
                this.Location = new Point(x1, y1);
                this.BringToFront();
            }
        }

        public void Screen()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                screen[24, i] = 3;
            }
            CreateGameField();
        }

        public void CreateGameField()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    gameField[i, j] = new Block(j * 32, i * 32);

                    Controls.Add(gameField[i, j]);
                }
            }
        }

        public void CreateNewBlock()
        {
            Random random = new Random();
            int rnd = random.Next(7);
            switch (rnd)
            {
                case 0:
                    CopyArray(tetramin1);
                    break;

                case 1:
                    CopyArray(tetramin2);
                    break;

                case 2:
                    CopyArray(tetramin3);
                    break;

                case 3:
                    CopyArray(tetramin4);
                    break;

                case 4:
                    CopyArray(tetramin5);
                    break;

                case 5:
                    CopyArray(tetramin6);
                    break;

                case 6:
                    CopyArray(tetramin7);
                    break;
            }
        }

        public void CopyArray(int[,] currentArray)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    screen[i, j] = currentArray[i, j];
                }
            }
        }

        public void CheckBlock()
        {
            int cnt_right = 0;
            int cnt_left = 0;


            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (screen[i, j] == 1)
                    {
                        if (screen[i + 1, j] >= 2)
                        {
                            flag_go = false;
                        }
                        else
                        {
                            flag_go = true;
                        }

                        if ((j + 1 <= 9) && (screen[i, j + 1] == 0))
                        {
                            cnt_left++;
                        }

                        if ((j - 1 >= 0) && (screen[i, j - 1] == 0))
                        {
                            cnt_right++;
                        }

                    }
                }
            }
            if (cnt_right == 4)
            {
                flag_right = true;
            }
            else
            {
                flag_right = false;
            }

            if (cnt_left == 4)
            {
                flag_left = true;
            }
            else
            {
                flag_left = false;
            }

        }

        public void GoBlock()
        {

            for (int i = 24; i >= 0; i--)
            {
                for (int j = 9; j >= 0; j--)
                {
                    if (screen[i, j] == 1)
                    {
                        screen[i + 1, j] = 1;
                        screen[i, j] = 0;
                    }
                }
            }
             
        }

        public void StopBlock()
        {

            for (int i = 24; i >= 0; i--)
            {
                for (int j = 9; j >= 0; j--)
                {
                    if (screen[i, j] == 1)
                    {
                        screen[i, j] = 2;
                    }
                }
            }

        }

        public void GameFieldChange()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (screen[i + 5, j] == 1)
                    {
                        gameField[i, j].BackColor = Color.White;
                    }
                    else if (screen[i + 5, j] == 2)
                    {
                        gameField[i, j].BackColor = Color.Red;
                    }
                    else if (screen[i + 5, j] == 3)
                    {
                        gameField[i, j].BackColor = Color.Blue;
                    }
                    else
                    {
                        gameField[i, j].BackColor = Color.Black;
                    }

                }
            }
        }


        public int flag_tick = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*Block first = new Block(96, 0);
            Block second = new Block(128, 0);
            Block third = new Block(160, 0);
            Block four = new Block(192, 0);
            Controls.Add(first);
            Controls.Add(second);
            Controls.Add(third);
            Controls.Add(four);*/


            if ((flag_go) && (blockstastus))
            {
                GoBlock();
                GameFieldChange();

            }
            else
            {
                StopBlock();
                CreateNewBlock();
                blockstastus = true;
            }

            flag_tick = 0;

        }

        public void RightLeft(int c)
        {
            for (int i = 24; i >= 0; i--)
            {
                for (int j = 9; j >= 0; j--)
                {
                    if (screen[i, j] == 1)
                    {
                        screen[i, j] = 0;
                        screen[i, j + c] = 1;
                    }
                    }
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (flag_tick == 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                        if (flag_right)
                        {
                            flag_tick = 1;
                            RightLeft(flag_tick);
                        }
                        break;

                    case Keys.A:
                        if (flag_left)
                        {
                            flag_tick = -1;
                            RightLeft(flag_tick);
                        }
                        break;
                }
            }
        }
    }
}
