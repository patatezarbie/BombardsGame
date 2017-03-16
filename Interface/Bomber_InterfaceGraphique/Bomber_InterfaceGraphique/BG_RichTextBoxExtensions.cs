using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public static class BG_RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void WriteScore(this RichTextBox box, List<Player> playerList)
        {
            //Need to sort the list by score before

            box.Clear();
            Random rnd = new Random();

            box.AppendText("Score" + Environment.NewLine + Environment.NewLine);
            foreach (var player in playerList)
            {
                Color randomColor = Color.FromArgb(rnd.Next(128), rnd.Next(128), rnd.Next(128));
                //Ajouter le score (player : score)
                box.AppendText(player.name + " : 10" + Environment.NewLine, randomColor);
            }
        }
    }
}
