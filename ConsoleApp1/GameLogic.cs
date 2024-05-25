using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineGame
{
    internal class GameLogic
    {
        private static int[,] playScreen;
        private static Random random = new Random();
        public static int PlayerMoneyTotal { get; private set; }
        public static int WageAmount { get; private set; }

        public static bool playAgain = true;
        public static GameState gameState = GameState.InProgress;
        public static int playMode;

        public static void Initialize(int initialMoney)
        {
            PlayerMoneyTotal = initialMoney;
        }

        public static void InitializePlayScreen()
        {
            playScreen = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];
            for (int h = 0; h < Constants.GRID_SIZE; h++)
            {
                for (int v = 0; v < Constants.GRID_SIZE; v++)
                {
                    playScreen[h, v] = random.Next(0, 2);
                }
            }
        }

        public static void DisplayPlayScreen()
        {
            for (int h = 0; h < Constants.GRID_SIZE; h++)
            {
                for (int v = 0; v < Constants.GRID_SIZE; v++)
                {
                    Console.Write(playScreen[h, v]);
                }
                Console.WriteLine();
            }
        }

        public static void DisplayEmptyPlayScreen()
        {
            for (int h = 0; h < Constants.GRID_SIZE; h++)
            {
                for (int v = 0; v < Constants.GRID_SIZE; v++)
                {
                    Console.Write("X");
                }
                Console.WriteLine();
            }
        }

        public static bool PutWageSum(int wageAmount)
        {
            if (wageAmount > 0 && wageAmount <= PlayerMoneyTotal)
            {
                WageAmount = wageAmount;
                return true;
            }
            return false;
        }

        public static bool SetPlayMode(int mode)
        {
            if (mode == Constants.PLAY_MODE_ONE_LINE || mode == Constants.PLAY_MODE_ALL_LINES || mode == Constants.PLAY_MODE_ALL_LINES_AND_DIAGONALS)
            {
                playMode = mode; // Set up playMode
                return true;
            }
            return false;
        }

        public static void PlaceBet()
        {
            PlayerMoneyTotal -= WageAmount;
        }

        public static bool CheckForWin(int mode)
        {
            if (mode == Constants.PLAY_MODE_ONE_LINE)
            {
                return CheckCenterLine();
            }

            if (mode == Constants.PLAY_MODE_ALL_LINES)
            {
                return CheckAllHorizontalLines();
            }

            if (mode == Constants.PLAY_MODE_ALL_LINES_AND_DIAGONALS)
            {
                return CheckAllHorizontalLines() || CheckDiagonals();
            }

            return false;
        }

        private static bool CheckCenterLine()
        {
            int middleRowIndex = Constants.GRID_SIZE / 2;
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                if (playScreen[middleRowIndex, j] != playScreen[middleRowIndex, 0])
                {
                    return false;
                }
            }
            PlayerMoneyTotal += WageAmount * Constants.MULTIPLIER;
            return true;
        }

        private static bool CheckAllHorizontalLines()
        {
            bool anyWinningLine = false;

            for (int h = 0; h < Constants.GRID_SIZE; h++)
            {
                bool isWinningLine = true;
                for (int v = 1; v < Constants.GRID_SIZE; v++)
                {
                    if (playScreen[h, v] != playScreen[h, 0])
                    {
                        isWinningLine = false;
                        break;
                    }
                }
                if (isWinningLine)
                {
                    PlayerMoneyTotal += WageAmount * Constants.MULTIPLIER;
                    anyWinningLine = true;
                }
            }
            return anyWinningLine;
        }

        private static bool CheckDiagonals()
        {
            bool isWinningDiagonal = true;
            bool isWinningDiagonalReverse = true;

            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (playScreen[i, i] != playScreen[0, 0])
                {
                    isWinningDiagonal = false;
                }

                if (playScreen[i, Constants.GRID_SIZE - 1 - i] != playScreen[0, Constants.GRID_SIZE - 1])
                {
                    isWinningDiagonalReverse = false;
                }
            }

            if (isWinningDiagonal || isWinningDiagonalReverse)
            {
                PlayerMoneyTotal += WageAmount * Constants.MULTIPLIER;
            }

            return isWinningDiagonal || isWinningDiagonalReverse;
        }
    }
}
