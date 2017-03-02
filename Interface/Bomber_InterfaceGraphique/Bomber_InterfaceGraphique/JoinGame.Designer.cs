namespace Bomber_InterfaceGraphique
{
    partial class JoinGame
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPseudo = new System.Windows.Forms.TextBox();
            this.tbxIPServer = new System.Windows.Forms.TextBox();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pseudo :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP Serveur :";
            // 
            // tbxPseudo
            // 
            this.tbxPseudo.Location = new System.Drawing.Point(92, 5);
            this.tbxPseudo.Name = "tbxPseudo";
            this.tbxPseudo.Size = new System.Drawing.Size(161, 20);
            this.tbxPseudo.TabIndex = 2;
            this.tbxPseudo.TextChanged += new System.EventHandler(this.tbxPseudo_TextChanged);
            // 
            // tbxIPServer
            // 
            this.tbxIPServer.Location = new System.Drawing.Point(92, 40);
            this.tbxIPServer.Name = "tbxIPServer";
            this.tbxIPServer.Size = new System.Drawing.Size(161, 20);
            this.tbxIPServer.TabIndex = 3;
            this.tbxIPServer.TextChanged += new System.EventHandler(this.tbxPseudo_TextChanged);
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnJoinGame.Enabled = false;
            this.btnJoinGame.Location = new System.Drawing.Point(13, 75);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(240, 23);
            this.btnJoinGame.TabIndex = 4;
            this.btnJoinGame.Text = "Rejoindre";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            // 
            // JoinGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 112);
            this.Controls.Add(this.btnJoinGame);
            this.Controls.Add(this.tbxIPServer);
            this.Controls.Add(this.tbxPseudo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "JoinGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rejoindre une partie";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPseudo;
        private System.Windows.Forms.TextBox tbxIPServer;
        private System.Windows.Forms.Button btnJoinGame;
    }
}