using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwingingDemo
{
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
            Cursor.Show();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Call first level and load it, then change screen and start the gameStopwatch
            Level1Screen.level = 1;
            Level1Screen.GameSetup();
            Form1.ChangeScreen(this, new Level1Screen());
            Level1Screen.gameStopwatch.Start();
        }

        private void tutorialButton_Click(object sender, EventArgs e)
        {
            //Call tutorial
            //Tutorial is level 0
            Level1Screen.level = 0;
            Form1.ChangeScreen(this, new Level1Screen());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //end program
            Application.Exit();
        }
    }
}
