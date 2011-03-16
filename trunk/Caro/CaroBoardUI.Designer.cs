namespace Caro
{
    partial class CaroBoardUI
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CaroStatus = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CaroTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CaroPosition = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CaroCurrentMove = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CaroCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CaroScore = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CaroMessage = new System.Windows.Forms.ToolStripLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.CaroStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CaroStatus
            // 
            this.CaroStatus.AutoSize = false;
            this.CaroStatus.BackColor = System.Drawing.Color.Lavender;
            this.CaroStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.CaroStatus.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.CaroStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.CaroTime,
            this.toolStripSeparator1,
            this.CaroPosition,
            this.toolStripSeparator3,
            this.CaroCurrentMove,
            this.toolStripSeparator6,
            this.CaroCount,
            this.toolStripSeparator5,
            this.CaroScore,
            this.toolStripSeparator4,
            this.CaroMessage});
            this.CaroStatus.Location = new System.Drawing.Point(0, 0);
            this.CaroStatus.Name = "CaroStatus";
            this.CaroStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.CaroStatus.Size = new System.Drawing.Size(360, 25);
            this.CaroStatus.TabIndex = 1;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // CaroTime
            // 
            this.CaroTime.Name = "CaroTime";
            this.CaroTime.Size = new System.Drawing.Size(41, 22);
            this.CaroTime.Text = "00:00";
            this.CaroTime.ToolTipText = "Played time";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CaroPosition
            // 
            this.CaroPosition.Name = "CaroPosition";
            this.CaroPosition.Size = new System.Drawing.Size(27, 22);
            this.CaroPosition.Text = "0:0";
            this.CaroPosition.ToolTipText = "Current Position";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // CaroCurrentMove
            // 
            this.CaroCurrentMove.Name = "CaroCurrentMove";
            this.CaroCurrentMove.Size = new System.Drawing.Size(27, 22);
            this.CaroCurrentMove.Text = "0:0";
            this.CaroCurrentMove.ToolTipText = "Current Move";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // CaroCount
            // 
            this.CaroCount.Name = "CaroCount";
            this.CaroCount.Size = new System.Drawing.Size(15, 22);
            this.CaroCount.Text = "0";
            this.CaroCount.ToolTipText = "Total symbols";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // CaroScore
            // 
            this.CaroScore.Name = "CaroScore";
            this.CaroScore.Size = new System.Drawing.Size(27, 22);
            this.CaroScore.Text = "0:0";
            this.CaroScore.ToolTipText = "Score: Player : Computer";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // CaroMessage
            // 
            this.CaroMessage.Name = "CaroMessage";
            this.CaroMessage.Size = new System.Drawing.Size(58, 22);
            this.CaroMessage.Text = "Message";
            this.CaroMessage.ToolTipText = "Message";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // CaroBoardUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CaroStatus);
            this.Name = "CaroBoardUI";
            this.Size = new System.Drawing.Size(360, 360);
            this.CaroStatus.ResumeLayout(false);
            this.CaroStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip CaroStatus;
        private System.Windows.Forms.ToolStripLabel CaroTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripLabel CaroPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel CaroMessage;
        private System.Windows.Forms.ToolStripLabel CaroCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel CaroScore;
        private System.Windows.Forms.ToolStripLabel CaroCurrentMove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

    }
}
