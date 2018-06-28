using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class IntroForm : Form
    {
        public IntroForm()
        {
            InitializeComponent();
        }

        [STAThread]
        private void newGameButton_Click(object sender, EventArgs e)
        {
            // hide this screen
            //this.Dispose();
            // move to setup screen
            SetupGameForm beginSetup = (new SetupGameForm() );
            beginSetup.Owner = this.Owner;
            beginSetup.Show();

            ////part 1 of setting up game (whos and how long)
            //Battle game = new Battle(2, 1);

            ////part 2 of setting up game ( whats each player)
            //GenericPlayer player1 = new PlayerController("Overlord 1", TankModel.GetTank(1), Battle.TankColour(1));
            //GenericPlayer player2 = new PlayerController("Lessor Lord 1", TankModel.GetTank(1), Battle.TankColour(2));

            ////part 3 of setting up game ( give game players )
            //game.SetPlayer(1, player1);
            //game.SetPlayer(2, player2);

            //// create new game ( call game )
            //game.NewGame();
        }
    }
}
