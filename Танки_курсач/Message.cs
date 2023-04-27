using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Танки_курсач
{
    [Serializable]
    public delegate void MessageHandler(Message message);
    [Serializable]
    public class Message
    {
        public PointF point;
        public int index;
        public int damage;
        public int direct;
    }

}
