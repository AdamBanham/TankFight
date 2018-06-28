using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class Scoreboard : Form
    {
        private Battle continueFight; // stores the current game
        private int[] scores; // stores the score of each player
        private string[] names; // stores the name of each player
        private int leader = 0;
        private int leaderIndex;



        /// <summary>
        /// shows a scoreboard to the player , before continuing or ending the fight.
        /// </summary>
        /// <param name="currentGame"></param>
        public Scoreboard(Battle currentGame)
        {
            InitializeComponent(); // make form
            continueFight = currentGame; // store game
            scores = new int[continueFight.NumPlayers()];
            names = new string[continueFight.NumPlayers()];
            for (int i = 1; i <= continueFight.NumPlayers(); i++)
            {
                scores[i-1] = continueFight.GetPlayerNumber(i).GetVictories(); // gets the score of a player
                names[i-1] = continueFight.GetPlayerNumber(i).Name(); // gets the player's name
                // add each entry into a textbox
                playerScores.Items.Add(string.Format("{0} current score is {1}\n", names[i-1], scores[i-1])); 
                // check if player has highest score
                if (scores[i-1] > leader)
                {
                    // set highscore to players score
                    leaderIndex = i - 1; // get players name index
                    leader = scores[i - 1]; // get their score for testing
                }
            }
            // set label to show leader of battle
            currentLeader.Text = string.Format("{0}", names[leaderIndex]);




        }

        /// <summary>
        /// the user wishes to continue on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            continueFight.NextRound();
            this.Hide(); // drop the view of form
            this.Dispose(); // destory form from memory
        }

    }
}
