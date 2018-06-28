namespace TankBattle
{
    partial class Scoreboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.currentLeader = new System.Windows.Forms.Label();
            this.continueGame = new System.Windows.Forms.Button();
            this.playerScores = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current leader is :";
            // 
            // currentLeader
            // 
            this.currentLeader.AutoSize = true;
            this.currentLeader.Location = new System.Drawing.Point(215, 23);
            this.currentLeader.Name = "currentLeader";
            this.currentLeader.Size = new System.Drawing.Size(0, 13);
            this.currentLeader.TabIndex = 2;
            // 
            // continueGame
            // 
            this.continueGame.Location = new System.Drawing.Point(120, 251);
            this.continueGame.Name = "continueGame";
            this.continueGame.Size = new System.Drawing.Size(75, 23);
            this.continueGame.TabIndex = 3;
            this.continueGame.Text = "Continue";
            this.continueGame.UseVisualStyleBackColor = true;
            this.continueGame.Click += new System.EventHandler(this.button1_Click);
            // 
            // playerScores
            // 
            this.playerScores.FormattingEnabled = true;
            this.playerScores.Location = new System.Drawing.Point(24, 56);
            this.playerScores.Name = "playerScores";
            this.playerScores.Size = new System.Drawing.Size(255, 186);
            this.playerScores.TabIndex = 4;
            // 
            // Scoreboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 286);
            this.Controls.Add(this.playerScores);
            this.Controls.Add(this.continueGame);
            this.Controls.Add(this.currentLeader);
            this.Controls.Add(this.label1);
            this.Name = "Scoreboard";
            this.Text = "Scoreboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continueGame;
        private System.Windows.Forms.Label currentLeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox playerScores;
    }
}