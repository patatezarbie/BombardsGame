using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public partial class JoinGame : Form
    {
        public JoinGame()
        {
            InitializeComponent();
        }

        private void tbxPseudo_TextChanged(object sender, EventArgs e)
        {
            if (this.tbxIPServer.Text.Length > 0 && this.tbxPseudo.Text.Length > 0)
                this.btnJoinGame.Enabled = true;
            else
                this.btnJoinGame.Enabled = false;
        }
    }
}
