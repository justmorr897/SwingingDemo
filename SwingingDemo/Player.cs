using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Media;

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

        SoundPlayer thwipPlayer = new SoundPlayer(Properties.Resources.thwip);
        SoundPlayer hitPlayer = new SoundPlayer(Properties.Resources.hit);

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void Respawn(Stopwatch _stopwatch)
        {
            //Respawn function called after player dies
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
           
            //reset the level, background image, and clear the lists
            Level1Screen.grappleOn = false;
            Level1Screen.drawPoint = -100;
            Level1Screen.obstacles.Clear();
            Level1Screen.springboards.Clear();
            Level1Screen.checkpoints.Clear();
            Level1Screen.MakeLevel(Level1Screen.level);
        }

        public void MoveBackgroundScreenAndObjects(int speed, string scrollDirection)
        {
            //Move the background image and all images on the screen while player is running
            if (Level1Screen.deathStopwatch.ElapsedMilliseconds < 1000 && Level1Screen.deathStopwatch.IsRunning)
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
            //Movement code with boundaries 
            if (Level1Screen.deathStopwatch.ElapsedMilliseconds < 1000 && Level1Screen.deathStopwatch.IsRunning)
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
            //Add a bunch of negative to the player's ySpeed to simulate a jump
            //Gravity will reduce this until back at ground with 0
            ySpeed = -50;
        }

        public void ApplySpeed()
        {
            //Apply speeds to the x and y position
            y += (int)ySpeed;
            x += (int)xSpeed;
        }

        public void PullTowards()
        {
            //If player is swinging, pull the player towards the endpoint of the rope
            //Makes a semicircular swing
            xSpeed += (Level1Screen.clickPoint.X - x) / 40;
            ySpeed += (Level1Screen.clickPoint.Y - y) / 32;
        }

        public void ApplyFriction()
        {
            //If there was no friction the speeds would be insane
            ySpeed *= friction;
            xSpeed *= friction;
        }

        public void CheckGrapple()
        {
            //Counter adds as player is swinging, to prevent someone from staying on a rope for too long
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
            //Check intersections with every obstacle
            foreach (Obstacle o in Level1Screen.obstacles)
            {
                #region Directional Intersections

                if (o.topRect.IntersectsWith(Level1Screen.playerRec))
                {
                    Level1Screen.hasIntersected = true;
                    //Displace player y so they 'stand' on the floor
                    y = o.y - 29;
                    Level1Screen.grappleToggle = false;

                    int rand = Level1Screen.randGen.Next(0, 2);

                    //Change player sprite after landing
                    //Make it random
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
                    if (Level1Screen.player.ySpeed > 25)
                    {
                        //If player falls from high place play hit sound
                        hitPlayer.Play();
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
                    ySpeed = 0;
                }
                else if (!Level1Screen.hasIntersected)
                {
                    Level1Screen.grappleToggle = true;
                }

                //If player interescting with other sides of objects offset player position based on which side it is
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
                        //If this wasn't here it would make a new grapple for every frame the arrow key is down
                    }
                    else
                    {
                        Level1Screen.grappleOn = true;

                        //Make a grapple point above and to the right of the player
                        Level1Screen.clickPoint = new Point(x + 150, y - 150);
                        //thwipPlayer.Play();
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
                        
                        //Make a grapple point above and to the left of the player
                        Level1Screen.clickPoint = new Point(x - 150, y - 150);
                        //thwipPlayer.Play();
                    }
                }
                #endregion

                #region Jump
                //If the player is intersecting with the floor and space is pressed, Jump is called
                //If not, the swing is ended for the player
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
