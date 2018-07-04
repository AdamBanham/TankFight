using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    abstract public class GenericPlayer
    {
        private string playerName;
        private TankModel playersTank;
        private Color playerColour;
        private int timesWon;

        /// <summary>
        /// sets up a player to begin playing the game
        /// </summary>
        /// <param name="name">name of a player</param>
        /// <param name="tank">player's tank</param>
        /// <param name="colour">colour of player's tank/s</param>
        public GenericPlayer(string name, TankModel tank, Color colour)
        {
            // set player up with all the passed information about the new player
            playerName = name;
            playersTank = tank;
            playerColour = colour;
            timesWon = 0;
        }

        /// <summary>
        /// returns the tank assoiated with this player
        /// </summary>
        /// <returns>tankModel of player's tank</returns>
        public TankModel GetTank()
        {
            return playersTank;
        }

        /// <summary>
        /// Returns the player's name
        /// </summary>
        /// <returns> a string of player's name</returns>
        public string Name()
        {
            return playerName;
        }

        /// <summary>
        /// returns the colour of a player
        /// </summary>
        /// <returns></returns>
        public Color PlayerColour()
        {
            return playerColour;
        }

        /// <summary>
        /// increment the times this player has won a round
        /// </summary>
        public void AddScore()
        {
            timesWon++;
        }

        /// <summary>
        /// returns the number of times a player has won
        /// </summary>
        /// <returns>a int of amount of times won</returns>
        public int GetVictories()
        {
            return timesWon;
        }

        public abstract void NewRound();

        public abstract void BeginTurn(BattleForm gameplayForm, Battle currentGame);

        public abstract void ReportHit(float x, float y);
    }
}
