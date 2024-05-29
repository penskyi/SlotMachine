using System;
using System.Data.SqlTypes;
using System.Security.AccessControl;

namespace SlotMachineGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic.Initialize(Constants.PLAYER_MONEY_TOTAL);

            while (GameLogic.playAgain)
            {
                GameLogic.gameState = GameState.InProgress; // we need to reset a game for each game session.
                while (GameLogic.gameState == GameState.InProgress)
                {               
                    UI.DisplayEmptyPlayScreen();
                    UI.DisplayCurrentMoney();
                    UI.GetUserInput();
                    UI.MakeSpin();
                    UI.ShowGameResult();
                }
                if (GameLogic.PlayerMoneyTotal > 0)
                {
                    UI.AskUserToPlayAgain();
                }
                else
                {
                    UI.ShowEndGameMessage();
                    GameLogic.playAgain = false; // ending the game
                }
            }
        }      
    }
}
