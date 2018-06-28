using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class SetupGameForm : Form
    {
        private RadioButton[] playerButtons = new RadioButton[7]; // used to store choice of players
        private RadioButton[] roundButtons = new RadioButton[5];// used to store choice of rounds

        public SetupGameForm()
        {
            InitializeComponent();
            // setup choice arrays for radio buttons
            
            // add all player choices into array
            playerButtons[0] = players2RB;
            playerButtons[1] = players3RB;
            playerButtons[2] = players4RB;
            playerButtons[3] = players5RB;
            playerButtons[4] = players6RB;
            playerButtons[5] = players7RB;
            playerButtons[6] = players8RB;
           
            // add all round choices to array
            roundButtons[0] = rounds1RB;
            roundButtons[1] = rounds3RB;
            roundButtons[2] = rounds5RB;
            roundButtons[3] = rounds10RB;
            roundButtons[4] = roundsCustom;

            this.Show();

        }

        /// <summary>
        /// enables the users to add their own amount of rounds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void roundsCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (roundsCustom.Enabled)
            {
                // if the user wants to add a custom round amount 
                customChoice.Enabled = true;
            }
            else
            {
                // if the user wants to choose a preset round amount
                customChoice.Enabled = false;
            }
            
        }

        /// <summary>
        /// used to setup the first half of the presetup of Battle Tanks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startSetup_Click(object sender, EventArgs e)
        {
            int numberPlayers = 0; // stores how many players
            int numberRounds = 0; // stores how many rounds
            
            bool playersChoosen = false ; // stores if a choice was made
            bool roundsChoosen = false ; // stores if a choice was made
            // get users choice of players and rounds
            //find the radiobutton choosen for players
            foreach (RadioButton button in playerButtons)
            {
                if (button.Checked)
                {
                    numberPlayers = int.Parse(button.Tag.ToString());
                    playersChoosen = true;
                }
            }
            if (!playersChoosen)
            {
                // if the user didnt choose a raidobutton choice then break
                MessageBox.Show("Please choose a radiobutton to decide now many players there are \n before contining");
                return;                
            }
            //find the radiobutton choosen for rounds
            foreach (RadioButton button in roundButtons)
            {
                if (button.Checked)
                {
                    numberRounds = (int.Parse( button.Tag.ToString() ) );
                    roundsChoosen = true;
                }
            }
            if (!roundsChoosen)
            {
                // if the user didnt choose a raidobutton choice then break
                MessageBox.Show("Please choose a radiobutton to decide now many rounds there are \n before contining");
                return;
            }           

            // create a new game using the choosen players amount and round amount
            Battle game = new Battle(numberPlayers, numberRounds);
            // hide this screen and move to player setup
            PlayerSetupForm newSetup = new PlayerSetupForm(numberPlayers, game);
            newSetup.Show();
            this.Dispose();

        }

        /// <summary>
        /// Used to change the tag of custom radiobutton to choice of rounds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customChoice_ValueChanged(object sender, EventArgs e)
        {
            roundsCustom.Tag = customChoice.Value;
        }
    }
}
