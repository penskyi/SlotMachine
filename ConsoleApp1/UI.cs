using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineGame
{
    public class UI
    {
        public static void DisplayEmptyPlayScreen()
        {
            GameLogic.InitializePlayScreen();
            GameLogic.DisplayEmptyPlayScreen();
        }
        public static void DisplayPlayScreen()
        {
            GameLogic.DisplayPlayScreen();
        }

        public static void GetUserInput()
        {
            int wageAmount = GetWageAmount();
            while (!GameLogic.PutWageSum(wageAmount))
            {
                DisplayMessage("Invalid wage amount. Please enter a valid wage greater than zero and within your remaining money.");
                wageAmount = GetWageAmount();
            }

            int playMode = GetPlayMode();
            if (!GameLogic.SetPlayMode(playMode))
            {
                DisplayMessage("Please enter a correct play mode.");
                return; // exit on error
            }
        }

        public static int GetWageAmount()
        {
            Console.WriteLine("Input your wage amount in $:");
            if (int.TryParse(Console.ReadLine(), out int wageAmount))
            {
                return wageAmount;
            }
            return -1;
        }

        public static int GetPlayMode()
        {
            Console.WriteLine("Which line do you want to play?\n1 (Center line)\n2 (All Horizontal Lines)\n3 (All vertical lines and diagonals)");
            if (int.TryParse(Console.ReadLine(), out int playMode))
            {
                return playMode;
            }
            return -1;
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayRemainingMoney()
        {
            DisplayMessage("Your Remaining Money Total is: " + GameLogic.PlayerMoneyTotal);
        }


        public static void MakeSpin()
        {
            Console.Clear();
            DisplayMessage("Spinning the wheels...");
            GameLogic.PlaceBet();
            GameLogic.CheckForWin(GameLogic.playMode);
        }
        public static void ShowGameResult()
        {
            DisplayPlayScreen();

            if (GameLogic.CheckForWin(GameLogic.playMode))
            {
                DisplayMessage("WIN! =)");
            }
            else
            {
                DisplayMessage("You Lost this round. Try again");
            }

            DisplayRemainingMoney();

            if (GameLogic.PlayerMoneyTotal <= 0)
            {
                GameLogic.gameState = GameState.GameOver;
                GameLogic.playAgain = false; // we're ending the game and exisitng while loop
            }
        }

        public static void AskUserToPlayAgain()
        {
            DisplayMessage("Do you want to play again? (y/n)");
            char response = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (response != 'y' && response != 'Y')
            {
                GameLogic.playAgain = false;
                GameLogic.gameState = GameState.GameOver;
            }
        }

        public static void ShowEndGameMessage()
        {
            DisplayMessage("Game Over! No more money left!");
        }
    }
}