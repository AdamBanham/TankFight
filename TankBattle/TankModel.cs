using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public abstract class TankModel
    {
        public const int WIDTH = 3;
        public const int HEIGHT = 3;
        public const int NUM_TANKS = 1;

        /// <summary>
        /// draws the tank into an array and returns it
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>returns a int[12,16] array contiaing a 1 for each pixel that makes up the tank's shape</returns>
        public abstract int[,] DisplayTank(float angle);

        /// <summary>
        ///  draws a line in graphic array between (x1,y1) and (x2,y2)
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        public static void SetLine(int[,] graphic, int X1, int Y1, int X2, int Y2)
        {
            while (!(X1==X2 && Y1 == Y2))
            {
                graphic[Y1, X1] = 1; // draw a dot at position
                //work out the next dot X cord

                if(X1 > X2) // check x2 is on left of x1
                {
                    X1--; //decrement X1 to move to left
                }
                if(X1 < X2) //check if x2 is on the right of x1
                {
                    X1++; //increment X1 to move to right
                }

                //work out the next dot Y cord
                if (Y1 > Y2) //check if Y2 is higher than Y1
                {
                    Y1--; // decrement Y1 to move up
                }
                if(Y1 < Y2) // check if Y2 is lower than Y1
                {
                    Y1++; //increment Y1 to move down
                }

            }// repeat until both x's and y's match

            // draw a dot on the destination point
            graphic[Y2, X2] = 1;

        }

        public Bitmap CreateTankBMP(Color tankColour, float angle)
        {
            int[,] tankGraphic = DisplayTank(angle);
            int height = tankGraphic.GetLength(0);
            int width = tankGraphic.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color tankOutline = Color.FromArgb(255, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tankGraphic[y, x] == 0)
                    {
                        bmp.SetPixel(x, y, transparent);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, tankColour);
                    }
                }
            }

            // Outline each pixel
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (tankGraphic[y, x] != 0)
                    {
                        if (tankGraphic[y - 1, x] == 0)
                            bmp.SetPixel(x, y - 1, tankOutline);
                        if (tankGraphic[y + 1, x] == 0)
                            bmp.SetPixel(x, y + 1, tankOutline);
                        if (tankGraphic[y, x - 1] == 0)
                            bmp.SetPixel(x - 1, y, tankOutline);
                        if (tankGraphic[y, x + 1] == 0)
                            bmp.SetPixel(x + 1, y, tankOutline);
                    }
                }
            }

            return bmp;
        }

        public abstract int GetTankHealth();

        public abstract string[] ListWeapons();

        public abstract void ActivateWeapon(int weapon, GameplayTank playerTank, Battle currentGame);

        /// <summary>
        /// Gets a model based off the given number
        /// </summary>
        /// <param name="tankNumber">a interger between 1 to 4 , will default to 1</param>
        /// <returns>a model for a tank type</returns>
        public static TankModel GetTank(int tankNumber)
        {
            switch (tankNumber)
            {
                case 1:
                    return new BasicTank();
                case 2:
                    return new QuickFireTank();
                case 3:
                    return new HeavyTank();
                case 4:
                    return new ArmoredTank();
                    
                default:
                    return new BasicTank();
            }
           
        }
    }

    public class BasicTank : TankModel
    {
        private int tankHealth = 100; // used to storage the base amount of health
        private string weaponName = "Iron shell"; // used to storage the tank's weapon

        /// <summary>
        /// Fire a specified weapon from the tank
        /// </summary>
        /// <param name="weapon">Weapon selected</param>
        /// <param name="playerTank">The tank firing</param>
        /// <param name="currentGame">The current battle</param>
        public override void ActivateWeapon(int weapon, GameplayTank playerTank, Battle currentGame)
        {
            float tankX; // used to storage the firing position of a tank
            float tankY;
            GenericPlayer firingPlayer; //used to storage owner of tank
            int damage, explosionRadius, earthDestruction;

            // get the current position of tank
            tankX = playerTank.GetX();
            tankY = playerTank.Y();
            // find the centre of tank by add half the width and height
            tankX = tankX + TankModel.WIDTH / 2;
            tankY = tankY + TankModel.HEIGHT / 2;
            // get the firing player of tank
            firingPlayer = playerTank.GetPlayerNumber();
            // create new explosion
            Explosion basicExplosion;
            //set power of weapon
            damage = 100; // damage done by bullet
            explosionRadius = 4; //size of explosion
            earthDestruction = 4; //damage to area around explosion
            basicExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
            //create new bullet
            Bullet basicBullet;
            basicBullet = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle(),
                                    playerTank.GetPower(),
                                    0.01f, //gravity 
                                    basicExplosion,
                                    firingPlayer)
                                    ;
            //add bullet to games current effects to continue flying through the environment
            currentGame.AddEffect(basicBullet);       
        }

        /// <summary>
        /// draws the tank into an array and returns it
        /// </summary>
        /// <param name="angle">-90 is straight left , 0 is straight up and 90 is straight left</param>
        /// <returns>returns a int[12,16] array contiaing a 1 for each pixel that makes up the tank's shape</returns>
        public override int[,] DisplayTank(float angle)
        {
            int[,] graphic = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                               { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0 },
                               { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            // draw the barrel of the tank based of the angle
           if (angle <=90 && angle > 45)
            {
                SetLine(graphic, 7, 6, 12, 6); // draw the barell facing down
            }
           if(angle <= 45 && angle > 10)
            {
                SetLine(graphic, 7, 6, 11, 2); // draw barrel facing halfway down
            }
           if(angle <= 10 && angle > -10)
            {
                SetLine(graphic, 7, 6, 7, 1); // draw the barell facing straight
            }
           if (angle <=-10 && angle >=-45)
            {
                SetLine(graphic, 7, 6, 3, 2); // draw the barell facing halfway up
            }
           if (angle < -45 && angle >=-90)
            {
                SetLine(graphic, 7, 6, 2, 6); // draw the barell facing up
            }

            return graphic;
        }

        /// <summary>
        /// Gets the maximun amount of health of a basic tank
        /// </summary>
        /// <returns></returns>
        public override int GetTankHealth()
        {
            return tankHealth;
        }

        /// <summary>
        /// Gets the weapons that this tank has available
        /// </summary>
        /// <returns>array of weapon names</returns>
        public override string[] ListWeapons()
        {
            return new string[] { weaponName };
        }

    }

    public class QuickFireTank : TankModel
    {
        private int tankHealth = 75;
        private string[] weaponNames = { "Double Tap", "Barrage" }; // used to storage the tank's weapon
        enum WeaponChoice {DoubleTap , Barrage};

        /// <summary>
        /// Fire a specified weapon from the tank
        /// </summary>
        /// <param name="weapon">Weapon selected</param>
        /// <param name="playerTank">The tank firing</param>
        /// <param name="currentGame">The current battle</param>
        public override void ActivateWeapon(int weapon, GameplayTank playerTank, Battle currentGame)
        {
            float tankX; // used to storage the firing position of a tank
            float tankY;
            GenericPlayer firingPlayer; //used to storage owner of tank
            int damage, explosionRadius, earthDestruction;
            Random unaccurateShot = new Random(); // used to make the missles fly off course a bit

            // get the current position of tank
            tankX = playerTank.GetX();
            tankY = playerTank.Y();
            // find the centre of tank by add half the width and height
            tankX = tankX + TankModel.WIDTH / 2;
            tankY = tankY + TankModel.HEIGHT / 2;
            // get the firing player of tank
            firingPlayer = playerTank.GetPlayerNumber();          
            //set power of weapon
            damage = 50; // damage done by bullet
            explosionRadius = 3; //size of explosion
            earthDestruction = 2; //damage to area around explosion

            //check which sort of shot to fire
            if ((WeaponChoice)weapon == WeaponChoice.DoubleTap)
            {
                // fire two to semi accurate missiles
                // create explosions for missiles
                Explosion firstExplosion;
                Explosion secondExplosion;
                firstExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                secondExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                //create new missile bullets
                Bullet firstMissile;
                Bullet secondMissile;
                firstMissile = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle() + (unaccurateShot.Next(-2,3) ),
                                    playerTank.GetPower() + (unaccurateShot.Next(-3, 4) ),
                                    0.01f, //gravity 
                                    firstExplosion,
                                    firingPlayer)
                                    ;
                secondMissile = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle() + (unaccurateShot.Next(-2, 3)),
                                    playerTank.GetPower() + (unaccurateShot.Next(-3, 4)),
                                    0.01f, //gravity 
                                    secondExplosion,
                                    firingPlayer)
                                    ;
                //add bullet to games current effects to continue flying through the environment
                currentGame.AddEffect(firstMissile);
                currentGame.AddEffect(secondMissile);
            }
            if ((WeaponChoice)weapon == WeaponChoice.Barrage)
            {
                // fire four missiles but they are very inaccurate shots
                //create explosions
                Explosion firstExplosion;
                Explosion secondExplosion;
                Explosion thridExplosion;
                Explosion fourthExplosion;
                firstExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                secondExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                thridExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                fourthExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                //create new missile bullets
                Bullet firstMissile;
                Bullet secondMissile;
                Bullet thridMissile;
                Bullet fourthMissile;
                firstMissile = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle() + (unaccurateShot.Next(-4, 9)),
                                    playerTank.GetPower() + (unaccurateShot.Next(-4, 9)),
                                    0.01f, //gravity 
                                    firstExplosion,
                                    firingPlayer)
                                    ;
                secondMissile = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle() + (unaccurateShot.Next(-4, 9)),
                                    playerTank.GetPower() + (unaccurateShot.Next(-4, 9)),
                                    0.01f, //gravity 
                                    secondExplosion,
                                    firingPlayer)
                                    ;
                thridMissile = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle() + (unaccurateShot.Next(-4, 9)),
                                    playerTank.GetPower() + (unaccurateShot.Next(-4, 9)),
                                    0.01f, //gravity 
                                    thridExplosion,
                                    firingPlayer)
                                    ;
                fourthMissile = new Bullet(tankX,
                                    tankY,
                                    playerTank.GetTankAngle() + (unaccurateShot.Next(-4, 9)),
                                    playerTank.GetPower() + (unaccurateShot.Next(-4, 9)),
                                    0.01f, //gravity 
                                    fourthExplosion,
                                    firingPlayer)
                                    ;
                //add bullet to games current effects to continue flying through the environment
                currentGame.AddEffect(firstMissile);
                currentGame.AddEffect(secondMissile);
                currentGame.AddEffect(thridMissile);
                currentGame.AddEffect(fourthMissile);
            }
            
        }

        /// <summary>
        /// creates the image of this tank model , depending on angle of battleTank
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public override int[,] DisplayTank(float angle)
        {
            int[,] graphic = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                               { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0 },
                               { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            // draw the barrel of the tank based of the angle
            if (angle > 15)
            {
                SetLine(graphic, 4, 4, 4, 2); // draw larger tower on left hand side
                SetLine(graphic, 6, 4, 6, 2);
                SetLine(graphic, 5, 2, 5, 2);
            }
            if (angle < -15)
            {
                //draw the larger tower on the right hand side
                SetLine(graphic, 8, 4, 8, 2);
                SetLine(graphic, 10, 4, 10, 2);
                SetLine(graphic, 9, 2, 9, 2);
            }
            return graphic;
        }

        /// <summary>
        /// gets the maximun health of this tank type
        /// </summary>
        /// <returns></returns>
        public override int GetTankHealth()
        {
            return tankHealth;
        }

        /// <summary>
        /// Gets the weapons that this tank has available
        /// </summary>
        /// <returns>array of weapon names</returns>
        public override string[] ListWeapons()
        {
            return weaponNames;
        }
    }

    public class HeavyTank : TankModel
    {
        private int tankHealth = 115;
        private string[] weaponNames = { "Big Bang", "Small Bangs" }; // used to storage the tank's weapon
        enum WeaponChoice { BigBang, SmallBangs };

        /// <summary>
        /// Fire a specified weapon from the tank
        /// </summary>
        /// <param name="weapon">Weapon selected</param>
        /// <param name="playerTank">The tank firing</param>
        /// <param name="currentGame">The current battle</param>
        public override void ActivateWeapon(int weapon, GameplayTank playerTank, Battle currentGame)
        {
            float tankX; // used to storage the firing position of a tank
            float tankY;
            GenericPlayer firingPlayer; //used to storage owner of tank
            int damage, explosionRadius, earthDestruction;
            Random unaccurateShot = new Random(); // used to make the missles fly off course a bit

            // get the current position of tank
            tankX = playerTank.GetX();
            tankY = playerTank.Y();
            // find the centre of tank by add half the width and height
            tankX = tankX + TankModel.WIDTH / 2;
            tankY = tankY + TankModel.HEIGHT / 2;
            // get the firing player of tank
            firingPlayer = playerTank.GetPlayerNumber();
            //set power of weapon
            damage = 5; // damage done by bullet
            explosionRadius = 10; //size of explosion
            earthDestruction = 10; //damage to area around explosion

            //check which sort of shot to fire
            if((WeaponChoice)weapon == WeaponChoice.BigBang)
            {
                //create a explosion for the bullet
                Explosion firstExplosion;
                firstExplosion = new Explosion(damage,
                                                explosionRadius,
                                                earthDestruction);
                //create a bullet for the shot
                Bullet firstBullet;
                firstBullet = new Bullet(tankX,
                                          tankY,
                                          playerTank.GetTankAngle(),
                                          playerTank.GetPower(),
                                          0.03f,
                                          firstExplosion,
                                          playerTank.GetPlayerNumber()
                                          );
                //add the bullet to the battle environment
                currentGame.AddEffect(firstBullet);
            }
            if ((WeaponChoice)weapon == WeaponChoice.SmallBangs)
            {
                //this weapon choice fires multiple 1-3 smaller bullets
                Random shotsFired = new Random();
                int numberOfShots = shotsFired.Next(1, 4);
                //create a explosion for the bullet
                Explosion firstExplosion;
                //create a weaker shot than a normal shot
                int makeWeaker = 60;
                int makePertcentageOfOrginalValue = 100;
                firstExplosion = new Explosion((damage*makeWeaker)/ makePertcentageOfOrginalValue,
                                               (explosionRadius*makeWeaker)/ makePertcentageOfOrginalValue,
                                               (earthDestruction*makeWeaker)/ makePertcentageOfOrginalValue)
                                               ;
                //create a bullet for the shot
                Bullet firstBullet;
                firstBullet = new Bullet(tankX,
                                          tankY,
                                          playerTank.GetTankAngle(),
                                          playerTank.GetPower(),
                                          0.01f,
                                          firstExplosion,
                                          playerTank.GetPlayerNumber()
                                          );
                //add the bullet to the battle environment
                currentGame.AddEffect(firstBullet);

                if (numberOfShots > 1)
                {
                    //fire a second shot
                    //create a explosion for the bullet
                    Explosion secondExplosion;
                    secondExplosion = new Explosion((damage * makeWeaker) / makePertcentageOfOrginalValue,
                                               (explosionRadius * makeWeaker) / makePertcentageOfOrginalValue,
                                               (earthDestruction * makeWeaker) / makePertcentageOfOrginalValue)
                                               ;
                    //create a bullet for the shot
                    Bullet secondBullet;
                    secondBullet = new Bullet(tankX,
                                              tankY,
                                              playerTank.GetTankAngle(),
                                              playerTank.GetPower(),
                                              0.015f,
                                              secondExplosion,
                                              playerTank.GetPlayerNumber()
                                              );
                    //add the bullet to the battle environment
                    currentGame.AddEffect(secondBullet);
                }
                if (numberOfShots > 2)
                {
                    //fire a third shot
                    //create a explosion for the bullet
                    Explosion thirdExplosion;
                    thirdExplosion = new Explosion((damage * makeWeaker) / makePertcentageOfOrginalValue,
                                               (explosionRadius * makeWeaker) / makePertcentageOfOrginalValue,
                                               (earthDestruction * makeWeaker) / makePertcentageOfOrginalValue)
                                               ;
                    //create a bullet for the shot
                    Bullet thirdBullet;
                    thirdBullet = new Bullet(tankX,
                                              tankY,
                                              playerTank.GetTankAngle(),
                                              playerTank.GetPower(),
                                              0.02f,
                                              thirdExplosion,
                                              playerTank.GetPlayerNumber()
                                              );
                    //add the bullet to the battle environment
                    currentGame.AddEffect(thirdBullet);
                }
            }
        }

        /// <summary>
        /// draw the image of this tank type
        /// </summary>
        /// <param name="angle">the angle of the tank</param>
        /// <returns></returns>
        public override int[,] DisplayTank(float angle)
        {
            int[,] graphic = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0 },
                               { 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0 },
                               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                               { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                               { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            //draw the barrel on tank

            if (angle > 65)
            {
                //draw the barrel facing to the left
                SetLine(graphic, 9, 5, 13, 5);
                SetLine(graphic, 8, 4, 13, 4);                    
            }
            if(angle >15 && angle <= 65)
            {
                //draw the barrel on the left , halfway drawn to the sky
                SetLine(graphic, 9, 5, 12, 2);
                SetLine(graphic, 8, 5, 11, 2);
            }
            if (angle > -15 && angle <= 15)
            {
                //draw the barrel pointing the sky
                SetLine(graphic, 9, 5, 9, 1);
                SetLine(graphic, 8, 5, 8, 1);
            }
            if (angle >-65 && angle <= -15)
            {
                //draw the barrel on the right , halfway drawn to the sky
                SetLine(graphic, 9, 5, 5, 1);
                SetLine(graphic, 8, 5, 4, 1);
            }
            if (angle <= -65)
            {
                //draw the barrel on the right facing to right
                SetLine(graphic, 8, 5, 4, 5);
                SetLine(graphic, 9, 4, 4, 4);
            }
            return graphic;
        }

        /// <summary>
        /// gets the maximun amount of health of this tank type
        /// </summary>
        /// <returns></returns>
        public override int GetTankHealth()
        {
            return tankHealth;
        }

        /// <summary>
        /// Gets the weapons that this tank has available
        /// </summary>
        /// <returns>array of weapon names</returns>
        public override string[] ListWeapons()
        {
            return weaponNames;
        }
    }

    public class ArmoredTank : TankModel
    {
        private int tankHealth = 150;
        private string[] weaponNames = { "Iron Shell", "Pierce Shot" }; // used to storage the tank's weapon
        enum WeaponChoice { IronShell, PierceShot };

        /// <summary>
        /// Fire a specified weapon from the tank
        /// </summary>
        /// <param name="weapon">Weapon selected</param>
        /// <param name="playerTank">The tank firing</param>
        /// <param name="currentGame">The current battle</param>
        public override void ActivateWeapon(int weapon, GameplayTank playerTank, Battle currentGame)
        {
            float tankX; // used to storage the firing position of a tank
            float tankY;
            GenericPlayer firingPlayer; //used to storage owner of tank
            int damage, explosionRadius, earthDestruction;
            Random unaccurateShot = new Random(); // used to make the missles fly off course a bit

            // get the current position of tank
            tankX = playerTank.GetX();
            tankY = playerTank.Y();
            // find the centre of tank by add half the width and height
            tankX = tankX + TankModel.WIDTH / 2;
            tankY = tankY + TankModel.HEIGHT / 2;
            // get the firing player of tank
            firingPlayer = playerTank.GetPlayerNumber();
            //set power of weapon
            damage = 80; // damage done by bullet
            explosionRadius = 4; //size of explosion
            earthDestruction = 4; //damage to area around explosion

            //check which sort of shot to fire;
            if((WeaponChoice)weapon == WeaponChoice.IronShell)
            {
                //fire a normal shot
                //create a explosion for the shot
                Explosion basicExplosion;
                basicExplosion = new Explosion(damage,
                                           explosionRadius,
                                           earthDestruction);
                //create new bullet
                Bullet basicBullet;
                basicBullet = new Bullet(tankX,
                                        tankY,
                                        playerTank.GetTankAngle(),
                                        playerTank.GetPower(),
                                        0.01f, //gravity 
                                        basicExplosion,
                                        firingPlayer)
                                        ;
                //add bullet to games current effects to continue flying through the environment
                currentGame.AddEffect(basicBullet);
            }
            if((WeaponChoice)weapon == WeaponChoice.PierceShot)
            {
                //fire a shot which will pierce the first thing it hits and cause a second explosion

                //fire a piecre shot with a weaker strength
                int weakerStr = 60;
                int makePercentageOfOrginial = 100;
                //create a explosion for the shot
                Explosion pierceExplosion;

                pierceExplosion = new Explosion((damage*weakerStr)/makePercentageOfOrginial,
                                               (explosionRadius*weakerStr)/makePercentageOfOrginial,
                                               (earthDestruction*weakerStr)/makePercentageOfOrginial
                                               );
                //create new bullet
                PierceBullet pierceBullet;
                pierceBullet = new PierceBullet(tankX,
                                        tankY,
                                        playerTank.GetTankAngle(),
                                        playerTank.GetPower(),
                                        0.01f, //gravity 
                                        pierceExplosion,
                                        firingPlayer)
                                        ;
                //add bullet to games current effects to continue flying through the environment
                currentGame.AddEffect(pierceBullet);
            }
        }

        /// <summary>
        /// draw the image of this tank type
        /// </summary>
        /// <param name="angle">the angle of the tank</param>
        /// <returns></returns>
        public override int[,] DisplayTank(float angle)
        {
            //the base of a tank
            int[,] graphic = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
                               { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
                               { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                               { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
                               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            //draw the barrel on the base
            if (angle > 65)
            {
                //draw the barrel facing straight right
                SetLine(graphic, 6, 3, 11, 3);
            }
            if (angle <= 65 && angle > 15)
            {
                //draw the barrel facing right , drawn halfway up
                SetLine(graphic, 6, 3, 9, 0);
            }
            if (angle <= 15 && angle > -15)
            {
                //draw the barrel facing up 
                SetLine(graphic, 6, 3, 6, 0);
            }
            if (angle <= -15 && angle > -65)
            {
                //draw the barrel facing left , drawn halfway up
                SetLine(graphic, 6, 3, 3, 0);
            }
            if( angle <= -65)
            {
                //draw the barrel facing left
                SetLine(graphic, 6, 3, 1, 3);
            }
            return graphic;
        }

        /// <summary>
        /// gets the maximun health of a tank of this type
        /// </summary>
        /// <returns></returns>
        public override int GetTankHealth()
        {
            return tankHealth;
        }

        /// <summary>
        /// Gets the weapons that this tank has available
        /// </summary>
        /// <returns>array of weapon names</returns>
        public override string[] ListWeapons()
        {
            return weaponNames;
        }
    }
}
