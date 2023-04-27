using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Танки_курсач
{
    [Serializable]
    class Enemy_2 : Tank
    {
        public static event MessageHandler MakeShot;
        public static event MessageHandler DeleteEnemy;
        public static event MessageHandler IncreasePoints;
        public static event MessageHandler Counts_Bots;
        private Random rnd;
        public Enemy_2(int x, int y, int direct) : base(x, y, direct)
        {
            image = Properties.Resources.tank_enemy_2;
            health = 1;
            cooldown = 50;
            message = new Message();
            rnd = new Random();
        }

        public void onShot()
        {
            message.point.X = x;
            message.point.Y = y;
            message.direct = direct;
            if (MakeShot != null)
            {
                MakeShot(message);
            }
        }

        public override void ezda(int i)
        {
            switch (rnd.Next(0, 5))
            {
                case 1:
                    {
                        dir = 1;
                        point.Y = -2;
                        point.X = 0;
                        break;
                    }
                case 2:
                    {
                        dir = 2;
                        point.Y = 2;
                        point.X = 0;
                        break;
                    }
                case 3:
                    {
                        dir = 3;
                        point.X = -2;
                        point.Y = 0;
                        break;
                    }
                case 4:
                    {
                        dir = 4;
                        point.X = 2;
                        point.Y = 0;
                        break;
                    }
            }
        }

        public override void reduceHealth(int dmg, int i)
        {
            health -= dmg;
            if (health <= 0)
            {
                message.index = i;
                if (DeleteEnemy != null)
                {
                    IncreasePoints(message);
                    DeleteEnemy(message);
                    Counts_Bots(message);
                }
            }
        }

        public override void Move(Graphics g)
        {
            if ((x + 64 + point.X <= 960) && (x + point.X >= 0) && (y + 64 + point.Y <= 832) && (y + point.Y >= 0))
            {
                switch (dir)
                {
                    case 1:
                        {
                            if (direct == 2)
                            {
                                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                                image.RotateFlip(RotateFlipType.Rotate180FlipY);
                            }
                            if (direct == 3)
                            { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                            if (direct == 4)
                            { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                            direct = 1;
                            break;
                        }
                    case 2:
                        {
                            if (direct == 1)
                            {
                                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                                image.RotateFlip(RotateFlipType.Rotate180FlipY);
                            }
                            if (direct == 3)
                            { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                            if (direct == 4)
                            { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                            direct = 2;
                            break;
                        }
                    case 3:
                        {
                            if (direct == 1)
                            { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                            if (direct == 2)
                            { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                            if (direct == 4)
                            {
                                image.RotateFlip(RotateFlipType.Rotate90FlipX);
                                image.RotateFlip(RotateFlipType.Rotate270FlipX);
                            }
                            direct = 3;
                            break;
                        }
                    case 4:
                        {
                            if (direct == 1)
                            { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                            if (direct == 2)
                            { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                            if (direct == 3)
                            {
                                image.RotateFlip(RotateFlipType.Rotate90FlipX);
                                image.RotateFlip(RotateFlipType.Rotate270FlipX);
                            }
                            direct = 4;
                            break;
                        }
                }
                x += point.X;
                y += point.Y;
                g.DrawImage(image, new Rectangle(x, y, image.Size.Width, image.Size.Height));
            }
            else
            { draw(g); }
            cooldown--;
            if (cooldown == 0)
            {
                message.point.X = x;
                message.point.Y = y;
                message.direct = direct;
                MakeShot(message);
                cooldown = 50;
            }
        }

        public override void draw(Graphics g)
        {

            switch (dir)
            {
                case 1:
                    {
                        if (direct == 2)
                        {
                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            image.RotateFlip(RotateFlipType.Rotate180FlipY);
                        }
                        if (direct == 3)
                        { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                        if (direct == 4)
                        { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                        direct = 1;
                        break;
                    }
                case 2:
                    {
                        if (direct == 1)
                        {
                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            image.RotateFlip(RotateFlipType.Rotate180FlipY);
                        }
                        if (direct == 3)
                        { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                        if (direct == 4)
                        { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                        direct = 2;
                        break;
                    }
                case 3:
                    {
                        if (direct == 1)
                        { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                        if (direct == 2)
                        { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                        if (direct == 4)
                        {
                            image.RotateFlip(RotateFlipType.Rotate90FlipX);
                            image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        }
                        direct = 3;
                        break;
                    }
                case 4:
                    {
                        if (direct == 1)
                        { image.RotateFlip(RotateFlipType.Rotate270FlipX); }
                        if (direct == 2)
                        { image.RotateFlip(RotateFlipType.Rotate90FlipX); }
                        if (direct == 3)
                        {
                            image.RotateFlip(RotateFlipType.Rotate90FlipX);
                            image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        }
                        direct = 4;
                        break;
                    }
            }
            g.DrawImage(image, new Rectangle(x, y, image.Size.Width, image.Size.Height));
        }
    }
}
