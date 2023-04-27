using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Танки_курсач
{
    [Serializable]
    public abstract class Tank
    {
        protected Bitmap image { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int direct { get; set; }
        protected int health { get; set; }
        protected  int cooldown { get; set; }

        public Message message;
        protected Point point;
        public int dir = 1;
        public Tank (int x, int y, int direct)
        {
            this.x = x;
            this.y = y;
            this.direct = direct;
        }
        public abstract void Move(Graphics g);
        public abstract void reduceHealth(int dmg, int i);

        public abstract void ezda(int i);

        public abstract void draw(Graphics g);
    }
}
