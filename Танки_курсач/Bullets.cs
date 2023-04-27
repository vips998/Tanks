using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Танки_курсач
{
    [Serializable]
    abstract class Bullets
    {
        protected Bitmap image;
        protected Message message;

        public float m_x { get; protected set; }  
        public float m_y { get; protected set; }
        
        public int damage { get; protected set; } 
        public int direct { get; protected set; }

        public Bullets (float x, float y, int dir)
        {
            m_x = x;
            m_y = y;
            direct = dir;
        }

        public abstract void move(Graphics g, int i);
    }
}
