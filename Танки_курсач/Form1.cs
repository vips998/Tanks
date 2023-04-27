using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Танки_курсач
{
    [Serializable]
    public partial class Form1 : Form
    {
        bool game = true;
        private Game_pole game_Pole;
        private Random rnd;
        BinaryFormatter binary = new BinaryFormatter();
/*        private string path = @"С:\Users\source\repos\records.txt";*/
        private int record;
        public Form1(bool newGame)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
            Enemy_1.IncreasePoints += Increase_Points;
            Enemy_2.IncreasePoints += Increase_Points;
            Enemy_3.IncreasePoints += Increase_Points;
            Enemy_1.Counts_Bots += Counts_Bots;
            Enemy_2.Counts_Bots += Counts_Bots;
            Enemy_3.Counts_Bots += Counts_Bots;
            Game_pole.ReducePoints += Reduce_Points;
            Game_pole.End_Game += End_Game;
            Player.Dead += End_Game;
            using (FileStream fs = new FileStream("game.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    game_Pole = (Game_pole)binary.Deserialize(fs);
                    record = (int)binary.Deserialize(fs);
                }
                label2.Text = "Рекорд: " + record.ToString();
            }
            menuStrip1.Visible = true;
            rnd = new Random();
            if (newGame)
            {
                timer_bot.Start();
                timer_bot2.Start();
                timer1.Start();
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                game_Pole = new Game_pole(this);
            }
            else
            {
                {
                    timer_bot.Start();
                    timer_bot2.Start();
                    timer1.Start();
                    game_Pole.followEvents();
                    game = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label1.Text = "Счет: " + game_Pole.points.ToString();
                    label4.Text = "Врагов осталось: " + game_Pole.count_bots.ToString();
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            if (game == true)
            {
                game_Pole.Update(g, this);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
			switch (e.KeyCode)
			{
				case Keys.W:
					{
                        game_Pole.player.ezda(1);
						break;
					}
				case Keys.S:
					{
                        game_Pole.player.ezda(2);
						break;
					}
				case Keys.A:
					{
                        game_Pole.player.ezda(3);
						break;
					}
				case Keys.D:
					{
                        game_Pole.player.ezda(4);
						break;
					}
                case Keys.Escape:
                    {
                        выходToolStripMenuItem_Click(sender, e);
                        break;
                    }
            }
        }

        private void End_Game(Message message)
        {
            timer1.Stop();
            timer_bot.Stop();
            timer_bot2.Stop();
            game = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            this.BackgroundImage = Properties.Resources.game_over;
            if (game_Pole.basa_player == null)
            {
                this.BackgroundImage = Properties.Resources.game_over;
            }
            if ((game_Pole.count_bots==0)||(game_Pole.basa_enemy==null))
            {
                this.BackgroundImage = Properties.Resources.game_win;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer_bot_Tick_1(object sender, EventArgs e)
        {
            int i = rnd.Next(0, 3);
            switch (i)
            {
                case 0:
                    {
                        game_Pole.spawnBots(i);
                        break;
                    }
                case 1:
                    {
                        game_Pole.spawnBots(i);
                        break;
                    }
                case 2:
                    {
                        game_Pole.spawnBots(i);
                        break;
                    }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (game == true)
            {
                game_Pole.player.onShot();
            }
        }

        private void timer_bot2_Tick(object sender, EventArgs e)
        {
            if (game_Pole.bots())
            {
                 game_Pole.Bots_rand();

            }
            else game_Pole.spawnBots(0);
        }
        private void Increase_Points(Message message)
        {
            game_Pole.points += 10;
            label1.Text = "Счет: " + game_Pole.points.ToString();
        }
        private void Reduce_Points(Message message)
        {
            game_Pole.points -= 5;
            label1.Text = "Счет: " + game_Pole.points.ToString();
        }
        private void Counts_Bots(Message message)
        {
            game_Pole.count_bots -= 1;
            label4.Text = "Врагов осталось: " + game_Pole.count_bots.ToString();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) // кнопка выход сверху
        {
            timer1.Stop();
            timer_bot.Stop();
            timer_bot2.Stop();
            this.Close();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (record < game_Pole.points)
            {
                record = game_Pole.points;
            }
            using (FileStream fs = new FileStream("game.dat", FileMode.OpenOrCreate))
            {
                binary.Serialize(fs, game_Pole);
                binary.Serialize(fs, this.record);
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }
    }
}
