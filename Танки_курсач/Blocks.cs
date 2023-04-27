using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Танки_курсач
{
    [Serializable]
    public abstract class Blocks
    {
        protected Bitmap image { get; set; }
        protected int health { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Point sizeImage;

        public Message message;
        public Blocks(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public abstract void reductHealth(int dmg, int i);

        public abstract void Draw(Graphics g);


    }
}
