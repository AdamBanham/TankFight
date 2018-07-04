using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class AIOpponent : GenericPlayer
    {
        // class variables
        private List <float> firedShotLandingX; // used to store angle the ai has fired
        private List <float> fireShotLandingY; // used to store the power of each shot
        private List <bool> shotHit; // used to store if a shot hit
        private Random myRandom; // used to make shots
        enum difficulty {easy=1,medium=2,hard=3};
        private difficulty aiSetting; // stores the type of ai Made
        private Battle currentFight;
        private float windOfLastTurn;

        /// <summary>
        /// constructor of AI player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tank"></param>
        /// <param name="colour"></param>
        public AIOpponent(string name, TankModel tank, Color colour ) : base(name, tank, colour)
        {
            // call constructors for private variables
            firedShotLandingX = new List<float>();
            fireShotLandingY = new List<float>();
            shotHit = new List<bool>();
            myRandom = new Random();
            aiSetting = (difficulty)2;
            
        }

        /// <summary>
        /// Runs when a new Round is started in a game to clear stored results
        /// </summary>
        public override void NewRound()
        {
            //clear previous round's shot fired
            firedShotLandingX.Clear();
            fireShotLandingY.Clear();
            shotHit.Clear();
            // change the aiSetting
            aiSetting = (difficulty)2;
        }

        /// <summary>
        /// Runs when a computer play gets a turn , works out how to fire shot depending on aiSetting
        /// </summary>
        /// <param name="gameplayForm"></param>
        /// <param name="currentGame"></param>
        public override void BeginTurn(BattleForm gameplayForm, Battle currentGame)
        {
            //grab the current fight
            currentFight = currentGame;

            if (aiSetting == difficulty.easy)
            {
                // this ai only knows how to fire and doesn't remember if it hits
                //take a random angle and power and fire
                TakeACrappyShot(gameplayForm);
            }
            else if (aiSetting == difficulty.medium)
            {
               
                //this ai knows how to fire , if it hit last time and where to shoot to hit a tank
                //check if last hit hit a player
                if (this.shotHit.Count()>0 && this.shotHit.Last())
                {
                    // re aim for previous shot so it might still hit tank again
                    CalForWind(currentGame);
                    // fire shot
                    gameplayForm.Fire();
                    // remove entry to reset in case shot doesnt hit
                    this.shotHit.Clear();
                }
                else // didnt hit with last shot
                {
                    //grab the current wind speed
                    windOfLastTurn = currentGame.GetWind();
                    // work out which way to shoot
                    int maxAngleFacing =  90;
                    int minAngleFacing = -90;
                    int [] fireRange;
                    // check where the tank is , if it close to the edge make sure to fire to the inside of battlfeild
                    fireRange = CalMinAndMaxAngle(minAngleFacing, maxAngleFacing, currentGame);
                    //grab min and max from fireRange
                    minAngleFacing = fireRange[0];
                    maxAngleFacing = fireRange[1];
                    // choose a random angle to fire at
                    float randomAngle = (myRandom.Next(minAngleFacing, maxAngleFacing));
                    //choose a random firepwoer
                    int randomFirepower = (myRandom.Next(10, 101));
                    //fire shot
                    TakeAimAndFire(randomAngle, randomFirepower, gameplayForm);
                }
            }
            else if (aiSetting == difficulty.hard)
            {
                //stub
            }
        }

        /// <summary>
        /// fires a shot from a tank 
        /// </summary>
        /// <param name="aim"></param>
        /// <param name="firepower"></param>
        /// <param name="gameplay"></param>
        private void TakeAimAndFire(float aim , int firepower, BattleForm gameplay)
        {
            // set firepower and angle
            gameplay.Aim((float)aim);
            gameplay.SetTankPower(firepower);
            //fire the shot
            gameplay.Fire();

        }

        /// <summary>
        /// used to work out how to hit the last target if the wind changed
        /// </summary>
        /// <param name="currentGame"></param>
        private void CalForWind (Battle currentGame)
        {
            // retake the shot but check for wind change
            // check to see if the wind is blowing more to east
            if (windOfLastTurn > currentGame.GetWind())
            {
                //check firing angle
                // if angle is positive , bullet will travel farer
                // but if angle is negative the bullet will travel less
                if (currentGame.GetPlayerTank().GetTankAngle() > 0)
                {
                    // reduce power to hit tank
                    currentGame.GetPlayerTank().SetTankPower((currentGame.GetPlayerTank().GetPower() * 98) / 100);
                }
                else
                {
                    // increase power to hit tank
                    currentGame.GetPlayerTank().SetTankPower((currentGame.GetPlayerTank().GetPower() * 102) / 100);
                }
            }
            else
            {
                // the wind is blowing more to west
                //check angle 
                if (currentGame.GetPlayerTank().GetTankAngle() <= 0)
                {
                    // tank is firing with the wind , reduce power
                    currentGame.GetPlayerTank().SetTankPower((currentGame.GetPlayerTank().GetPower() * 98) / 100);
                }
                else
                {
                    // tank is firing against the wind , increase power
                    currentGame.GetPlayerTank().SetTankPower((currentGame.GetPlayerTank().GetPower() * 102) / 100);
                }
            }
        }

        private int[] CalMinAndMaxAngle (int min , int max , Battle currentGame)
        {
            // check where the tank is , if it close to the edge make sure to fire to the inside of battlfeild
            for (int inrange = 0; inrange < 5; inrange++)
            {
                if (currentGame.GetPlayerTank().GetX() == (Battlefield.WIDTH / currentGame.NumPlayers() / 2 + inrange) ||
                    currentGame.GetPlayerTank().GetX() == (Battlefield.WIDTH / currentGame.NumPlayers() / 2 - inrange))
                {
                    // player is on the left hand side of map , should only shoot to the right
                    min = 1;
                }
                if (currentGame.GetPlayerTank().GetX() == ((Battlefield.WIDTH - (Battlefield.WIDTH / currentGame.NumPlayers()) / 2) + inrange) ||
                    currentGame.GetPlayerTank().GetX() == ((Battlefield.WIDTH - (Battlefield.WIDTH / currentGame.NumPlayers()) / 2) - inrange)
                                                          )
                {
                    // player is on the right side of map , should only shoot to the left
                    max = -5;
                }
            }
            //check if there are other players to the left
            int playersToLeft = 0;
            for (int playerNum = 1; playerNum <= currentGame.NumPlayers(); playerNum++)
            {
                // check to see if a player is left of current player
                if(currentGame.GetPlayerTank().GetX() > currentGame.GetBattleTank(playerNum).GetX()
                    &&
                    currentGame.GetBattleTank(playerNum).TankExists() )
                {
                    //increment to show that a player is to the left
                    playersToLeft++;
                }
            }
            // if no player to left exist set minimun angle to 1
            min = (playersToLeft == 0) ? (1) : (min);
            //check if there are other players to the right
            int playersToRight = 0;
            for (int playerNum = 1; playerNum <= currentGame.NumPlayers(); playerNum++)
            {
                // check to see if a player is right of current player
                if (currentGame.GetPlayerTank().GetX() < currentGame.GetBattleTank(playerNum).GetX() 
                    &&
                    currentGame.GetBattleTank(playerNum).TankExists() )
                {
                    //increment to show that a player is to the right
                    playersToRight++;
                }
            }
            // if no player to right exist set maximun angle to 1
            max = (playersToRight == 0) ? (-5) : (max);            
            return new int[2] { min, max };
        }
        /// <summary>
        /// function to fire a bad shot
        /// </summary>
        private void TakeACrappyShot(BattleForm gameplayForm)
        {
            // choose a random angle to fire at
            float randomAngle = (myRandom.Next(-90, 91));
            //choose a random firepwoer
            int randomFirepower = (myRandom.Next(10, 101));
            //fire shot
            TakeAimAndFire(randomAngle, randomFirepower, gameplayForm);
        }

        /// <summary>
        /// runs when a AI tank hits another tank , storing the power and angle
        /// </summary>
        /// <param name="angle">the angle in which a shot was fired</param>
        /// <param name="power">the amount of power behide the shot fired</param>
        public override void ReportHit(float x, float y)
        {
            // record a fired shot it
            firedShotLandingX.Add(x);
            fireShotLandingY.Add(y);
            // check all players alive in fight and see hit locations matches
            try
            {
                for (int playerNum = 1; playerNum < currentFight.NumPlayers(); playerNum++)
                {
                    // check that player is alive and shot didnt hit the tank it came from
                    if (!(currentFight.GetPlayerTank() == currentFight.GetBattleTank(playerNum)) &&
                        currentFight.GetBattleTank(playerNum).TankExists())
                    {
                        // check X location of player ( give or take -10 or 5 )
                        for (int rangeCheck = 0; rangeCheck < 10; rangeCheck++)
                            if (currentFight.GetBattleTank(playerNum).GetX() == (int)x + rangeCheck ||
                                currentFight.GetBattleTank(playerNum).GetX() == (int)x - rangeCheck)
                            {

                                this.shotHit.Add(true);
                                break;
                            }
                    }

                }
            }
             catch (IndexOutOfRangeException error)
            {
                MessageBox.Show("Error handling players in AIOpponent.ReportHit");
            }
        }
    }
}
