namespace TankBattle
{
    partial class BattleForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleForm));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.powerLabel = new System.Windows.Forms.Label();
            this.angleLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.powerTrack = new System.Windows.Forms.TrackBar();
            this.angleUpDown = new System.Windows.Forms.NumericUpDown();
            this.currentPlayerLabel = new System.Windows.Forms.Label();
            this.weaponSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.windspeedLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.battleformTimer = new System.Windows.Forms.Timer(this.components);
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(0, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(800, 600);
            this.displayPanel.TabIndex = 0;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.controlPanel.Controls.Add(this.powerLabel);
            this.controlPanel.Controls.Add(this.angleLabel);
            this.controlPanel.Controls.Add(this.button1);
            this.controlPanel.Controls.Add(this.powerTrack);
            this.controlPanel.Controls.Add(this.angleUpDown);
            this.controlPanel.Controls.Add(this.currentPlayerLabel);
            this.controlPanel.Controls.Add(this.weaponSelect);
            this.controlPanel.Controls.Add(this.label3);
            this.controlPanel.Controls.Add(this.windspeedLabel);
            this.controlPanel.Controls.Add(this.label1);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(800, 37);
            this.controlPanel.TabIndex = 1;
            // 
            // powerLabel
            // 
            this.powerLabel.AutoSize = true;
            this.powerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerLabel.Location = new System.Drawing.Point(340, 9);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(46, 13);
            this.powerLabel.TabIndex = 4;
            this.powerLabel.Text = "Power:";
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.angleLabel.Location = new System.Drawing.Point(223, 10);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(43, 13);
            this.angleLabel.TabIndex = 3;
            this.angleLabel.Text = "Angle:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(725, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "Fire!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BattleForm_KeyDown);
            // 
            // powerTrack
            // 
            this.powerTrack.LargeChange = 10;
            this.powerTrack.Location = new System.Drawing.Point(392, 5);
            this.powerTrack.Maximum = 100;
            this.powerTrack.Minimum = 5;
            this.powerTrack.Name = "powerTrack";
            this.powerTrack.Size = new System.Drawing.Size(158, 45);
            this.powerTrack.TabIndex = 8;
            this.powerTrack.TabStop = false;
            this.powerTrack.Value = 5;
            this.powerTrack.ValueChanged += new System.EventHandler(this.powerTrack_ValueChanged);
            // 
            // angleUpDown
            // 
            this.angleUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.angleUpDown.InterceptArrowKeys = false;
            this.angleUpDown.Location = new System.Drawing.Point(269, 7);
            this.angleUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.angleUpDown.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.angleUpDown.Name = "angleUpDown";
            this.angleUpDown.Size = new System.Drawing.Size(50, 20);
            this.angleUpDown.TabIndex = 7;
            this.angleUpDown.TabStop = false;
            this.angleUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.angleUpDown.ValueChanged += new System.EventHandler(this.angleUpDown_ValueChanged);
            // 
            // currentPlayerLabel
            // 
            this.currentPlayerLabel.AutoSize = true;
            this.currentPlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentPlayerLabel.Location = new System.Drawing.Point(12, 6);
            this.currentPlayerLabel.Name = "currentPlayerLabel";
            this.currentPlayerLabel.Size = new System.Drawing.Size(135, 18);
            this.currentPlayerLabel.TabIndex = 0;
            this.currentPlayerLabel.Text = "currentPlayerName";
            this.currentPlayerLabel.UseMnemonic = false;
            // 
            // weaponSelect
            // 
            this.weaponSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponSelect.FormattingEnabled = true;
            this.weaponSelect.Location = new System.Drawing.Point(629, 5);
            this.weaponSelect.Name = "weaponSelect";
            this.weaponSelect.Size = new System.Drawing.Size(81, 21);
            this.weaponSelect.TabIndex = 12;
            this.weaponSelect.TabStop = false;
            this.weaponSelect.SelectedIndexChanged += new System.EventHandler(this.weaponSelect_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(565, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Weapon:";
            // 
            // windspeedLabel
            // 
            this.windspeedLabel.AutoSize = true;
            this.windspeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windspeedLabel.Location = new System.Drawing.Point(162, 16);
            this.windspeedLabel.Name = "windspeedLabel";
            this.windspeedLabel.Size = new System.Drawing.Size(27, 13);
            this.windspeedLabel.TabIndex = 1;
            this.windspeedLabel.Text = "0 W";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wind ";
            // 
            // battleformTimer
            // 
            this.battleformTimer.Interval = 20;
            this.battleformTimer.Tick += new System.EventHandler(this.battleformTimer_Tick);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 629);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "BattleForm";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BattleForm_KeyDown);
            this.Resize += new System.EventHandler(this.BattleForm_Resize);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar powerTrack;
        private System.Windows.Forms.NumericUpDown angleUpDown;
        private System.Windows.Forms.Label currentPlayerLabel;
        private System.Windows.Forms.ComboBox weaponSelect;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label windspeedLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer battleformTimer;
    }
}

