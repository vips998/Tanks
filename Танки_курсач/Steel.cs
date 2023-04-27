using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace Танки_курсач
{
    [Serializable]
    class Steel : Blocks
    {
        public static event MessageHandler DeleteBlockSteel;
        public Steel(int x, int y) : base(x, y)
        {
            image = Properties.Resources.steel;
            health = 4;
            message = new Message();
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new Rectangle(x, y, image.Size.Width, image.Size.Height));
        }
        public override void reductHealth(int dmg, int i)
        {
            health -= dmg;
            if (health <= 0)
            {
                message.index = i;
                if (DeleteBlockSteel != null)
                {
                    DeleteBlockSteel(message);
                }
            }
        }

    }
}
