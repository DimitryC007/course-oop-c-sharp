using OtheloLogic;
using Ex02.ConsoleUtils;
using System;
using System.Threading;

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
            PrintBoard();
            int skipCount = 0;
            //while (true)
            while(skipCount < 2)
            {
                
                GameReport gameReport = null;
                //gameReport = _gameLogic.MakeMove();
                _gameLogic.FindAllAvailableMoves();
                bool isThereAnyMove = _gameLogic.IsThereMoves();

                if (!isThereAnyMove)
                {
                    skipCount++;
                    _gameLogic.SwitchPlayer();
                    continue;
                }
                
                

               

                                
                //Human
                if (!_gameLogic.CurrentPlayer.IsComputer)
                {
                

                    Console.WriteLine(Messages.PlayerTurnIndicationMessage(_gameLogic.CurrentPlayer.Name));
                    string userInput = GetPlayerMove();
                    if (userInput == _exitKey)
                    {
                   
                        Console.WriteLine("Hope you had a great time");
                        return;
                    }

                    gameReport = _gameLogic.MakeMove(userInput);

                    while(gameReport.MoveStatus == MoveStatus.MoveFailure)
                    {
                        Console.WriteLine("Unavailable move, Please try again");
                        userInput = GetPlayerMove();
                        gameReport = _gameLogic.MakeMove(userInput);
                    }

                    if (gameReport.MoveStatus == MoveStatus.MoveSkipped)
                    {
                        skipCount++;
                    }

                    else
                    {
                        skipCount = 0;
                    }

                    //if (skipCount == 2)
                    //{
                    //    break;
                    //}


                }
                //Computer
                if (_gameLogic.CurrentPlayer.IsComputer)
                {
                    ///TODO: add message computer is playing his turn now from messages
                    //Console.WriteLine("Computer is playing now");

                    gameReport = _gameLogic.MakeMove();
                }


                if (gameReport.GameStatus == GameStatus.GameOver)
                {
                    //endgame();
                  
                    break;
                }

                _gameLogic.SwitchPlayer();
                
                PrintBoard();
                PrintScore();

                if (_gameLogic.CurrentPlayer.IsComputer)
                {
                    Console.WriteLine("Computer is playing now");
                    Thread.Sleep(2000);
                }
                    
                ///TODO: check game report and behave accordingly to it
            }

            int winner = PrintScore();
            Console.WriteLine("Winner is player {0} ", _gameLogic.GetPlayerName(winner));
            return;
            //string playerAnswer = PlayAgain();
            ////Console.Write("Do you want to play again? (y/n)");
            
            //if(playerAnswer.ToLower() == "y")
            //{
            //    LaunchGame();
            //    PlayGame();
            //}

            //else
            //{
            //    return;
            //}
            
        }

        public void PlayAgain()
        {
            string playerAnswer = GetPlayAgainAnswer();
            //Console.Write("Do you want to play again? (y/n)");

            if (playerAnswer.ToLower() == "y")
            {
                LaunchGame();
                PlayGame();
            }

            else
            {
                return;
            }
        }

        private string GetPlayAgainAnswer()
        {
            Console.Write("Do you want to play again? (y/n)");
            bool isValidAnswer = false;
            string PlayerAnswer = string.Empty;

            while (!isValidAnswer)
            {
                PlayerAnswer = Console.ReadLine();
                isValidAnswer = Validations.IsPlayAgainAnswerValid(PlayerAnswer.ToLower());

                if(!isValidAnswer)
                {
                    Console.WriteLine("Please input (y/n)");
                }
            }

            return PlayerAnswer;
        }

        private void PrintAvailableMoves()
        {
            
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
            //Console.WriteLine(Messages.PlayerTurnIndicationMessage(_gameLogic.CurrentPlayer.Name));
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
                printDivide(_gameLogic.Board.Size);
                Console.Write($"{rows + 1} |");

                for (int cols = 0; cols < _gameLogic.Board.Size; cols++)
                {

                    if (!_gameLogic.Board.IsCellEmpty(rows, cols))
                    {
                        if (_gameLogic.Board.GetCellValue(rows, cols) == 0)
                            Console.Write(" X ");
                        else
                            Console.Write(" 0 ");
                    }
                    else
                        Console.Write("   ");
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            printDivide(_gameLogic.Board.Size);
            Console.WriteLine();

        }

        private void printDivide(int boardSize)
        {
            Console.Write("  ");
            for (int i = 0; i < boardSize * 4 + 1; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        private int PrintScore()
        {
            int xCount = 0;
            int oCount = 0;
            for(int rows = 0;rows < _gameLogic.Board.Size; rows++)
            {
                for(int cols = 0; cols < _gameLogic.Board.Size; cols++)
                {
                    if(!_gameLogic.Board.IsCellEmpty(rows,cols))
                    {
                        if (_gameLogic.Board.GetCellValue(rows, cols) == 0)
                            oCount++;
                        else
                            xCount++;
                    }
                }
            }

            Console.WriteLine("X Count: {0}, O Count: {1}", oCount, xCount);
            return oCount >= xCount ? 0 : 1;
        }

       
    }
}
