using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SwingingDemo
{
    public class SpringBoard
    {
        public int x, y;
        public int width = 100;
        public int height = 20;
        public Rectangle springboardRect;

        public SpringBoard(int _x, int _y)
        {
            x = _x;
            y = _y;
            springboardRect = new Rectangle(x, y, width, height);
        }

        public void IsSpringBoardTouched()
        {
            foreach (SpringBoard s in Level1Screen.springboards)
            {
                if (Level1Screen.playerRec.IntersectsWith(s.springboardRect))
                {
                    Level1Screen.player.ySpeed = -90;
                    Level1Screen.isJump = true;
                    Level1Screen.grappleOn = false;
                    Level1Screen.grappleToggle = true;
                    Level1Screen.isSprung = true;
                }
            }

            if (Level1Screen.isSprung)
            {
                Level1Screen.grappleToggle = false;

                if (Level1Screen.player.y < 150)
                {
                    Level1Screen.isSprung = false;
                }
            }
        }
    }
}
