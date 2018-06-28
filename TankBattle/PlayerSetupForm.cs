using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class PlayerSetupForm : Form
    {
        private int totalPlayers; // stores how many players need to setup
        private int setupPlayers; // stores how many players are good to go
        private GenericPlayer[] Players; // stores the reference to each player
        private Battle currentBattle; // stores the reference to the fight
        private Color background; // stores the colour of background
        private Bitmap tankImage; // stores a new image

        public PlayerSetupForm(int numPlayers, Battle newBattle)
        {
            InitializeComponent();
            // setup default settings of form
            totalPlayers = numPlayers;
            setupPlayers = 0;
            currentBattle = newBattle;
            Players = new GenericPlayer[totalPlayers];
            // set the background to the first player
            SetBackground(setupPlayers + 1);
            // create images of tanks for each choice        
            CreateTank();
            // change the title to show current player number
            ChangeTitle();
            // change the label to show player number
            ChangePlayerLabel();
            // setup progress bar to track number of players left to setup
            this.setupProgress.Maximum = numPlayers - 1;
            this.setupProgress.Value = 0;
            //update form for user
            this.Update();
        }
        /// <summary>
        /// updates the player's name label to show current player number
        /// </summary>
        public void ChangePlayerLabel()
        {
            this.playerName.Text = string.Format("Player #{0}'s name is :", setupPlayers + 1);
        }

        /// <summary>
        /// creates new tank iamges attached to radiobuttons in player's colour
        /// </summary>
        /// <param name="tank">tank to make</param>
        public void CreateTank ()
        {
            TankModel tank;

            tank = new BasicTank();
            tankImage = tank.CreateTankBMP(this.BackColor, 45);
            basicTank.Image = tankImage;

            tank = new HeavyTank();
            tankImage = tank.CreateTankBMP(this.BackColor, 45);
            this.HeavyTank.Image = tankImage;

            tank = new ArmoredTank();
            tankImage = tank.CreateTankBMP(this.BackColor, 45);
            this.armoredTank.Image = tankImage;

            tank = new QuickFireTank();
            tankImage = tank.CreateTankBMP(this.BackColor, 45);
            this.quickFireTank.Image = tankImage;


        }  


        /// <summary>
        /// updates the current title of form to match current player number
        /// </summary>
        public void ChangeTitle ()
        {
            this.Text = string.Format("Setup Player#{0}",setupPlayers+1);
        }


        /// <summary>
        /// sets the background of form to the player's colour
        /// </summary>
        /// <param name="playerNum">target player number</param>
        public void SetBackground(int playerNum)
        {
            // set the background to corsponding colour of player's number
            switch (playerNum)
            {
                case 1:
                    this.BackColor = Color.Azure;
                    return;
                case 2:
                    this.BackColor=  Color.Red;
                    return;
                case 3:
                    this.BackColor = Color.LightSeaGreen;
                    return;
                case 4:
                    this.BackColor = Color.Yellow;
                    return;
                case 5:
                    this.BackColor = Color.PeachPuff;
                    return;
                case 6:
                    this.BackColor = Color.CadetBlue;
                    return;
                case 7:
                    this.BackColor = Color.ForestGreen;
                    return;
                case 8:
                    this.BackColor = Color.Fuchsia;
                    return;
                default:
                    this.BackColor = Color.WhiteSmoke;
                    return;
            }
            //set the progress bar to same colour
            setupProgress.BackColor = this.BackColor;
        }

        /// <summary>
        /// used to move to the next player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextPlayer_Click(object sender, EventArgs e)
        {
            string playerName;
            int tankChoice = 1;

            //check that a choice has been made for both controller and tank
            if (!(humanChoice.Checked || computerChoice.Checked))
            {
                MessageBox.Show("Looks like you haven't setup a controller for this player \n please choose either human or computer");
                return;
            }
            if (!(basicTank.Checked || HeavyTank.Checked || quickFireTank.Checked || armoredTank.Checked))
            {
                MessageBox.Show("Looks like you haven't setup a tank for this player \n please select one to continue");
                return;
            }
            //find the choice of tank for this player
            if (basicTank.Checked)
            {
                tankChoice = 1;
            }
            if (quickFireTank.Checked)
            {
                tankChoice = 2;
            }
            if (HeavyTank.Checked)
            {
                tankChoice = 3;
            }
            if (armoredTank.Checked)
            {
                tankChoice = 4;
            }
            //stub more choices to come for future references

            //find the player's name
            if (inputtedName.Text == "")
            {
                // if no name inputted then go to basic name
                playerName = string.Format("Player {0}", setupPlayers + 1);
            }
            else
            {
                //if name inputted then set player to that name
                playerName = inputtedName.Text;
            }

            // add player to array of GenericPlayers
            if (humanChoice.Checked)
            {
                // Human player made 
                Players[setupPlayers] = new PlayerController(playerName, TankModel.GetTank(tankChoice), Battle.TankColour(setupPlayers + 1));
            }
            if (computerChoice.Checked)
            {
                Players[setupPlayers] = new AIOpponent(playerName, TankModel.GetTank(tankChoice), Battle.TankColour(setupPlayers + 1));
            }

            // add entry of new player into the game
            currentBattle.SetPlayer(setupPlayers+1, Players[setupPlayers]);

            // now move to the next player to setup

            setupPlayers++;

            //check to see if the next player is the last player
            if (setupPlayers+1 == totalPlayers)
            {
                //change the nextplayer text to show that it will start the game on next click
                NextPlayer.Text = "Start Game!!!";
                NextPlayer.ForeColor = Color.Red;
            }
            //check to see if all players are setup
            if (setupPlayers == totalPlayers)
            {
                //start the game
                currentBattle.NewGame();
                this.Dispose();
                return;
            }

            //reset for next player
            Reset();
        }

        /// <summary>
        /// setups the form for a new player
        /// </summary>
        public void Reset ()
        {
            //update title
            ChangeTitle();
            //update background
            SetBackground(setupPlayers+1);
            //update player label
            ChangePlayerLabel();
            //update tank colour
            CreateTank();
            //empty text feild for player name
            inputtedName.Text = "";
            //increment progress bar
            this.setupProgress.PerformStep();
            this.setupProgress.Update();
            //update the form for user
            this.Update();
        }
    }
}
