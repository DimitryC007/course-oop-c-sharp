using OtheloLogic;
using Ex02.ConsoleUtils;
using System;

namespace OtheloUI
{
    public class UserInterface
    {
        private GameLogic m_GameLogic;
        private const string k_ExitKey = "Q";
        private const int k_PlayAgain = 1;

        public void LaunchGame()
        {
            Screen.Clear();
            PrintMessage(Messages.s_WelcomeMessage);
            PrintMessage(Messages.s_LineMessage);
            GameSettings gameSettings = GetGameSettings();
            m_GameLogic = new GameLogic(gameSettings);
            PrintBoard();
        }

        public bool PlayGame()
        {
            string userInput = string.Empty;

            while (true)
            {                
                GameReport gameReport = m_GameLogic.CheckHasAnyMove();

                if (gameReport.GameStatus == eGameStatuses.GameOver)
                {
                    PrintMessage(Messages.GameReportMessage(gameReport));
                    return RestartGame();
                }

                if (gameReport.MoveStatus == eMoveStatuses.MoveSkipped)
                {
                    PrintBoard();
                    PrintMessage(Messages.PlayerMoveIndicationMessage(gameReport.LastMovePlayerName, gameReport.MoveStatus));
                    continue;
                }

                PrintMessage(Messages.PlayerTurnIndicationMessage(m_GameLogic.m_CurrentPlayer.Name));

                //Human
                if (!m_GameLogic.m_CurrentPlayer.IsComputer)
                {
                    userInput = GetPlayerMove();

                    if (userInput == k_ExitKey)
                    {
                        PrintMessage(Messages.s_ExitGameMessage);
                        return false;
                    }
                }

                gameReport = m_GameLogic.MakeMove(userInput);
                PrintBoard();
                PrintMessage(Messages.PlayerMoveIndicationMessage(gameReport.LastMovePlayerName, gameReport.MoveStatus));
            }
        }

        private bool RestartGame()
        {
            PrintMessage(Messages.s_AnotherRoundMessage);
            string anotherRoundChoise = string.Empty;
            bool isValidMove = false;

            while (!isValidMove)
            {
                anotherRoundChoise = Console.ReadLine();
                isValidMove = Validations.IsAnotherRoundChoiseValid(anotherRoundChoise);

                if (!isValidMove)
                {
                    Console.WriteLine(Messages.s_InputNotValidMessage);
                }
            }

            return int.Parse(anotherRoundChoise) == k_PlayAgain;
        }

        private GameSettings GetGameSettings()
        {
            GameSettings gameSettings = new GameSettings(2);
            gameSettings.Players[1] = GetPlayer(1);
            gameSettings.Players[0] = GetOponent();
            gameSettings.MatrixSize = GetMatrixSize();

            return gameSettings;
        }

        private string GetPlayerName(int playerNumber)
        {
            bool isPlayerNameValid = false;
            string playerName = string.Empty;

            while (!isPlayerNameValid)
            {
                Console.WriteLine(Messages.GetPlayerNameMessage(playerNumber));
                playerName = Console.ReadLine();
                isPlayerNameValid = Validations.IsPlayerNameInputValid(playerName);

                if (!isPlayerNameValid)
                {
                    Console.WriteLine(Messages.s_InvalidPlayerNameMessage);
                }
            }

            return playerName;
        }

        private Player GetPlayer(int playerNumber)
        {
            string FirstPlayerName = GetPlayerName(playerNumber);
            return new Player(FirstPlayerName, false);
        }

        private Player GetOponent()
        {
            bool isSecondOponentValid = false;
            string secondOponentInput = string.Empty;

            while (!isSecondOponentValid)
            {
                Console.WriteLine(Messages.s_AgainstWhoPlayingMessage);
                secondOponentInput = Console.ReadLine();
                isSecondOponentValid = Validations.IsSecondOponentValid(secondOponentInput);

                if (!isSecondOponentValid)
                {
                    Console.WriteLine(Messages.s_InputNotValidMessage);
                }
            }

            int secondOponentChoise = int.Parse(secondOponentInput);
            bool isComputer = secondOponentChoise == 2;
            if (isComputer)
            {
                return new Player("Computer", isComputer);
            }
            return GetPlayer(2);
        }

        private string GetPlayerMove()
        {
            Console.WriteLine(Messages.s_PlayerMoveMessage);

            string playerMove = string.Empty;
            bool isValidMove = false;

            while (!isValidMove)
            {
                playerMove = Console.ReadLine();
                isValidMove = Validations.IsMoveInputValid(playerMove, k_ExitKey);

                if (!isValidMove)
                {
                    Console.WriteLine(Messages.s_InputNotValidMessage);
                }
            }

            return playerMove;
        }

        private int GetMatrixSize()
        {
            bool isMatrixSizeValid = false;
            string matrixSize = string.Empty;

            while (!isMatrixSizeValid)
            {
                Console.WriteLine(Messages.s_MatrixSizeMessage);
                matrixSize = Console.ReadLine();

                isMatrixSizeValid = Validations.IsMatrixSizeInputValid(matrixSize);

                if (!isMatrixSizeValid)
                {
                    Console.WriteLine(Messages.s_InputNotValidMessage);
                }
            }

            return int.Parse(matrixSize);
        }

        private void PrintBoard()
        {
            Screen.Clear();
            Console.Write("   ");

            for (int columns = 0; columns < m_GameLogic.m_Board.m_Size; columns++)
            {
                Console.Write(" {0}  ", Convert.ToChar(columns + (int)'A'));
            }

            Console.WriteLine();

            for (int rows = 0; rows < m_GameLogic.m_Board.m_Size; rows++)
            {
                PrintDivide(m_GameLogic.m_Board.m_Size);
                Console.Write($"{rows + 1} |");

                for (int cols = 0; cols < m_GameLogic.m_Board.m_Size; cols++)
                {

                    if (!m_GameLogic.m_Board.IsCellEmpty(rows, cols))
                    {
                        if (m_GameLogic.m_Board.GetCellValue(rows, cols) == 0)
                        {
                            Console.Write(" O ");
                        }
                        else
                        {
                            Console.Write(" X ");
                        }
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.Write("|");
                }

                Console.WriteLine();
            }

            PrintDivide(m_GameLogic.m_Board.m_Size);
            Console.WriteLine();
        }

        private void PrintDivide(int boardSize)
        {
            Console.Write("  ");

            for (int i = 0; i < boardSize * 4 + 1; i++)
            {
                Console.Write("=");
            }

            Console.WriteLine();
        }

        private void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
