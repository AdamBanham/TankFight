using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Battlefield
    {
        public const int WIDTH = 160;
        public const int HEIGHT = 120;
        private bool[,] terrain = new bool[HEIGHT, WIDTH]; // stores the shape of the terrain
        private Random myRandom = new Random(); // used to generate terrain
        private int startingHeight;


        /// <summary>
        /// Randomly generates the terrain on which the tanks will battle.
        /// </summary>
        public Battlefield()
        {
            bool madeTerrain = true; //used to store if a piece of terrain was made during random choosing process

            //choose a random height between min(0) and max (height - tankmodel.height) to begin generation
            startingHeight = myRandom.Next(TankModel.HEIGHT*3, (Battlefield.HEIGHT- TankModel.HEIGHT * 3)); 
            
            //generation terrain in array up to startingHeight
            for(int height = startingHeight; height < Battlefield.HEIGHT; height++)
            {
                for (int width = 0; width < Battlefield.WIDTH; width++ )
                {
                    terrain[height, width] = true;
                }
            }
            //now randomanly decide whether to create terrain above startingHeight
            for(int height = startingHeight-1; madeTerrain && height > (TankModel.HEIGHT*3) ; height--) // start from the level above the starting point
            {
                madeTerrain = false; // reset bool for new level
                for(int width = 0; width < Battlefield.WIDTH; width++) // go along the width of the battlefield and create terrain
                {
                    if (myRandom.Next(0, 2) == 1) // coin flip to create terrain
                    {
                        if ((height + 1 < HEIGHT)) // if making terrain ensure that there is some below current
                        {
                            // generate a small triangle of terrain
                            //     -
                            //    ---
                            for (int extrawidth = 0; extrawidth < 3; extrawidth++)
                            {
                                if (width+extrawidth < WIDTH && terrain[height + 1, width + extrawidth])
                                {
                                    terrain[height, width+extrawidth] = true; //make terrain
                                    madeTerrain = true; // ensure that loop continues for the next height
                                    if (extrawidth == 1) //if in the middle of triangle then create terrain above base as well
                                    {
                                        terrain[height-1, width + extrawidth] = true; //make terrain
                                    }
                                }
                            }
                        }
                    }
                    
                    
                }
            }

            //for (int y = 0; y < terrain.GetLength(0); y++)
            //{
            //    for (int x = 0; x < terrain.GetLength(1); x++)
            //    {
            //        if (terrain[y, x])
            //        {
            //            Console.Write("X");
            //        }
            //        else
            //        {
            //            Console.Write(".");
            //        }
            //    }
            //    Console.Write("\n");
            //}

        }

        /// <summary>
        /// checks location for terrain
        /// </summary>
        /// <param name="x">never less than 0 or greater than width</param>
        /// <param name="y">never be less than 0 or greater than height</param>
        /// <returns>true = terrain , false = no terrain</returns>
        public bool Get(int x, int y)
        {
            bool isTerrain = false; // used to work out if there is terrain at location
            if ((y >= HEIGHT || y < 0) || (x >= WIDTH || x < 0))
            {
                // if the wanted check is outside the current screen true false
                return isTerrain;
            }
            if (terrain[y, x]) // checks location 
            {
                isTerrain = true; // change bool to show terrain
            }
            return isTerrain;
        }

        /// <summary>
        /// finds out if a tank will fit at location
        /// </summary>
        /// <param name="x">top left location of tank</param>
        /// <param name="y">top left location of tank</param>
        /// <returns>bool ; true location works ; false location doesnt work</returns>
        public bool TankFits(int x, int y)
        {
            bool badLocation; // used to storage if the location meets conditions to place tank
            int colisionCount = 0; // counts how many tiles are in location
            // check the box of 3x4 below target location for terrain 
            for (int height = y; height < y + TankModel.HEIGHT &&
                                height < Battlefield.HEIGHT

                                ;
                                height++) // check each row below for terrain 
            {
                for (int width = x; width < x + TankModel.WIDTH &&
                                            width < Battlefield.WIDTH
                                            ;
                                            width++) // check each range of a row for terrain
                {
                    if (Get(width, height)) //check point for terrain
                    {
                        colisionCount++; // increment tile count for terrain
                    }
                }

                
            }
            if (colisionCount == 0) // if there are zero tiles in tank model
                {
                    badLocation = false;
                }
            else
            {
                badLocation = true;
            }

            return badLocation;
        }

        /// <summary>
        /// finds the highest y point that a tank can be placed
        /// </summary>
        /// <param name="x"></param>
        /// <returns>returns the y cordinate</returns>
        public int PlaceTankVertically(int x)
        {
            int highPoint=0;
            //check the height of the tank for the lowest point with
            for(int height = 0; height < HEIGHT; height++)
            {
                if (!TankFits(x, height))// check to see tank will fit at location
                {
                    highPoint = height; // get the highpoint
                    
                }
            }
            return highPoint;
        }

        /// <summary>
        ///  destroys all terrain within a circle 
        /// </summary>
        /// <param name="destroyX">centre of circle (X)</param>
        /// <param name="destroyY">centre of circle (Y)</param>
        /// <param name="radius">radius from centre</param>
        public void DestroyTiles(float destroyX, float destroyY, float radius)
        {
            //check the whole map for points inside destruction circle
            for(float height = 0; height < Battlefield.HEIGHT; height++)
            {
                for(float width = 0; width < Battlefield.WIDTH; width++)
                {
                    //work out distance between current point and destruction point
                    float distance = (float)Math.Sqrt(Math.Pow(destroyX - width, 2) + (Math.Pow(destroyY - height, 2)));
                    if (distance < radius) // check if point is inside circle
                    {
                        terrain[(int)height,(int)width] = false; //remove terrain from point
                    }
                }
            }
        }

        /// <summary>
        /// moves any loose terrain down one tile
        /// </summary>
        /// <returns></returns>
        public bool ProcessGravity()
        {
            bool movedTerrain = false; // used to store if any terrain is moved
            //loop through all possible points and find all floating terrain but we do it in reverse so the maximun move for a tile of terrain is one
            for(int height= Battlefield.HEIGHT-2; height >= 0; height--) 
            {
                for( int width = 0; width < Battlefield.WIDTH; width++)
                {
                    if(terrain[height,width] && !terrain[height + 1,width]) // if terrain found and none found below it
                    {
                        movedTerrain = true; //terrain drops a tile
                        for(int checkabove = 0; height - checkabove >= 0 && terrain[height-checkabove,width]; checkabove++) //check above tiles and move all terrain down 
                        {
                            terrain[height - checkabove, width] = false; // remove terrain from current position
                            terrain[height - checkabove + 1, width] = true; // add terrain below current position
                        }

                    }
                }
            }


            return movedTerrain;
        }

        private int GetStartingHeight ()
        {
            return startingHeight;
        }
    }
}
