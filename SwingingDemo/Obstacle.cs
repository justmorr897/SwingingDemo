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

            topRect = new Rectangle(x, y, width, 20);
            bottomRec = new Rectangle(x, y + height - 10, width, 10);

            leftRec = new Rectangle(x, y + 20, 10, height - 20);
            rightRec = new Rectangle(x + width - 10, y + 20, 10, height - 25);
        }

        //public void CheckOtherIntersections(Player _player)
        //{
        //    foreach (Obstacle o in Level1Screen.obstacles)
        //    {
        //        //if (o.topRect.IntersectsWith(Level1Screen.playerRec))
        //        //{
        //        //    Level1Screen.hasIntersected = true;
        //        //    _player.y = o.y - 29;
        //        //    _player.ySpeed = 0;
        //        //    Level1Screen.grappleToggle = false;
        //        //}
        //        //else if (!Level1Screen.hasIntersected)
        //        //{
        //        //    Level1Screen.grappleToggle = true;
        //        //}

        //        //if (o.bottomRec.IntersectsWith(Level1Screen.playerRec))
        //        //{
        //        //    y = o.bottomRec.Y + 10;
        //        //    _player.ySpeed = 0;
        //        //}


        //        //if (o.leftRec.IntersectsWith(Level1Screen.playerRec))
        //        //{
        //        //    x = o.leftRec.X - width;
        //        //    _player.xSpeed = 0;
        //        //}


        //        //if (o.rightRec.IntersectsWith(Level1Screen.playerRec))
        //        //{
        //        //    x = o.rightRec.X + width - 10;
        //        //    _player.xSpeed = 0;
        //        //}


        //        //if (Level1Screen.rightArrowDown && Level1Screen.grappleToggle && o.topRect.IntersectsWith(Level1Screen.playerRec) == false)
        //        //{
        //        //    if (Level1Screen.grappleOn)
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        Level1Screen.grappleOn = true;
        //        //        Level1Screen.clickPoint = new Point(_player.x + 150, _player.y - 150);
        //        //    }

        //        //}

        //        //if (Level1Screen.leftArrowDown && Level1Screen.grappleToggle && o.topRect.IntersectsWith(Level1Screen.playerRec) == false)
        //        //{
        //        //    if (Level1Screen.grappleOn)
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        Level1Screen.grappleOn = true;
        //        //        Level1Screen.clickPoint = new Point(_player.x - 150, _player.y - 150);
        //        //    }

        //        //}

        //        //if ((Level1Screen.spaceDown || Level1Screen.upArrowDown) && o.topRect.IntersectsWith(Level1Screen.playerRec))
        //        //{
        //        //    Level1Screen.isJump = true;
        //        //    _player.Jump();
        //        //    Level1Screen.grappleCounter = 0;
        //        //}
        //        //else if ((Level1Screen.spaceDown || Level1Screen.upArrowDown) && o.obstacleRect.IntersectsWith(Level1Screen.playerRec) == false)
        //        //{
        //        //    Level1Screen.grappleOn = false;
        //        //}
        //    }
        //}

        //private void Level1Screen_MouseClick(object sender, MouseEventArgs e)
        //{
        //    clickCounter++;
        //    if (clickCounter % 2 == 0)
        //    {
        //        grappleOn = false;
        //        clickPoint = new Point(0, 0);
        //        player.y += 10;
        //    }
        //    else
        //    {
        //        grappleOn = true;
        //        clickPoint = e.Location;
        //        player.y -= 10;

        //    }
        //}
    }
}
