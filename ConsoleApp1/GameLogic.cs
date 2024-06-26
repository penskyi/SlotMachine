﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineGame
{
    internal class GameLogic
    {
        public static int[,] playScreen;
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
                    playScreen[h, v] = random.Next(Constants.GAME_BOARD_PLAY_RANGE_MIN, Constants.GAME_BOARD_PLAY_RANGE_MAX);
                }
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
                return CheckAllVerticalLinesAndDiagonals();
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
            bool allWinningLines = true;

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
                if (!isWinningLine)
                {
                    allWinningLines = false;
                    break; // exit the loop if any of the horizontal lines is not winning the round.
                }
            }
            if (allWinningLines)
            {
                PlayerMoneyTotal += WageAmount * Constants.MULTIPLIER;
            }
            return allWinningLines;
        }

        private static bool CheckAllVerticalLinesAndDiagonals()
        {
            bool allVerticalsWinning = true;
            for (int v = 0; v < Constants.GRID_SIZE; v++)
            {
                bool isVerticalWinning = true;
                for (int h = 1; h < Constants.GRID_SIZE; h++)
                {
                    if (playScreen[h, v] != playScreen[0, v])
                    {
                        isVerticalWinning = false;
                        break;
                    }
                }
                if (!isVerticalWinning)
                {
                    allVerticalsWinning = false;
                    break;
                }
            }
            // Check both diagonals
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
            bool allWinning = allVerticalsWinning && isWinningDiagonal && isWinningDiagonalReverse;
            // If all verticals and both diagonals are winning, only then declare a win
            if (allWinning)
            {
                PlayerMoneyTotal += WageAmount * Constants.MULTIPLIER;
            }

            return allWinning;
        }

        public static bool IsValidMove()
        {
            if (int.TryParse(UI.userInput, out UI.move) && UI.move >= Constants.PLAY_MODE_CHOICE_MIN && UI.move <= Constants.PLAY_MODE_CHOICE_MAX)
            {
                return true;
            }
            return false;
        }
    }
}
