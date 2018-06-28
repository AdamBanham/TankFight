using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class PlayerController : GenericPlayer
    {
        public PlayerController(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            //do nothing here as this is a human player
        }

        public override void NewRound()
        {
            //do nothing here as this is a human player
        }

        public override void BeginTurn(BattleForm gameplayForm, Battle currentGame)
        {
            gameplayForm.EnableTankButtons(); // give player controls to fire
            gameplayForm.Focus(); // make sure the form has focus for keyboard controls
        }

        public override void ReportHit(float x, float y)
        {
            //do nothing here as this is a human player
        }
    }
}
