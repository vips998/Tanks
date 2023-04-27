using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Танки_курсач
{
    [Serializable]
    class Player : Tank
    {
        public static event MessageHandler MakeShoot;
        public static event MessageHandler Dead;
        private Bitmap imageHealth;
        public Player(int x, int y, int direct) : base(x, y, direct)
        {
            this.x -= 30;
            image = Properties.Resources.player;
            imageHealth = Properties.Resources.playerhealth;
            imageHealth.MakeTransparent();
            health = 5;
            message = new Message();
        }

        public void onShot()
        {
            message.point.X = x;
            message.point.Y = y;
            message.direct = direct;
            if(MakeShoot != null)
            {
                MakeShoot(message);
            }
        }

        public override void ezda(int i)
        {
            switch (i)
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
            else draw(g);
            for (int i = 0; i < health; i++)
            {
                g.DrawImage(imageHealth, new Rectangle((i*40)+1000,300, 40, 40));
            }
        }

        public override void reduceHealth(int dmg, int i)
        {
            health -= dmg;
            if (health <= 0)
                Dead?.Invoke(message);
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
            for (int i = 0; i < health; i++)
            {
                g.DrawImage(imageHealth, new Rectangle((i * 40) + 1000, 300, 40, 40));
            }
        }
    }
}
