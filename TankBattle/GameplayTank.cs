using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class GameplayTank
    {
        private GenericPlayer tanksPlayer;
        private int tankPosX;
        private int tankPosY;
        private Battle tankInGame;
        private TankModel tanksModel;
        private int tankDurbility;
        private float currentAngle;
        private int currentPower;
        private int currentWeapon;
        private Bitmap tankBmp;


        public GameplayTank(GenericPlayer player, int tankX, int tankY, Battle game)
        {
            //sets up the default values of tank via passed information in constructor
            tanksPlayer = player;
            tankPosX = tankX;
            tankPosY = tankY;
            tankInGame = game;
            //find the tank model via player
            tanksModel = tanksPlayer.GetTank();
            //gets the health of said model
            tankDurbility = tanksModel.GetTankHealth();
            //set the default values for angle , power and weapon
            currentAngle = 0f;
            currentPower = 25;
            currentWeapon = 0;
            //draw tank on feild of battle and save bitmap to class
            tankBmp = tanksModel.CreateTankBMP(tanksPlayer.PlayerColour(), currentAngle);
        }

        /// <summary>
        /// returns the tanks owner
        /// </summary>
        /// <returns></returns>
        public GenericPlayer GetPlayerNumber()
        {
            return tanksPlayer;
        }

        /// <summary>
        /// returns the model assoicated with the owner
        /// </summary>
        /// <returns></returns>
        public TankModel GetTank()
        {
            return tanksPlayer.GetTank();
        }

        /// <summary>
        /// returns the current angle
        /// </summary>
        /// <returns>-90 is pointing to the left , 90 is pointing to the right while 0 means the tank is pointing up</returns>
        public float GetTankAngle()
        {
            return currentAngle;
        }

        /// <summary>
        ///  sets the current aiming angle
        /// </summary>
        /// <param name="angle">new aiming angle</param>
        public void Aim(float angle)
        {
            currentAngle = angle;
            tanksModel.DisplayTank(currentAngle);
            tankBmp = tanksModel.CreateTankBMP(tanksPlayer.PlayerColour(), currentAngle); 
        }

        /// <summary>
        /// returns the tanks current firing power
        /// </summary>
        /// <returns>minimum is 5 and maximum is 100</returns>
        public int GetPower()
        {
            int mimimumPower = 5;
            int maximumPower = 100;
            // work out if power meets firing condition of min and max
            if ((currentPower<5 || currentPower > 100))
            {
                // if it doesnt then return either the min or max depending on value
                if (currentPower < 5)
                {
                    return mimimumPower;
                }
                else
                {
                    return maximumPower;
                }
            }
            else
            // if current power meets condition of firing then return current value
            {
                return currentPower; 
            }
            
        }

        /// <summary>
        /// set the current firing power of tank
        /// </summary>
        /// <param name="power">new firing power</param>
        public void SetTankPower(int power)
        {
            currentPower = power;
        }

        /// <summary>
        /// returns the choice of current weapon
        /// </summary>
        /// <returns>returns the index number of weapon</returns>
        public int GetWeapon()
        {
            return currentWeapon;
        }

        /// <summary>
        /// changes the choice of weapon being used by tank
        /// </summary>
        /// <param name="newWeapon">new weapon index choice</param>
        public void SetWeapon(int newWeapon)
        {
            currentWeapon = newWeapon;
        }

        /// <summary>
        /// draws the tank to the grpahics , scaled to the provided displaySize.
        /// Also shows current durability as percentage
        /// </summary>
        /// <param name="graphics">draw tank on this graphic field</param>
        /// <param name="displaySize">scaling size of said feild</param>
        public void Paint(Graphics graphics, Size displaySize)
        {
            //work out where to draw tank in graphics
            int drawX1 = displaySize.Width * tankPosX / Battlefield.WIDTH;
            int drawY1 = displaySize.Height * tankPosY / Battlefield.HEIGHT;
            int drawX2 = displaySize.Width * (tankPosX + TankModel.WIDTH) / Battlefield.WIDTH;
            int drawY2 = displaySize.Height * (tankPosY + TankModel.HEIGHT) / Battlefield.HEIGHT;

            //draw tank on graphics
            graphics.DrawImage(tankBmp, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            // draw current durability on tank
            // work out centre of tank
            int drawY3 = displaySize.Height * (tankPosY - TankModel.HEIGHT) / Battlefield.HEIGHT;
            //select font and colour of text
            Font durFont = new Font("Arial", 8);
            Brush durBrush = new SolidBrush(Color.White);
            //work out durability percentage of tank using the default value for tankModel
            int defaultDur = tanksModel.GetTankHealth();
            int durPercentage = tankDurbility * 100 / defaultDur;
            //if tank has been damaged then show a percentage on tank
            if (durPercentage < 100)
            {
                //draw on tank
                graphics.DrawString(durPercentage + "%", durFont, durBrush, new Point(drawX1, drawY3));
            }
        }

        /// <summary>
        /// returns the current horinzontal position of tank
        /// </summary>
        /// <returns>a int for the horinontal X position</returns>
        public int GetX()
        {
            return tankPosX;
        }

        /// <summary>
        /// returns the current vertical position of tank
        /// </summary>
        /// <returns>a int for the vertical Y position</returns>
        public int Y()
        {
            return tankPosY;
        }


        /// <summary>
        /// this causes the tank to fire its current weapon
        /// </summary>
        public void Fire()
        {
            GetTank().ActivateWeapon(currentWeapon, this, tankInGame);
        }

        /// <summary>
        /// does damage to tanks current durability
        /// </summary>
        /// <param name="damageAmount">amount of damage taken</param>
        public void DamagePlayer(int damageAmount)
        {
            tankDurbility = tankDurbility - damageAmount;
        }

        /// <summary>
        ///  checks the tanks current durability
        /// </summary>
        /// <returns>returns true is durability is greater than 0 otherwise returns false</returns>
        public bool TankExists()
        {
            bool tankAlive;
            // check current durbility to see if tank is alive
            if (tankDurbility > 0)
            {
                tankAlive = true; // tank can still fight
            }
            else
            {
                tankAlive = false; // tank is destroyed
            }

            return tankAlive;
        }

        /// <summary>
        /// moves the tank to fall down one tile is possible
        /// </summary>
        /// <returns>true if tank has fallen one tile down , otherwise false</returns>
        public bool ProcessGravity()
        {
            bool tankMoved;
            Battlefield currentFight;
            int fallingDamage = 1;

            // check to see if tank is still alive
            if (!TankExists())
            {
                // if destroyed 
                tankMoved = false;
                return tankMoved;
            }
            else
            {
                //find current battefield
                currentFight = tankInGame.GetLevel();
                //test location below tank for terrain
                if (currentFight.TankFits(tankPosX, tankPosY + 1))
                {
                    // if terrain is found then tank doesnt move
                    tankMoved = false;
                    return tankMoved;
                }
                else
                {
                    //tank falls and takes damage
                    tankPosY++;
                    DamagePlayer(fallingDamage);
                    //check to see if tank has fallen to the bottom of map
                    if (tankPosY == (Battlefield.HEIGHT - TankModel.HEIGHT))
                    {
                        //destroy tank as it has fallen off the map
                        tankDurbility = -1;
                    }
                    // tank has moved so change bool and return
                    tankMoved = true;
                    return tankMoved;

                }

            }


        }
    }
}
