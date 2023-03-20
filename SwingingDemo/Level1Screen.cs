using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace SwingingDemo
{
    public partial class Level1Screen : UserControl
    {
        //SolidBrush pinkBrush = new SolidBrush(Color.FromArgb(240, 199, 253, 255));
        SolidBrush clearBlueBrush = new SolidBrush(Color.FromArgb(105, 246, 225, 255));
        SolidBrush pinkBrush = new SolidBrush(Color.FromArgb(255, 247, 110, 255));
        SolidBrush blueBrush = new SolidBrush(Color.FromArgb(255, 103, 251, 243));
        SolidBrush greenBrush = new SolidBrush(Color.FromArgb(255, 148, 254, 121));

        Pen tealPen = new Pen(Color.Teal, 5);

        Bitmap image;
        Bitmap rightFrame1, rightFrame2, rightFrame3, rightFrame4, rightFrame5, rightFrame6;
        Bitmap leftFrame1, leftFrame2, leftFrame3, leftFrame4, leftFrame5, leftFrame6;
        Bitmap flipframe1, flipframe2, flipframe3, flipframe4;

        List<Bitmap> rightRunningFrames = new List<Bitmap>();
        List<Bitmap> leftRunningFrames = new List<Bitmap>();
        List<Bitmap> flipFrames = new List<Bitmap>();

        public static List<Obstacle> obstacles = new List<Obstacle>();
        public static List<SpringBoard> springboards = new List<SpringBoard>();
        public static List<Checkpoint> checkpoints = new List<Checkpoint>();

        Obstacle wall;
        Obstacle obstacleObject = new Obstacle(1, 1, 1, 1);
        SpringBoard springBoardObject = new SpringBoard(1, 1);
        Checkpoint checkpointObject = new Checkpoint(1,1);

        public static Random randGen = new Random();
        public static Stopwatch stopwatch = new Stopwatch();
        Stopwatch animationStopwatch = new Stopwatch();
        Stopwatch springboardStopwatch = new Stopwatch();

        public static Player player;
        public static Rectangle playerRec;
        public static Point clickPoint;
        public static Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown, spaceDown, aKeyDown, dkeyDown, sKeyDown, wKeyDown, isDrawing, grappleOn, hasIntersected, grappleToggle, isDead, isJump, isSprung, enterKeyDown;

        public static int lives = 5;
        public static int level, tutorialCounter, grappleCounter = 0;
        public static int drawPoint = -100;
        int animationTicker, animationCounter = 0;
        int buildingSpeed = 10;

        public static Bitmap hero = new Bitmap(Properties.Resources._41);

        public Level1Screen()
        {
            InitializeComponent();
            ListInitlialization();
            GameSetup();
        }

        public void ListInitlialization()
        {
            image = new Bitmap(Properties.Resources.City_Scape);

            rightFrame1 = new Bitmap(Properties.Resources._0);
            rightFrame2 = new Bitmap(Properties.Resources._1);
            rightFrame3 = new Bitmap(Properties.Resources._2);
            rightFrame4 = new Bitmap(Properties.Resources._3);
            rightFrame5 = new Bitmap(Properties.Resources._4);
            rightFrame6 = new Bitmap(Properties.Resources._5);

            leftFrame1 = new Bitmap(Properties.Resources._0);
            leftFrame2 = new Bitmap(Properties.Resources._1);
            leftFrame3 = new Bitmap(Properties.Resources._2);
            leftFrame4 = new Bitmap(Properties.Resources._3);
            leftFrame5 = new Bitmap(Properties.Resources._4);
            leftFrame6 = new Bitmap(Properties.Resources._5);

            rightRunningFrames.Add(rightFrame1);
            rightRunningFrames.Add(rightFrame2);
            rightRunningFrames.Add(rightFrame3);
            rightRunningFrames.Add(rightFrame4);
            rightRunningFrames.Add(rightFrame5);
            rightRunningFrames.Add(rightFrame6);

            leftFrame1.RotateFlip(RotateFlipType.RotateNoneFlipX);
            leftFrame2.RotateFlip(RotateFlipType.RotateNoneFlipX);
            leftFrame3.RotateFlip(RotateFlipType.RotateNoneFlipX);
            leftFrame4.RotateFlip(RotateFlipType.RotateNoneFlipX);
            leftFrame5.RotateFlip(RotateFlipType.RotateNoneFlipX);
            leftFrame6.RotateFlip(RotateFlipType.RotateNoneFlipX);

            leftRunningFrames.Add(leftFrame1);
            leftRunningFrames.Add(leftFrame2);
            leftRunningFrames.Add(leftFrame3);
            leftRunningFrames.Add(leftFrame4);
            leftRunningFrames.Add(leftFrame5);
            leftRunningFrames.Add(leftFrame6);

            flipframe1 = new Bitmap(Properties.Resources._29);
            flipframe2 = new Bitmap(Properties.Resources._30);
            flipframe3 = new Bitmap(Properties.Resources._31);
            flipframe4 = new Bitmap(Properties.Resources._32);

            flipFrames.Add(flipframe1);
            flipFrames.Add(flipframe2);
            flipFrames.Add(flipframe3);
            flipFrames.Add(flipframe4);
        }
        public static void GameSetup()
        {
            player = new Player(25, 0);
            obstacles.Clear();
            checkpoints.Clear();
            springboards.Clear();

            MakeLevel(level);
            stopwatch.Start();
        }

        public static void MakeLevel(int _level)
        {
            if(_level == 0)
            {
                player.x = 250;
                player.y = -50;

                MakeObstacle(-100, 300, 1300, 100);
                MakeCheckpoint(850, 200);
                MakeCheckpoint(1195, 200);
                MakeObstacle(1380, 300, 300, 100);
                MakeCheckpoint(1800, 125);
                MakeObstacle(2200, 600, 300, 100);
                MakeSpringBoard(2600, 600);
                MakeObstacle(2800, 200, 300, 1000);
                MakeCheckpoint(3080, 100);
            }
            else if (_level == 1)
            {
                MakeObstacle(-50, 200, 600, 100);
                MakeObstacle(750, 200, 100, 800);
                MakeObstacle(1150, 500, 100, 800);
                MakeObstacle(1350, 500, 100, 800);
                MakeObstacle(1550, 400, 100, 800);
                MakeObstacle(1800, 300, 100, 800);
                MakeSpringBoard(2200, 500);
                MakeObstacle(2550, 300, 100, 800);
                MakeObstacle(3300, 500, 100, 800);
                MakeCheckpoint(3330, 400);
            }
            else if (_level == 2)
            {
                MakeObstacle(-50, 200, 700, 100);
                MakeObstacle(900, 200, 100, 300);
                MakeObstacle(700, 100, 100, 100);
                MakeObstacle(700, 300, 100, 100);
                MakeObstacle(700, 500, 100, 100);
                MakeObstacle(700, 500, 100, 100);
                MakeObstacle(1000, 500, 100, 100);
                MakeObstacle(1200, 500, 100, 100);
                MakeObstacle(1500, 500, 100, 100);
                MakeObstacle(1800, 500, 100, 100);
                MakeObstacle(2100, 500, 100, 100);
                MakeCheckpoint(2180, 400);
            }
            else if (_level == 3)
            {
                MakeObstacle(-50, 400, 400, 100);
                MakeObstacle(400, 200, 100, 300);
                MakeObstacle(700, 100, 100, 100);
                MakeObstacle(700, 200, 100, 100);
                MakeObstacle(700, 500, 600, 100);
            }
        }

        public static void MakeObstacle(int x, int y, int width, int height)
        {
            Obstacle obstacle = new Obstacle(x, y, width, height);
            obstacles.Add(obstacle);
        }

        public static void MakeSpringBoard(int x, int y)
        {
            SpringBoard springboard = new SpringBoard(x, y);
            springboards.Add(springboard);
        }

        public static void MakeCheckpoint(int x, int y)
        {
            Checkpoint checkpoint = new Checkpoint(x, y);
            checkpoints.Add(checkpoint);
        }

        private void tutorialTimer_Tick(object sender, EventArgs e)
        {
            if (level == 0)
            {
                label1.Visible = true;

                if (tutorialCounter == 0)
                {
                    label1.Text = "Press Space To Start";
                    level1GameEngine.Stop();

                    if (spaceDown)
                    {
                        TutorialCheckpointChange();
                    }
                }

                if(tutorialCounter == 1 && playerRec.IntersectsWith(obstacles[0].topRect))
                {
                    label1.Text = "Press A and D Keys to Run To The Checkpoint";
                    aKeyLabel.Visible = true;
                    dKeyLabel.Visible = true;
                    level1GameEngine.Stop();

                    if (aKeyDown || dkeyDown)
                    {
                        TutorialCheckpointChange();
                        label1.Location = new Point(320, 15);
                    }
                }

                if(tutorialCounter == 2 && playerRec.IntersectsWith(checkpoints[0].checkpointRect))
                {
                    label1.Text = "Checkpoints Will Progress The Level";
                    label1.Text += "\n\nPress Enter To Continue";
                    level1GameEngine.Stop();

                    if (enterKeyDown)
                    {
                        TutorialCheckpointChange();
                        dKeyLabel.Location = new Point(dKeyLabel.Location.X + 125, 260);
                        label1.Location = new Point(320, 100);

                    }
                }

                if(tutorialCounter == 3)
                {
                    label1.Text = "Run Towards The Ledge";
                    dKeyLabel.Visible = true;
                    level1GameEngine.Stop();

                    if (dkeyDown)
                    {
                        TutorialCheckpointChange();
                        spaceKeyLabel.Location = new Point(spaceKeyLabel.Location.X + 300, 175);
                    }
                }

                if (tutorialCounter == 4 && playerRec.IntersectsWith(checkpoints[1].checkpointRect))
                {
                    spaceKeyLabel.Visible = true;
                    dkeyDown = true;
                    label1.Text = "Press Space To Jump";
                    level1GameEngine.Stop();

                    if (spaceDown)
                    {
                        TutorialCheckpointChange();
                        label1.Location = new Point(420, 25);
                    }
                }

                if (tutorialCounter == 5 && playerRec.IntersectsWith(obstacles[1].topRect))
                {
                    dkeyDown = false;
                    label1.Text = "Good Job\n\nJump To Traverse Vertically\n\nSpace To Continue";
                    level1GameEngine.Stop();

                    if (spaceDown)
                    {
                        TutorialCheckpointChange();
                        spaceKeyLabel.Location = new Point(spaceKeyLabel.Location.X + 275, 175);
                        dKeyLabel.Location = new Point(dKeyLabel.Location.X + 175, 260);
                        label1.Location = new Point(770, 25);
                    }
                }

                if (tutorialCounter == 6)
                {
                    label1.Text = "Run And Jump To Next Checkpoint";
                    dKeyLabel.Visible = true;
                    spaceKeyLabel.Visible = true;
                    level1GameEngine.Stop();

                    if (dkeyDown)
                    {
                        TutorialCheckpointChange();
                        dKeyLabel.Location = new Point(dKeyLabel.Location.X + 100, 260);
                    }
                }

                if (tutorialCounter == 7 && playerRec.IntersectsWith(checkpoints[2].checkpointRect))
                {
                    dKeyLabel.Text = "-->";
                    dKeyLabel.Visible = true;
                    label1.Text = "You Aren't Going To Make It!!!";
                    label1.Text += "\n\nPress The Right Arrow Key To Swing";

                    level1GameEngine.Stop();

                    if (rightArrowDown)
                    {
                        TutorialCheckpointChange();
                        label1.Location = new Point(650, 150);
                    }
                }

                if (tutorialCounter == 8 && playerRec.IntersectsWith(obstacles[2].topRect))
                {
                    label1.Text = "You Made It!";
                    label1.Text += "\n\nJump On The Pink Platform";
                    level1GameEngine.Stop();

                    if (dkeyDown)
                    {
                        TutorialCheckpointChange();
                        label1.Location = new Point(675, 75);
                    }

                }

                if (tutorialCounter == 9 && playerRec.IntersectsWith(springboards[0].springboardRect))
                {
                    dkeyDown = true;
                    label1.Text = "A SpringBoard Will Launch You In The Air";
                    label1.Text += "\n\nPress Enter To Continue";

                    level1GameEngine.Stop();

                    if (enterKeyDown)
                    {
                        TutorialCheckpointChange();
                        label1.Location = new Point(625, 50);
                    }
                }

                if (tutorialCounter == 10 && playerRec.IntersectsWith(checkpoints[3].checkpointRect))
                {
                    dkeyDown = false;
                    label1.Text = "Tutorial Done";
                    label1.Text += "\n\nPress Enter To Go To Level 1";

                    level1GameEngine.Stop();

                    if (enterKeyDown)
                    {
                        level1GameEngine.Start();
                        tutorialCounter = 11;
                        label1.Text = "";
                    }
                }

                if (tutorialCounter == 11)
                {
                    level = 1;
                    GameSetup();
                }
            }
        }

        public void TutorialCheckpointChange()
        {
            tutorialCounter++;
            level1GameEngine.Start();
            dKeyLabel.Visible = false;
            aKeyLabel.Visible = false;
            spaceKeyLabel.Visible = false;
            label1.Text = "";
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (isJump)
            {
                animationCounter++;
                if (animationCounter == 4)
                {
                    animationCounter = 0;
                }

                hero = new Bitmap(flipFrames[animationCounter]);
            }
            else if (rightArrowDown || dkeyDown)
            {
                animationTicker++;
                if (animationTicker == 6)
                {
                    animationTicker = 0;
                }

                hero = new Bitmap(rightRunningFrames[animationTicker]);
            }
            else if (leftArrowDown || aKeyDown)
            {
                animationTicker++;
                if (animationTicker == 6)
                {
                    animationTicker = 0;
                }

                hero = new Bitmap(leftRunningFrames[animationTicker]);
            }
            //else
            //{
            //    hero = new Bitmap(Properties.Resources._38);
            //}

            if (grappleOn)
            {
                hero = new Bitmap(Properties.Resources._18);
            }
        }

        private void level1GameEngine_Tick(object sender, EventArgs e)
        {
            playerRec = new Rectangle(player.x, player.y, player.width, player.height);
            player.ySpeed += 4;

            if(level == 0)
            {
                tutorialTimer.Enabled = true;
            }

            if ((rightArrowDown || dkeyDown) && player.x < this.Width - player.width)
            {
                player.Move("right", this.Width, this.Height);
                player.MoveBackgroundScreenAndObjects(buildingSpeed, "right");
            }

            if ((leftArrowDown || aKeyDown) && player.x > 0)
            {
                player.Move("left", this.Width, this.Height);
                player.MoveBackgroundScreenAndObjects(buildingSpeed, "left");
            }

            grappleToggle = true;

            //foreach (Obstacle o in obstacles)
            //{
            //    #region Directional Intersections

            //    if (o.topRect.IntersectsWith(playerRec))
            //    {
            //        hasIntersected = true;
            //        player.y = o.y - 29;
            //        player.ySpeed = 0;
            //        grappleToggle = false;

            //        int rand = randGen.Next(0, 2);

            //        if (isJump)
            //        {
            //            if (rand == 1)
            //            {
            //                hero = new Bitmap(Properties.Resources._33);
            //            }
            //            else
            //            {
            //                hero = new Bitmap(Properties.Resources._37);
            //            }
            //            isJump = false;
            //        }
            //        else if (player.ySpeed > 10)
            //        {
            //            if (rand == 1)
            //            {
            //                hero = new Bitmap(Properties.Resources._33);
            //            }
            //            else
            //            {
            //                hero = new Bitmap(Properties.Resources._37);
            //                player.y += 5;
            //            }
            //        }
            //    }
            //    else if (!hasIntersected)
            //    {
            //        grappleToggle = true;
            //    }

            //    if (o.bottomRec.IntersectsWith(playerRec))
            //    {
            //        player.y = o.bottomRec.Y + 10;
            //        player.ySpeed = 0;
            //    }

            //    if (o.leftRec.IntersectsWith(playerRec))
            //    {
            //        player.x = o.leftRec.X - player.width;
            //        player.xSpeed = 0;
            //    }

            //    if (o.rightRec.IntersectsWith(playerRec))
            //    {
            //        player.x = o.rightRec.X + player.width - 10;
            //        player.xSpeed = 0;
            //    }
            //    #endregion

            //    #region Grapple Logic
            //    //if (rightArrowDown && grappleToggle && (o.topRect.IntersectsWith(playerRec) == false))
            //    //{
            //    //    if (grappleOn)
            //    //    {

            //    //    }
            //    //    else
            //    //    {
            //    //        grappleOn = true;
            //    //        clickPoint = new Point(player.x + 150, player.y - 150);
            //    //    }

            //    //}

            //    //if (leftArrowDown && grappleToggle && o.topRect.IntersectsWith(playerRec) == false)
            //    //{
            //    //    if (grappleOn)
            //    //    {

            //    //    }
            //    //    else
            //    //    {
            //    //        grappleOn = true;
            //    //        clickPoint = new Point(player.x - 150, player.y - 150);
            //    //    }

            //    //}
            //    #endregion

            //    #region Jump
            //    //if ((spaceDown || upArrowDown) && o.topRect.IntersectsWith(playerRec))
            //    //{
            //    //    player.Jump();
            //    //    grappleCounter = 0;
            //    //}
            //    //else if ((spaceDown || upArrowDown) && o.obstacleRect.IntersectsWith(playerRec) == false)
            //    //{
            //    //    grappleOn = false;
            //    //}
            //    #endregion
            //}

            player.CheckPlayerIntersections();
            springBoardObject.IsSpringBoardTouched();
            checkpointObject.IsCheckpointTouched();

            player.CheckGrapple();
            player.ApplySpeed();
            player.ApplyFriction();

            if (player.y > this.Height)
            {
                stopwatch.Reset();
                stopwatch.Start();
                player.Respawn(stopwatch);
                RespawnAnimation();
            }

            if (stopwatch.ElapsedMilliseconds > 1400)
            {
                isDead = false;
            }
            else
            {
                player.xSpeed = 0;
                grappleOn = false;
                grappleToggle = false;
            }

            if(level == 4)
            {
                Form1.ChangeScreen(this, new MenuScreen());
                level1GameEngine.Stop();
                aKeyDown = false;
                dkeyDown = false;
            }

            Refresh();
        }

        public void RespawnAnimation()
        {
            wall = new Obstacle(270, 0, 30, this.Height);
            obstacles.Add(wall);
        }

        private void Level1Screen_Paint(object sender, PaintEventArgs e)
        {
            if (isDead && level != 0)
            {

            }
            else
            {
                obstacles.Remove(wall);
            }

            e.Graphics.DrawImage(image, drawPoint, 0);

            if (grappleOn)
            {
                e.Graphics.DrawLine(tealPen, player.x + 10, player.y, clickPoint.X, clickPoint.Y);
            }

            foreach (Obstacle o in obstacles)
            {
                e.Graphics.FillRectangle(clearBlueBrush, o.obstacleRect);
            }

            foreach (Obstacle o in obstacles)
            {
                e.Graphics.FillRectangle(blueBrush, o.leftRec);
                e.Graphics.FillRectangle(blueBrush, o.rightRec);
                e.Graphics.FillRectangle(blueBrush, o.topRect);
                e.Graphics.FillRectangle(blueBrush, o.bottomRec);
            }

            foreach (SpringBoard s in springboards)
            {
                e.Graphics.FillRectangle(pinkBrush, s.springboardRect);
            }

            foreach (Checkpoint c in checkpoints)
            {
                e.Graphics.FillRectangle(greenBrush, c.checkpointRect);
            }

            e.Graphics.DrawImage(hero, player.x, player.y - 7);
        }

        private void Level1Screen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.D:
                    dkeyDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Enter:
                    enterKeyDown = true;
                    break;
            }
        }

        private void Level1Screen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    grappleCounter = 0;
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    grappleCounter = 0;
                    rightArrowDown = false;
                    break;
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.D:
                    dkeyDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Enter:
                    enterKeyDown = false;
                    break;
            }
        }
    }
}
