namespace Caro
{
    partial class Test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test));
            this.board = new Caro.CaroBoardUI2();
            this.SuspendLayout();
            // 
            // board
            // 
            this.board.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.board.BackColor = System.Drawing.Color.Transparent;
            this.board.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("board.BackgroundImage")));
            this.board.Location = new System.Drawing.Point(12, 14);
            this.board.MaximumSize = new System.Drawing.Size(565, 565);
            this.board.MinimumSize = new System.Drawing.Size(565, 565);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(565, 565);
            this.board.TabIndex = 0;
            this.board.Load += new System.EventHandler(this.board_Load);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 589);
            this.Controls.Add(this.board);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private CaroBoardUI2 board;
    }
}