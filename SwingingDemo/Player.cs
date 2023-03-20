using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace SwingingDemo
{
    public class Player
    {
        public int x, y;
        public int speed = 3;
        public double ySpeed = 0;
        public double xSpeed = 0;
        public int width = 30;
        public int height = 40;
        public static double friction = 0.85;

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void Respawn(Stopwatch _stopwatch)
        {
            Level1Screen.isDead = true;

            if(Level1Screen.level == 0)
            {
                x = 250;
                y = -50;
                Level1Screen.tutorialCounter = 0;
            }
            else
            {
                x = 25;
                y = -50;
            }
           

            Level1Screen.grappleOn = false;
            Level1Screen.drawPoint = -100;

            Level1Screen.obstacles.Clear();
            Level1Screen.springboards.Clear();
            Level1Screen.checkpoints.Clear();
            Level1Screen.MakeLevel(Level1Screen.level);
        }

        public void MoveBackgroundScreenAndObjects(int speed, string scrollDirection)
        {
            if (Level1Screen.stopwatch.ElapsedMilliseconds < 1000 && Level1Screen.stopwatch.IsRunning)
            {

            }
            else
            {
                if (scrollDirection == "right")
                {
                    Level1Screen.drawPoint--;
                    foreach (Obstacle o in Level1Screen.obstacles)
                    {
                        o.topRect.X -= speed;
                        o.bottomRec.X -= speed;
                        o.leftRec.X -= speed;
                        o.rightRec.X -= speed;
                        o.obstacleRect.X -= speed;
                    }

                    foreach(SpringBoard s in Level1Screen.springboards)
                    {
                        s.springboardRect.X -= speed;
                    }

                    foreach (Checkpoint c in Level1Screen.checkpoints)
                    {
                        c.checkpointRect.X -= speed;
                    }
                }
                else if (scrollDirection == "left")
                {
                    Level1Screen.drawPoint++;
                    foreach (Obstacle o in Level1Screen.obstacles)
                    {
                        o.topRect.X += speed;
                        o.bottomRec.X += speed;
                        o.leftRec.X += speed;
                        o.rightRec.X += speed;
                        o.obstacleRect.X += speed;
                    }

                    foreach (SpringBoard s in Level1Screen.springboards)
                    {
                        s.springboardRect.X += speed;
                    }

                    foreach (Checkpoint c in Level1Screen.checkpoints)
                    {
                        c.checkpointRect.X += speed;
                    }
                }
            }

           
           
        }

        public void Move(string direction, int screenWidth, int screenHeight)
        {
            if (Level1Screen.stopwatch.ElapsedMilliseconds < 1000 && Level1Screen.stopwatch.IsRunning)
            {

            }
            else
            {
                if (direction == "left")
                {
                    if (x < 0)
                    {
                        x = 5;
                    }
                    else
                    {
                        x -= speed;
                    }

                }
                if (direction == "right")
                {
                    if (x > screenWidth - width)
                    {
                        x = screenWidth - width;
                    }
                    else
                    {
                        x += speed;
                    }

                }
            }
        }

        public void Jump()
        {
            ySpeed = -45;
        }

        public void ApplySpeed()
        {
            
            y += (int)ySpeed;
            x += (int)xSpeed;
        }

        public void PullTowards()
        {
            xSpeed += (Level1Screen.clickPoint.X - x) / 40;
            ySpeed += (Level1Screen.clickPoint.Y - y) / 32;
        }

        public void ApplyFriction()
        {
            ySpeed *= friction;
            xSpeed *= friction;

        }

        public void CheckGrapple()
        {
            if (Level1Screen.grappleOn)
            {
                Level1Screen.grappleCounter++;
            }

            if (Level1Screen.grappleCounter > 20)
            {
                Level1Screen.grappleOn = false;
            }

            if (Level1Screen.grappleOn)
            {
                PullTowards();
            }
        }

        public void CheckPlayerIntersections()
        {
            foreach (Obstacle o in Level1Screen.obstacles)
            {
                #region Directional Intersections

                if (o.topRect.IntersectsWith(Level1Screen.playerRec))
                {
                    Level1Screen.hasIntersected = true;
                    y = o.y - 29;
                    ySpeed = 0;
                    Level1Screen.grappleToggle = false;

                    int rand = Level1Screen.randGen.Next(0, 2);

                    if (Level1Screen.isJump)
                    {
                        if (rand == 1)
                        {
                            Level1Screen.hero = new Bitmap(Properties.Resources._33);
                        }
                        else
                        {
                            Level1Screen.hero = new Bitmap(Properties.Resources._37);
                        }
                        Level1Screen.isJump = false;
                    }
                    else if (Level1Screen.player.ySpeed > 10)
                    {
                        if (rand == 1)
                        {
                            Level1Screen.hero = new Bitmap(Properties.Resources._33);
                        }
                        else
                        {
                            Level1Screen.hero = new Bitmap(Properties.Resources._37);
                            Level1Screen.player.y += 5;
                        }
                    }
                }
                else if (!Level1Screen.hasIntersected)
                {
                    Level1Screen.grappleToggle = true;
                }

                if (o.bottomRec.IntersectsWith(Level1Screen.playerRec))
                {
                    y = o.bottomRec.Y + 10;
                    ySpeed = 0;
                }

                if (o.leftRec.IntersectsWith(Level1Screen.playerRec))
                {
                    x = o.leftRec.X - width;
                    xSpeed = 0;
                }

                if (o.rightRec.IntersectsWith(Level1Screen.playerRec))
                {
                    x = o.rightRec.X + width - 10;
                    xSpeed = 0;
                }
                #endregion

                #region Grapple Logic
                if (Level1Screen.rightArrowDown && Level1Screen.grappleToggle && o.topRect.IntersectsWith(Level1Screen.playerRec) == false)
                {
                    if (Level1Screen.grappleOn)
                    {

                    }
                    else
                    {
                        Level1Screen.grappleOn = true;
                        Level1Screen.clickPoint = new Point(x + 150, y - 150);
                    }

                }

                if (Level1Screen.leftArrowDown && Level1Screen.grappleToggle && o.topRect.IntersectsWith(Level1Screen.playerRec) == false)
                {
                    if (Level1Screen.grappleOn)
                    {

                    }
                    else
                    {
                        Level1Screen.grappleOn = true;
                        Level1Screen.clickPoint = new Point(x - 150, y - 150);
                    }

                }
                #endregion

                #region Jump
                if ((Level1Screen.spaceDown || Level1Screen.upArrowDown) && o.topRect.IntersectsWith(Level1Screen.playerRec))
                {
                    Level1Screen.isJump = true;
                    Jump();
                    Level1Screen.grappleCounter = 0;
                }
                else if ((Level1Screen.spaceDown || Level1Screen.upArrowDown) && o.obstacleRect.IntersectsWith(Level1Screen.playerRec) == false)
                {
                    Level1Screen.grappleOn = false;
                }
                #endregion
            }

        }
    }
}
