namespace Bomber_InterfaceGraphique
{
    partial class FrmView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmView));
            this.ms_Menu = new System.Windows.Forms.MenuStrip();
            this.TSMIMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIJoinGame = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIQuitGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtb_score = new System.Windows.Forms.RichTextBox();
            this.ms_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ms_Menu
            // 
            this.ms_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIMenu,
            this.TSMIDebug});
            this.ms_Menu.Location = new System.Drawing.Point(0, 0);
            this.ms_Menu.Name = "ms_Menu";
            this.ms_Menu.Size = new System.Drawing.Size(1008, 24);
            this.ms_Menu.TabIndex = 1;
            this.ms_Menu.Text = "Menu";
            // 
            // TSMIMenu
            // 
            this.TSMIMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIJoinGame,
            this.TSMIQuitGame,
            this.toolStripMenuItem1,
            this.TSMIQuit});
            this.TSMIMenu.Name = "TSMIMenu";
            this.TSMIMenu.Size = new System.Drawing.Size(50, 20);
            this.TSMIMenu.Text = "Menu";
            this.TSMIMenu.Click += new System.EventHandler(this.TSMIMenu_Click);
            // 
            // TSMIJoinGame
            // 
            this.TSMIJoinGame.Name = "TSMIJoinGame";
            this.TSMIJoinGame.Size = new System.Drawing.Size(157, 22);
            this.TSMIJoinGame.Text = "Rejoindre partie";
            this.TSMIJoinGame.Click += new System.EventHandler(this.TSMIJoinGame_Click);
            // 
            // TSMIQuitGame
            // 
            this.TSMIQuitGame.Name = "TSMIQuitGame";
            this.TSMIQuitGame.Size = new System.Drawing.Size(157, 22);
            this.TSMIQuitGame.Text = "Quitter partie";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // TSMIQuit
            // 
            this.TSMIQuit.Name = "TSMIQuit";
            this.TSMIQuit.Size = new System.Drawing.Size(157, 22);
            this.TSMIQuit.Text = "Quitter";
            this.TSMIQuit.Click += new System.EventHandler(this.TSMIQuit_Click);
            // 
            // TSMIDebug
            // 
            this.TSMIDebug.Name = "TSMIDebug";
            this.TSMIDebug.Size = new System.Drawing.Size(54, 20);
            this.TSMIDebug.Text = "Debug";
            this.TSMIDebug.Click += new System.EventHandler(this.TSMIDebug_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(708, 491);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 153);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // rtb_score
            // 
            this.rtb_score.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_score.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_score.Location = new System.Drawing.Point(708, 27);
            this.rtb_score.Name = "rtb_score";
            this.rtb_score.ReadOnly = true;
            this.rtb_score.Size = new System.Drawing.Size(300, 461);
            this.rtb_score.TabIndex = 3;
            this.rtb_score.Text = "";
            // 
            // FrmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 642);
            this.Controls.Add(this.rtb_score);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ms_Menu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.ms_Menu;
            this.MaximizeBox = false;
            this.Name = "FrmView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bombards Game";
            this.Load += new System.EventHandler(this.FrmView_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmView_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmView_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmView_KeyUp);
            this.ms_Menu.ResumeLayout(false);
            this.ms_Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip ms_Menu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem TSMIMenu;
        private System.Windows.Forms.ToolStripMenuItem TSMIJoinGame;
        private System.Windows.Forms.ToolStripMenuItem TSMIQuitGame;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem TSMIQuit;
        private System.Windows.Forms.ToolStripMenuItem TSMIDebug;
        private System.Windows.Forms.RichTextBox rtb_score;
    }
}

