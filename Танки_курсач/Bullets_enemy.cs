using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Танки_курсач
{
    [Serializable]
    class Bullets_enemy : Bullets
    {
        public static event MessageHandler DeleteShoot;
        public Bullets_enemy(float x, float y, int dir) : base(x, y, dir)
        {
            image = Properties.Resources.bullet;
            damage = 1;
            message = new Message();
            switch (dir)
            {
                case 2:
                    {
                        image.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    }
                case 3:
                    {
                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    }
                case 4:
                    {
                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    }
            }
        }
        public override void move(Graphics g, int i)
        {
            if ((m_y <= -1) || (m_y >= 832) || (m_x <= -1) || (m_x >= 880))
            {
                if (DeleteShoot != null)
                {
                    message.index = i;
                    DeleteShoot(message);
                }
            }
            else
            {
                switch (direct)
                {
                    case 1:
                        {
                            m_y -= 5;
                            g.DrawImage(image, new RectangleF(m_x + 27, m_y - 20, image.Size.Width, image.Size.Height));
                            break;
                        }
                    case 2:
                        {
                            m_y += 5;
                            g.DrawImage(image, new RectangleF(m_x + 27, m_y + 68, image.Size.Width, image.Size.Height));
                            break;
                        }
                    case 3:
                        {
                            m_x -= 5;

                            g.DrawImage(image, new RectangleF(m_x - 20, m_y + 27, image.Size.Width, image.Size.Height));
                            break;
                        }
                    case 4:
                        {
                            m_x += 5;

                            g.DrawImage(image, new RectangleF(m_x + 64, m_y + 27, image.Size.Width, image.Size.Height));
                            break;
                        }
                }
            }
        }

    }
}
