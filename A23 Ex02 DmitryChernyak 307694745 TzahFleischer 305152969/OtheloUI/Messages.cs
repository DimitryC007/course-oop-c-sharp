using OtheloLogic;
using System;

namespace OtheloUI
{
    public static class Messages
    {
        public static string s_InvalidPlayerNameMessage = "Player name is not acceptable, please try again";
        public static string s_MatrixSizeMessage = $"Enter matrix size:{Environment.NewLine}- for 6x6 enter 6{Environment.NewLine}- for 8x8 enter 8";
        public static string s_AgainstWhoPlayingMessage = $"Who do you want to play against?{Environment.NewLine}- for second Player enter 1{Environment.NewLine}- for computer enter 2";
        public static string s_InputNotValidMessage = "Input not valid, please try again";
        public static string s_PlayerMoveMessage = "Enter your move:";
        public static string s_ExitGameMessage = "Exiting the game";
        public static string s_AnotherRoundMessage = $"Game Over: Do you want to play another round?{Environment.NewLine}- for another round enter 1{Environment.NewLine}- for exit enter 2";
        public static string s_WelcomeMessage = "Welcome to Othelo game";
        public static string s_LineMessage = "----------------------";

        public static string GameReportMessage(GameReport gameReport)
        {
            return string.Format("| Game report | Winner: {0} Points: {1} | Loser: {2} Points: {3} |",
                gameReport.Winner.Name, gameReport.WinnerPoints, gameReport.Loser.Name, gameReport.LoserPoints);
        }

        public static string PlayerMoveIndicationMessage(string playerName, eMoveStatuses moveStatus)
        {
            string moveText = string.Empty;
            
            if (moveStatus == eMoveStatuses.MoveSkipped)
            {
                moveText = "Skipped";
            }

            if (moveStatus == eMoveStatuses.MoveSuccess)
            {
                moveText = "Success";
            }

            if (moveStatus == eMoveStatuses.CellIsTaken)
            {
                moveText = "Cell is taken";
            }

            if (moveStatus == eMoveStatuses.MoveFailure)
            {
                moveText = "Failed";
            }

            return string.Format("| Last move | Player: {0} | Move status: {1} |", playerName, moveText);
        }

        public static string PlayerTurnIndicationMessage(string playerName)
        {
            return string.Format("It's {0}'s turn", playerName);
        }

        public static string GetPlayerNameMessage(int playerNumber)
        {
            return string.Format("Enter {0} player Name:", playerNumber == 1 ? "first" : "second");
        }
    }

}
