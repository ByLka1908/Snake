using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Game : Form
    {
        /// <summary>
        /// Змейка
        /// </summary>
        Point[] shake;
        /// <summary>
        /// Яблоко
        /// </summary>
        Point apple;
        /// <summary>
        /// Длина змейки
        /// </summary>
        int len;
        /// <summary>
        /// Направление
        /// </summary>
        int direction;// 1 - left; 2 - right; 3 - up; 4 - botton
        public Game()
        {
            InitializeComponent();
            len = 5;
            shake = new Point[200];
            direction = 3;
            StartGame();
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        private void StartGame()
        {
            for (int i = 0; i < 5; i++)
            {
                shake[i].X = 100;
                shake[i].Y = 100 + i * 10;
            }
            timer1.Start();
            apple.X = 10;
            apple.Y = 10;
        }

        /// <summary>
        /// Остановки
        /// </summary>
        private void GameStop()
        {
            timer1.Stop();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 198; i >= 0; i--)
            {
                shake[i+1].X = shake[i].X;
                shake[i+1].Y = shake[i].Y;         
            }

            #region Направление
            if (direction == 1)
            {
                shake[0].X = shake[1].X - 10;
                shake[0].Y = shake[1].Y;
                
                if(shake[0].X < 0)
                {
                    GameStop();
                    MessageBox.Show("Error");
                    StartGame();
                    return;
                }
            }
            if(direction == 2)
            {
                shake[0].X = shake[1].X + 10;
                shake[0].Y = shake[1].Y;
                if (shake[0].X > 490)
                {
                    GameStop();
                    MessageBox.Show("Error");
                    StartGame();
                    return;
                }
            }
            if(direction == 3)
            {
                shake[0].X = shake[1].X;
                shake[0].Y = shake[1].Y - 10;
                if (shake[0].Y < 0)
                {
                    GameStop();
                    MessageBox.Show("Error");
                    StartGame();
                    return;
                }

            }
            if(direction == 4)
            {
                shake[0].X = shake[1].X;
                shake[0].Y = shake[1].Y + 10;
                if (shake[0].Y > 490)
                {
                    GameStop();
                    MessageBox.Show("Error");
                    StartGame();
                    return;
                }
            }
            #endregion

            #region Червь
            SolidBrush snak = new SolidBrush(Color.Brown);

            for (int i = 0; i<len;i++)
            {
                e.Graphics.FillEllipse(snak, shake[i].X, shake[i].Y,10,10);
            }
            #endregion

            #region Яблоко
            SolidBrush g = new SolidBrush(Color.Green);
            e.Graphics.FillEllipse(g, apple.X, apple.Y, 10, 10);
            #endregion

            
            if (shake[0].X == apple.X && shake[0].Y == apple.Y)//Поедание яблока
            {
                len++;
                Random r = new Random();
                apple.X = r.Next(0, 50) * 10;
                apple.Y = r.Next(0, 50) * 10;
            }        
        }

        /// <summary>
        /// Таймер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        /// <summary>
        /// Кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                direction = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                direction = 2;
            }
            if (e.KeyCode == Keys.Up)
            {
                direction = 3;
            }
            if (e.KeyCode == Keys.Down)
            {
                direction = 4;
            }
        }
    }
}
