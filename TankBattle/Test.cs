using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TankBattle;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TankBattleTestSuite
{
    class RequirementException : Exception
    {
        public RequirementException()
        {
        }

        public RequirementException(string message) : base(message)
        {
        }

        public RequirementException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    class Test
    {
        #region Testing Code

        private delegate bool TestCase();

        private static string ErrorDescription = null;

        private static void SetErrorDescription(string desc)
        {
            ErrorDescription = desc;
        }

        private static bool FloatEquals(float a, float b)
        {
            if (Math.Abs(a - b) < 0.01) return true;
            return false;
        }

        private static Dictionary<string, string> unitTestResults = new Dictionary<string, string>();

        private static void Passed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[passed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                throw new Exception("ErrorDescription found for passing test case");
            }
            Console.WriteLine();
        }
        private static void Failed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[failed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                Console.Write("\n{0}", ErrorDescription);
                ErrorDescription = null;
            }
            Console.WriteLine();
        }
        private static void FailedToMeetRequirement(string name, string comment)
        {
            Console.Write("[      ] ");
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("{0}", comment);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void DoTest(TestCase test)
        {
            // Have we already completed this test?
            if (unitTestResults.ContainsKey(test.Method.ToString()))
            {
                return;
            }

            bool passed = false;
            bool metRequirement = true;
            string exception = "";
            try
            {
                passed = test();
            }
            catch (RequirementException e)
            {
                metRequirement = false;
                exception = e.Message;
            }
            catch (Exception e)
            {
                exception = e.GetType().ToString();
            }

            string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
            string fnName = test.Method.ToString().Split('0')[1];

            if (metRequirement)
            {
                if (passed)
                {
                    unitTestResults[test.Method.ToString()] = "Passed";
                    Passed(string.Format("{0}.{1}", className, fnName), exception);
                }
                else
                {
                    unitTestResults[test.Method.ToString()] = "Failed";
                    Failed(string.Format("{0}.{1}", className, fnName), exception);
                }
            }
            else
            {
                unitTestResults[test.Method.ToString()] = "Failed";
                FailedToMeetRequirement(string.Format("{0}.{1}", className, fnName), exception);
            }
            Cleanup();
        }

        private static Stack<string> errorDescriptionStack = new Stack<string>();


        private static void Requires(TestCase test)
        {
            string result;
            bool wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

            if (!wasTested)
            {
                // Push the error description onto the stack (only thing that can change, not that it should)
                errorDescriptionStack.Push(ErrorDescription);

                // Do the test
                DoTest(test);

                // Pop the description off
                ErrorDescription = errorDescriptionStack.Pop();

                // Get the proper result for out
                wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

                if (!wasTested)
                {
                    throw new Exception("This should never happen");
                }
            }

            if (result == "Failed")
            {
                string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
                string fnName = test.Method.ToString().Split('0')[1];

                throw new RequirementException(string.Format("-> {0}.{1}", className, fnName));
            }
            else if (result == "Passed")
            {
                return;
            }
            else
            {
                throw new Exception("This should never happen");
            }

        }

        #endregion

        #region Test Cases
        private static Battle InitialiseGame()
        {
            Requires(TestBattle0Battle);
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);
            Requires(TestBattle0SetPlayer);

            Battle game = new Battle(2, 1);
            TankModel tank = TankModel.GetTank(1);
            GenericPlayer player1 = new PlayerController("player1", tank, Color.Orange);
            GenericPlayer player2 = new PlayerController("player2", tank, Color.Purple);
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);
            return game;
        }
        private static void Cleanup()
        {
            while (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Dispose();
            }
        }
        private static bool TestBattle0Battle()
        {
            Battle game = new Battle(2, 1);
            return true;
        }
        private static bool TestBattle0NumPlayers()
        {
            Requires(TestBattle0Battle);

            Battle game = new Battle(2, 1);
            return game.NumPlayers() == 2;
        }
        private static bool TestBattle0GetNumRounds()
        {
            Requires(TestBattle0Battle);

            Battle game = new Battle(3, 5);
            return game.GetNumRounds() == 5;
        }
        private static bool TestBattle0SetPlayer()
        {
            Requires(TestBattle0Battle);
            Requires(TestTankModel0GetTank);

            Battle game = new Battle(2, 1);
            TankModel tank = TankModel.GetTank(1);
            GenericPlayer player = new PlayerController("playerName", tank, Color.Orange);
            game.SetPlayer(1, player);
            return true;
        }
        private static bool TestBattle0GetPlayerNumber()
        {
            Requires(TestBattle0Battle);
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            Battle game = new Battle(2, 1);
            TankModel tank = TankModel.GetTank(1);
            GenericPlayer player = new PlayerController("playerName", tank, Color.Orange);
            game.SetPlayer(1, player);
            return game.GetPlayerNumber(1) == player;
        }
        private static bool TestBattle0TankColour()
        {
            Color[] arrayOfColours = new Color[8];
            for (int i = 0; i < 8; i++)
            {
                arrayOfColours[i] = Battle.TankColour(i + 1);
                for (int j = 0; j < i; j++)
                {
                    if (arrayOfColours[j] == arrayOfColours[i]) return false;
                }
            }
            return true;
        }
        private static bool TestBattle0CalcPlayerLocations()
        {
            int[] positions = Battle.CalcPlayerLocations(8);
            for (int i = 0; i < 8; i++)
            {
                if (positions[i] < 0) return false;
                if (positions[i] > 160) return false;
                for (int j = 0; j < i; j++)
                {
                    if (positions[j] == positions[i]) return false;
                }
            }
            return true;
        }
        private static bool TestBattle0RandomReorder()
        {
            int[] ar = new int[100];
            for (int i = 0; i < 100; i++)
            {
                ar[i] = i;
            }
            Battle.RandomReorder(ar);
            for (int i = 0; i < 100; i++)
            {
                if (ar[i] != i)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestBattle0NewGame()
        {
            Battle game = InitialiseGame();
            game.NewGame();

            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestBattle0GetLevel()
        {
            Requires(TestBattlefield0Battlefield);
            Battle game = InitialiseGame();
            game.NewGame();
            Battlefield battlefield = game.GetLevel();
            if (battlefield != null) return true;

            return false;
        }
        private static bool TestBattle0GetPlayerTank()
        {
            Requires(TestBattle0Battle);
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);
            Requires(TestBattle0SetPlayer);
            Requires(TestGameplayTank0GetPlayerNumber);

            Battle game = new Battle(2, 1);
            TankModel tank = TankModel.GetTank(1);
            GenericPlayer player1 = new PlayerController("player1", tank, Color.Orange);
            GenericPlayer player2 = new PlayerController("player2", tank, Color.Purple);
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);

            game.NewGame();
            GameplayTank ptank = game.GetPlayerTank();
            if (ptank.GetPlayerNumber() != player1 && ptank.GetPlayerNumber() != player2)
            {
                return false;
            }
            if (ptank.GetTank() != tank)
            {
                return false;
            }

            return true;
        }

        private static bool TestTankModel0GetTank()
        {
            TankModel tank = TankModel.GetTank(1);
            if (tank != null) return true;
            else return false;
        }
        private static bool TestTankModel0DisplayTank()
        {
            Requires(TestTankModel0GetTank);
            TankModel tank = TankModel.GetTank(1);

            int[,] tankGraphic = tank.DisplayTank(45);
            if (tankGraphic.GetLength(0) != 12) return false;
            if (tankGraphic.GetLength(1) != 16) return false;
            // We don't really care what the tank looks like, but the 45 degree tank
            // should at least look different to the -45 degree tank
            int[,] tankGraphic2 = tank.DisplayTank(-45);
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (tankGraphic2[y, x] != tankGraphic[y, x])
                    {
                        return true;
                    }
                }
            }

            SetErrorDescription("Tank with turret at -45 degrees looks the same as tank with turret at 45 degrees");

            return false;
        }
        private static void DisplayLine(int[,] array)
        {
            string report = "";
            report += "A line drawn from 3,0 to 0,3 on a 4x4 array should look like this:\n";
            report += "0001\n";
            report += "0010\n";
            report += "0100\n";
            report += "1000\n";
            report += "The one produced by TankModel.SetLine() looks like this:\n";
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    report += array[y, x] == 1 ? "1" : "0";
                }
                report += "\n";
            }
            SetErrorDescription(report);
        }
        private static bool TestTankModel0SetLine()
        {
            int[,] ar = new int[,] { { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 } };
            TankModel.SetLine(ar, 3, 0, 0, 3);

            // Ideally, the line we want to see here is:
            // 0001
            // 0010
            // 0100
            // 1000

            // However, as we aren't that picky, as long as they have a 1 in every row and column
            // and nothing in the top-left and bottom-right corners

            int[] rows = new int[4];
            int[] cols = new int[4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (ar[y, x] == 1)
                    {
                        rows[y] = 1;
                        cols[x] = 1;
                    }
                    else if (ar[y, x] > 1 || ar[y, x] < 0)
                    {
                        // Only values 0 and 1 are permitted
                        SetErrorDescription(string.Format("Somehow the number {0} got into the array.", ar[y, x]));
                        return false;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (rows[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
                if (cols[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
            }
            if (ar[0, 0] == 1)
            {
                DisplayLine(ar);
                return false;
            }
            if (ar[3, 3] == 1)
            {
                DisplayLine(ar);
                return false;
            }

            return true;
        }
        private static bool TestTankModel0GetTankHealth()
        {
            Requires(TestTankModel0GetTank);
            // As long as it's > 0 we're happy
            TankModel tank = TankModel.GetTank(1);
            if (tank.GetTankHealth() > 0) return true;
            return false;
        }
        private static bool TestTankModel0ListWeapons()
        {
            Requires(TestTankModel0GetTank);
            // As long as there's at least one result and it's not null / a blank string, we're happy
            TankModel tank = TankModel.GetTank(1);
            if (tank.ListWeapons().Length == 0) return false;
            if (tank.ListWeapons()[0] == null) return false;
            if (tank.ListWeapons()[0] == "") return false;
            return true;
        }

        private static GenericPlayer CreateTestingPlayer()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            TankModel tank = TankModel.GetTank(1);
            GenericPlayer player = new PlayerController("player1", tank, Color.Aquamarine);
            return player;
        }

        private static bool TestGenericPlayer0PlayerController()
        {
            Requires(TestTankModel0GetTank);

            TankModel tank = TankModel.GetTank(1);
            GenericPlayer player = new PlayerController("player1", tank, Color.Aquamarine);
            if (player != null) return true;
            return false;
        }
        private static bool TestGenericPlayer0GetTank()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            TankModel tank = TankModel.GetTank(1);
            GenericPlayer p = new PlayerController("player1", tank, Color.Aquamarine);
            if (p.GetTank() == tank) return true;
            return false;
        }
        private static bool TestGenericPlayer0Name()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            const string PLAYER_NAME = "kfdsahskfdajh";
            TankModel tank = TankModel.GetTank(1);
            GenericPlayer p = new PlayerController(PLAYER_NAME, tank, Color.Aquamarine);
            if (p.Name() == PLAYER_NAME) return true;
            return false;
        }
        private static bool TestGenericPlayer0PlayerColour()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestGenericPlayer0PlayerController);

            Color playerColour = Color.Chartreuse;
            TankModel tank = TankModel.GetTank(1);
            GenericPlayer p = new PlayerController("player1", tank, playerColour);
            if (p.PlayerColour() == playerColour) return true;
            return false;
        }
        private static bool TestGenericPlayer0AddScore()
        {
            GenericPlayer p = CreateTestingPlayer();
            p.AddScore();
            return true;
        }
        private static bool TestGenericPlayer0GetVictories()
        {
            Requires(TestGenericPlayer0AddScore);

            GenericPlayer p = CreateTestingPlayer();
            int wins = p.GetVictories();
            p.AddScore();
            if (p.GetVictories() == wins + 1) return true;
            return false;
        }
        private static bool TestPlayerController0NewRound()
        {
            GenericPlayer p = CreateTestingPlayer();
            p.NewRound();
            return true;
        }
        private static bool TestPlayerController0BeginTurn()
        {
            Requires(TestBattle0NewGame);
            Requires(TestBattle0GetPlayerNumber);
            Battle game = InitialiseGame();

            game.NewGame();

            // Find the gameplay form
            BattleForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    gameplayForm = f as BattleForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Battle.NewGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in BattleForm");
                return false;
            }

            // Disable the control panel to check that NewTurn enables it
            controlPanel.Enabled = false;

            game.GetPlayerNumber(1).BeginTurn(gameplayForm, game);

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after HumanPlayer.NewTurn()");
                return false;
            }
            return true;

        }
        private static bool TestPlayerController0ReportHit()
        {
            GenericPlayer p = CreateTestingPlayer();
            p.ReportHit(0, 0);
            return true;
        }

        private static bool TestGameplayTank0GameplayTank()
        {
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            return true;
        }
        private static bool TestGameplayTank0GetPlayerNumber()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            if (playerTank.GetPlayerNumber() == p) return true;
            return false;
        }
        private static bool TestGameplayTank0GetTank()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGenericPlayer0GetTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            if (playerTank.GetTank() == playerTank.GetPlayerNumber().GetTank()) return true;
            return false;
        }
        private static bool TestGameplayTank0GetTankAngle()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            float angle = playerTank.GetTankAngle();
            if (angle >= -90 && angle <= 90) return true;
            return false;
        }
        private static bool TestGameplayTank0Aim()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0GetTankAngle);
            float angle = 75;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.Aim(angle);
            if (FloatEquals(playerTank.GetTankAngle(), angle)) return true;
            return false;
        }
        private static bool TestGameplayTank0GetPower()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);

            playerTank.GetPower();
            return true;
        }
        private static bool TestGameplayTank0SetTankPower()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0GetPower);
            int power = 65;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.SetTankPower(power);
            if (playerTank.GetPower() == power) return true;
            return false;
        }
        private static bool TestGameplayTank0GetWeapon()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);

            playerTank.GetWeapon();
            return true;
        }
        private static bool TestGameplayTank0SetWeapon()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0GetWeapon);
            int weapon = 3;
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.SetWeapon(weapon);
            if (playerTank.GetWeapon() == weapon) return true;
            return false;
        }
        private static bool TestGameplayTank0Paint()
        {
            Requires(TestGameplayTank0GameplayTank);
            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.Paint(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestGameplayTank0GetX()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, x, y, game);
            if (playerTank.GetX() == x) return true;
            return false;
        }
        private static bool TestGameplayTank0Y()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, x, y, game);
            if (playerTank.Y() == y) return true;
            return false;
        }
        private static bool TestGameplayTank0Fire()
        {
            Requires(TestGameplayTank0GameplayTank);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.Fire();
            return true;
        }
        private static bool TestGameplayTank0DamagePlayer()
        {
            Requires(TestGameplayTank0GameplayTank);
            GenericPlayer p = CreateTestingPlayer();

            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            playerTank.DamagePlayer(10);
            return true;
        }
        private static bool TestGameplayTank0TankExists()
        {
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0DamagePlayer);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            GameplayTank playerTank = new GameplayTank(p, 32, 32, game);
            if (!playerTank.TankExists()) return false;
            playerTank.DamagePlayer(playerTank.GetTank().GetTankHealth());
            if (playerTank.TankExists()) return false;
            return true;
        }
        private static bool TestGameplayTank0ProcessGravity()
        {
            Requires(TestBattle0GetLevel);
            Requires(TestBattlefield0DestroyTiles);
            Requires(TestGameplayTank0GameplayTank);
            Requires(TestGameplayTank0DamagePlayer);
            Requires(TestGameplayTank0TankExists);
            Requires(TestGameplayTank0GetTank);
            Requires(TestTankModel0GetTankHealth);

            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            // Unfortunately we need to rely on DestroyTerrain() to get rid of any terrain that may be in the way
            game.GetLevel().DestroyTiles(Battlefield.WIDTH / 2.0f, Battlefield.HEIGHT / 2.0f, 20);
            GameplayTank playerTank = new GameplayTank(p, Battlefield.WIDTH / 2, Battlefield.HEIGHT / 2, game);
            int oldX = playerTank.GetX();
            int oldY = playerTank.Y();

            playerTank.ProcessGravity();

            if (playerTank.GetX() != oldX)
            {
                SetErrorDescription("Caused X coordinate to change.");
                return false;
            }
            if (playerTank.Y() != oldY + 1)
            {
                SetErrorDescription("Did not cause Y coordinate to increase by 1.");
                return false;
            }

            int initialArmour = playerTank.GetTank().GetTankHealth();
            // The tank should have lost 1 armour from falling 1 tile already, so do
            // (initialArmour - 2) damage to the tank then drop it again. That should kill it.

            if (!playerTank.TankExists())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.DamagePlayer(initialArmour - 2);
            if (!playerTank.TankExists())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.ProcessGravity();
            if (playerTank.TankExists())
            {
                SetErrorDescription("Tank survived despite taking enough falling damage to destroy it");
                return false;
            }

            return true;
        }
        private static bool TestBattlefield0Battlefield()
        {
            Battlefield battlefield = new Battlefield();
            return true;
        }
        private static bool TestBattlefield0Get()
        {
            Requires(TestBattlefield0Battlefield);

            bool foundTrue = false;
            bool foundFalse = false;
            Battlefield battlefield = new Battlefield();
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.Get(x, y))
                    {
                        foundTrue = true;
                    }
                    else
                    {
                        foundFalse = true;
                    }
                }
            }

            if (!foundTrue)
            {
                SetErrorDescription("IsTileAt() did not return true for any tile.");
                return false;
            }

            if (!foundFalse)
            {
                SetErrorDescription("IsTileAt() did not return false for any tile.");
                return false;
            }

            return true;
        }
        private static bool TestBattlefield0TankFits()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0Get);

            Battlefield battlefield = new Battlefield();
            for (int y = 0; y <= Battlefield.HEIGHT - TankModel.HEIGHT; y++)
            {
                for (int x = 0; x <= Battlefield.WIDTH - TankModel.WIDTH; x++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < TankModel.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < TankModel.WIDTH; ix++)
                        {

                            if (battlefield.Get(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        if (battlefield.TankFits(x, y))
                        {
                            SetErrorDescription("Found collision where there shouldn't be one");
                            return false;
                        }
                    }
                    else
                    {
                        if (!battlefield.TankFits(x, y))
                        {
                            SetErrorDescription("Didn't find collision where there should be one");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private static bool TestBattlefield0PlaceTankVertically()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0Get);

            Battlefield battlefield = new Battlefield();
            for (int x = 0; x <= Battlefield.WIDTH - TankModel.WIDTH; x++)
            {
                int lowestValid = 0;
                for (int y = 0; y <= Battlefield.HEIGHT - TankModel.HEIGHT; y++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < TankModel.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < TankModel.WIDTH; ix++)
                        {

                            if (battlefield.Get(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        lowestValid = y;
                    }
                }

                int placedY = battlefield.PlaceTankVertically(x);
                if (placedY != lowestValid)
                {
                    SetErrorDescription(string.Format("Tank was placed at {0},{1} when it should have been placed at {0},{2}", x, placedY, lowestValid));
                    return false;
                }
            }
            return true;
        }
        private static bool TestBattlefield0DestroyTiles()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0Get);

            Battlefield battlefield = new Battlefield();
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.Get(x, y))
                    {
                        battlefield.DestroyTiles(x, y, 0.5f);
                        if (battlefield.Get(x, y))
                        {
                            SetErrorDescription("Attempted to destroy terrain but it still exists");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            SetErrorDescription("Did not find any terrain to destroy");
            return false;
        }
        private static bool TestBattlefield0ProcessGravity()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0Get);
            Requires(TestBattlefield0DestroyTiles);

            Battlefield battlefield = new Battlefield();
            for (int x = 0; x < Battlefield.WIDTH; x++)
            {
                if (battlefield.Get(x, Battlefield.HEIGHT - 1))
                {
                    if (battlefield.Get(x, Battlefield.HEIGHT - 2))
                    {
                        // Seek up and find the first non-set tile
                        for (int y = Battlefield.HEIGHT - 2; y >= 0; y--)
                        {
                            if (!battlefield.Get(x, y))
                            {
                                // Do a gravity step and make sure it doesn't slip down
                                battlefield.ProcessGravity();
                                if (!battlefield.Get(x, y + 1))
                                {
                                    SetErrorDescription("Moved down terrain even though there was no room");
                                    return false;
                                }

                                // Destroy the bottom-most tile
                                battlefield.DestroyTiles(x, Battlefield.HEIGHT - 1, 0.5f);

                                // Do a gravity step and make sure it does slip down
                                battlefield.ProcessGravity();

                                if (battlefield.Get(x, y + 1))
                                {
                                    SetErrorDescription("Terrain didn't fall");
                                    return false;
                                }

                                // Otherwise this seems to have worked
                                return true;
                            }
                        }


                    }
                }
            }
            SetErrorDescription("Did not find any appropriate terrain to test");
            return false;
        }
        private static bool TestAttackEffect0SetCurrentGame()
        {
            Requires(TestExplosion0Explosion);
            Requires(TestBattle0Battle);

            AttackEffect weaponEffect = new Explosion(1, 1, 1);
            Battle game = new Battle(2, 1);
            weaponEffect.SetCurrentGame(game);
            return true;
        }
        private static bool TestBullet0Bullet()
        {
            Requires(TestExplosion0Explosion);
            GenericPlayer player = CreateTestingPlayer();
            Explosion explosion = new Explosion(1, 1, 1);
            Bullet projectile = new Bullet(25, 25, 45, 30, 0.02f, explosion, player);
            return true;
        }
        private static bool TestBullet0Tick()
        {
            Requires(TestBattle0NewGame);
            Requires(TestExplosion0Explosion);
            Requires(TestBullet0Bullet);
            Requires(TestAttackEffect0SetCurrentGame);
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Explosion explosion = new Explosion(1, 1, 1);

            Bullet projectile = new Bullet(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.SetCurrentGame(game);
            projectile.Tick();

            // We can't really test this one without a substantial framework,
            // so we just call it and hope that everything works out

            return true;
        }
        private static bool TestBullet0Paint()
        {
            Requires(TestBattle0NewGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestExplosion0Explosion);
            Requires(TestBullet0Bullet);
            Requires(TestAttackEffect0SetCurrentGame);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the projectile
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Explosion explosion = new Explosion(1, 1, 1);

            Bullet projectile = new Bullet(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.SetCurrentGame(game);
            projectile.Paint(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestExplosion0Explosion()
        {
            GenericPlayer player = CreateTestingPlayer();
            Explosion explosion = new Explosion(1, 1, 1);

            return true;
        }
        private static bool TestExplosion0Explode()
        {
            Requires(TestExplosion0Explosion);
            Requires(TestAttackEffect0SetCurrentGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);

            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Explosion explosion = new Explosion(1, 1, 1);
            explosion.SetCurrentGame(game);
            explosion.Explode(25, 25);

            return true;
        }
        private static bool TestExplosion0Tick()
        {
            Requires(TestExplosion0Explosion);
            Requires(TestAttackEffect0SetCurrentGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);
            Requires(TestExplosion0Explode);

            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Explosion explosion = new Explosion(1, 1, 1);
            explosion.SetCurrentGame(game);
            explosion.Explode(25, 25);
            explosion.Tick();

            // Again, we can't really test this one without a full framework

            return true;
        }
        private static bool TestExplosion0Paint()
        {
            Requires(TestExplosion0Explosion);
            Requires(TestAttackEffect0SetCurrentGame);
            Requires(TestBattle0GetPlayerNumber);
            Requires(TestBattle0NewGame);
            Requires(TestExplosion0Explode);
            Requires(TestExplosion0Tick);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the explosion
            GenericPlayer p = CreateTestingPlayer();
            Battle game = InitialiseGame();
            game.NewGame();
            GenericPlayer player = game.GetPlayerNumber(1);
            Explosion explosion = new Explosion(10, 10, 10);
            explosion.SetCurrentGame(game);
            explosion.Explode(25, 25);
            // Step it for a bit so we can be sure the explosion is visible
            for (int i = 0; i < 10; i++)
            {
                explosion.Tick();
            }
            explosion.Paint(graphics, bitmapSize);

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }

        private static BattleForm InitialiseBattleForm(out NumericUpDown angleCtrl, out TrackBar powerCtrl, out Button fireCtrl, out Panel controlPanel, out ListBox weaponSelect)
        {
            Requires(TestBattle0NewGame);

            Battle game = InitialiseGame();

            angleCtrl = null;
            powerCtrl = null;
            fireCtrl = null;
            controlPanel = null;
            weaponSelect = null;

            game.NewGame();
            BattleForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    gameplayForm = f as BattleForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Battle.NewGame() did not create a BattleForm and that is the only way BattleForm can be tested");
                return null;
            }

            bool foundDisplayPanel = false;
            bool foundControlPanel = false;

            foreach (Control c in gameplayForm.Controls)
            {
                // The only controls should be 2 panels
                if (c is Panel)
                {
                    // Is this the control panel or the display panel?
                    Panel p = c as Panel;

                    // The display panel will have 0 controls.
                    // The control panel will have separate, of which only a few are mandatory
                    int controlsFound = 0;
                    bool foundFire = false;
                    bool foundAngle = false;
                    bool foundAngleLabel = false;
                    bool foundPower = false;
                    bool foundPowerLabel = false;


                    foreach (Control pc in p.Controls)
                    {
                        controlsFound++;

                        // Mandatory controls for the control panel are:
                        // A 'Fire!' button
                        // A NumericUpDown for controlling the angle
                        // A TrackBar for controlling the power
                        // "Power:" and "Angle:" labels

                        if (pc is Label)
                        {
                            Label lbl = pc as Label;
                            if (lbl.Text.ToLower().Contains("angle"))
                            {
                                foundAngleLabel = true;
                            }
                            else
                            if (lbl.Text.ToLower().Contains("power"))
                            {
                                foundPowerLabel = true;
                            }
                        }
                        else
                        if (pc is Button)
                        {
                            Button btn = pc as Button;
                            if (btn.Text.ToLower().Contains("fire"))
                            {
                                foundFire = true;
                                fireCtrl = btn;
                            }
                        }
                        else
                        if (pc is TrackBar)
                        {
                            foundPower = true;
                            powerCtrl = pc as TrackBar;
                        }
                        else
                        if (pc is NumericUpDown)
                        {
                            foundAngle = true;
                            angleCtrl = pc as NumericUpDown;
                        }
                        else
                        if (pc is ListBox)
                        {
                            weaponSelect = pc as ListBox;
                        }
                    }

                    if (controlsFound == 0)
                    {
                        foundDisplayPanel = true;
                    }
                    else
                    {
                        if (!foundFire)
                        {
                            SetErrorDescription("Control panel lacks a \"Fire!\" button OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngle)
                        {
                            SetErrorDescription("Control panel lacks an angle NumericUpDown OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPower)
                        {
                            SetErrorDescription("Control panel lacks a power TrackBar OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngleLabel)
                        {
                            SetErrorDescription("Control panel lacks an \"Angle:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPowerLabel)
                        {
                            SetErrorDescription("Control panel lacks a \"Power:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }

                        foundControlPanel = true;
                        controlPanel = p;
                    }

                }
                else
                {
                    SetErrorDescription(string.Format("Unexpected control ({0}) named \"{1}\" found in BattleForm", c.GetType().FullName, c.Name));
                    return null;
                }
            }

            if (!foundDisplayPanel)
            {
                SetErrorDescription("No display panel found");
                return null;
            }
            if (!foundControlPanel)
            {
                SetErrorDescription("No control panel found");
                return null;
            }
            return gameplayForm;
        }

        private static bool TestBattleForm0BattleForm()
        {
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            return true;
        }
        private static bool TestBattleForm0EnableTankButtons()
        {
            Requires(TestBattleForm0BattleForm);
            Battle game = InitialiseGame();
            game.NewGame();

            // Find the gameplay form
            BattleForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    gameplayForm = f as BattleForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Battle.NewGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in BattleForm");
                return false;
            }

            // Disable the control panel to check that EnableControlPanel enables it
            controlPanel.Enabled = false;

            gameplayForm.EnableTankButtons();

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after BattleForm.EnableTankButtons()");
                return false;
            }
            return true;

        }
        private static bool TestBattleForm0Aim()
        {
            Requires(TestBattleForm0BattleForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            float testAngle = 27;

            gameplayForm.Aim(testAngle);
            if (FloatEquals((float)angle.Value, testAngle)) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set angle to {0} but angle is {1}", testAngle, (float)angle.Value));
                return false;
            }
        }
        private static bool TestBattleForm0SetTankPower()
        {
            Requires(TestBattleForm0BattleForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            int testPower = 71;

            gameplayForm.SetTankPower(testPower);
            if (power.Value == testPower) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set power to {0} but power is {1}", testPower, power.Value));
                return false;
            }
        }
        private static bool TestBattleForm0SetWeapon()
        {
            Requires(TestBattleForm0BattleForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            gameplayForm.SetWeapon(0);

            // WeaponSelect is optional behaviour, so it's okay if it's not implemented here, as long as the method works.
            return true;
        }
        private static bool TestBattleForm0Fire()
        {
            Requires(TestBattleForm0BattleForm);
            // This is something we can't really test properly without a proper framework, so for now we'll just click
            // the button and make sure it disables the control panel
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            controlPanel.Enabled = true;
            fire.PerformClick();
            if (controlPanel.Enabled)
            {
                SetErrorDescription("Control panel still enabled immediately after clicking fire button");
                return false;
            }

            return true;
        }
        private static void UnitTests()
        {
            DoTest(TestBattle0Battle);
            DoTest(TestBattle0NumPlayers);
            DoTest(TestBattle0GetNumRounds);
            DoTest(TestBattle0SetPlayer);
            DoTest(TestBattle0GetPlayerNumber);
            DoTest(TestBattle0TankColour);
            DoTest(TestBattle0CalcPlayerLocations);
            DoTest(TestBattle0RandomReorder);
            DoTest(TestBattle0NewGame);
            DoTest(TestBattle0GetLevel);
            DoTest(TestBattle0GetPlayerTank);
            DoTest(TestTankModel0GetTank);
            DoTest(TestTankModel0DisplayTank);
            DoTest(TestTankModel0SetLine);
            DoTest(TestTankModel0GetTankHealth);
            DoTest(TestTankModel0ListWeapons);
            DoTest(TestGenericPlayer0PlayerController);
            DoTest(TestGenericPlayer0GetTank);
            DoTest(TestGenericPlayer0Name);
            DoTest(TestGenericPlayer0PlayerColour);
            DoTest(TestGenericPlayer0AddScore);
            DoTest(TestGenericPlayer0GetVictories);
            DoTest(TestPlayerController0NewRound);
            DoTest(TestPlayerController0BeginTurn);
            DoTest(TestPlayerController0ReportHit);
            DoTest(TestGameplayTank0GameplayTank);
            DoTest(TestGameplayTank0GetPlayerNumber);
            DoTest(TestGameplayTank0GetTank);
            DoTest(TestGameplayTank0GetTankAngle);
            DoTest(TestGameplayTank0Aim);
            DoTest(TestGameplayTank0GetPower);
            DoTest(TestGameplayTank0SetTankPower);
            DoTest(TestGameplayTank0GetWeapon);
            DoTest(TestGameplayTank0SetWeapon);
            DoTest(TestGameplayTank0Paint);
            DoTest(TestGameplayTank0GetX);
            DoTest(TestGameplayTank0Y);
            DoTest(TestGameplayTank0Fire);
            DoTest(TestGameplayTank0DamagePlayer);
            DoTest(TestGameplayTank0TankExists);
            DoTest(TestGameplayTank0ProcessGravity);
            DoTest(TestBattlefield0Battlefield);
            DoTest(TestBattlefield0Get);
            DoTest(TestBattlefield0TankFits);
            DoTest(TestBattlefield0PlaceTankVertically);
            DoTest(TestBattlefield0DestroyTiles);
            DoTest(TestBattlefield0ProcessGravity);
            DoTest(TestAttackEffect0SetCurrentGame);
            DoTest(TestBullet0Bullet);
            DoTest(TestBullet0Tick);
            DoTest(TestBullet0Paint);
            DoTest(TestExplosion0Explosion);
            DoTest(TestExplosion0Explode);
            DoTest(TestExplosion0Tick);
            DoTest(TestExplosion0Paint);
            DoTest(TestBattleForm0BattleForm);
            DoTest(TestBattleForm0EnableTankButtons);
            DoTest(TestBattleForm0Aim);
            DoTest(TestBattleForm0SetTankPower);
            DoTest(TestBattleForm0SetWeapon);
            DoTest(TestBattleForm0Fire);
        }
        
        #endregion
        
        #region CheckClasses

        private static bool CheckClasses()
        {
            string[] classNames = new string[] { "Program", "AIOpponent", "Battlefield", "Explosion", "BattleForm", "Battle", "PlayerController", "Bullet", "GenericPlayer", "GameplayTank", "TankModel", "AttackEffect" };
            string[][] classFields = new string[][] {
                new string[] { "Main" }, // Program
                new string[] { }, // AIOpponent
                new string[] { "Get","TankFits","PlaceTankVertically","DestroyTiles","ProcessGravity","WIDTH","HEIGHT"}, // Battlefield
                new string[] { "Explode" }, // Explosion
                new string[] { "EnableTankButtons","Aim","SetTankPower","SetWeapon","Fire","InitBuffer"}, // BattleForm
                new string[] { "NumPlayers","CurrentRound","GetNumRounds","SetPlayer","GetPlayerNumber","GetBattleTank","TankColour","CalcPlayerLocations","RandomReorder","NewGame","StartRound","GetLevel","DrawPlayers","GetPlayerTank","AddEffect","ProcessWeaponEffects","RenderEffects","RemoveWeaponEffect","CheckCollidedTank","DamagePlayer","ProcessGravity","EndTurn","CheckWinner","NextRound","GetWind"}, // Battle
                new string[] { }, // PlayerController
                new string[] { }, // Bullet
                new string[] { "GetTank","Name","PlayerColour","AddScore","GetVictories","NewRound","BeginTurn","ReportHit"}, // GenericPlayer
                new string[] { "GetPlayerNumber","GetTank","GetTankAngle","Aim","GetPower","SetTankPower","GetWeapon","SetWeapon","Paint","GetX","Y","Fire","DamagePlayer","TankExists","ProcessGravity"}, // GameplayTank
                new string[] { "DisplayTank","SetLine","CreateTankBMP","GetTankHealth","ListWeapons","ActivateWeapon","GetTank","WIDTH","HEIGHT","NUM_TANKS"}, // TankModel
                new string[] { "SetCurrentGame","Tick","Paint"} // AttackEffect
            };

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("Checking classes for public methods...");
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    if (type.Namespace != "TankBattle")
                    {
                        Console.WriteLine("Public type {0} is not in the TankBattle namespace.", type.FullName);
                        return false;
                    }
                    else
                    {
                        int typeIdx = -1;
                        for (int i = 0; i < classNames.Length; i++)
                        {
                            if (type.Name == classNames[i])
                            {
                                typeIdx = i;
                                classNames[typeIdx] = null;
                                break;
                            }
                        }
                        foreach (MemberInfo memberInfo in type.GetMembers())
                        {
                            string memberName = memberInfo.Name;
                            bool isInherited = false;
                            foreach (MemberInfo parentMemberInfo in type.BaseType.GetMembers())
                            {
                                if (memberInfo.Name == parentMemberInfo.Name)
                                {
                                    isInherited = true;
                                    break;
                                }
                            }
                            if (!isInherited)
                            {
                                if (typeIdx != -1)
                                {
                                    bool fieldFound = false;
                                    if (memberName[0] != '.')
                                    {
                                        foreach (string allowedFields in classFields[typeIdx])
                                        {
                                            if (memberName == allowedFields)
                                            {
                                                fieldFound = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fieldFound = true;
                                    }
                                    if (!fieldFound)
                                    {
                                        Console.WriteLine("The public field \"{0}\" is not one of the authorised fields for the {1} class.\n", memberName, type.Name);
                                        Console.WriteLine("Remove it or change its access level.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    //Console.WriteLine("{0} passed.", type.FullName);
                }
            }
            for (int i = 0; i < classNames.Length; i++)
            {
                if (classNames[i] != null)
                {
                    Console.WriteLine("The class \"{0}\" is missing.", classNames[i]);
                    return false;
                }
            }
            Console.WriteLine("All public methods okay.");
            return true;
        }
        
        #endregion

        public static void Main()
        {
            if (CheckClasses())
            {
                UnitTests();

                int passed = 0;
                int failed = 0;
                foreach (string key in unitTestResults.Keys)
                {
                    if (unitTestResults[key] == "Passed")
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                }

                Console.WriteLine("\n{0}/{1} unit tests passed", passed, passed + failed);
                if (failed == 0)
                {
                    Console.WriteLine("Starting up TankBattle...");
                    Program.Main();
                    return;
                }
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
