/// <summary>
/// APP NAME : Slot Machine
/// AUTHOR : Preetinder Singh Brar
/// STUDENT NUMBER : 200334111
/// CREATE DATE : 17 April 2017
/// DESCRIPTION : The program that simulates a slot machine.
/// </summary>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachine
{
    public partial class SlotMachineForm : Form
    {
        //PRIVATE VARIABLES ***************************************************
        private int playerMoney = 100;
        private int winnings = 0;
        private int jackpot = 5000;
        private float turn = 0.0f;
        private int playerBet = 0;
        private float winNumber = 0.0f;
        private float lossNumber = 0.0f;
        private string[] spinResult;
        private string fruits = "";
        private float winRatio = 0.0f;
        private float lossRatio = 0.0f;
        private int grapes = 0;
        private int bananas = 0;
        private int oranges = 0;
        private int cherries = 0;
        private int bars = 0;
        private int bells = 0;
        private int sevens = 0;
        private int blanks = 0;

        private Random random = new Random();

        public SlotMachineForm()
        {
            InitializeComponent();
            updateTextBoxes();
        }

        /// <summary>
        /// Helper method to update all text boxes
        /// </summary>
        private void updateTextBoxes()
        {
            WinnerTextBox.Text = winnings.ToString("C", CultureInfo.CurrentCulture);
            BetTextBox.Text = playerBet.ToString("C", CultureInfo.CurrentCulture);
            TotalCreditTextBox.Text = playerMoney.ToString("C", CultureInfo.CurrentCulture);
            if (playerMoney < playerBet || playerBet == 0)
            {
                SpinPictureBox.Visible = false;
            }
            else
            {
                SpinPictureBox.Visible = true;
            }
        }


        /* Utility function to show Player Stats */
        private void showPlayerStats()
        {
            winRatio = winNumber / turn;
            lossRatio = lossNumber / turn;
            string stats = "";
            stats += ("Jackpot: " + jackpot + "\n");
            stats += ("Player Money: " + playerMoney + "\n");
            stats += ("Turn: " + turn + "\n");
            stats += ("Wins: " + winNumber + "\n");
            stats += ("Losses: " + lossNumber + "\n");
            stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
            stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
            MessageBox.Show(stats, "Player Stats");
        }

        /* Utility function to reset all fruit tallies*/
        private void resetFruitTally()
        {
            grapes = 0;
            bananas = 0;
            oranges = 0;
            cherries = 0;
            bars = 0;
            bells = 0;
            sevens = 0;
            blanks = 0;
        }

        /* Utility function to reset the player stats */
        private void resetAll()
        {
            playerMoney = 100;
            winnings = 0;
            jackpot = 5000;
            turn = 0;
            playerBet = 0;
            winNumber = 0;
            lossNumber = 0;
            winRatio = 0.0f;
        }

        /* Check to see if the player won the jackpot */
        private void checkJackPot()
        {
            /* compare two random values */
            var jackPotTry = this.random.Next(51) + 1;
            var jackPotWin = this.random.Next(51) + 1;
        }

        /* Utility function to show a win message and increase player money */
        private void showWinMessage()
        {
            playerMoney += winnings;
            //MessageBox.Show("You Won: $" + winnings, "Winner!");
            resetFruitTally();
            checkJackPot();
        }

        /* Utility function to show a loss message and reduce player money */
        private void showLossMessage()
        {
            playerMoney -= playerBet;
            //MessageBox.Show("You Lost!", "Loss!");
            resetFruitTally();
        }

        /* Utility function to check if a value falls within a range of bounds */
        private bool checkRange(int value, int lowerBounds, int upperBounds)
        {
            return (value >= lowerBounds && value <= upperBounds) ? true : false;

        }

        /* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
        private string[] Reels()
        {
            string[] betLine = { " ", " ", " " };
            int[] outCome = { 0, 0, 0 };

            for (var spin = 0; spin < 3; spin++)
            {
                outCome[spin] = this.random.Next(65) + 1;

                if (checkRange(outCome[spin], 1, 27))
                {  // 41.5% probability
                    betLine[spin] = "blank";
                    blanks++;
                }
                else if (checkRange(outCome[spin], 28, 37))
                { // 15.4% probability
                    betLine[spin] = "Grapes";
                    grapes++;
                }
                else if (checkRange(outCome[spin], 38, 46))
                { // 13.8% probability
                    betLine[spin] = "Banana";
                    bananas++;
                }
                else if (checkRange(outCome[spin], 47, 54))
                { // 12.3% probability
                    betLine[spin] = "Orange";
                    oranges++;
                }
                else if (checkRange(outCome[spin], 55, 59))
                { //  7.7% probability
                    betLine[spin] = "Cherry";
                    cherries++;
                }
                else if (checkRange(outCome[spin], 60, 62))
                { //  4.6% probability
                    betLine[spin] = "Bar";
                    bars++;
                }
                else if (checkRange(outCome[spin], 63, 64))
                { //  3.1% probability
                    betLine[spin] = "Bell";
                    bells++;
                }
                else if (checkRange(outCome[spin], 65, 65))
                { //  1.5% probability
                    betLine[spin] = "Seven";
                    sevens++;
                }

            }
            Slot1PictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(betLine[0].ToLower());
            Slot2PictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(betLine[1].ToLower());
            Slot3PictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(betLine[2].ToLower());
            return betLine;
        }

        /* This function calculates the player's winnings, if any */
        private void determineWinnings()
        {
            if (blanks == 0)
            {
                if (grapes == 3)
                {
                    winnings = playerBet * 10;
                }
                else if (bananas == 3)
                {
                    winnings = playerBet * 20;
                }
                else if (oranges == 3)
                {
                    winnings = playerBet * 30;
                }
                else if (cherries == 3)
                {
                    winnings = playerBet * 40;
                }
                else if (bars == 3)
                {
                    winnings = playerBet * 50;
                }
                else if (bells == 3)
                {
                    winnings = playerBet * 75;
                }
                else if (sevens == 3)
                {
                    winnings = playerBet * 100;
                }
                else if (grapes == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (bananas == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (oranges == 2)
                {
                    winnings = playerBet * 3;
                }
                else if (cherries == 2)
                {
                    winnings = playerBet * 4;
                }
                else if (bars == 2)
                {
                    winnings = playerBet * 5;
                }
                else if (bells == 2)
                {
                    winnings = playerBet * 10;
                }
                else if (sevens == 2)
                {
                    winnings = playerBet * 20;
                }
                else if (sevens == 1)
                {
                    winnings = playerBet * 5;
                }
                else
                {
                    winnings = playerBet * 1;
                }
                winNumber++;
                showWinMessage();
            }
            else
            {
                lossNumber++;
                winnings = 0;
                showLossMessage();
            }

        }

        /// <summary>
        /// Event handler to spin the reels on spin picture box click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpinPictureBox_Click(object sender, EventArgs e)
        {
            if (playerMoney == 0)
            {
                MessageBox.Show("Player money is zero!!");
            }
            else if (playerBet > playerMoney)
            {
                MessageBox.Show("Player bet is greator than player money!!");
            }
            else if (playerBet < 0)
            {
                MessageBox.Show("All bets must be a positive $ amount.", "Incorrect Bet");
            }
            else if (playerBet <= playerMoney)
            {
                spinResult = Reels();
                //fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
                //MessageBox.Show(fruits);
                determineWinnings();
                //turn++;
                //showPlayerStats();
            }
            else
            {
                MessageBox.Show("Please enter a valid bet amount");
            }
            updateTextBoxes();
        }

        /// <summary>
        /// event handler to exit the application on exit picture box click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// event handler to reset everything on reset picture box click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetPictureBox_Click(object sender, EventArgs e)
        {
            this.resetAll();
            updateTextBoxes();
        }

        /// <summary>
        /// event handler to set bet value based on the bet picture box click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (Convert.ToInt32(pictureBox.Tag) > playerMoney)
            {
                MessageBox.Show("Selected bet is greater than player money");
                return;
            }
            playerBet = Convert.ToInt32(pictureBox.Tag);
            updateTextBoxes();
        }
    }
}
