using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace GameOfLife
{
    partial class Form1 : Form
    {
        const int mapSize = 15;
        const int cellSize = 30;
        const int timerInterval = 100;
        int[,] lifeArr = new int[mapSize, mapSize];
        int[,] varLifeArr = new int[mapSize, mapSize];
        Button[,] cells = new Button[mapSize, mapSize];

        bool isPlaying = false;

        Timer mainTimer;

        int offset = 25;


        public Form1()
        {
            InitializeComponent();
            setFormSize();
            BuildMenu();
            init();
        }

        void setFormSize()
        {
            this.Width = (mapSize + 1) * cellSize;
            this.Height = (mapSize + 1) * cellSize;
        }



        public int lifeRes(int i, int j)
        {
            int x, y;
            int count = 0;
            int heigh = lifeArr.GetLength(0);
            int width = lifeArr.GetLength(1);
            for (int k = 0; k < 8; k++)
            {
                switch (k)
                {
                    case 0:
                        x = i;
                        y = j;
                        if (x <= 0) x = width;
                        if (y <= 0) y = heigh;

                        if (lifeArr[x - 1, y - 1] == 1) count++;
                        break;
                    case 1:
                        x = i;
                        y = j;
                        if (x <= 0) x = width;

                        if (lifeArr[x - 1, y] == 1) count++;
                        break;
                    case 2:
                        x = i;
                        y = j;
                        if (x <= 0) x = width;
                        if (y >= heigh - 1) y = -1;
                        if (lifeArr[x - 1, y + 1] == 1) count++;
                        break;
                    case 3:
                        x = i;
                        y = j;

                        if (y >= heigh - 1) y = -1;
                        if (lifeArr[x, y + 1] == 1) count++;
                        break;
                    case 4:
                        x = i;
                        y = j;

                        if (x >= width - 1) x = -1;
                        if (y >= heigh - 1) y = -1;
                        if (lifeArr[x + 1, y + 1] == 1) count++;
                        break;
                    case 5:
                        x = i;
                        y = j;

                        if (x >= width - 1) x = -1;

                        if (lifeArr[x + 1, y] == 1) count++;
                        break;
                    case 6:
                        x = i;
                        y = j;

                        if (y <= 0) y = heigh;
                        if (x >= width - 1) x = -1;

                        if (lifeArr[x + 1, y - 1] == 1) count++;
                        break;
                    case 7:
                        x = i;
                        y = j;

                        if (y <= 0) y = heigh;

                        if (lifeArr[x, y - 1] == 1) count++;
                        break;
                }
            }
            return count;
        }
        public void aroundLifeSet(int i, int j)
        {
            int count;
            int x, y;
            int heigh = lifeArr.GetLength(0);
            int width = lifeArr.GetLength(1);
            for (int k = 0; k < 8; k++)
            {
                count = 0;
                switch (k)
                {
                    case 0:
                        x = i;
                        y = j;
                        if (x <= 0) x = width;
                        if (y <= 0) y = heigh;
                        if (lifeArr[x - 1, y - 1] == 0)
                        {
                            count = lifeRes(x - 1, y - 1);
                            if (count == 3)
                                varLifeArr[x - 1, y - 1] = 1;
                        }
                        break;
                    case 1:
                        x = i;
                        y = j;
                        if (x <= 0) x = width;
                        if (lifeArr[x - 1, y] == 0)
                        {
                            count = lifeRes(x - 1, y);
                            if (count == 3)
                                varLifeArr[x - 1, y] = 1;
                        }
                        break;
                    case 2:
                        x = i;
                        y = j;
                        if (x <= 0) x = width;
                        if (y >= heigh - 1) y = -1;
                        if (lifeArr[x - 1, y + 1] == 0)
                        {
                            count = lifeRes(x - 1, y + 1);
                            if (count == 3)
                                varLifeArr[x - 1, y + 1] = 1;
                        }
                        break;
                    case 3:
                        x = i;
                        y = j;
                        if (y >= heigh - 1) y = -1;
                        if (lifeArr[x, y + 1] == 0)
                        {
                            count = lifeRes(x, y + 1);
                            if (count == 3)
                                varLifeArr[x, y + 1] = 1;
                        }
                        break;
                    case 4:
                        x = i;
                        y = j;
                        if (x >= width - 1) x = -1;
                        if (y >= heigh - 1) y = -1;
                        if (lifeArr[x + 1, y + 1] == 0)
                        {
                            count = lifeRes(x + 1, y + 1);
                            if (count == 3)
                                varLifeArr[x + 1, y + 1] = 1;
                        }
                        break;
                    case 5:
                        x = i;
                        y = j;
                        if (x >= width - 1) x = -1;
                        if (lifeArr[x + 1, y] == 0)
                        {
                            count = lifeRes(x + 1, y);
                            if (count == 3)
                                varLifeArr[x + 1, y] = 1;
                        }
                        break;
                    case 6:
                        x = i;
                        y = j;
                        if (y <= 0) y = heigh;
                        if (x >= width - 1) x = -1;
                        if (lifeArr[x + 1, y - 1] == 0)
                        {
                            count = lifeRes(x + 1, y - 1);
                            if (count == 3)
                                varLifeArr[x + 1, y - 1] = 1;
                        }
                        break;
                    case 7:
                        x = i;
                        y = j;
                        if (y <= 0) y = heigh;
                        if (lifeArr[x, y - 1] == 0)
                        {
                            count = lifeRes(x, y - 1);
                            if (count == 3)
                                varLifeArr[x, y - 1] = 1;
                        }
                        break;
                }
            }
        }

        void CalculateNextState()
        {
            int count = 0;
            for (int i = 0; i < lifeArr.GetLength(0); i++)
            {
                for (int j = 0; j < lifeArr.GetLength(1); j++)
                {
                    if (lifeArr[i, j] == 1)
                    {
                        count = lifeRes(i, j);
                        if (count == 2 || count == 3) varLifeArr[i, j] = 1;
                        else varLifeArr[i, j] = 0;

                        aroundLifeSet(i, j);
                    }

                }

            }
            updateLifeArr();
        }

        public void initVarLifeArr()
        {
            for (int i = 0; i < lifeArr.GetLength(0); i++)
            {
                for (int j = 0; j < lifeArr.GetLength(1); j++)
                {
                    varLifeArr[i, j] = lifeArr[i, j];
                }

            }
        }
        public void updateLifeArr()
        {
            for (int i = 0; i < lifeArr.GetLength(0); i++)
            {
                for (int j = 0; j < lifeArr.GetLength(1); j++)
                {
                    lifeArr[i, j] = varLifeArr[i, j];
                }

            }
        }

        public void init()
        {
            mainTimer = new Timer();
            mainTimer.Interval = timerInterval;
            mainTimer.Tick += new EventHandler(UpdateStates);
            lifeArr = initMap();
            initVarLifeArr();
            initCells();
        }

        private void UpdateStates(object sender, EventArgs e)
        {            
            CalculateNextState();
            DisplayMap();
            if (CheckGenerationDead())
            {
                mainTimer.Stop();
                MessageBox.Show("Поколение себя изжило :(");
            }
        }
        void BuildMenu()
        {
            var menu = new MenuStrip();

            var restart = new ToolStripMenuItem("Начать заново");
            restart.Click += new EventHandler(Restart);

            var play = new ToolStripMenuItem("Начать симуляцию");
            play.Click += new EventHandler(Play);

            menu.Items.Add(play);
            menu.Items.Add(restart);

            this.Controls.Add(menu);
        }
        private void Restart(object sender, EventArgs e)
        {
            mainTimer.Stop();
            ClearGame();
        }
        private void Play(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                mainTimer.Start();
            }
        }
        void ClearGame()
        {
            isPlaying = false;
            mainTimer = new Timer();
            mainTimer.Interval = timerInterval;
            mainTimer.Tick += new EventHandler(UpdateStates);
            lifeArr = initMap();
            varLifeArr = initMap();
            ResetCells();
        }
        void ResetCells()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    cells[i, j].BackColor = Color.White;
                }
            }
        }

        bool CheckGenerationDead()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (lifeArr[i, j] == 1)
                        return false;
                }
            }
            return true;
        }

        int[,] initMap()
        {
            int[,] arr = new int[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    arr[i, j] = 0;
                }
            }
            return arr;
        }
        void DisplayMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (lifeArr[i, j] == 1)
                        cells[i, j].BackColor = Color.Black;
                    else cells[i, j].BackColor = Color.White;
                }
            }
        }
        void initCells()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Click += new EventHandler(onCellClick);
                    this.Controls.Add(button);
                    cells[i, j] = button;
                }
            }
        }

        private void onCellClick(object sender, EventArgs e)
        {
            var pressedButton = sender as Button;
            if (!isPlaying)
            {
                var i = pressedButton.Location.Y / cellSize;
                var j = pressedButton.Location.X / cellSize;

                if (lifeArr[i, j] == 0)
                {
                    lifeArr[i, j] = 1;
                    cells[i, j].BackColor = Color.Black;
                }
                else
                {
                    lifeArr[i, j] = 0;
                    cells[i, j].BackColor = Color.White;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                isPlaying = true;
            }
        }
    }
}
