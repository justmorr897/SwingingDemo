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
        //Constant width and height
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
            //If player intersects with any springboard
            //Add a lot to ySpeed to launch them in air
            //Change booleans for game engine
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

            //If the player is in the air after bouncing on a springboard
            //Don't let them swing until the player y is less than 150
            //Prevents some bugs and improves gameplay
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
