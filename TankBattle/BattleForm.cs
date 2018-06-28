using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class BattleForm : Form
    {
        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Battle currentGame;
        GameplayTank currentTank;
        GenericPlayer currentPlayer;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        public BattleForm(Battle game)
        {
            //dont touch

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            //dont touch above code

            //set default vales of battle form
            //set current game to battleform
            currentGame = game;
            //randomanly chose a background image and colour
            //set the sets to be choosen from randomness
            string[] randomBackground = { "Images\\background1.jpg",
                                          "Images\\background2.jpg",
                                          "Images\\background3.jpg",
                                          "Images\\background4.jpg"};

            Color[] randomColours = { Color.FromArgb(255, 0, 0, 0),
                                      Color.FromArgb(255, 73, 58, 47),
                                      Color.FromArgb(255, 148, 116, 93),
                                      Color.FromArgb(255, 133, 119, 109) };
            //choose a random set
            Random myRandom = new Random();
            int randomChoice = myRandom.Next(0, randomBackground.Length);
            //set choice
            landscapeColour = randomColours[randomChoice];
            backgroundImage = Image.FromFile(randomBackground[randomChoice]);
            
            // call intialization 

            InitializeComponent();

            //do more after
            //set graphics
            backgroundGraphics = InitBuffer();
            gameplayGraphics = InitBuffer();
            //draw the background to the user's view
            DrawBackground();
            //draw the gameplay of the areana now
            DrawGameplay();
            // start the first turn of the round
            NewTurn();

        }

        /// <summary>
        /// method is used to draw the gameplay elements of the screen
        /// (that is, everything but the terrain).
        /// </summary>
        private void DrawGameplay()
        {
            //render background to gameplay
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            //draw the players tanks
            currentGame.DrawPlayers(gameplayGraphics.Graphics, displayPanel.Size);
            // draw any effects in play to gameplay
            currentGame.RenderEffects(gameplayGraphics.Graphics, displayPanel.Size);
        }


        private void NewTurn()
        {
            // find the next player and their tank
            currentTank = currentGame.GetPlayerTank();
            currentPlayer = currentTank.GetPlayerNumber();
            //set the title of a form to current round of total rounds
            this.Text = string.Format("Tank battle - Round {0} of {1}", currentGame.CurrentRound(), currentGame.GetNumRounds());
            // set backcolor of controlpanel to currentplayers colour
            controlPanel.BackColor = currentPlayer.PlayerColour();
            // show the current player's name
            currentPlayerLabel.Text = currentPlayer.Name();
            // set angle to current gameplaytank's angle
            this.Aim(currentTank.GetTankAngle());
            //show current tank's power
            this.SetTankPower(currentTank.GetPower());
            //show current windspeed
            //postive values should state its a eastly wind while negative values come from windly west
            if (currentGame.GetWind() >= 0)
            {
                windspeedLabel.Text = string.Format("{0} E", currentGame.GetWind());
            }
            else
            {
                windspeedLabel.Text = string.Format("{0} W", (currentGame.GetWind()*-1)); // times by a negative number to shows a flat value for wind
            }
            //clear weapon choices in weaponSelect
            weaponSelect.Items.Clear();
            // find all weapons available to current tank
            foreach (string weapon in currentTank.GetTank().ListWeapons())
            {
                // add each weapon to selection choice of weaponSelect
                weaponSelect.Items.Add(weapon);
            }
            //set the current weapon to be used of current tank
            SetWeapon(currentTank.GetWeapon());

            if (currentPlayer is PlayerController)
            {
                // give controls for firing a weapon
                currentPlayer.BeginTurn(this, currentGame);
            }
            else if (currentPlayer is AIOpponent)
            {
                //run the firing command on currentplayer
                currentPlayer.BeginTurn(this, currentGame);
            }
                           
        }



        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// turns on the controls for human player use
        /// </summary>
        public void EnableTankButtons()
        {
            // turn on all buttons in control panel
            controlPanel.Enabled = true;
        }

        /// <summary>
        /// alters the value of angleUpDown for controling the angle of a shot
        /// </summary>
        /// <param name="angle">new angle</param>
        public void Aim(float angle)
        {
            angleUpDown.Value = (int)angle;

        }

        /// <summary>
        /// alters the value of powerTrack for firing a shot
        /// </summary>
        /// <param name="power">new power setting</param>
        public void SetTankPower(int power)
        {
            powerTrack.Value = power;
        }

        /// <summary>
        /// changes the select item in weaponSelect
        /// </summary>
        /// <param name="weapon">index of new weapon choice</param>
        public void SetWeapon(int weapon)
        {
            weaponSelect.SelectedIndex = weapon;
            weaponSelect.Update();
        }

        /// <summary>
        /// fires a shot for the current player
        /// </summary>
        public void Fire()
        {
            //disable the control planet 
            controlPanel.Enabled = false;
            //FIRE THE SHOT !!!!
            currentGame.GetPlayerTank().Fire();
            
            //start the timer
            battleformTimer.Enabled = true;
        }
        
        /// <summary>
        /// draws the terrain of the battle in its current state.
        /// </summary>
        private void DrawBackground()
        {
            //set variables needed to draw terrain
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));
            Random myRandom = new Random();
            Battlefield battlefield = currentGame.GetLevel();
            //create a brush for the color of terrain
            Brush brush = new SolidBrush(Color.WhiteSmoke);
            int random; // stores randomize colour choice

            //test for terrain inside the battle
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                // go along each row looking for giving each block a colour
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    //top of the terrain should be white for skyscrappers
                    if(y <= 10)
                    {
                        brush = new SolidBrush(Color.WhiteSmoke);
                    }
                    //following the skyscrappers tops , their should be lights and other colour to represent window glare
                    if (Battlefield.HEIGHT / 3.6 > y && y > 10)
                    {
                        random = myRandom.Next(0, 16);
                        switch (random)
                        {
                            default:
                                brush = new SolidBrush(Color.Silver);
                                break;
                            case (1):
                                brush = new SolidBrush(Color.SlateGray);
                                break;
                            case (2):
                                brush = new SolidBrush(Color.LightYellow);
                                break;
                        }

                    }
                    //following the body of a building their should be a green landing to be the base of a building
                    if (y >= Battlefield.HEIGHT / 3.6 && y < Battlefield.HEIGHT / 3.3)
                    {
                        random = myRandom.Next(0, 15);
                        switch (random)
                        {
                            default:
                                brush = new SolidBrush(Color.ForestGreen);
                                break;
                            case (1):
                                brush = new SolidBrush(Color.Silver);
                                break;
                            case (2):
                                brush = new SolidBrush(Color.SaddleBrown);
                                break;
                        }
                        
                    }
                    // now the terrain in below ground level , shows diffferent mineral desposits and roots growing underground
                    if (y > Battlefield.HEIGHT / 3.3 && Battlefield.HEIGHT / 2 > y)
                    {
                        random = myRandom.Next(0, 25);
                        switch (random)
                        {
                            default:
                                brush = new SolidBrush(Color.SaddleBrown);
                                break;
                            case (1):
                                brush = new SolidBrush(Color.LightGray);
                                break;
                            case (2):
                                brush = new SolidBrush(Color.SlateGray);
                                break;
                            case (3):
                                brush = new SolidBrush(Color.DarkSlateGray);
                                break;
                            case (4):
                                brush = new SolidBrush(Color.ForestGreen);
                                break;
                        }
                    }

                    // this terrain is deep in the ground and should be only ground and hard desposits
                    if (Battlefield.HEIGHT /2 <= y && y < Battlefield.HEIGHT * .80)
                    {
                        random = myRandom.Next(1, 25);
                        switch (random)
                        {
                            
                            default:
                                brush = new SolidBrush(Color.SaddleBrown);
                                break;
                            case (2):
                                brush = new SolidBrush(Color.DarkSlateGray);
                                break;
                            case (3):
                                brush = new SolidBrush(Color.SlateGray);
                                break;
                            
                        }
                    
                        
                    }
                    // terrain is nearing the bottom of the map and black should be introduced
                    if (y >= Battlefield.HEIGHT*.80 && y < Battlefield.HEIGHT * .91)
                    {
                        random = myRandom.Next(1, 5);
                        switch (random)
                        {

                            default:
                                brush = new SolidBrush(Color.DarkSlateGray);
                                break;
                            case (2):
                                brush = new SolidBrush(Color.SaddleBrown);
                                break;
                            case (1):
                                brush = new SolidBrush(Color.SaddleBrown);
                                break;
                            case (3):
                                brush = new SolidBrush(Color.Black);
                                break;
                        }




                    }
                    // at the bottom of the map , should only be blackness and a bit of hard desposits
                    if (y >= Battlefield.HEIGHT * .91)
                        {
                        random = myRandom.Next(1, 10);
                        switch (random)
                        {

                            default:
                                brush = new SolidBrush(Color.Black);
                                break;
                            //case (1):
                            //    brush = new SolidBrush(Color.SaddleBrown);
                            //    break;
                            case (3):
                                brush = new SolidBrush(Color.DarkSlateGray);
                                break;
                        }
                        
                        }
                    // draw the block to the graphics to be rendered on the screen
                    if (battlefield.Get(x, y))
                    {
                        // find the points of the block scaled to the displayPanel size
                        int drawX1 = displayPanel.Width * x / Battlefield.WIDTH;
                        int drawY1 = displayPanel.Height * y / Battlefield.HEIGHT;
                        int drawX2 = displayPanel.Width * (x + 1) / Battlefield.WIDTH;
                        int drawY2 = displayPanel.Height * (y + 1) / Battlefield.HEIGHT;
                        //draw block
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitBuffer()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        /// <summary>
        /// when the fire button is press , fires the current tanks bullet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Fire();
        }

        /// <summary>
        /// when the angle is changed on the form the current tanks aim is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void angleUpDown_ValueChanged(object sender, EventArgs e)
        {
            currentTank.Aim((float)angleUpDown.Value);
            // update the tank graphics to match the angle of the barrel of tank
            DrawGameplay();
            //refresh the screen
            displayPanel.Invalidate();
            currentTank.Paint(gameplayGraphics.Graphics, displayPanel.Size);
        }


        /// <summary>
        /// when the power bar is adjusted , the current tank power is set to the adjustment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void powerTrack_ValueChanged(object sender, EventArgs e)
        {
            currentTank.SetTankPower(powerTrack.Value);
            // set the label to show the current power of shot
            powerLabel.Text = string.Format("Power: {0}", powerTrack.Value);
        }


        /// <summary>
        /// when the weapon choice is changed , updates the current tanks choice of weapon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weaponSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTank.SetWeapon(weaponSelect.SelectedIndex);
            //get rid of focus so keyboard controls dont change the select weapon
            this.ActiveControl = null;
        }

        /// <summary>
        /// handles all the drawing of effects , bullets and manages the animations of a turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void battleformTimer_Tick(object sender, EventArgs e)
        {
            bool animationsDone;
            bool continueFight;
            // run all current effects to see if graphics are all done
            animationsDone = currentGame.ProcessWeaponEffects();
            //store results
            //check results from effects
            if ( !animationsDone)
            {
                //animations are finished
                //run clean up from attack and store result 
                animationsDone = currentGame.ProcessGravity();
                //update screen
                DrawBackground();
                DrawGameplay();
                // trigger redraw of screen
                displayPanel.Invalidate();
                //check to see if anything moved from gravity effects from clean up
                if (animationsDone)
                {
                    //if tanks moved from gavity
                    return;
                }
                else
                {
                    // nothing moved from gavity 
                    battleformTimer.Enabled = false;
                    // move through the game cycle as currentplayers turn is over
                    continueFight = currentGame.EndTurn();
                    if (continueFight)
                    {
                        // round not over yet or no winner
                        NewTurn();

                    }
                    else
                    {
                        // round over , winner is clear
                        //close form
                        Dispose();
                        //move to the next round
                        //currentGame.NextRound();
                        Scoreboard thescore = new Scoreboard(currentGame);
                        thescore.Show();
                        return;

                    }// end of continueFight check

                }// end of animationsDone gravity check

            } // end of animationsDone chech
            else
            {
                // attack animations are still ongoing
                DrawGameplay();
                // draw animations and trigger redraw on screen
                displayPanel.Invalidate();
                return;
            }
        }

        /// <summary>
        /// allows the use of a keyboard to control the firing of a tank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keypressed"></param>
        private void BattleForm_KeyDown(object sender, KeyEventArgs keypressed )
        {
           
           if (keypressed.KeyCode == Keys.Enter)
            {
                // if enter was hit then the player wants to fire their bullet
                if (controlPanel.Enabled)
                {
                    //if a human player is active then allow a shot to be fired
                    Fire();
                }
            }
           if (keypressed.KeyCode == Keys.Left)
            {
                // if key pressed was left then reduce firing angle
                // but check it doesnt go outside bounds of -90
                if (currentTank.GetTankAngle()-5 >= -90 )
                {
                    currentTank.Aim(currentTank.GetTankAngle() - 5);
                    currentTank.Paint(gameplayGraphics.Graphics, displayPanel.Size);
                }                
                // update form with new angle
                angleUpDown.Value = (decimal)currentTank.GetTankAngle();
            }

           if (keypressed.KeyCode == Keys.Right)
            {
                // if key pressed was right then increase firing angle
                //but check it doesn't go outsides bounds of 90
                if (currentTank.GetTankAngle()+5 <= 90)
                {
                    currentTank.Aim(currentTank.GetTankAngle() + 5);
                    currentTank.Paint(gameplayGraphics.Graphics, displayPanel.Size);
                }
                //update form with new angle
                angleUpDown.Value = (decimal) currentTank.GetTankAngle();
            }
           if (keypressed.KeyCode == Keys.Up)
            {
                //if the key pressed was up then increase firing power
                // but check it doesn't go outside bounds of 100%
                if (currentTank.GetPower() + 2 <= 100 )
                {
                    currentTank.SetTankPower(currentTank.GetPower() + 2);
                }
                //update form with new power setting
                powerTrack.Value = (currentTank.GetPower());
                powerLabel.Text = string.Format("Power: {0}", currentTank.GetPower());
            }
            if (keypressed.KeyCode == Keys.Down)
            {
                //if the key pressed was up then decrease firing power
                // but check it doesn't go outside bounds of 0%
                if (currentTank.GetPower() - 1 >= 0 )
                {
                    currentTank.SetTankPower(currentTank.GetPower() - 2);
                }
                //update form with new power setting
                powerTrack.Value = (currentTank.GetPower());
                powerLabel.Text = string.Format("Power: {0}", currentTank.GetPower());
            }


        }

        /// <summary>
        /// resizing controls for the battlefield
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BattleForm_Resize(object sender, EventArgs e)
        {
            //update the size of the display panel to match the new size
            displayPanel.Width = this.Width;
            displayPanel.Height = this.Height - controlPanel.Height;
            //update the user's view of screen
            displayPanel.Update();
            // change the parameters for drawing background
            levelHeight = displayPanel.Height;
            levelWidth = displayPanel.Width;

            //set graphics
            backgroundGraphics = InitBuffer();
            gameplayGraphics = InitBuffer();
            //draw the background to the user's view
            DrawBackground();
            //draw the gameplay of the areana now
            DrawGameplay();
            // update user's view of screen
            displayPanel.Update();
            //render changes of screen
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);


        }
    }
}
