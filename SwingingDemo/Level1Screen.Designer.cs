namespace SwingingDemo
{
    partial class Level1Screen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.level1GameEngine = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.dKeyLabel = new System.Windows.Forms.Label();
            this.aKeyLabel = new System.Windows.Forms.Label();
            this.tutorialTimer = new System.Windows.Forms.Timer(this.components);
            this.spaceKeyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // level1GameEngine
            // 
            this.level1GameEngine.Enabled = true;
            this.level1GameEngine.Interval = 25;
            this.level1GameEngine.Tick += new System.EventHandler(this.level1GameEngine_Tick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Ravie", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(170, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(539, 391);
            this.label1.TabIndex = 0;
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 90;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // dKeyLabel
            // 
            this.dKeyLabel.BackColor = System.Drawing.Color.Transparent;
            this.dKeyLabel.Font = new System.Drawing.Font("Ravie", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dKeyLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.dKeyLabel.Location = new System.Drawing.Point(501, 412);
            this.dKeyLabel.Name = "dKeyLabel";
            this.dKeyLabel.Size = new System.Drawing.Size(138, 62);
            this.dKeyLabel.TabIndex = 4;
            this.dKeyLabel.Text = "D -->";
            this.dKeyLabel.Visible = false;
            // 
            // aKeyLabel
            // 
            this.aKeyLabel.BackColor = System.Drawing.Color.Transparent;
            this.aKeyLabel.Font = new System.Drawing.Font("Ravie", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aKeyLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.aKeyLabel.Location = new System.Drawing.Point(189, 412);
            this.aKeyLabel.Name = "aKeyLabel";
            this.aKeyLabel.Size = new System.Drawing.Size(138, 62);
            this.aKeyLabel.TabIndex = 5;
            this.aKeyLabel.Text = "<-- A";
            this.aKeyLabel.Visible = false;
            // 
            // tutorialTimer
            // 
            this.tutorialTimer.Interval = 16;
            this.tutorialTimer.Tick += new System.EventHandler(this.tutorialTimer_Tick);
            // 
            // spaceKeyLabel
            // 
            this.spaceKeyLabel.BackColor = System.Drawing.Color.Transparent;
            this.spaceKeyLabel.Font = new System.Drawing.Font("Ravie", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spaceKeyLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.spaceKeyLabel.Location = new System.Drawing.Point(318, 188);
            this.spaceKeyLabel.Name = "spaceKeyLabel";
            this.spaceKeyLabel.Size = new System.Drawing.Size(189, 377);
            this.spaceKeyLabel.TabIndex = 6;
            this.spaceKeyLabel.Text = "   /\\\r\n    |\r\n    | \r\nSpace";
            this.spaceKeyLabel.Visible = false;
            // 
            // Level1Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.spaceKeyLabel);
            this.Controls.Add(this.aKeyLabel);
            this.Controls.Add(this.dKeyLabel);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "Level1Screen";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Level1Screen_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Level1Screen_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Level1Screen_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer level1GameEngine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Label dKeyLabel;
        private System.Windows.Forms.Label aKeyLabel;
        private System.Windows.Forms.Timer tutorialTimer;
        private System.Windows.Forms.Label spaceKeyLabel;
    }
}
