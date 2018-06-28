namespace TankBattle
{
    partial class SetupGameForm
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
            this.playersLabel = new System.Windows.Forms.Label();
            this.choosePlayers = new System.Windows.Forms.GroupBox();
            this.players8RB = new System.Windows.Forms.RadioButton();
            this.players7RB = new System.Windows.Forms.RadioButton();
            this.players6RB = new System.Windows.Forms.RadioButton();
            this.players5RB = new System.Windows.Forms.RadioButton();
            this.players4RB = new System.Windows.Forms.RadioButton();
            this.players3RB = new System.Windows.Forms.RadioButton();
            this.players2RB = new System.Windows.Forms.RadioButton();
            this.chooseRounds = new System.Windows.Forms.GroupBox();
            this.rounds10RB = new System.Windows.Forms.RadioButton();
            this.rounds5RB = new System.Windows.Forms.RadioButton();
            this.rounds3RB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rounds1RB = new System.Windows.Forms.RadioButton();
            this.roundsCustom = new System.Windows.Forms.RadioButton();
            this.customChoice = new System.Windows.Forms.NumericUpDown();
            this.startSetup = new System.Windows.Forms.Button();
            this.choosePlayers.SuspendLayout();
            this.chooseRounds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customChoice)).BeginInit();
            this.SuspendLayout();
            // 
            // playersLabel
            // 
            this.playersLabel.AutoSize = true;
            this.playersLabel.Font = new System.Drawing.Font("Modern No. 20", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersLabel.Location = new System.Drawing.Point(155, 21);
            this.playersLabel.Name = "playersLabel";
            this.playersLabel.Size = new System.Drawing.Size(73, 14);
            this.playersLabel.TabIndex = 0;
            this.playersLabel.Text = "select a number";
            // 
            // choosePlayers
            // 
            this.choosePlayers.Controls.Add(this.players8RB);
            this.choosePlayers.Controls.Add(this.players7RB);
            this.choosePlayers.Controls.Add(this.players6RB);
            this.choosePlayers.Controls.Add(this.players5RB);
            this.choosePlayers.Controls.Add(this.players4RB);
            this.choosePlayers.Controls.Add(this.players3RB);
            this.choosePlayers.Controls.Add(this.players2RB);
            this.choosePlayers.Controls.Add(this.playersLabel);
            this.choosePlayers.Font = new System.Drawing.Font("Modern No. 20", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choosePlayers.Location = new System.Drawing.Point(23, 26);
            this.choosePlayers.Name = "choosePlayers";
            this.choosePlayers.Size = new System.Drawing.Size(397, 69);
            this.choosePlayers.TabIndex = 1;
            this.choosePlayers.TabStop = false;
            this.choosePlayers.Text = "How many players are there?";
            // 
            // players8RB
            // 
            this.players8RB.AutoSize = true;
            this.players8RB.Location = new System.Drawing.Point(343, 38);
            this.players8RB.Name = "players8RB";
            this.players8RB.Size = new System.Drawing.Size(34, 22);
            this.players8RB.TabIndex = 7;
            this.players8RB.TabStop = true;
            this.players8RB.Tag = "8";
            this.players8RB.Text = "8";
            this.players8RB.UseVisualStyleBackColor = true;
            // 
            // players7RB
            // 
            this.players7RB.AutoSize = true;
            this.players7RB.Location = new System.Drawing.Point(290, 38);
            this.players7RB.Name = "players7RB";
            this.players7RB.Size = new System.Drawing.Size(34, 22);
            this.players7RB.TabIndex = 6;
            this.players7RB.TabStop = true;
            this.players7RB.Tag = "7";
            this.players7RB.Text = "7";
            this.players7RB.UseVisualStyleBackColor = true;
            // 
            // players6RB
            // 
            this.players6RB.AutoSize = true;
            this.players6RB.Location = new System.Drawing.Point(234, 38);
            this.players6RB.Name = "players6RB";
            this.players6RB.Size = new System.Drawing.Size(34, 22);
            this.players6RB.TabIndex = 5;
            this.players6RB.TabStop = true;
            this.players6RB.Tag = "6";
            this.players6RB.Text = "6";
            this.players6RB.UseVisualStyleBackColor = true;
            // 
            // players5RB
            // 
            this.players5RB.AutoSize = true;
            this.players5RB.Location = new System.Drawing.Point(179, 38);
            this.players5RB.Name = "players5RB";
            this.players5RB.Size = new System.Drawing.Size(34, 22);
            this.players5RB.TabIndex = 4;
            this.players5RB.TabStop = true;
            this.players5RB.Tag = "5";
            this.players5RB.Text = "5";
            this.players5RB.UseVisualStyleBackColor = true;
            // 
            // players4RB
            // 
            this.players4RB.AutoSize = true;
            this.players4RB.Location = new System.Drawing.Point(129, 38);
            this.players4RB.Name = "players4RB";
            this.players4RB.Size = new System.Drawing.Size(34, 22);
            this.players4RB.TabIndex = 3;
            this.players4RB.TabStop = true;
            this.players4RB.Tag = "4";
            this.players4RB.Text = "4";
            this.players4RB.UseVisualStyleBackColor = true;
            // 
            // players3RB
            // 
            this.players3RB.AutoSize = true;
            this.players3RB.Location = new System.Drawing.Point(79, 38);
            this.players3RB.Name = "players3RB";
            this.players3RB.Size = new System.Drawing.Size(34, 22);
            this.players3RB.TabIndex = 2;
            this.players3RB.TabStop = true;
            this.players3RB.Tag = "3";
            this.players3RB.Text = "3";
            this.players3RB.UseVisualStyleBackColor = true;
            // 
            // players2RB
            // 
            this.players2RB.AutoSize = true;
            this.players2RB.Location = new System.Drawing.Point(20, 38);
            this.players2RB.Name = "players2RB";
            this.players2RB.Size = new System.Drawing.Size(34, 22);
            this.players2RB.TabIndex = 1;
            this.players2RB.TabStop = true;
            this.players2RB.Tag = "2";
            this.players2RB.Text = "2";
            this.players2RB.UseVisualStyleBackColor = true;
            // 
            // chooseRounds
            // 
            this.chooseRounds.Controls.Add(this.customChoice);
            this.chooseRounds.Controls.Add(this.roundsCustom);
            this.chooseRounds.Controls.Add(this.rounds10RB);
            this.chooseRounds.Controls.Add(this.rounds5RB);
            this.chooseRounds.Controls.Add(this.rounds3RB);
            this.chooseRounds.Controls.Add(this.label1);
            this.chooseRounds.Controls.Add(this.rounds1RB);
            this.chooseRounds.Font = new System.Drawing.Font("Modern No. 20", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.chooseRounds.Location = new System.Drawing.Point(23, 117);
            this.chooseRounds.Name = "chooseRounds";
            this.chooseRounds.Size = new System.Drawing.Size(397, 77);
            this.chooseRounds.TabIndex = 2;
            this.chooseRounds.TabStop = false;
            this.chooseRounds.Text = "How many rounds are beening played?";
            // 
            // rounds10RB
            // 
            this.rounds10RB.AutoSize = true;
            this.rounds10RB.Location = new System.Drawing.Point(194, 49);
            this.rounds10RB.Name = "rounds10RB";
            this.rounds10RB.Size = new System.Drawing.Size(42, 22);
            this.rounds10RB.TabIndex = 6;
            this.rounds10RB.TabStop = true;
            this.rounds10RB.Tag = "10";
            this.rounds10RB.Text = "10";
            this.rounds10RB.UseVisualStyleBackColor = true;
            // 
            // rounds5RB
            // 
            this.rounds5RB.AutoSize = true;
            this.rounds5RB.Location = new System.Drawing.Point(139, 49);
            this.rounds5RB.Name = "rounds5RB";
            this.rounds5RB.Size = new System.Drawing.Size(34, 22);
            this.rounds5RB.TabIndex = 5;
            this.rounds5RB.TabStop = true;
            this.rounds5RB.Tag = "5";
            this.rounds5RB.Text = "5";
            this.rounds5RB.UseVisualStyleBackColor = true;
            // 
            // rounds3RB
            // 
            this.rounds3RB.AutoSize = true;
            this.rounds3RB.Location = new System.Drawing.Point(79, 49);
            this.rounds3RB.Name = "rounds3RB";
            this.rounds3RB.Size = new System.Drawing.Size(34, 22);
            this.rounds3RB.TabIndex = 4;
            this.rounds3RB.TabStop = true;
            this.rounds3RB.Tag = "3";
            this.rounds3RB.Text = "3";
            this.rounds3RB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "select a number or add your own amount";
            // 
            // rounds1RB
            // 
            this.rounds1RB.AutoSize = true;
            this.rounds1RB.Location = new System.Drawing.Point(20, 49);
            this.rounds1RB.Name = "rounds1RB";
            this.rounds1RB.Size = new System.Drawing.Size(34, 22);
            this.rounds1RB.TabIndex = 2;
            this.rounds1RB.TabStop = true;
            this.rounds1RB.Tag = "1";
            this.rounds1RB.Text = "1";
            this.rounds1RB.UseVisualStyleBackColor = true;
            // 
            // roundsCustom
            // 
            this.roundsCustom.AutoSize = true;
            this.roundsCustom.Location = new System.Drawing.Point(261, 49);
            this.roundsCustom.Name = "roundsCustom";
            this.roundsCustom.Size = new System.Drawing.Size(76, 22);
            this.roundsCustom.TabIndex = 7;
            this.roundsCustom.TabStop = true;
            this.roundsCustom.Tag = "";
            this.roundsCustom.Text = "custom";
            this.roundsCustom.UseVisualStyleBackColor = true;
            this.roundsCustom.CheckedChanged += new System.EventHandler(this.roundsCustom_CheckedChanged);
            // 
            // customChoice
            // 
            this.customChoice.Enabled = false;
            this.customChoice.InterceptArrowKeys = false;
            this.customChoice.Location = new System.Drawing.Point(343, 46);
            this.customChoice.Name = "customChoice";
            this.customChoice.Size = new System.Drawing.Size(47, 25);
            this.customChoice.TabIndex = 8;
            this.customChoice.ValueChanged += new System.EventHandler(this.customChoice_ValueChanged);
            // 
            // startSetup
            // 
            this.startSetup.Font = new System.Drawing.Font("Modern No. 20", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.startSetup.Location = new System.Drawing.Point(23, 213);
            this.startSetup.Name = "startSetup";
            this.startSetup.Size = new System.Drawing.Size(397, 38);
            this.startSetup.TabIndex = 3;
            this.startSetup.Text = "Setup Individual Players";
            this.startSetup.UseVisualStyleBackColor = true;
            this.startSetup.Click += new System.EventHandler(this.startSetup_Click);
            // 
            // SetupGameForm
            // 
            this.ClientSize = new System.Drawing.Size(450, 273);
            this.Controls.Add(this.startSetup);
            this.Controls.Add(this.chooseRounds);
            this.Controls.Add(this.choosePlayers);
            this.Name = "SetupGameForm";
            this.Text = "Tank Battle skirmish";
            this.choosePlayers.ResumeLayout(false);
            this.choosePlayers.PerformLayout();
            this.chooseRounds.ResumeLayout(false);
            this.chooseRounds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customChoice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label playersLabel;
        private System.Windows.Forms.GroupBox choosePlayers;
        private System.Windows.Forms.RadioButton players8RB;
        private System.Windows.Forms.RadioButton players7RB;
        private System.Windows.Forms.RadioButton players6RB;
        private System.Windows.Forms.RadioButton players5RB;
        private System.Windows.Forms.RadioButton players4RB;
        private System.Windows.Forms.RadioButton players3RB;
        private System.Windows.Forms.RadioButton players2RB;
        private System.Windows.Forms.GroupBox chooseRounds;
        private System.Windows.Forms.RadioButton rounds10RB;
        private System.Windows.Forms.RadioButton rounds5RB;
        private System.Windows.Forms.RadioButton rounds3RB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rounds1RB;
        private System.Windows.Forms.NumericUpDown customChoice;
        private System.Windows.Forms.RadioButton roundsCustom;
        private System.Windows.Forms.Button startSetup;
    }
}
