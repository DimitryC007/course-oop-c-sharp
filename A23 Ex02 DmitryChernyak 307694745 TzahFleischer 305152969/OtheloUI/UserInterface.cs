using OtheloLogic;
using Ex02.ConsoleUtils;
using System;

namespace OtheloUI
{
    public class UserInterface
    {
        private GameLogic _gameLogic;
        private const string ExitKey = "Q";
        private const int PlayAgain = 1;

        public void LaunchGame()
        {
            Screen.Clear();
            PrintMessage(Messages.WelcomeMessage);
            PrintMessage(Messages.LineMessage);
            GameSettings gameSettings = GetGameSettings();
            _gameLogic = new GameLogic(gameSettings);
            PrintBoard();
        }

        public bool PlayGame()
        {
            string userInput = string.Empty;

            while (true)
            {                
                GameReport gameReport = _gameLogic.CheckHasAnyMove();

                if (gameReport.GameStatus == GameStatus.GameOver)
                {
                    PrintMessage(Messages.GameReportMessage(gameReport));
                    return RestartGame();
                }

                if (gameReport.MoveStatus == MoveStatus.MoveSkipped)
                {
                    PrintBoard();
                    PrintMessage(Messages.PlayerMoveIndicationMessage(gameReport.LastMovePlayerName, gameReport.MoveStatus));
                    continue;
                }

                PrintMessage(Messages.PlayerTurnIndicationMessage(_gameLogic.CurrentPlayer.Name));

                //Human
                if (!_gameLogic.CurrentPlayer.IsComputer)
                {
                    userInput = GetPlayerMove();
                    if (userInput == ExitKey)
                    {
                        PrintMessage(Messages.ExitGameMessage);
                        return false;
                    }
                }

                gameReport = _gameLogic.MakeMove(userInput);
                PrintBoard();
                PrintMessage(Messages.PlayerMoveIndicationMessage(gameReport.LastMovePlayerName, gameReport.MoveStatus));
            }
        }

        private bool RestartGame()
        {
            PrintMessage(Messages.AnotherRoundMessage);
            string anotherRoundChoise = string.Empty;
            bool isValidMove = false;

            while (!isValidMove)
            {
                anotherRoundChoise = Console.ReadLine();
                isValidMove = Validations.IsAnotherRoundChoiseValid(anotherRoundChoise);

                if (!isValidMove)
                {
                    Console.WriteLine(Messages.InputNotValidMessage);
                }
            }
            return int.Parse(anotherRoundChoise) == PlayAgain;
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
                    Console.WriteLine(Messages.InvalidPlayerNameMessage);
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
                Console.WriteLine(Messages.AgainstWhoPlayingMessage);
                secondOponentInput = Console.ReadLine();
                isSecondOponentValid = Validations.IsSecondOponentValid(secondOponentInput);

                if (!isSecondOponentValid)
                {
                    Console.WriteLine(Messages.InputNotValidMessage);
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
            Console.WriteLine(Messages.PlayerMoveMessage);

            string playerMove = string.Empty;
            bool isValidMove = false;

            while (!isValidMove)
            {
                playerMove = Console.ReadLine();
                isValidMove = Validations.IsMoveInputValid(playerMove, ExitKey);

                if (!isValidMove)
                {
                    Console.WriteLine(Messages.InputNotValidMessage);
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
                Console.WriteLine(Messages.MatrixSizeMessage);
                matrixSize = Console.ReadLine();

                isMatrixSizeValid = Validations.IsMatrixSizeInputValid(matrixSize);

                if (!isMatrixSizeValid)
                {
                    Console.WriteLine(Messages.InputNotValidMessage);
                }
            }

            return int.Parse(matrixSize);
        }

        private void PrintBoard()
        {
            Screen.Clear();
            Console.Write("   ");
            for (int columns = 0; columns < _gameLogic.Board.Size; columns++)
            {
                Console.Write(" {0}  ", Convert.ToChar(columns + (int)'A'));
            }
            Console.WriteLine();

            for (int rows = 0; rows < _gameLogic.Board.Size; rows++)
            {
                PrintDivide(_gameLogic.Board.Size);
                Console.Write($"{rows + 1} |");

                for (int cols = 0; cols < _gameLogic.Board.Size; cols++)
                {

                    if (!_gameLogic.Board.IsCellEmpty(rows, cols))
                    {
                        if (_gameLogic.Board.GetCellValue(rows, cols) == 0)
                            Console.Write(" O ");
                        else
                            Console.Write(" X ");
                    }
                    else
                        Console.Write("   ");
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            PrintDivide(_gameLogic.Board.Size);
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
