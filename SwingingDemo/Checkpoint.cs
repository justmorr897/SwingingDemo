using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SwingingDemo
{
    public class Checkpoint
    {
        public int x, y;
        public int width = 20;
        public int height = 100;
        public Rectangle checkpointRect;

        public Checkpoint(int _x, int _y)
        {
            x = _x;
            y = _y;

            checkpointRect = new Rectangle(x, y, width, height);
        }

        public void IsCheckpointTouched()
        {
            foreach (Checkpoint c in Level1Screen.checkpoints)
            {
                if (Level1Screen.playerRec.IntersectsWith(c.checkpointRect))
                {
                    if (Level1Screen.level == 0)
                    {

                    }
                    else
                    {
                        Level1Screen.level++;
                        Level1Screen.grappleOn = false;
                        Level1Screen.stopwatch.Reset();
                        Level1Screen.stopwatch.Start();
                        Level1Screen.GameSetup();
                        break;
                    }
                }
            }
        }
    }
}
