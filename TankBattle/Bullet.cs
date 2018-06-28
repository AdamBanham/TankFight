using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Bullet : AttackEffect
    {
        private float bulletX; // bullets current position
        private float bulletY; // bullets current position
        private float bulletGravity; // the current gravity affecting bullet
        private Explosion bulletExplosion; // the explosions for this bullet
        private GenericPlayer bulletOwner; // who fired the bullet
        private float velocityX; // how fast the bullet is moving
        private float velocityY; // how fast the bullet is moving

        /// <summary>
        /// creates a new bullet
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angle"></param>
        /// <param name="power"></param>
        /// <param name="gravity"></param>
        /// <param name="explosion"></param>
        /// <param name="player"></param>
        public Bullet(float x, float y, float angle, float power, float gravity, Explosion explosion, GenericPlayer player)
        {
            // set default values of the new bullet
            bulletX = x;
            bulletY = y;
            bulletGravity = gravity;
            bulletExplosion = explosion;
            bulletOwner = player;
            // now work out how fast this bullet is moving 
            // workout direction and quickness of bullet
            float angleRadians = (90 - angle) * (float)Math.PI / 180; // direction of bullet
            float magnitude = power / 50; // movement vector of bullet
            // calculate velocity
            velocityX = (float)Math.Cos(angleRadians) * magnitude;
            velocityY = (float)Math.Sin(angleRadians) * -magnitude;
        }

        /// <summary>
        /// moves the given projectile according to its angle, power, gravity and the wind. 
        /// </summary>
        public override void Tick()
        {
            // each tick occures ten times in a frame
            //loop procedure 10 times
            for(int Loop = 0; Loop <10; Loop++)
            {
                // increase the location of bullet by velocity
                bulletX = bulletX + velocityX;
                bulletY = bulletY + velocityY;
                // move the bullet according to the current wind
                // get windspeed
                float wind = this.currrentGame.GetWind() / 1000.0f ;
                // move bullet's horizontal postion by adding the wind
                bulletX = bulletX + (wind);
                //check to see if bullet has moved outside game bounds
                if (bulletX > Battlefield.WIDTH | bulletY > Battlefield.HEIGHT | bulletX < 0)
                {
                    //destroy bullet as its gone too far
                    this.currrentGame.RemoveWeaponEffect(this);
                    return; //exit function as bullet doesnt exist anymore
                }
                else // bullet inside bounds
                {
                    // check to see if a bullet has hit a tank or collision 
                    if (this.currrentGame.CheckCollidedTank(bulletX, bulletY))
                    {
                        //record the hit if the owner's tank isnt at location
                        if (!CheckPlayerOwner(bulletOwner, (int)bulletX))
                        {
                            bulletOwner.ReportHit(bulletX, bulletY);
                        }
                        
                        //cause explosion
                        bulletExplosion.Explode(bulletX, bulletY);
                        //add the explosion to effects
                        this.currrentGame.AddEffect(bulletExplosion);
                        //destroy this bullet
                        this.currrentGame.RemoveWeaponEffect(this);
                        return; // exit function as bullet doesnt exist anymore
                    }
                }
                // calucalte fall due to gravity on next frame
                velocityY = velocityY + bulletGravity;
                //end loop
            }
        // function end
        }

        /// <summary>
        /// draws the Bullet as a small white circle.
        /// </summary>
        /// <param name="graphics">bullet is drawn on this graphics</param>
        /// <param name="size">how big the bullet is</param>
        public override void Paint(Graphics graphics, Size size)
        {
            //work out the location to draw the bullet
            float paintX = (float)bulletX * size.Width / Battlefield.WIDTH;
            float paintY = (float)bulletY * size.Height / Battlefield.HEIGHT;
            float paintSize = size.Width / Battlefield.HEIGHT;

            // create the point to draw the bullet
            RectangleF paintPoint = new RectangleF(paintX - paintSize / 2.0f, paintY - paintSize / 2.0f, paintSize, paintSize);
            // create the brush to draw bullet
            Brush paintBrush = new SolidBrush(Color.WhiteSmoke);
            // draw bullet on graphics
            graphics.FillEllipse(paintBrush, paintPoint);
        }

        /// <summary>
        /// checks if a bullet is about to hit the tank that shot it
        /// </summary>
        /// <param name="owner">who owns the shot</param>
        /// <param name="x"> the location of explosion</param>
        /// <returns></returns>
        private bool CheckPlayerOwner (GenericPlayer owner , int x)
        {
            bool hittingSelf = false;

            for (int playerNum = 1; playerNum <= this.currrentGame.NumPlayers();playerNum++)
            {
                // check the x of each player against the owner of bullet
                for(int rangeCheck = 0; rangeCheck < 5; rangeCheck++)
                {
                    if( this.currrentGame.GetBattleTank(playerNum-1).GetX() == x - rangeCheck ||
                        this.currrentGame.GetBattleTank(playerNum-1).GetX() == x + rangeCheck)
                    {
                        //this player is near the x value
                        //check player isnt owner
                        if(this.currrentGame.GetPlayerNumber(playerNum) == owner)
                        {
                            hittingSelf = true;
                            return hittingSelf;
                        }
                    }
                }
            }

            return hittingSelf;
        }
    }

    /// <summary>
    /// this bullet will generate another bullet once it collides with something
    /// </summary>
    public class PierceBullet : AttackEffect
    {
        private float bulletX; // bullets current position
        private float bulletY; // bullets current position
        private float bulletGravity; // the current gravity affecting bullet
        private Explosion bulletExplosion; // the explosions for this bullet
        private GenericPlayer bulletOwner; // who fired the bullet
        private float velocityX; // how fast the bullet is moving
        private float velocityY; // how fast the bullet is moving
        private float bulletAngle; // stores the angle of bullet
        private float bulletPower; // stores the power of the bullet

        /// <summary>
        /// creates a new bullet
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angle"></param>
        /// <param name="power"></param>
        /// <param name="gravity"></param>
        /// <param name="explosion"></param>
        /// <param name="player"></param>
        public PierceBullet(float x, float y, float angle, float power, float gravity, Explosion explosion, GenericPlayer player)
        {
            // set default values of the new bullet
            bulletX = x;
            bulletY = y;
            bulletGravity = gravity;
            bulletExplosion = explosion;
            bulletOwner = player;
            // now work out how fast this bullet is moving 
            // workout direction and quickness of bullet
            float angleRadians = (90 - angle) * (float)Math.PI / 180; // direction of bullet
            float magnitude = power / 50; // movement vector of bullet
            // calculate velocity
            velocityX = (float)Math.Cos(angleRadians) * magnitude;
            velocityY = (float)Math.Sin(angleRadians) * -magnitude;
            //store bullet properties
            bulletAngle = angleRadians;
            bulletPower = magnitude;
        }

        /// <summary>
        /// moves the given projectile according to its angle, power, gravity and the wind. 
        /// </summary>
        public override void Tick()
        {
            // each tick occures ten times in a frame
            //loop procedure 10 times
            for (int Loop = 0; Loop < 10; Loop++)
            {
                // increase the location of bullet by velocity
                bulletX = bulletX + velocityX;
                bulletY = bulletY + velocityY;
                // move the bullet according to the current wind
                // get windspeed
                float wind = this.currrentGame.GetWind() / 1000.0f;
                // move bullet's horizontal postion by adding the wind
                bulletX = bulletX + (wind);
                //check to see if bullet has moved outside game bounds
                if (bulletX > Battlefield.WIDTH | bulletY > Battlefield.HEIGHT | bulletX < 0)
                {
                    //destroy bullet as its gone too far
                    this.currrentGame.RemoveWeaponEffect(this);
                    return; //exit function as bullet doesnt exist anymore
                }
                else // bullet inside bounds
                {
                    // check to see if a bullet has hit a tank or collision 
                    if (this.currrentGame.CheckCollidedTank(bulletX, bulletY))
                    {
                        //record the hit if the owner's tank isnt at location
                        if (!CheckPlayerOwner(bulletOwner, (int)bulletX))
                        {
                            bulletOwner.ReportHit(bulletX, bulletY);
                        }

                        //cause explosion
                        bulletExplosion.Explode(bulletX, bulletY);
                        //add the explosion to effects
                        this.currrentGame.AddEffect(bulletExplosion);
                        //destroy this bullet
                        this.currrentGame.RemoveWeaponEffect(this);
                        //create a new bullet and explosion for the inner shell 
                        //add it to the environment
                        InnerShell();
                        
                        return; // exit function as bullet doesnt exist anymore
                    }
                }
                // calucalte fall due to gravity on next frame
                velocityY = velocityY + bulletGravity;
                ////work out the new bullet angle
                //// in the constructor we have a formula to work out the velocity from the angle
                //// to find the angle we just reverse the formula

                //// velocityY = Math.Sin(angleRadians) * -bulletPower
                //// velocityY/-bulletPower = Math.Sin(angleRadians)
                //// Math.Asin(velocityY/-bulletPower) = angleRadians
                ////now we can get the radians on the angle for the bullet

                //bulletAngle =-1f*(float)Math.Asin(  (Math.Abs(velocityY) / bulletPower))  ;
                //// use the radians to get the using the formula in the constructor
                //// radians = (90 - angle) * (float)Math.PI / 180
                //// radians/(Math.PI/180) = 90-angle
                //// (radians/(Math.PI/180))-90 = -angle
                ////-1 * ( (radians/ (Math.PI/180) ) -90 ) =  angle
                //bulletAngle = -1f * ((bulletAngle / (float)(Math.PI / 180f)) - 90f);

                //end loop
            }
            // function end
        }

        /// <summary>
        /// draws the Bullet as a small white circle.
        /// </summary>
        /// <param name="graphics">bullet is drawn on this graphics</param>
        /// <param name="size">how big the bullet is</param>
        public override void Paint(Graphics graphics, Size size)
        {
            //work out the location to draw the bullet
            float paintX = (float)bulletX * size.Width / Battlefield.WIDTH;
            float paintY = (float)bulletY * size.Height / Battlefield.HEIGHT;
            float paintSize = size.Width / Battlefield.HEIGHT;

            // create the point to draw the bullet
            RectangleF paintPoint = new RectangleF(paintX - paintSize / 2.0f, paintY - paintSize / 2.0f, paintSize, paintSize);
            // create the brush to draw bullet
            Brush paintBrush = new SolidBrush(Color.WhiteSmoke);
            // draw bullet on graphics
            graphics.FillEllipse(paintBrush, paintPoint);
        }

        /// <summary>
        /// checks if a bullet is about to hit the tank that shot it
        /// </summary>
        /// <param name="owner">who owns the shot</param>
        /// <param name="x"> the location of explosion</param>
        /// <returns></returns>
        private bool CheckPlayerOwner(GenericPlayer owner, int x)
        {
            bool hittingSelf = false;

            for (int playerNum = 1; playerNum <= this.currrentGame.NumPlayers(); playerNum++)
            {
                // check the x of each player against the owner of bullet
                for (int rangeCheck = 0; rangeCheck < 5; rangeCheck++)
                {
                    if (this.currrentGame.GetBattleTank(playerNum - 1).GetX() == x - rangeCheck ||
                        this.currrentGame.GetBattleTank(playerNum - 1).GetX() == x + rangeCheck)
                    {
                        //this player is near the x value
                        //check player isnt owner
                        if (this.currrentGame.GetPlayerNumber(playerNum) == owner)
                        {
                            hittingSelf = true;
                            return hittingSelf;
                        }
                    }
                }
            }

            return hittingSelf;
        }

        /// <summary>
        /// creates the inner shell and fires it after the first explosion
        /// </summary>
        private void InnerShell()
        {
            //work out the new bullet angle
            // in the constructor we have a formula to work out the velocity from the angle
            // to find the angle we just reverse the formula

            // velocityY = Math.Sin(angleRadians) * -bulletPower
            // velocityY/-bulletPower = Math.Sin(angleRadians)
            // Math.Asin(velocityY/-bulletPower) = angleRadians
            //now we can get the radians on the angle for the bullet
            double newBulletAngle;
            // before arc sin can be done we need to find the current radian minus any whole rotations
            double fullRadiansCal = velocityY / bulletPower;
            //reduce the radians to find current heading
            while (fullRadiansCal > 1)
            {
                fullRadiansCal -= 1;
            }
            //work out radians angle for bullet
            newBulletAngle = -1* Math.Asin(Math.Abs(fullRadiansCal));
            // use the radians to get the using the formula in the constructor
            // radians = (90 - angle) * (float)Math.PI / 180
            // radians/(Math.PI/180) = 90-angle
            // (radians/(Math.PI/180))-90 = -angle
            //-1 * ( (radians/ (Math.PI/180) ) -90 ) =  angle
            newBulletAngle = -1 * ((newBulletAngle / (Math.PI / 180)) - 90);

            int innerDamage = 30;
            int innerRadius = 3;
            int innerDestRadius = 4;
            int travelDist = 3;
            bulletPower = bulletPower * 50; // gets the orginal power of the bullet
            //create explosion
            Explosion innerExplosion;
            innerExplosion = new Explosion(innerDamage,
                                           innerRadius,
                                           innerDestRadius);
            //create new bullet
            Bullet innerBullet;
            // check the velocity to see if the bullet was falling or rising 
            if (velocityY <= 0)
            {
                newBulletAngle *= -1f;
            }
            // work out which way the bullet is travelling to adjust the next shots begining point
            if(bulletAngle > 0)
            {
                //bullet is travelling to the left , new bullet should appear to the left
                bulletX = bulletX + travelDist;
                bulletY = bulletY + travelDist;

            }
            if (bulletAngle < 0)
            {
                //bullet is travelling to the right , new should appear to the right
                bulletX = bulletX - travelDist;
                bulletY = bulletY - travelDist;
            }
            //check the angle isn't outside bounds -90 or 90
            if (newBulletAngle < -90 || newBulletAngle > 90)
            {
                //change back to limits of game
                if(newBulletAngle < -90)
                {
                    newBulletAngle = -90;
                }
                if(newBulletAngle > 90)
                {
                    newBulletAngle = 90;
                }
            }


            innerBullet = new Bullet(bulletX,
                                      bulletY,
                                      (float)newBulletAngle,
                                      bulletPower,
                                      this.bulletGravity,
                                      innerExplosion,
                                      bulletOwner
                                      );
            //add the bullet into the battle environment
            currrentGame.AddEffect(innerBullet);
        }
    }
}
