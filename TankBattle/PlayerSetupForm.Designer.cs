namespace TankBattle
{
    partial class PlayerSetupForm
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
            this.playerSettings = new System.Windows.Forms.GroupBox();
            this.tankChoices = new System.Windows.Forms.GroupBox();
            this.armoredTank = new System.Windows.Forms.RadioButton();
            this.quickFireTank = new System.Windows.Forms.RadioButton();
            this.HeavyTank = new System.Windows.Forms.RadioButton();
            this.basicTank = new System.Windows.Forms.RadioButton();
            this.controllerChoice = new System.Windows.Forms.GroupBox();
            this.computerChoice = new System.Windows.Forms.RadioButton();
            this.humanChoice = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.Label();
            this.inputtedName = new System.Windows.Forms.TextBox();
            this.NextPlayer = new System.Windows.Forms.Button();
            this.setupProgress = new System.Windows.Forms.ProgressBar();
            this.playerSettings.SuspendLayout();
            this.tankChoices.SuspendLayout();
            this.controllerChoice.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerSettings
            // 
            this.playerSettings.BackColor = System.Drawing.Color.White;
            this.playerSettings.Controls.Add(this.tankChoices);
            this.playerSettings.Controls.Add(this.controllerChoice);
            this.playerSettings.Controls.Add(this.playerName);
            this.playerSettings.Controls.Add(this.inputtedName);
            this.playerSettings.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerSettings.Location = new System.Drawing.Point(29, 12);
            this.playerSettings.Name = "playerSettings";
            this.playerSettings.Size = new System.Drawing.Size(547, 211);
            this.playerSettings.TabIndex = 0;
            this.playerSettings.TabStop = false;
            // 
            // tankChoices
            // 
            this.tankChoices.Controls.Add(this.armoredTank);
            this.tankChoices.Controls.Add(this.quickFireTank);
            this.tankChoices.Controls.Add(this.HeavyTank);
            this.tankChoices.Controls.Add(this.basicTank);
            this.tankChoices.Location = new System.Drawing.Point(260, 88);
            this.tankChoices.Name = "tankChoices";
            this.tankChoices.Size = new System.Drawing.Size(261, 106);
            this.tankChoices.TabIndex = 4;
            this.tankChoices.TabStop = false;
            this.tankChoices.Text = "Which tank did you want to use ?";
            // 
            // armoredTank
            // 
            this.armoredTank.AutoSize = true;
            this.armoredTank.Location = new System.Drawing.Point(147, 76);
            this.armoredTank.MaximumSize = new System.Drawing.Size(50, 50);
            this.armoredTank.Name = "armoredTank";
            this.armoredTank.Padding = new System.Windows.Forms.Padding(5);
            this.armoredTank.Size = new System.Drawing.Size(24, 23);
            this.armoredTank.TabIndex = 3;
            this.armoredTank.TabStop = true;
            this.armoredTank.UseVisualStyleBackColor = true;
            // 
            // quickFireTank
            // 
            this.quickFireTank.AutoSize = true;
            this.quickFireTank.Location = new System.Drawing.Point(63, 76);
            this.quickFireTank.MaximumSize = new System.Drawing.Size(50, 50);
            this.quickFireTank.Name = "quickFireTank";
            this.quickFireTank.Padding = new System.Windows.Forms.Padding(5);
            this.quickFireTank.Size = new System.Drawing.Size(24, 23);
            this.quickFireTank.TabIndex = 2;
            this.quickFireTank.TabStop = true;
            this.quickFireTank.UseVisualStyleBackColor = true;
            // 
            // HeavyTank
            // 
            this.HeavyTank.AutoSize = true;
            this.HeavyTank.Location = new System.Drawing.Point(147, 41);
            this.HeavyTank.MaximumSize = new System.Drawing.Size(50, 50);
            this.HeavyTank.Name = "HeavyTank";
            this.HeavyTank.Padding = new System.Windows.Forms.Padding(5);
            this.HeavyTank.Size = new System.Drawing.Size(24, 23);
            this.HeavyTank.TabIndex = 1;
            this.HeavyTank.TabStop = true;
            this.HeavyTank.UseVisualStyleBackColor = true;
            // 
            // basicTank
            // 
            this.basicTank.AutoSize = true;
            this.basicTank.Location = new System.Drawing.Point(63, 41);
            this.basicTank.MaximumSize = new System.Drawing.Size(50, 50);
            this.basicTank.Name = "basicTank";
            this.basicTank.Padding = new System.Windows.Forms.Padding(5);
            this.basicTank.Size = new System.Drawing.Size(24, 23);
            this.basicTank.TabIndex = 0;
            this.basicTank.TabStop = true;
            this.basicTank.UseVisualStyleBackColor = true;
            // 
            // controllerChoice
            // 
            this.controllerChoice.Controls.Add(this.computerChoice);
            this.controllerChoice.Controls.Add(this.humanChoice);
            this.controllerChoice.Controls.Add(this.label2);
            this.controllerChoice.Location = new System.Drawing.Point(73, 88);
            this.controllerChoice.Name = "controllerChoice";
            this.controllerChoice.Size = new System.Drawing.Size(169, 107);
            this.controllerChoice.TabIndex = 3;
            this.controllerChoice.TabStop = false;
            // 
            // computerChoice
            // 
            this.computerChoice.AutoSize = true;
            this.computerChoice.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.computerChoice.Location = new System.Drawing.Point(9, 70);
            this.computerChoice.Name = "computerChoice";
            this.computerChoice.Size = new System.Drawing.Size(75, 19);
            this.computerChoice.TabIndex = 4;
            this.computerChoice.TabStop = true;
            this.computerChoice.Text = "Computer";
            this.computerChoice.UseVisualStyleBackColor = true;
            // 
            // humanChoice
            // 
            this.humanChoice.AutoSize = true;
            this.humanChoice.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humanChoice.Location = new System.Drawing.Point(9, 45);
            this.humanChoice.Name = "humanChoice";
            this.humanChoice.Size = new System.Drawing.Size(66, 19);
            this.humanChoice.TabIndex = 3;
            this.humanChoice.TabStop = true;
            this.humanChoice.Text = "Human";
            this.humanChoice.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Controller of Player";
            // 
            // playerName
            // 
            this.playerName.AutoSize = true;
            this.playerName.Font = new System.Drawing.Font("Modern No. 20", 18.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerName.Location = new System.Drawing.Point(26, 47);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(231, 26);
            this.playerName.TabIndex = 1;
            this.playerName.Text = "Player #{0}\'s name is :";
            // 
            // inputtedName
            // 
            this.inputtedName.Location = new System.Drawing.Point(260, 47);
            this.inputtedName.Name = "inputtedName";
            this.inputtedName.Size = new System.Drawing.Size(262, 25);
            this.inputtedName.TabIndex = 0;
            // 
            // NextPlayer
            // 
            this.NextPlayer.Font = new System.Drawing.Font("Modern No. 20", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextPlayer.Location = new System.Drawing.Point(277, 229);
            this.NextPlayer.Name = "NextPlayer";
            this.NextPlayer.Size = new System.Drawing.Size(299, 31);
            this.NextPlayer.TabIndex = 1;
            this.NextPlayer.Text = "Next Player";
            this.NextPlayer.UseVisualStyleBackColor = true;
            this.NextPlayer.Click += new System.EventHandler(this.NextPlayer_Click);
            // 
            // setupProgress
            // 
            this.setupProgress.BackColor = System.Drawing.SystemColors.Control;
            this.setupProgress.Location = new System.Drawing.Point(30, 235);
            this.setupProgress.Maximum = 8;
            this.setupProgress.Name = "setupProgress";
            this.setupProgress.Size = new System.Drawing.Size(242, 21);
            this.setupProgress.Step = 1;
            this.setupProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.setupProgress.TabIndex = 2;
            this.setupProgress.Value = 1;
            // 
            // PlayerSetupForm
            // 
            this.ClientSize = new System.Drawing.Size(604, 272);
            this.Controls.Add(this.setupProgress);
            this.Controls.Add(this.NextPlayer);
            this.Controls.Add(this.playerSettings);
            this.Name = "PlayerSetupForm";
            this.Text = "Setup Player#";
            this.playerSettings.ResumeLayout(false);
            this.playerSettings.PerformLayout();
            this.tankChoices.ResumeLayout(false);
            this.tankChoices.PerformLayout();
            this.controllerChoice.ResumeLayout(false);
            this.controllerChoice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox playerSettings;
        private System.Windows.Forms.Button NextPlayer;
        private System.Windows.Forms.ProgressBar setupProgress;
        private System.Windows.Forms.GroupBox tankChoices;
        private System.Windows.Forms.RadioButton armoredTank;
        private System.Windows.Forms.RadioButton quickFireTank;
        private System.Windows.Forms.RadioButton HeavyTank;
        private System.Windows.Forms.RadioButton basicTank;
        private System.Windows.Forms.GroupBox controllerChoice;
        private System.Windows.Forms.RadioButton computerChoice;
        private System.Windows.Forms.RadioButton humanChoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label playerName;
        private System.Windows.Forms.TextBox inputtedName;
    }
}
