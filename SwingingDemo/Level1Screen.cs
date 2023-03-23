using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Media;

namespace SwingingDemo
{
    public partial class Level1Screen : UserControl
    {
        #region Brushes and Pens
        SolidBrush clearBlueBrush = new SolidBrush(Color.FromArgb(105, 246, 225, 255));
        SolidBrush pinkBrush = new SolidBrush(Color.FromArgb(255, 247, 110, 255));
        SolidBrush blueBrush = new SolidBrush(Color.FromArgb(255, 103, 251, 243));
        SolidBrush greenBrush = new SolidBrush(Color.FromArgb(255, 148, 254, 121));
        SolidBrush orangeBrush = new SolidBrush(Color.FromArgb(255, 249, 77, 81));

        Pen tealPen = new Pen(Color.Teal, 5);
        #endregion

        #region Bitmaps
        Bitmap image;
        Bitmap rightFrame1, rightFrame2, rightFrame3, rightFrame4, rightFrame5, rightFrame6;
        Bitmap leftFrame1, leftFrame2, leftFrame3, leftFrame4, leftFrame5, leftFrame6;
        Bitmap flipframe1, flipframe2, flipframe3, flipframe4;
        public static Bitmap hero = new Bitmap(Properties.Resources._41);
        #endregion

        #region Lists
        List<Bitmap> rightRunningFrames = new List<Bitmap>();
        List<Bitmap> leftRunningFrames = new List<Bitmap>();
        List<Bitmap> flipFrames = new List<Bitmap>();
        List<SolidBrush> brushes = new List<SolidBrush>();
        public static List<Obstacle> obstacles = new List<Obstacle>();
        public static List<SpringBoard> springboards = new List<SpringBoard>();
        public static List<Checkpoint> checkpoints = new List<Checkpoint>();
        #endregion

        #region Class Objects to reference later
        Obstacle wall;
        Obstacle obstacleObject = new Obstacle(1, 1, 1, 1);
        SpringBoard springBoardObject = new SpringBoard(1, 1);
        Checkpoint checkpointObject = new Checkpoint(1, 1);

        SoundPlayer runSoundPlayer = new SoundPlayer(Properties.Resources.run); 
        #endregion

        #region Stopwatches
        public static Stopwatch deathStopwatch = new Stopwatch();
        public static Stopwatch gameStopwatch = new Stopwatch();
        #endregion

        #region Other Public Static Things
        public static Random randGen = new Random();

        public static Player player;
        public static Rectangle playerRec;
        public static Point clickPoint;
        public static Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown, spaceDown, aKeyDown, dkeyDown, grappleOn, hasIntersected, grappleToggle, isDead, isJump, isSprung, enterKeyDown;

        public static int level, deaths, tutorialCounter, grappleCounter = 0;
        public static int drawPoint = -100;

        public static double gameStopwatchValue;
        #endregion

        int animationTicker, animationCounter = 0;
        int buildingSpeed = 10;

        public Level1Screen()
        {
            InitializeComponent();
            ListInitlialization();
            GameSetup();
            //Hiding the cursor
            Cursor.Hide();
        }

        public void ListInitlialization()
        {
            image = new Bitmap(Properties.Resources.City_Scape);
            
            brushes.Add(blueBrush);
            brushes.Add(blueBrush);
            brushes.Add(greenBrush);
            brushes.Add(pinkBrush);
            brushes.Add(orangeBrush);

            //Adding running animation frames to lists
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

            //Adding flip animation frames to list
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
            //Function Run on Level Load
            //Resets player location
            //Clears Object Lists
            player = new Player(25, 0);
            obstacles.Clear();
            checkpoints.Clear();
            springboards.Clear();

            //Creates the New level
            MakeLevel(level);

            deathStopwatch.Reset();
            deathStopwatch.Start();
            gameStopwatch.Start();

            //Reset Drawpoint to reset background image
            drawPoint = -100;
        }

        public static void MakeLevel(int _level)
        {
            //Level 0 is the tutorial
            if(_level == 0)
            {
                player.x = 250;
                player.y = -50;

                #region Make Tutorial
                MakeObstacle(-100, 300, 1300, 100);
                MakeCheckpoint(850, 200);
                MakeCheckpoint(1195, 200);
                MakeObstacle(1380, 300, 300, 100);
                MakeCheckpoint(1800, 125);
                MakeObstacle(2200, 600, 300, 100);
                MakeSpringBoard(2600, 600);
                MakeObstacle(2800, 200, 300, 1000);
                MakeCheckpoint(3080, 100); 
                #endregion
            }
            else if (_level == 1)
            {
                #region Make Level 1
                MakeObstacle(-50, 200, 600, 100);
                MakeObstacle(750, 200, 100, 800);
                MakeObstacle(1150, 500, 100, 800);
                MakeObstacle(1350, 500, 100, 800);
                MakeObstacle(1550, 400, 100, 800);
                MakeObstacle(1800, 300, 100, 800);
                MakeSpringBoard(2150, 500);
                MakeObstacle(2450, 300, 100, 800);
                MakeObstacle(3150, 500, 100, 800);
                MakeCheckpoint(3180, 400); 
                #endregion
            }
            else if (_level == 2)
            {
                #region Make Level 2
                MakeObstacle(-50, 200, 700, 100);
                MakeObstacle(700, 300, 100, 100);
                MakeObstacle(900, 200, 100, 300);
                MakeObstacle(1200, 500, 100, 100);
                MakeObstacle(1500, 500, 100, 100);
                MakeObstacle(1800, 500, 100, 100);
                MakeObstacle(2100, 500, 100, 100);
                MakeCheckpoint(2180, 400); 
                #endregion
            }
            else if (_level == 3)
            {
                #region Make Level 3
                MakeObstacle(-50, 400, 400, 100);
                MakeObstacle(400, 300, 100, 300);
                MakeObstacle(700, 100, 100, 300);
                MakeObstacle(700, 500, 600, 100);
                MakeCheckpoint(1280, 400); 
                #endregion
            }
        }

        public static void MakeObstacle(int x, int y, int width, int height)
        {
            //Function for making a new obstacle and adding it to list
            Obstacle obstacle = new Obstacle(x, y, width, height);
            obstacles.Add(obstacle);
        }

        public static void MakeSpringBoard(int x, int y)
        {
            //Function for making a new springboard and adding it to list
            SpringBoard springboard = new SpringBoard(x, y);
            springboards.Add(springboard);
        }

        public static void MakeCheckpoint(int x, int y)
        {
            //Function for making a new checkpoint and adding it to list
            Checkpoint checkpoint = new Checkpoint(x, y);
            checkpoints.Add(checkpoint);
        }

        private void tutorialTimer_Tick(object sender, EventArgs e)
        {
            //Code for tutorial
            //Tutorial Counter keeps track of progress in tutorial
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
            //Function to reduce lines in tutorial code
            tutorialCounter++;
            level1GameEngine.Start();
            dKeyLabel.Visible = false;
            aKeyLabel.Visible = false;
            spaceKeyLabel.Visible = false;
            label1.Text = "";
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            //This timer is for the animation cycle of the player character
            //If player is jumping go through jump animation frames
            if (isJump)
            {
                animationCounter++;
                if (animationCounter == 4)
                {
                    //If at the last frame, begin the sequence again at the first frame
                    animationCounter = 0;
                }

                hero = new Bitmap(flipFrames[animationCounter]);
            }

            //If player is running go through running animation frames based on direction
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
            
            //If player is swinging change sprite to swinging sprite based on right or left
            if (grappleOn && player.xSpeed > 0)
            {
                hero = new Bitmap(Properties.Resources._18);
            }
            else if (grappleOn && player.xSpeed < 0)
            {
                hero = new Bitmap(Properties.Resources._18);
                hero.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }

        private void level1GameEngine_Tick(object sender, EventArgs e)
        {
            //make a rectangle based on the player's position and add 4 to the player ySpeed as 'gravity'
            playerRec = new Rectangle(player.x, player.y, player.width, player.height);
            player.ySpeed += 4;

            if(level == 0)
            {
                //If the tutorial button was clicked this will run and start the tutorial
                tutorialTimer.Enabled = true;
            }

            //Move left and right code, moves the background image and all objects on the screen with it
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

            //Check interesection with all objects
            player.CheckPlayerIntersections();
            springBoardObject.IsSpringBoardTouched();
            checkpointObject.IsCheckpointTouched();

            //Apply speeds to the player based on previous parameters
            player.CheckGrapple();
            player.ApplySpeed();
            player.ApplyFriction();

            //If player falls below bottom of screen they die
            if (player.y > this.Height)
            {
                deaths++;
                deathStopwatch.Reset();
                deathStopwatch.Start();
                player.Respawn(deathStopwatch);
            }

            //Stopwatch to prevent player from moving and swinging immediately after respawn
            if (deathStopwatch.ElapsedMilliseconds > 1400)
            {
                isDead = false;
            }
            else
            {
                player.xSpeed = 0;
                grappleOn = false;
                grappleToggle = false;
            }

            //Check if Game Over
            //If true, end gameStopwatch and store the value in a variable
            //Change screen to EndScreen
            //Stop game engine
            if(level == 4)
            {
                gameStopwatchValue = gameStopwatch.ElapsedMilliseconds;
                gameStopwatch.Stop();
                gameStopwatch.Reset();
                Form1.ChangeScreen(this, new EndScreen());
                level1GameEngine.Stop();
                aKeyDown = false;
                dkeyDown = false;
            }

            Refresh();
        }

        private void Level1Screen_Paint(object sender, PaintEventArgs e)
        {
            //Draw the background cityscape image at a new point Drawpoint as the player moves
            e.Graphics.DrawImage(image, drawPoint, 0);

            //If player is swinging draw the rope
            if (grappleOn)
            {
                e.Graphics.DrawLine(tealPen, player.x + 10, player.y, clickPoint.X, clickPoint.Y);
            }

            //Draw the whole rectangle with slightly clear color
            foreach (Obstacle o in obstacles)
            {
                e.Graphics.FillRectangle(clearBlueBrush, o.obstacleRect);
            }

            //Draw edges with color depending on what level it is
            foreach (Obstacle o in obstacles)
            {
                e.Graphics.FillRectangle(brushes[level], o.leftRec);
                e.Graphics.FillRectangle(brushes[level], o.rightRec);
                e.Graphics.FillRectangle(brushes[level], o.topRect);
                e.Graphics.FillRectangle(brushes[level], o.bottomRec);
            }

            //Draw all the springboards and checkpoints
            foreach (SpringBoard s in springboards)
            {
                e.Graphics.FillRectangle(pinkBrush, s.springboardRect);
            }

            foreach (Checkpoint c in checkpoints)
            {
                e.Graphics.FillRectangle(greenBrush, c.checkpointRect);
            }

            //Draw the image of the hero at the player's location
            //Some sprites have different heights which cause visual bugs
            e.Graphics.DrawImage(hero, player.x, player.y - 7);
        }

        private void Level1Screen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Check Key Down Inputs
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
            //Check Key Up Events
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
