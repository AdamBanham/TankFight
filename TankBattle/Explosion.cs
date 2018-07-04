using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Explosion : AttackEffect
    {
        private int effectDamage;
        private int effectRadius;
        private int DestRadius;
        private float effectX;
        private float effectY;
        private float effectLifespan;
        

        /// <summary>
        /// stores default values for a new explosion
        /// </summary>
        /// <param name="explosionDamage">Damage done to tank</param>
        /// <param name="explosionRadius">Do damage tanks in this radius</param>
        /// <param name="earthDestructionRadius">Damage terrain in this raidus</param>
        public Explosion(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            //set default values of new explosion
            effectDamage = explosionDamage;
            effectRadius = explosionRadius;
            DestRadius = earthDestructionRadius;
        }

        /// <summary>
        /// Detonates the explosion at the specified location
        /// Default lifespan of 1.0f for this explosion.
        /// </summary>
        /// <param name="x">Location of Explosion (X)</param>
        /// <param name="y">Location of Explosion (Y)</param>
        public void Explode(float x, float y)
        {
            // sets the default values of this explosion
            effectX = x;
            effectY = y;
            effectLifespan = 1.0f;
        }

        /// <summary>
        /// reduces the current lifespan and checks if lifepsan is reduced to zero then explosion happens
        /// </summary>
        public override void Tick()
        {
            //reduce lifespan by a tick (.05)
            effectLifespan = effectLifespan - 0.05f;
            //check to see if lifespan of explosion has expired
            if ( effectLifespan <= 0)
            {
                //now explosion happens , damaging tanks around location
                this.currrentGame.DamagePlayer(effectX, effectY, effectDamage, effectRadius);
                //now explosion destroys terrain as well
                this.currrentGame.GetLevel().DestroyTiles(effectX, effectY, DestRadius);
                //now explosion is removed from the game as it has occured
                this.currrentGame.RemoveWeaponEffect(this);
            }

        }

        /// <summary>
        /// Draws one frame of the Explosion.
        /// The idea is to draw a circle that expands, cycling from yellow to red and then fading out.
        /// </summary>
        /// <param name="graphics">draw explosion on this graphics</param>
        /// <param name="displaySize">Scaling to this displaySize</param>
        public override void Paint(Graphics graphics, Size displaySize)
        {
            //work out the centre of explosion
            float paintX = (float)effectX * displaySize.Width / Battlefield.WIDTH;
            float paintY = (float)effectY * displaySize.Height / Battlefield.HEIGHT;
            //create the radius of painted explosion
            float paintRadius = displaySize.Width * 
                                (float) ((1.0 - effectLifespan) *
                                effectRadius * 3.0 / 2.0) /
                                Battlefield.WIDTH;
            // create colour pigments for paint
            int alpha = 0, red = 0, green = 0, blue = 0;
            //check lifespan to see if its done to a third of time left
            if (effectLifespan < 1.0 / 3.0)
            {
                // set colour pigments
                red = 255;
                alpha = (int)(effectLifespan * 3.0 * 255);
            } else if (effectLifespan < 2.0 / 3.0) // if lifespan is not at 1/3 of time left but less then 2/3
            {
                //set colour pigments
                red = 255;
                alpha = 255;
                green = (int)((effectLifespan * 3.0 - 1.0) * 255);
            } else  // if lifespan is above 2/3 then
            {
                // set colour pigments
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((effectLifespan * 3.0 - 2.0) * 255);
            }
            // create a pointer for the location of painted explosion
            RectangleF paintPoint = new RectangleF(paintX - paintRadius, paintY - paintRadius, paintRadius * 2, paintRadius * 2);
            // create a brush to draw the explosion using colour pigments 
            Brush paintBrush = new SolidBrush(Color.FromArgb(alpha, red, green, blue));
            // draw the explosion on the graphics
            graphics.FillEllipse(paintBrush, paintPoint);
        }

        /// <summary>
        /// Gets the current lifespan of the effect
        /// </summary>
        /// <returns>returns the lifespan in seconds</returns>
        private float Lifespan()
        {
            return effectLifespan;
        }

    }

    // unused class for piercing bullets
    //public class PierceExplosion : Explosion
    //{
    //    public PierceExplosion(int explosionDamage, int explosionRadius, int earthDestructionRadius) : base(explosionDamage, explosionRadius, earthDestructionRadius)
    //    {
    //    }

    //    public override void  Tick()
    //    {
    //        // do the normal run of a explosion tick
    //        base.Tick();
    //        //but if the explosions occurs then create a new bullet 
            
    //    }
    //}
}
