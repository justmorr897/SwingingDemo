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
    public partial class EndScreen : UserControl
    {
        public EndScreen()
        {
            InitializeComponent();
            Cursor.Show();
            EndScreenStats();
        }

        public void EndScreenStats()
        {
            //Put player stats on labels for that run
            deathLabel.Text += $" {Level1Screen.deaths}";
            timeLabel.Text += $"\n {Math.Round(Level1Screen.gameStopwatchValue / 1000, 2)} Seconds";
        }

        private void tutorialButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new Level1Screen());
            Level1Screen.level = 0;
            Level1Screen.GameSetup();
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new Level1Screen());
            Level1Screen.level = 1;
            Level1Screen.GameSetup();

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
