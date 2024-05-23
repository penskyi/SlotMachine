using SlotMachineGame;
using System;
using System.Data.SqlTypes;
using System.Security.AccessControl;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int playerMoneyTotal = 10;
            Random random = new Random();

            while (playerMoneyTotal > 0)
            {
                int[,] playScreen = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];

                for (int h = 0; h < Constants.GRID_SIZE; h++)
                {
                    for (int v = 0; v < Constants.GRID_SIZE; v++)
                    {
                        playScreen[h, v] = random.Next(0, 2);
                    }
                }

                for (int h = 0; h < Constants.GRID_SIZE; h++)
                {
                    for (int v = 0; v < Constants.GRID_SIZE; v++)
                    {
                        Console.Write(playScreen[h, v]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Input your wage amount in $:");
                int wageAmount;
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out wageAmount) && wageAmount > 0)
                {
                    Console.WriteLine("You bid was placed...");
                }
                else
                {
                    Console.WriteLine("Invalide wage amount. Please put a valide wage greater then zero");
                    continue;
                }


                Console.WriteLine("Your Remaining Money Total is:" + playerMoneyTotal);

                Console.WriteLine("Which line do you want to play?\n1 (Center line)\n2 (All Horizontal Lines)\n3 (All vertical lines and diagonals)");
                int playLine;
                string userInputPlayLine = Console.ReadLine();
                if (int.TryParse(userInputPlayLine, out playLine))
                {
                    Console.WriteLine("Your play mode amount is accepted...");
                }
                else
                {
                    Console.WriteLine("Please enter the correct play mode, it should be a number");
                    continue;
                }


                if (playLine != Constants.PLAY_MODE_ONE_LINE && playLine != Constants.PLAYMODE_ALL_LINES && playLine != Constants.PLAYMODE_ALL_LINES_AND_DIAGONALS)
                {
                    Console.WriteLine("Please select a correct mode");
                    continue;
                }

                Console.WriteLine("Spinning the wheels...");
                playerMoneyTotal -= wageAmount;

                // Check winning combinations based on the user choice 

                if (playLine == Constants.PLAY_MODE_ONE_LINE)
                {
                    int gridSize = Constants.GRID_SIZE;
                    int middleRowIndex = gridSize / 2;
                    for (int j = 0; j < gridSize; j++)
                    {
                        if (playScreen[middleRowIndex, j] != playScreen[middleRowIndex, 0])
                        {
                            Console.WriteLine("You Lost this round. Try again");
                            break;
                        }
                        Console.WriteLine("WIN! =)");
                        playerMoneyTotal += wageAmount * Constants.MULTIPLIER;
                        continue;
                    }
                }

                if (playLine == Constants.PLAYMODE_ALL_LINES)
                {
                    // Check all horizontal lines
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
                            playerMoneyTotal += wageAmount * Constants.MULTIPLIER;
                            Console.WriteLine("You won on the line " + (h + 1));
                        }
                        else
                            Console.WriteLine("You lost as line " + (h + 1) + " has not match");
                    }
                }

                if (playLine == Constants.PLAYMODE_ALL_LINES_AND_DIAGONALS)
                {
                    // Implement logic to check winning combination all vertical lines and diagonals
                    // Return true if winning combination is found, false otherwise
                    // Check all horizontal lines
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
                            playerMoneyTotal += wageAmount * Constants.MULTIPLIER;
                            Console.WriteLine("You won on the line " + (h + 1));
                        }
                        else
                            Console.WriteLine("You lost as line " + (h + 1) + " has not match");
                    }

                    // check diagonal
                    bool isWinningDiagonal = true;
                    for (int i = 1; i < Constants.GRID_SIZE; i++)
                    {
                        if (playScreen[i, i] != playScreen[0, 0])
                        {
                            isWinningDiagonal = false;
                            break;
                        }
                    }
                    if (isWinningDiagonal)
                    {
                        playerMoneyTotal += wageAmount * Constants.MULTIPLIER;
                        Console.WriteLine("You won on the diagonal");
                    }
                    else
                    {
                        Console.WriteLine("You lost in the diagonale");
                    }


                    // check reverse diagonal
                    bool isWinningDiagonalReverse = true;
                    for (int i = Constants.GRID_SIZE - 1; i >= 0; i--)
                    {
                        if (playScreen[i, Constants.GRID_SIZE - 1 - i] != playScreen[0, Constants.GRID_SIZE - 1])
                        {
                            isWinningDiagonalReverse = false;
                            break;
                        }
                    }
                    if (isWinningDiagonalReverse)
                    {
                        playerMoneyTotal += wageAmount * Constants.MULTIPLIER;
                        Console.WriteLine("You won on the reverse diagonal");
                    }
                    else
                    {
                        Console.WriteLine("You lost in the reverse diagonale");
                    }
                }

                if (playerMoneyTotal <= 0)
                {
                    Console.WriteLine("Game Over! No more money left!");
                    break;
                }
            }
        }
    }
}
