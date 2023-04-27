using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Танки_курсач
{
    [Serializable]
    class Game_pole
    {
        private List<Tank> enemies;
        public Player player;
        private List<Bullets> bullets_player;
        public List<Bullets> bullets_enemy;
        private List<Blocks> block;
        public Basa_player basa_player;
        public Basa_enemy basa_enemy;
        private Message message;
        private Random rnd;
        public static event MessageHandler ReducePoints;
        public static event MessageHandler End_Game;
        public int count_bots { get; set; }
        public int points { get; set; }
        public bool prov { get; set; }
        string[,] objects = {{"g","b","g","g","g","g","g","g","g","g","g","g","g"},
                             {"g","b","g","b","g","b","q","b","g","g","b","g","b"},
                             {"g","s","g","b","g","s","g","b","g","g","s","g","b"},
                             {"g","b","g","b","g","b","g","s","g","g","b","q","b"},
                             {"g","g","g","b","g","b","g","g","g","g","g","g","b"},
                             {"g","s","g","g","r","g","g","b","g","g","s","g","g"},
                             {"g","b","g","g","b","b","g","s","g","g","b","g","g"},
                             {"g","d","g","g","g","g","g","g","g","g","g","g","g"},
                             {"g","g","g","g","g","g","g","g","g","g","g","g","g"},
                             {"g","b","g","b","g","b","g","b","g","g","b","g","b"},
                             {"g","s","g","b","g","s","g","b","g","g","s","g","b"},
                             {"g","b","g","b","g","b","g","s","g","g","b","q","b"},
                             {"g","g","g","b","g","b","g","g","g","g","g","g","b"},
                             {"g","s","g","g","r","g","g","b","g","e","s","g","b"},
                             {"g","g","b","b","b","g","g","b","g","g","g","s","s"}};
        public Game_pole(Form1 Form)
        {
            Point start = new Point(448,192);
            player = new Player(start.X, start.Y, 1);
            enemies = new List<Tank>();
            rnd = new Random();
            bullets_player = new List<Bullets>();
            bullets_enemy= new List<Bullets>();
            block = new List<Blocks>();
            message = new Message();
            followEvents();
            spawnBlocks();
            points = 0;
            count_bots = 20;
            prov = false;
        }

        public void followEvents()
        {
            Player.MakeShoot += Player_Shot;
            Enemy_1.MakeShot += Enemy_Shot;
            Enemy_1.DeleteEnemy += Enemy_Delete;
            Enemy_2.MakeShot += Enemy_Shot;
            Enemy_2.DeleteEnemy += Enemy_Delete;
            Enemy_3.MakeShot += Enemy_Shot;
            Enemy_3.DeleteEnemy += Enemy_Delete;
            Bullets_player.DeleteShoot += PlayerBullet_Delete;
            Bullets_enemy.DeleteShoot += EnemyBullet_Delete;
            Brick.DeleteBlockBrick += Block_Delete;
            Steel.DeleteBlockSteel += Block_Delete;
            Basa_player.DeleteBasa_player += DeleteBasa_player;
            Basa_enemy.DeleteBasa_enemy += DeleteBasa_enemy;
        }
        private void Player_Shot(Message message)
        {
            bullets_player.Add(new Bullets_player(message.point.X , message.point.Y, message.direct));
        }
        private void PlayerBullet_Delete(Message message)
        {
            bullets_player.RemoveAt(message.index);
        }

        private void Enemy_Shot(Message message)
        {
            bullets_enemy.Add(new Bullets_enemy(message.point.X, message.point.Y, message.direct));
        }

        private void EnemyBullet_Delete(Message message)
        {
            bullets_enemy.RemoveAt(message.index);
        }
        private void Block_Delete(Message message)
        {
            block.RemoveAt(message.index);
        }

        private void DeleteBasa_player(Message message)
        {
            basa_player = null;
            End_Game(message);
        }
        private void DeleteBasa_enemy(Message message)
        {
            basa_enemy = null;
            End_Game(message);
        }
        private void Enemy_Delete(Message message)
        {
            enemies.RemoveAt(message.index);
        }
        public void spawnBlocks()
        {
            for (int i = 0; i < 15; i++) //26
            {
                for (int j = 0; j < 13; j++) //48
                {

                    if (objects[i, j] == "g")
                    {

                    }
                    else if (objects[i, j] == "b")
                    {
                        block.Add(new Brick(i * 64, j * 64));
                    }
                    else if (objects[i, j] == "s")
                    {
                        block.Add(new Steel(i*64, j * 64));
                    }
                    else if (objects[i, j] == "d") // база игрока
                    {
                        basa_player = new Basa_player(i * 64, j * 64);
                    }
                    else if (objects[i, j] == "e") // база противника
                    {
                        basa_enemy = new Basa_enemy(i * 64, j * 64);
                    }

                    else if (objects[i, j] == "q")
                    {
                        
                    }
                    else if (objects[i, j] == "r")
                    {
                        
                    }
                }
            }
        }
        private void check()   // пуля игрока
        {
            bool stop = false;
            for (int i = 0; i < bullets_player.Count; i++)
            {
                    switch(bullets_player[i].direct)
                    {
                        case 1:
                            {
                            if (basa_enemy != null)
                            {
                                if ((bullets_player[i].m_y - 20 >= basa_enemy.y) && (bullets_player[i].m_y - 20 <= basa_enemy.y + 64) && ((bullets_player[i].m_x + 30 >= basa_enemy.x && bullets_player[i].m_x + 30 <= basa_enemy.x + 63) || (bullets_player[i].m_x + 40 >= basa_enemy.x && bullets_player[i].m_x + 40 <= basa_enemy.x + 63)))
                                {
                                    basa_enemy.reductHealth(bullets_player[i].damage, 0);
                                    bullets_player.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < enemies.Count; j++)
                                {
                                    if ((bullets_player[i].m_y - 20 >= enemies[j].y) && (bullets_player[i].m_y - 20 <= enemies[j].y + 64) && ((bullets_player[i].m_x + 30 >= enemies[j].x && bullets_player[i].m_x + 30 <= enemies[j].x + 63) || (bullets_player[i].m_x + 40 >= enemies[j].x && bullets_player[i].m_x + 40 <= enemies[j].x + 63)))
                                    {
                                        message.index = j;
                                        enemies[j].reduceHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_player[i].m_y - 20 >= block[j].y) && (bullets_player[i].m_y - 20 <= block[j].y + 64) && ((bullets_player[i].m_x + 30 >= block[j].x && bullets_player[i].m_x + 30 <= block[j].x + 63) || (bullets_player[i].m_x + 40 >= block[j].x && bullets_player[i].m_x + 40 <= block[j].x + 63)))
                                    {
                                        message.index = j;
                                        block[j].reductHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            } 
                                break;
                            }
                        case 2:
                            {
                            if (basa_enemy != null)
                            {
                                if ((bullets_player[i].m_y + 90 >= basa_enemy.y) && (bullets_player[i].m_y + 90 <= basa_enemy.y + 64) && ((bullets_player[i].m_x + 30 >= basa_enemy.x && bullets_player[i].m_x + 30 <= basa_enemy.x + 63) || (bullets_player[i].m_x + 40 >= basa_enemy.x && bullets_player[i].m_x + 40 <= basa_enemy.x + 63)))
                                {
                                    basa_enemy.reductHealth(bullets_player[i].damage, 0);
                                    bullets_player.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < enemies.Count; j++)
                                {
                                    if ((bullets_player[i].m_y + 90 >= enemies[j].y) && (bullets_player[i].m_y + 90 <= enemies[j].y + 64) && ((bullets_player[i].m_x + 30 >= enemies[j].x && bullets_player[i].m_x + 30 <= enemies[j].x + 63) || (bullets_player[i].m_x + 40 >= enemies[j].x && bullets_player[i].m_x + 40 <= enemies[j].x + 63)))
                                    {
                                        message.index = j;
                                        enemies[j].reduceHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_player[i].m_y + 90 >= block[j].y) && (bullets_player[i].m_y + 90 <= block[j].y + 64) && ((bullets_player[i].m_x + 30 >= block[j].x && bullets_player[i].m_x + 30 <= block[j].x + 63) || (bullets_player[i].m_x + 40 >= block[j].x && bullets_player[i].m_x + 40 <= block[j].x + 63)))
                                    {
                                        message.index = j;
                                        block[j].reductHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        case 3:
                            {
                            if (basa_enemy != null)
                            {
                                if ((bullets_player[i].m_y + 35 >= basa_enemy.y) && (bullets_player[i].m_y + 25 <= basa_enemy.y + 64) && ((bullets_player[i].m_x - 20 >= basa_enemy.x && bullets_player[i].m_x - 20 <= basa_enemy.x + 63) || (bullets_player[i].m_x - 10 >= basa_enemy.x && bullets_player[i].m_x - 10 <= basa_enemy.x + 63)))
                                {
                                    basa_enemy.reductHealth(bullets_player[i].damage, 0);
                                    bullets_player.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < enemies.Count; j++)
                                {
                                    if ((bullets_player[i].m_y + 35 >= enemies[j].y) && (bullets_player[i].m_y + 25 <= enemies[j].y + 64) && ((bullets_player[i].m_x - 20 >= enemies[j].x && bullets_player[i].m_x - 20 <= enemies[j].x + 63) || (bullets_player[i].m_x - 10 >= enemies[j].x && bullets_player[i].m_x - 10 <= enemies[j].x + 63)))
                                    {
                                        message.index = j;
                                        enemies[j].reduceHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_player[i].m_y + 35 >= block[j].y) && (bullets_player[i].m_y + 25 <= block[j].y + 64) && ((bullets_player[i].m_x - 20 >= block[j].x && bullets_player[i].m_x - 20 <= block[j].x + 63) || (bullets_player[i].m_x - 10 >= block[j].x && bullets_player[i].m_x - 10 <= block[j].x + 63)))
                                    {
                                        message.index = j;
                                        block[j].reductHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }      
                            break;
                        }
                        case 4:
                           {
                            if (basa_enemy != null)
                            {
                                if ((bullets_player[i].m_y + 35 >= basa_enemy.y) && (bullets_player[i].m_y + 25 <= basa_enemy.y + 64) && ((bullets_player[i].m_x + 90 >= basa_enemy.x && bullets_player[i].m_x + 90 <= basa_enemy.x + 63) || (bullets_player[i].m_x + 80 >= basa_enemy.x && bullets_player[i].m_x + 80 <= basa_enemy.x + 63)))
                                {
                                    basa_enemy.reductHealth(bullets_player[i].damage, 0);
                                    bullets_player.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < enemies.Count; j++)
                                {
                                    if ((bullets_player[i].m_y + 35 >= enemies[j].y) && (bullets_player[i].m_y + 25 <= enemies[j].y + 64) && ((bullets_player[i].m_x + 90 >= enemies[j].x && bullets_player[i].m_x + 90 <= enemies[j].x + 63) || (bullets_player[i].m_x + 80 >= enemies[j].x && bullets_player[i].m_x + 80 <= enemies[j].x + 63)))
                                    {
                                        message.index = j;
                                        enemies[j].reduceHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_player[i].m_y + 35 >= block[j].y) && (bullets_player[i].m_y + 25 <= block[j].y + 64) && ((bullets_player[i].m_x + 90 >= block[j].x && bullets_player[i].m_x + 90 <= block[j].x + 63) || (bullets_player[i].m_x + 80 >= block[j].x && bullets_player[i].m_x + 80 <= block[j].x + 63)))
                                    {
                                        message.index = j;
                                        block[j].reductHealth(bullets_player[i].damage, j);
                                        bullets_player.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
            }
        }
        private void check_bots(int i)   // пуля ботов
        {
            bool stop = false;
                switch (bullets_enemy[i].direct)
                {
                    case 1:
                        {
                        if (basa_player != null)
                        {
                            if ((bullets_enemy[i].m_y - 20 >= basa_player.y) && (bullets_enemy[i].m_y - 20 <= basa_player.y + 64) && ((bullets_enemy[i].m_x + 30 >= basa_player.x && bullets_enemy[i].m_x + 30 <= basa_player.x + 63) || (bullets_enemy[i].m_x + 40 >= basa_player.x && bullets_enemy[i].m_x + 40 <= basa_player.x + 63)))
                            {
                                basa_player.reductHealth(bullets_enemy[i].damage, 1);
                                bullets_enemy.RemoveAt(i);
                                stop = true;
                                prov = true;
                                break;
                            }
                        }
                            if (!stop)
                            {
                                if ((bullets_enemy[i].m_y - 20 >= player.y) && (bullets_enemy[i].m_y - 20 <= player.y + 64) && ((bullets_enemy[i].m_x + 30 >= player.x && bullets_enemy[i].m_x + 30 <= player.x + 63) || (bullets_enemy[i].m_x + 40 >= player.x && bullets_enemy[i].m_x + 40 <= player.x + 63)))
                                {
                                    message.index = i;
                                    player.reduceHealth(bullets_enemy[i].damage, i);
                                    player.x = 448;
                                    player.y = 128;
                                    ReducePoints(message);
                                    bullets_enemy.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_enemy[i].m_y - 20 >= block[j].y) && (bullets_enemy[i].m_y - 20 <= block[j].y + 64) && ((bullets_enemy[i].m_x + 30 >= block[j].x && bullets_enemy[i].m_x + 30 <= block[j].x + 63) || (bullets_enemy[i].m_x + 40 >= block[j].x && bullets_enemy[i].m_x + 40 <= block[j].x + 63)))
                                    {
                                        message.index = i;
                                    block[j].reductHealth(bullets_enemy[i].damage, j);
                                        bullets_enemy.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                        if (basa_player != null)
                        {
                            if ((bullets_enemy[i].m_y + 90 >= basa_player.y) && (bullets_enemy[i].m_y + 90 <= basa_player.y + 64) && ((bullets_enemy[i].m_x + 30 >= basa_player.x && bullets_enemy[i].m_x + 30 <= basa_player.x + 63) || (bullets_enemy[i].m_x + 40 >= basa_player.x && bullets_enemy[i].m_x + 40 <= basa_player.x + 63)))
                            {
                                basa_player.reductHealth(bullets_enemy[i].damage, 1);
                                bullets_enemy.RemoveAt(i);
                                stop = true;
                                prov = true;
                                break;
                            }
                        }
                            if (!stop)
                            {
                                if ((bullets_enemy[i].m_y + 90 >= player.y) && (bullets_enemy[i].m_y + 90 <= player.y + 64) && ((bullets_enemy[i].m_x + 30 >= player.x && bullets_enemy[i].m_x + 30 <= player.x + 63) || (bullets_enemy[i].m_x + 40 >= player.x && bullets_enemy[i].m_x + 40 <= player.x + 63)))
                                {
                                    message.index = i;
                                    player.reduceHealth(bullets_enemy[i].damage, i);
                                    player.x = 448;
                                    player.y = 128;
                                    ReducePoints(message);
                                    bullets_enemy.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_enemy[i].m_y + 90 >= block[j].y) && (bullets_enemy[i].m_y + 90 <= block[j].y + 64) && ((bullets_enemy[i].m_x + 30 >= block[j].x && bullets_enemy[i].m_x + 30 <= block[j].x + 63) || (bullets_enemy[i].m_x + 40 >= block[j].x && bullets_enemy[i].m_x + 40 <= block[j].x + 63)))
                                    {
                                        message.index = i;
                                        block[j].reductHealth(bullets_enemy[i].damage, j);
                                        bullets_enemy.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                        if (basa_player != null)
                        {
                            if ((bullets_enemy[i].m_y + 35 >= basa_player.y) && (bullets_enemy[i].m_y + 25 <= basa_player.y + 64) && ((bullets_enemy[i].m_x - 20 >= basa_player.x && bullets_enemy[i].m_x - 20 <= basa_player.x + 63) || (bullets_enemy[i].m_x - 10 >= basa_player.x && bullets_enemy[i].m_x - 10 <= basa_player.x + 63)))
                            {
                                basa_player.reductHealth(bullets_enemy[i].damage, 1);
                                bullets_enemy.RemoveAt(i);
                                stop = true;
                                prov = true;
                                break;
                            }
                        }
                            if (!stop)
                            {
                                if ((bullets_enemy[i].m_y + 35 >= player.y) && (bullets_enemy[i].m_y + 25 <= player.y + 64) && ((bullets_enemy[i].m_x - 20 >= player.x && bullets_enemy[i].m_x - 20 <= player.x + 63) || (bullets_enemy[i].m_x - 10 >= player.x && bullets_enemy[i].m_x - 10 <= player.x + 63)))
                                {
                                    message.index = i;
                                    player.reduceHealth(bullets_enemy[i].damage, i);
                                    player.x = 448;
                                    player.y = 128;
                                    ReducePoints(message);
                                    bullets_enemy.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_enemy[i].m_y + 35 >= block[j].y) && (bullets_enemy[i].m_y + 25 <= block[j].y + 64) && ((bullets_enemy[i].m_x - 20 >= block[j].x && bullets_enemy[i].m_x - 20 <= block[j].x + 63) || (bullets_enemy[i].m_x - 10 >= block[j].x && bullets_enemy[i].m_x - 10 <= block[j].x + 63)))
                                    {
                                        message.index = i;
                                        block[j].reductHealth(bullets_enemy[i].damage, j);
                                        bullets_enemy.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                        if (basa_player != null)
                        {
                            if ((bullets_enemy[i].m_y + 35 >= basa_player.y) && (bullets_enemy[i].m_y + 25 <= basa_player.y + 64) && ((bullets_enemy[i].m_x + 90 >= basa_player.x && bullets_enemy[i].m_x + 90 <= basa_player.x + 63) || (bullets_enemy[i].m_x + 80 >= basa_player.x && bullets_enemy[i].m_x + 80 <= basa_player.x + 63)))
                            {
                                basa_player.reductHealth(bullets_enemy[i].damage, 1);
                                bullets_enemy.RemoveAt(i);
                                stop = true;
                                prov = true;
                                break;
                            }
                        }
                            if (!stop)
                            {
                                if ((bullets_enemy[i].m_y + 35 >= player.y) && (bullets_enemy[i].m_y + 25 <= player.y + 64) && ((bullets_enemy[i].m_x + 90 >= player.x && bullets_enemy[i].m_x + 90 <= player.x + 63) || (bullets_enemy[i].m_x + 80 >= player.x && bullets_enemy[i].m_x + 80 <= player.x + 63)))
                                {
                                    message.index = i;
                                    player.reduceHealth(bullets_enemy[i].damage, i);
                                    player.x = 448;
                                    player.y = 128;
                                    ReducePoints(message);
                                    bullets_enemy.RemoveAt(i);
                                    stop = true;
                                    break;
                                }
                            }
                            if (!stop)
                            {
                                for (int j = 0; j < block.Count; j++)
                                {
                                    if ((bullets_enemy[i].m_y + 35 >= block[j].y) && (bullets_enemy[i].m_y + 25 <= block[j].y + 64) && ((bullets_enemy[i].m_x + 90 >= block[j].x && bullets_enemy[i].m_x + 90 <= block[j].x + 63) || (bullets_enemy[i].m_x + 80 >= block[j].x && bullets_enemy[i].m_x + 80 <= block[j].x + 63)))
                                    {
                                        message.index = i;
                                        block[j].reductHealth(bullets_enemy[i].damage, j);
                                        bullets_enemy.RemoveAt(i);
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        }

                }
        }
        public bool check_drive(Form1 Form) // езда игрока
        {
            bool prov = true;
            switch (player.dir)
            {
                case 1:
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((player.y == enemies[i].y + 64) && ((player.x >= enemies[i].x && player.x <= enemies[i].x + 63) || (player.x + 63 >= enemies[i].x && player.x + 63 <= enemies[i].x + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i=0; i< block.Count; i++)
                        {
                            if ((player.y == block[i].y+64) && ((player.x >= block[i].x && player.x <= block[i].x + 63) || (player.x + 63 >= block[i].x && player.x + 63 <= block[i].x + 63))) 
                            {
                                prov = false;
                                break;
                            }
                        }
                        if ((player.y == basa_player.y + 64) && ((player.x >= basa_player.x && player.x <= basa_player.x + 63) || (player.x + 63 >= basa_player.x && player.x + 63 <= basa_player.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((player.y == basa_enemy.y + 64) && ((player.x >= basa_enemy.x && player.x <= basa_enemy.x + 63) || (player.x + 63 >= basa_enemy.x && player.x + 63 <= basa_enemy.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                    }

                case 2:
                    {

                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((player.y + 64 == enemies[i].y) && ((player.x >= enemies[i].x && player.x <= enemies[i].x + 63) || (player.x + 63 >= enemies[i].x && player.x + 63 <= enemies[i].x + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                        {
                            if ((player.y +64 == block[i].y) && ((player.x >= block[i].x && player.x <= block[i].x + 63) || (player.x + 63 >= block[i].x && player.x + 63 <= block[i].x + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        if ((player.y + 64 == basa_player.y) && ((player.x >= basa_player.x && player.x <= basa_player.x + 63) || (player.x + 63 >= basa_player.x && player.x + 63 <= basa_player.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((player.y + 64 == basa_enemy.y) && ((player.x >= basa_enemy.x && player.x <= basa_enemy.x + 63) || (player.x + 63 >= basa_enemy.x && player.x + 63 <= basa_enemy.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                    }

                case 3:
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((player.x == enemies[i].x + 64) && ((player.y >= enemies[i].y && player.y <= enemies[i].y + 63) || (player.y + 63 >= enemies[i].y && player.y + 63 <= enemies[i].y + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                        {
                            if ((player.x == block[i].x+64) && ((player.y >= block[i].y && player.y <= block[i].y + 63) || (player.y + 63 >= block[i].y && player.y + 63 <= block[i].y + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        if ((player.x == basa_player.x + 64) && ((player.y >= basa_player.y && player.y <= basa_player.y + 63) || (player.y + 63 >= basa_player.y && player.y + 63 <= basa_player.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((player.x == basa_enemy.x + 64) && ((player.y >= basa_enemy.y && player.y <= basa_enemy.y + 63) || (player.y + 63 >= basa_enemy.y && player.y + 63 <= basa_enemy.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                    }
                case 4:
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((player.x + 64 == enemies[i].x) && ((player.y >= enemies[i].y && player.y <= enemies[i].y + 63) || (player.y + 63 >= enemies[i].y && player.y + 63 <= enemies[i].y + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                        {
                            if ((player.x +64 == block[i].x) && ((player.y >= block[i].y && player.y <= block[i].y + 63) || (player.y + 63 >= block[i].y && player.y + 63 <= block[i].y + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        if ((player.x + 64 == basa_player.x) && ((player.y >= basa_player.y && player.y <= basa_player.y + 63) || (player.y + 63 >= basa_player.y && player.y + 63 <= basa_player.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((player.x + 64 == basa_enemy.x) && ((player.y >= basa_enemy.y && player.y <= basa_enemy.y + 63) || (player.y + 63 >= basa_enemy.y && player.y + 63 <= basa_enemy.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                    }
            }
            return prov;
        }

        public bool check_drive_bots(Form1 Form, int j) // езда ботов
        {
            bool prov = true;
                switch (enemies[j].dir)
                {
                    case 1:
                        {

                            if ((enemies[j].y == player.y + 64) && ((enemies[j].x >= player.x && enemies[j].x <= player.x + 63) || (enemies[j].x + 63 >= player.x && enemies[j].x + 63 <= player.x + 63)))
                            {
                                 prov = false;
                                 break;
                            }
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((enemies[j].y == enemies[i].y + 64) && ((enemies[j].x >= enemies[i].x && enemies[j].x <= enemies[i].x + 63) || (enemies[j].x + 63 >= enemies[i].x && enemies[j].x + 63 <= enemies[i].x + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                            {
                                if ((enemies[j].y == block[i].y + 64) && ((enemies[j].x >= block[i].x && enemies[j].x <= block[i].x + 63) || (enemies[j].x + 63 >= block[i].x && enemies[j].x + 63 <= block[i].x + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                            }
                        if ((enemies[j].y == basa_player.y + 64) && ((enemies[j].x >= basa_player.x && enemies[j].x <= basa_player.x + 63) || (enemies[j].x + 63 >= basa_player.x && enemies[j].x + 63 <= basa_player.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((enemies[j].y == basa_enemy.y + 64) && ((enemies[j].x >= basa_enemy.x && enemies[j].x <= basa_enemy.x + 63) || (enemies[j].x + 63 >= basa_enemy.x && enemies[j].x + 63 <= basa_enemy.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                        }

                    case 2:
                        {

                            if ((enemies[j].y + 64 == player.y) && ((enemies[j].x >= player.x && enemies[j].x <= player.x + 63) || (enemies[j].x + 63 >= player.x && enemies[j].x + 63 <= player.x + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((enemies[j].y + 64 == enemies[i].y) && ((enemies[j].x >= enemies[i].x && enemies[j].x <= enemies[i].x + 63) || (enemies[j].x + 63 >= enemies[i].x && enemies[j].x + 63 <= enemies[i].x + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                            {
                                if ((enemies[j].y + 64 == block[i].y) && ((enemies[j].x >= block[i].x && enemies[j].x <= block[i].x + 63) || (enemies[j].x + 63 >= block[i].x && enemies[j].x + 63 <= block[i].x + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                            }
                        if ((enemies[j].y + 64 == basa_player.y) && ((enemies[j].x >= basa_player.x && enemies[j].x <= basa_player.x + 63) || (enemies[j].x + 63 >= basa_player.x && enemies[j].x + 63 <= basa_player.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((enemies[j].y + 64 == basa_enemy.y) && ((enemies[j].x >= basa_enemy.x && enemies[j].x <= basa_enemy.x + 63) || (enemies[j].x + 63 >= basa_enemy.x && enemies[j].x + 63 <= basa_enemy.x + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                        }

                    case 3:
                        {

                            if ((enemies[j].x == player.x + 64) && ((enemies[j].y >= player.y && enemies[j].y <= player.y + 63) || (enemies[j].y + 63 >= player.y && enemies[j].y + 63 <= player.y + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((enemies[j].x == enemies[i].x + 64) && ((enemies[j].y >= enemies[i].y && enemies[j].y <= enemies[i].y + 63) || (enemies[j].y + 63 >= enemies[i].y && enemies[j].y + 63 <= enemies[i].y + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                            {
                                if ((enemies[j].x == block[i].x + 64) && ((enemies[j].y >= block[i].y && enemies[j].y <= block[i].y + 63) || (enemies[j].y + 63 >= block[i].y && enemies[j].y + 63 <= block[i].y + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                            }
                        if ((enemies[j].x == basa_player.x + 64) && ((enemies[j].y >= basa_player.y && enemies[j].y <= basa_player.y + 63) || (enemies[j].y + 63 >= basa_player.y && enemies[j].y + 63 <= basa_player.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((enemies[j].x == basa_enemy.x + 64) && ((enemies[j].y >= basa_enemy.y && enemies[j].y <= basa_enemy.y + 63) || (enemies[j].y + 63 >= basa_enemy.y && enemies[j].y + 63 <= basa_enemy.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                        }
                    case 4:
                        {

                            if ((enemies[j].x + 64 == player.x) && ((enemies[j].y >= player.y && enemies[j].y <= player.y + 63) || (enemies[j].y + 63 >= player.y && enemies[j].y + 63 <= player.y + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if ((enemies[j].x + 64 == enemies[i].x) && ((enemies[j].y >= enemies[i].y && enemies[j].y <= enemies[i].y + 63) || (enemies[j].y + 63 >= enemies[i].y && enemies[j].y + 63 <= enemies[i].y + 63)))
                            {
                                prov = false;
                                break;
                            }
                        }
                        for (int i = 0; i < block.Count; i++)
                            {
                                if ((enemies[j].x + 64 == block[i].x) && ((enemies[j].y >= block[i].y && enemies[j].y <= block[i].y + 63) || (enemies[j].y + 63 >= block[i].y && enemies[j].y + 63 <= block[i].y + 63)))
                                {
                                    prov = false;
                                    break;
                                }
                            }
                        if ((enemies[j].x + 64 == basa_player.x) && ((enemies[j].y >= basa_player.y && enemies[j].y <= basa_player.y + 63) || (enemies[j].y + 63 >= basa_player.y && enemies[j].y + 63 <= basa_player.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        if ((enemies[j].x + 64 == basa_enemy.x) && ((enemies[j].y >= basa_enemy.y && enemies[j].y <= basa_enemy.y + 63) || (enemies[j].y + 63 >= basa_enemy.y && enemies[j].y + 63 <= basa_enemy.y + 63)))
                        {
                            prov = false;
                            break;
                        }
                        break;
                        }
                }
                return prov;
        }


        public void Update(Graphics g, Form1 Form)
        {
            if (check_drive(Form))  // езда игрока
            {
                player.Move(g);
            }
            else
            {
                player.draw(g);
            }
            for (int i = 0; i < enemies.Count; i++) // езда ботов
            {
                if (check_drive_bots(Form, i))
                {
                    enemies[i].Move(g);
                }
                else enemies[i].draw(g);
            }
            for (int i = 0; i < block.Count; i++)  // отрисовка блоков
            {
                block[i].Draw(g);
            }
            basa_player.Draw(g); // отрисовка базы игрока
            basa_enemy.Draw(g); // отрисовка базы противника
            for (int i = 0; i < bullets_player.Count; i++) // пули игрока
            {
                if (basa_enemy != null)
                {
                    bullets_player[i].move(g, i);
                }
            }
            for (int i = 0; i < bullets_enemy.Count; i++) // пули ботов
            {
                if (basa_player != null)
                {
                    bullets_enemy[i].move(g, i);
                }
            }
            check();
            for(int i=0; i< bullets_enemy.Count; i++)
            {
                check_bots(i);
                if (prov)
                {
                    prov = false;
                    break;
                }
            }
            if(count_bots==0)
            {
                End_Game(message);
            }
        }


        public void spawnBots(int i)
        {
            if (count_bots > 3)
            {
                if (enemies.Count < 4)
                {
                    if (i == 0)
                    {
                        enemies.Add(new Enemy_1(rnd.Next(1, 12) * 64, rnd.Next(8, 10) * 64, 1));
                    }
                    if (i == 1)
                    {
                        enemies.Add(new Enemy_2(rnd.Next(7, 9) * 64, rnd.Next(3, 12) * 64, 1));
                    }
                    if (i == 2)
                    {
                        enemies.Add(new Enemy_3(rnd.Next(5, 12) * 64, rnd.Next(8, 10) * 64, 1));
                    }
                }
            }
        }

        public void Bots_rand()
        {
            int k = rnd.Next(0, enemies.Count);
            enemies[k].ezda(k);
        }

        public bool bots()
        {
            if (enemies.Count != 0)
            {
                return true;
            }
            else return false;
        }
    }
}
