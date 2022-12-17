using OtheloLogic;
using Ex02.ConsoleUtils;
using System;

namespace OtheloUI
{
    public class UserInterface
    {
        private GameLogic _gameLogic;
        private const string _exitKey = "Q";

        public void LaunchGame()
        {
            GameSettings gameSettings = GetGameSettings();
            _gameLogic = new GameLogic(gameSettings);
        }

        public void PlayGame()
        {
            while (true)
            {
                //PrintBoard(_gameLogic._board.Matrix);
                GameReport gameReport = null;
                //Human
                if (!_gameLogic.CurrentPlayer.IsComputer)
                {
                    string userInput = GetPlayerMove();
                    if (userInput == _exitKey)
                    {
                        ///TODO: add message exit game
                        return;
                    }
                    gameReport = _gameLogic.MakeMove(userInput);

                }
                //Computer
                if (_gameLogic.CurrentPlayer.IsComputer)
                {
                    ///TODO: add message computer is playing his turn now from messages
                    Console.WriteLine("Computer is playing now");
                    gameReport = _gameLogic.MakeMove();
                }
                ///TODO: check game report and behave accordingly to it
            }
        }

        private GameSettings GetGameSettings()
        {
            GameSettings gameSettings = new GameSettings(2);
            gameSettings.Players[0] = GetPlayer(1);
            gameSettings.Players[1] = GetOponent();
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
            Console.WriteLine(Messages.PlayerTurnIndicationMessage(_gameLogic.CurrentPlayer.Name));
            Console.WriteLine(Messages.PlayerMoveMessage);

            string playerMove = string.Empty;
            bool isValidMove = false;
            
            while (!isValidMove)
            {
                playerMove = Console.ReadLine();
                isValidMove = Validations.IsMoveInputValid(playerMove, _exitKey);

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

        private void PrintBoard(Cell[,] matrix)
        {
            Screen.Clear();
            int matrixSize = GetMatrixSize(); // need to get size from game setting
            Console.Write("   ");
            for(int columns = 0; columns < matrixSize; columns++)
            {
                Console.Write(" {0}  ", Convert.ToChar(columns + (int)'A'));
            }
            Console.WriteLine();
            
            for(int rows = 0; rows < matrixSize; rows++)
            {
                printDivide(matrixSize);
                Console.Write($"{rows + 1} |");

                for(int cols = 0; cols < matrixSize; cols++)
                {

                    if (matrix[rows, cols].IsTaken)
                    {
                        if (matrix[rows, cols].Value == 0)
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

        }

        private void printDivide(int matrixSize)
        {
            Console.Write("   ");
            for(int i = 0;i < matrixSize * 4 + 1; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }
    }
}
