using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SwingingDemo
{
    public class Obstacle
    {
        public int x, y, width, height;
        public Rectangle obstacleRect, topRect, bottomRec, leftRec, rightRec;

        public Obstacle(int _x, int _y, int _width, int _height)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            obstacleRect = new Rectangle(x, y, width, height);

            //Make other rectangles on the top, bottom, left, and right of given obstacle
            topRect = new Rectangle(x, y, width, 20);
            bottomRec = new Rectangle(x, y + height - 10, width, 10);
            leftRec = new Rectangle(x, y + 20, 10, height - 20);
            rightRec = new Rectangle(x + width - 10, y + 20, 10, height - 25);
        }
    }
}
