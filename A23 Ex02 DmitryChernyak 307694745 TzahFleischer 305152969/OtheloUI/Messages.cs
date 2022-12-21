using System;

namespace OtheloUI
{
    public static class Messages
    {
        public static string InvalidPlayerNameMessage = "Player name is not acceptable, please try again";
        public static string MatrixSizeMessage = $"Enter matrix size:{Environment.NewLine}- for 6x6 enter 6{Environment.NewLine}- for 8x8 enter 8";
        public static string AgainstWhoPlayingMessage = $"Who do you want to play against?{Environment.NewLine}- for second Player enter 1{Environment.NewLine}- for computer enter 2";
        public static string InputNotValidMessage = "Input not valid, please try again";
        public static string PlayerMoveMessage = "Enter your move:";

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
