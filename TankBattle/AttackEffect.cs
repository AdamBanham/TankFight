using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public abstract class AttackEffect
    {
        protected Battle currrentGame;

        /// <summary>
        /// sets current game to effects
        /// </summary>
        /// <param name="game">current game</param>
        public void SetCurrentGame(Battle game)
        {
            currrentGame = game;
        }

        public abstract void Tick();
        public abstract void Paint(Graphics graphics, Size displaySize);
    }
}
