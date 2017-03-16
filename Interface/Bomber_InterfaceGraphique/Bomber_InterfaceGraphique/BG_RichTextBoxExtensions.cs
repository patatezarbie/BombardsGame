using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bomber_InterfaceGraphique
{
    public static class BG_RichTextBoxExtensions
    {
        /// <summary>
        /// Add a string with a certain color to a RichTextBox
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        /// <summary>
        /// Write the scoreboard in a RichTextBox and randomize the color of each line except the first one
        /// </summary>
        /// <param name="box">The RichTextBox</param>
        /// <param name="playerList">The list of player</param>
        public static void WriteScore(this RichTextBox box, List<Player> playerList)
        {
            //NEED TO SORT THE LIST BY SCORE BEFORE

            //Clear the list before showing the scoreboard
            box.Clear();
            Random rnd = new Random();

            //Add the text "Score" as first line
            box.AppendText("Score" + Environment.NewLine + Environment.NewLine);
            //Show the name and the score of each player
            foreach (var player in playerList)
            {
                Color randomColor = Color.FromArgb(rnd.Next(128), rnd.Next(128), rnd.Next(128));
                //NEED TO ADD THE SCORE [Player class needed]
                box.AppendText(player.name + " : 10" + Environment.NewLine, randomColor);
            }
        }
    }
}
