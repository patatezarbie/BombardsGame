using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public partial class JoinGame : Form
    {
        private const string EXPRESSION_REGEXP = @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$";
        public string Pseudo { get; set; }
        public string IPServer { get; set; }
        public Regex Regexp { get; set; }



        public JoinGame()
        {
            InitializeComponent();
        }

        private void JoinGame_Load(object sender, EventArgs e)
        {
            this.Regexp = new Regex(EXPRESSION_REGEXP);
        }

        private void tbxPseudo_TextChanged(object sender, EventArgs e)
        {
            if (this.tbxIPServer.Text.Length > 0 && this.Regexp.IsMatch(this.tbxIPServer.Text) && this.tbxPseudo.Text.Length > 0)
                this.btnJoinGame.Enabled = true;
            else
                this.btnJoinGame.Enabled = false;
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            this.Pseudo = this.tbxPseudo.Text;
            this.IPServer = this.tbxIPServer.Text;
        }

       
    }
}
