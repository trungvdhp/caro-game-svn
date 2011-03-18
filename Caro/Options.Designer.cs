namespace Caro
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelTrackbar = new System.Windows.Forms.Label();
            this.op_trackComputerLevel = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.op_comboPlayerSymbol = new System.Windows.Forms.ComboBox();
            this.op_comboGameLaw = new System.Windows.Forms.ComboBox();
            this.op_comboFirstPlayer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.op_trackComputerLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.labelTrackbar);
            this.groupBox1.Controls.Add(this.op_trackComputerLevel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.op_comboPlayerSymbol);
            this.groupBox1.Controls.Add(this.op_comboGameLaw);
            this.groupBox1.Controls.Add(this.op_comboFirstPlayer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(315, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(234, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelTrackbar
            // 
            this.labelTrackbar.AutoSize = true;
            this.labelTrackbar.Location = new System.Drawing.Point(369, 55);
            this.labelTrackbar.Name = "labelTrackbar";
            this.labelTrackbar.Size = new System.Drawing.Size(13, 13);
            this.labelTrackbar.TabIndex = 4;
            this.labelTrackbar.Text = "3";
            // 
            // op_trackComputerLevel
            // 
            this.op_trackComputerLevel.Location = new System.Drawing.Point(280, 46);
            this.op_trackComputerLevel.Maximum = 8;
            this.op_trackComputerLevel.Minimum = 1;
            this.op_trackComputerLevel.Name = "op_trackComputerLevel";
            this.op_trackComputerLevel.Size = new System.Drawing.Size(83, 45);
            this.op_trackComputerLevel.TabIndex = 3;
            this.op_trackComputerLevel.Value = 3;
            this.op_trackComputerLevel.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Player\'s Symbol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Computer Level";
            // 
            // op_comboPlayerSymbol
            // 
            this.op_comboPlayerSymbol.FormattingEnabled = true;
            this.op_comboPlayerSymbol.Items.AddRange(new object[] {
            "X",
            "O"});
            this.op_comboPlayerSymbol.Location = new System.Drawing.Point(280, 13);
            this.op_comboPlayerSymbol.Name = "op_comboPlayerSymbol";
            this.op_comboPlayerSymbol.Size = new System.Drawing.Size(83, 21);
            this.op_comboPlayerSymbol.TabIndex = 1;
            this.op_comboPlayerSymbol.Text = "X";
            this.op_comboPlayerSymbol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_keypress);
            // 
            // op_comboGameLaw
            // 
            this.op_comboGameLaw.Enabled = false;
            this.op_comboGameLaw.FormattingEnabled = true;
            this.op_comboGameLaw.Items.AddRange(new object[] {
            "Gomoku",
            "Caro"});
            this.op_comboGameLaw.Location = new System.Drawing.Point(93, 52);
            this.op_comboGameLaw.Name = "op_comboGameLaw";
            this.op_comboGameLaw.Size = new System.Drawing.Size(83, 21);
            this.op_comboGameLaw.TabIndex = 1;
            this.op_comboGameLaw.Text = "Gomoku";
            this.op_comboGameLaw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_keypress);
            // 
            // op_comboFirstPlayer
            // 
            this.op_comboFirstPlayer.FormattingEnabled = true;
            this.op_comboFirstPlayer.Items.AddRange(new object[] {
            "Computer",
            "Player"});
            this.op_comboFirstPlayer.Location = new System.Drawing.Point(93, 13);
            this.op_comboFirstPlayer.Name = "op_comboFirstPlayer";
            this.op_comboFirstPlayer.Size = new System.Drawing.Size(83, 21);
            this.op_comboFirstPlayer.TabIndex = 1;
            this.op_comboFirstPlayer.Text = "Player";
            this.op_comboFirstPlayer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_keypress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Law";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Player";
            // 
            // Options
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(420, 156);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.Shown += new System.EventHandler(this.Options_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.op_trackComputerLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelTrackbar;
        private System.Windows.Forms.TrackBar op_trackComputerLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox op_comboPlayerSymbol;
        private System.Windows.Forms.ComboBox op_comboFirstPlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox op_comboGameLaw;
        private System.Windows.Forms.Label label5;
    }
}