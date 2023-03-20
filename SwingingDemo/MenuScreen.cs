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
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Level1Screen.level = 1;
            Form1.ChangeScreen(this, new Level1Screen());
        }

        private void tutorialButton_Click(object sender, EventArgs e)
        {
            Level1Screen.level = 0;
            Form1.ChangeScreen(this, new Level1Screen());

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
