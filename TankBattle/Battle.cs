using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TankBattle
{
    public class Battle
    {
        // private variables for class
        private GenericPlayer[] Players; // used to store number of players
        private int numRounds; // used to store number of rounds to be played
        private List<AttackEffect> theAttackEffects;
        private int currentRound; // stores the number of played rounds
        private int startingPlayer; // stores the current starting player for each round
        private int currentPlayer;// stores whos turn it is during a round
        private Battlefield newBattlefield; // fightzone in a round
        private GameplayTank[] Tanks; // tanks in a round
        private int windSpeed; // the current windspeed
        private BattleForm roundBattleForm;

        

        public Battle(int numOfPlayers, int numOfRounds)
        {   
            // test inputs , so players and rounds are in accapetable ranges
            if (!(2 <= numOfPlayers && numOfPlayers <= 8))
            {
                throw new NotImplementedException();
            }

            if(!(1 <= numOfRounds && numOfRounds <= 100))
            {
                throw new NotImplementedException();
            }

            Players = new GenericPlayer[numOfPlayers]; // set the amount of players
            numRounds = numOfRounds; // set the rounds
            theAttackEffects = new List<AttackEffect>(); // create array of attack effects
        }


        /// <summary>
        /// returns the number of players in this battle
        /// </summary>
        /// <returns></returns>
        public int NumPlayers()
        {
            return Players.Count();
        }

        /// <summary>
        /// returns the current round number
        /// </summary>
        /// <returns></returns>
        public int CurrentRound()
        {
            return currentRound;
        }

        /// <summary>
        /// returns the number of rounds to be played for this battle
        /// </summary>
        /// <returns></returns>
        public int GetNumRounds()
        {
            return numRounds;
        }

        /// <summary>
        /// sets which slot the player is in the players array
        /// </summary>
        /// <param name="playerNum"></param>
        /// <param name="player"></param>
        public void SetPlayer(int playerNum, GenericPlayer player)
        {
            // set the player in the players array , playerNum is indexed from 1 must be reduced by one to set in array which is indexed from 0
            Players[playerNum - 1] = player;
        }

        /// <summary>
        /// returns the player in players array via the playerNum
        /// </summary>
        /// <param name="playerNum"></param>
        /// <returns>returns the reference of a player</returns>
        public GenericPlayer GetPlayerNumber(int playerNum)
        {
            // returns a player given a player number , to cal for index change 
            return Players[playerNum - 1];
        }

        /// <summary>
        /// returns the tank for the playernum given.
        /// </summary>
        /// <param name="playerNum">Wanted players tank to return</param>
        /// <returns>a reference to a GameplayTank for player</returns>
        public GameplayTank GetBattleTank(int playerNum)
        {
            try
            {
                return Tanks[playerNum - 1];
            }
            catch (IndexOutOfRangeException error)
            {
                MessageBox.Show(error.StackTrace);
                throw error;
            }
        }

        /// <summary>
        /// returns a colour given a player number
        /// </summary>
        /// <param name="playerNum"></param>
        /// <returns>the colour based off the playerNum</returns>
        public static Color TankColour(int playerNum)
        {
            // check playernumber and return a colour
            switch (playerNum)
            {
                case 1:
                    return Color.Azure;
                case 2:
                    return Color.Red;
                case 3:
                    return Color.LightSeaGreen;
                case 4:
                    return Color.Yellow;
                case 5:
                    return Color.PeachPuff;
                case 6:
                    return Color.CadetBlue;
                case 7:
                    return Color.ForestGreen;
                case 8:
                    return Color.Fuchsia;
                default:
                    return Color.WhiteSmoke;                    
            }
        }


        /// <summary>
        /// given a number of players , returns an array giving the horizontal positions of those players on the map
        /// </summary>
        /// <param name="numPlayers"></param>
        /// <returns>a array of x positions for the numPlayers</returns>
        public static int[] CalcPlayerLocations(int numPlayers)
        {
            int[] positions = new int[numPlayers]; // needs to be sort from smallest to largest
            int spacing = Battlefield.WIDTH; // get the length of the battlefield and accounts the models of tanks
            // work out the spacing of each placement
            // every player should be the same distance from each other
            //but the left most and right most must be the same distance apart from the left and right boundaries ( half the normal spacing )

            spacing = spacing / (numPlayers); //  work out equal spacing

            positions[0] = spacing / 2; // first player is set half an spacing away from the left wall

            for (int i = 1; i < positions.Length; i++) // for every other member , add a spacing to the previous placement distance plus the length of a tank model
            {
                positions[i] = positions[i - 1] + spacing ; 
            }
            
            return positions;
        }

        /// <summary>
        /// reorders an array , so the order of all elements are changed 
        /// </summary>
        /// <param name="array"></param>
        public static void RandomReorder(int[] array)
        {
            bool movedElement = false;
            int[] randomArray = new int[array.Length]; 
            int selectedInput;
            bool[] usedPosition = new bool[array.Length];
            Random randomValue = new Random();

            if (array.Length > 1) // if the array has more than one entry then swap each element to a new element
            {
                
                movedElement = false;
                // for each element in the array place it into the new array in a different position.
                while (!(movedElement))
                {
                    randomArray = new int[array.Length];
                    // set all usedpositions to false
                    for (int i = 0; i < usedPosition.Length; i++)
                    {
                        usedPosition[i] = false;
                    }

                    for (int i = 0; i < array.Length; i++)
                    {
                        selectedInput = randomValue.Next(0, array.Length); // generates a new number for the element from the start of the array to the less than the max 
                                                                           // checked that the element isnt going back into the same place in the array 
                                                                           // check selected input is empty
                        while ((usedPosition[selectedInput]))
                        {

                            selectedInput = randomValue.Next(0, array.Length); // reroll number
                        }
                        if (!(i == selectedInput))
                        {
                            movedElement = true;
                        }
                        //add the element into the reorder array
                        //change the position to used
                        randomArray[selectedInput] = array[i];
                        usedPosition[selectedInput] = true;
                    }
                }

                for (int i = 0; i < randomArray.Length; i++)  // set the array to the reordered array
                    {
                    array[i] = randomArray[i];
                    }

            }
            else
            {
                // an array of one element cant be changed;
            }
        }

        /// <summary>
        /// Begins a new game
        /// </summary>
        public void NewGame()
        {
            currentRound = 1; // sets the round to one
            startingPlayer = 0; //the first player made will start first
            StartRound();
        }

        /// <summary>
        /// Begins a new round of gameplay , which consists of multiple turns
        /// </summary>
        public void StartRound()
        {
            int[] positions;
            
            int minWindSpeed = -100;
            int maxWindSPeed = 100;
            Random randomvalue = new Random();
            currentPlayer = startingPlayer; // sets the first turn to the starting player
            newBattlefield = new Battlefield() ;
            positions = CalcPlayerLocations(Players.Length);

            foreach (GenericPlayer player in Players)
            {
                player.NewRound(); // setup each player for a new round
            }

            RandomReorder(positions); //shuffle array of positions
            Tanks = new GameplayTank[Players.Length]; // set the number of tanks to the number of players

            for ( int i = 0; i < Tanks.Length; i++ ) // initialising Tanks array 
            {
                Tanks[i] = new GameplayTank(Players[i], // the player
                                            positions[i], // random Hoz position
                                            newBattlefield.PlaceTankVertically(positions[i]),// cals Vert position
                                            this // reference to this class
                                            );
            }
            windSpeed = randomvalue.Next(minWindSpeed, maxWindSPeed); // set the current windspeed
            roundBattleForm = new BattleForm(this) ;
            roundBattleForm.Show();
            // check to see if a player is human 
            if (Players[currentPlayer] is PlayerController)
            {
                // activite controls for firing a weapon
                Players[currentPlayer].BeginTurn(roundBattleForm, this);
            }
        }

        /// <summary>
        /// returns the current battlefield used by the game
        /// </summary>
        /// <returns></returns>
        public Battlefield GetLevel()
        {
            return newBattlefield;
        }

        /// <summary>
        /// Tells all existing tanks to draw themselves.
        /// </summary>
        /// <param name="graphics">Used in paint method</param>
        /// <param name="displaySize">Used in paint method</param>
        public void DrawPlayers(Graphics graphics, Size displaySize)
        {
            foreach (GameplayTank tank in Tanks) // loop through each tank
            {
                if (tank.TankExists()) // if tank is alive
                {
                    // draw tank
                    tank.Paint(graphics, displaySize);
                }
            }
        }

        /// <summary>
        /// returns the gameplay tank associated with the current player.
        /// </summary>
        /// <returns>a gameplayTank associated with a player</returns>
        public GameplayTank GetPlayerTank()
        {
            return Tanks[currentPlayer];
        }

        /// <summary>
        /// adds the given attack effect to AttackEffects
        /// </summary>
        /// <param name="weaponEffect"> This weapon effect is added</param>
        public void AddEffect(AttackEffect weaponEffect)
        {
            theAttackEffects.Add(weaponEffect); // adds the element to the list

            // call setcurrentgame on new element
            // finds the last element of list ( the newly added attackeffect and calls function
            theAttackEffects[(theAttackEffects.Count-1)].SetCurrentGame(this); 
        }

        /// <summary>
        /// loops through AttackEffects and calls tick on each 
        /// </summary>
        /// <returns>reutrns true if ticked were called</returns>
        public bool ProcessWeaponEffects()
        {
            bool tickCalled = false; // bool to store if ticks occured
            int ticked=0; // counter for ticks
            //loop though attack effects
            //foreach (AttackEffect effect in theAttackEffects)
            for (int i = 0; i < theAttackEffects.Count();i++)
            {
                theAttackEffects[i].Tick(); // call tick for element
                ticked++;
            }
            // check to see if ticks occured
            if (ticked > 0)
            {
                tickCalled = true; // ticked occured change bool
            }

            return tickCalled; 
        }

        /// <summary>
        ///  calls all effects in use and paints each one
        /// </summary>
        /// <param name="graphics">Used in paint method to draw effect</param>
        /// <param name="displaySize">Used in paint method to draw effect</param>
        public void RenderEffects(Graphics graphics, Size displaySize)
        {
            // loop through all elements in attackeffects
            foreach (AttackEffect effect in theAttackEffects)
            {
                // call paint for each element
                effect.Paint(graphics, displaySize);
            }
        }

        /// <summary>
        /// removes referenced weapon effect from attackeffects
        /// </summary>
        /// <param name="weaponEffect">remove this effect</param>
        public void RemoveWeaponEffect(AttackEffect weaponEffect)
        {
            theAttackEffects.Remove(weaponEffect); // removes the element
        }

        /// <summary>
        /// This method checks if a Bullet at proX,proY will hit something
        /// </summary>
        /// <param name="projectileX">current X cord of bullet</param>
        /// <param name="projectileY">current Y cord of bullet</param>
        /// <returns>returns true if terrain or a tank is at location X,Y</returns>
        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
            //check bounds of proX , proY are inside map boundaires
            if (!(projectileX > 0 || Battlefield.WIDTH > projectileX))
                      {
                return false;
                }
            if (!(projectileY > 0 || Battlefield.HEIGHT > projectileY))
            {
                return false;
            }
            // check if battlefield has something at location
            if (newBattlefield.Get((int)projectileX,(int)projectileY))
            {
                return true; // this bullet will hit terrain
            }
            // check if a tank is at location
            foreach (GameplayTank tank in Tanks)
            {
                if (!((Tanks[currentPlayer]) == tank) && tank.TankExists() ) // check that the tank firing isnt checked
                {
                    // tanks have width and hieght and each combination needs to be checked against proX and proY
                    for (int Height = 0; Height <= TankModel.HEIGHT; Height++) //check Height
                    {
                        for (int Width = 0; Width <= TankModel.WIDTH; Width++) //check Width
                        {
                            if ((tank.GetX() + Width) == (int)projectileX && (tank.Y()+Height) == (int)projectileY) // check locations against each other
                            {
                             return true; // this bullet will hit a tank
                            }
                        }
                    }
                }
            }
            return false; // if none of the conditions are true then bullet will not hit
        }

        /// <summary>
        /// This method inflicts up to explosionDamage damage on any tanks within explosion radius.
        /// </summary>
        /// <param name="damageX">bullet hit X location </param>
        /// <param name="damageY">bullet hit Y location </param>
        /// <param name="explosionDamage">Damage done</param>
        /// <param name="radius">Explosion circle</param>
        public void DamagePlayer(float damageX, float damageY, float explosionDamage, float radius)
        {
            string floatingText; // used to store the display text of a hit
            float tankX; //used to store centre of tank
            float tankY; // used to store centre of tank
            float distance; // used to store distance between explosion and tank
            double damage; // used to store the damage done to tank
            radius = (float)(radius * 1.05); // adjust radius to match visuals of explosion
            
            foreach (GameplayTank tank in Tanks) // loop through each tank and check if it is inside explosion
            {
                damage = 0; //reset damage
                //reset variables for new tank
                tankX = 0; tankY = 0; distance = 0; damage = 0;
                if (tank.TankExists() && !(tank == this.GetPlayerTank()) ) // tank still in game and that tank isn't the current tank
                {
                    // store cordinates as float for accuratity
                    tankX = (float)(tank.GetX()); // find the centre of a tank by adding half the width
                    tankY = (float)(tank.Y()); // find the centre of a tank by adding half the height 
                    // calculate distance between bullet hit (damageX,damageY) and tank
                    // using elucian distnat = sqrt ((x1-x2,2)+(y1-y2,2))
                    distance =(float) Math.Sqrt(  Math.Pow((damageX - tankX+TankModel.WIDTH/2), 2) +
                                                  Math.Pow((damageY - tankY-TankModel.HEIGHT/2), 2)
                                                  );
                    if (distance <= radius) // if tank is within the radius of explosion
                    {
                        // if tank is over half the radius away
                        damage = (distance > radius / 2) ? (explosionDamage * .35) : ((double)explosionDamage);
                        tank.DamagePlayer((int)damage);
                        // create the text to display what type of hit occured
                        floatingText = (damage == (double)explosionDamage) ? ("Perfect Hit!") : ("Near Miss!");
                        HitText displayText = new HitText(floatingText, damageX, damageY);
                        AddEffect(displayText);
                    }
                }
            }
        }

        /// <summary>
        /// Clears/moves any floating terrain and/or tanks that are floating in the air down to the lowest terrain, returns false if nothing is moved
        /// </summary>
        /// <returns></returns>
        public bool ProcessGravity()
        {
            bool somethingMoved = false; // used to tell if anything has been moved
            // check the terrain and change bool if anything moved
            somethingMoved = newBattlefield.ProcessGravity();
            foreach (GameplayTank tank in Tanks) // check tanks floating in the air
            {
                //processGravity on each tank
                somethingMoved = (tank.ProcessGravity()) ? (true) : (somethingMoved);
                //change moved if tank dropped
            }
            return somethingMoved;
        }


        /// <summary>
        /// This method determines if another round needs to happen 
        /// </summary>
        /// <returns> if 0 or 1 tanks are left returns false otherwise it will return true</returns>
        public bool EndTurn()
        {
            int tanksAlive = 0;
            bool endRound = false;

            foreach( GameplayTank tank in Tanks) // check how many tanks are alive
            {
                // if alive increment total tanksAlive
                tanksAlive = (tank.TankExists()) ? (tanksAlive+1) : (tanksAlive);
            }
            if (tanksAlive > 1) // more than one player still alive
            {
                do
                {
                    currentPlayer++; // move to the next player
                                     // check that index doesnt go outside bounds of player array
                    if (currentPlayer > Players.Length-1)
                    {
                        currentPlayer = 0; //if it does reset to the first player
                    }
                }while (!(Tanks[currentPlayer].TankExists())); // check to see if player's tank is still alive

                Random myRandom = new Random(); // used to chanage windspeed

                //change up windspeed between -10 and 10 of current value
                windSpeed = windSpeed + myRandom.Next(-3, 3);
                //check that windspeed doesn't leave range of -100 to 100
                if (windSpeed < -100)
                {
                    windSpeed = -100;
                }
                if (windSpeed > 100)
                {
                    windSpeed = 100;
                }

                // round is still going so change endRound to true
                endRound = true;
            }
            else // if one tank remaining calls checkWinner
            {
                this.CheckWinner();
            }
            return endRound;
        }

        /// <summary>
        /// This method finds out which player won the round and rewards that player with a point
        /// </summary>
        public void CheckWinner()
        {
            // find the player with a tank still alive
            foreach (GameplayTank tank in Tanks)
            {
                if (tank.TankExists()) // tank is alive
                {
                    tank.GetPlayerNumber().AddScore(); // add score to the owner of tank
                }
            }
        }

        /// <summary>
        /// moves on the next round and checks if all rounds have been played
        /// </summary>
        public void NextRound()
        {
            currentRound++; // move to the next round
            if (currentRound <= numRounds) // check to see if all rounds have been played
            {
                startingPlayer++; // set new starting player
                if (startingPlayer >= Players.Length) // check that the starting player isnt outside bounds of array
                {
                    startingPlayer = 0; // reset if outside bounds
                }
                //call next round
                this.StartRound();
            }
            else // all rounds have been played
            {
                // show IntroForm again
                
            }
        }
        
        /// <summary>
        /// returns the current windspeed
        /// </summary>
        /// <returns></returns>
        public int GetWind()
        {
            return windSpeed;
        }
    }
}
