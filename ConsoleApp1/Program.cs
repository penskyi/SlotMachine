using System.Data.SqlTypes;

namespace ConsoleApp1
{
    internal class Program
    {
        const int GRID_LENGHT_HORIZONTAL = 3;
        const int GRID_LENGHT_VERTICAL = 3;
        const int MULTIPLIER = 2;

        static void Main(string[] args)
        {
            int playerMoneyTotal = 10;

            while (playerMoneyTotal > 0)
            {
                Random random = new Random();
                int[,] playScreen = new int[GRID_LENGHT_HORIZONTAL, GRID_LENGHT_VERTICAL];

                for (int h = 0; h < GRID_LENGHT_HORIZONTAL; h++)
                {
                    for (int v = 0; v < GRID_LENGHT_VERTICAL; v++)
                    {
                        playScreen[h, v] = random.Next(0, 2);
                    }
                }

                for (int h = 0; h < GRID_LENGHT_HORIZONTAL; h++)
                {
                    for (int v = 0; v < GRID_LENGHT_VERTICAL; v++)
                    {
                        Console.Write(playScreen[h, v]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Input your wage amount in $:");
                int wageAmount = Convert.ToInt32(Console.ReadLine());
                playerMoneyTotal -= wageAmount;

                Console.WriteLine("Your Remaining Money Total is:" + playerMoneyTotal);

                Console.WriteLine("Which line do you want to play?\n1 (Center line)\n2 (All Horizontal Lines)\n3 (All vertical lines and diagonals)");
                int playLine = Convert.ToInt32(Console.ReadLine());

                // Check winning combinations based on the user choice 

                if (playLine == 1)
                {
                    // Implement logic to check winning combination for the center line
                    // Return true if winning combination is found, false otherwise
                }

                if (playLine == 2)
                {
                    // Implement logic to check winning combination for all horizontal lines
                    // Return true if winning combination is found, false otherwise
                }

                if (playLine == 3)
                {
                    // Implement logic to check winning combination all vertical lines and diagonals
                    // Return true if winning combination is found, false otherwise
                }

                else
                {
                    Console.WriteLine("Please choose a correct Play Line");
                    continue;
                }

                // calculate number of winnings * Mi;tiplier

                // add money if any winnings + you win
                
                
                // finish game if out of money 

                
            }
        }
    }
}
