using System;
using System.Collections.Generic;
using System.Threading;

namespace OtheloLogic
{
    public enum GameStatus
    {
        GameOver = 0, // no moves left
        InProgress = 1, // game in progress
    }

    public enum MoveStatus
    {
        CellIsTaken = 0, // we need to know cell is taken to ask input from the user again 
        MoveSuccess = 1, // continue playing
        MoveFailure = 2, // input not valid from --> User
        MoveSkipped = 3, // doesn't have a move to make
        HasMoveToMake = 4 // has move to make
    }

    public class GameLogic
    {
        public Board Board { get; }
        private Player[] _players;
        public Player CurrentPlayer => _players[_playerIndex];
        private int _playerIndex = 1;
        private int _playerValue => _playerIndex;
        private int _oponentValue => _playerIndex == 0 ? 1 : 0;
        private Dictionary<char, int> _characterDict = new Dictionary<char, int>();
        private CurrentPlayerPoolMoves _currentPlayerPoolMoves;
        private int _skippedTurns = 0;
        private const int MaxSkippedTurnsAllowed = 2;

        public GameLogic(GameSettings gameSettings)
        {
            Board = new Board(gameSettings.MatrixSize);
            _currentPlayerPoolMoves = new CurrentPlayerPoolMoves(Board);
            _players = gameSettings.Players;
            InitializeInputDictionary(gameSettings.MatrixSize);
        }

        public GameReport CheckHasAnyMove()
        {
            _currentPlayerPoolMoves.InitializeAvailablePlayerMoves(_oponentValue);
            GameReport gameReport = InitializeGameReport();

            if (Board.IsFull || IsGameOver())
            {
                return CalcGameReport(MoveStatus.MoveFailure);
            }

            if (!_currentPlayerPoolMoves.HasAnyMove)
            {
                gameReport.MoveStatus = MoveStatus.MoveSkipped;
                _skippedTurns++;
                Sleep(2);
                if (_skippedTurns == MaxSkippedTurnsAllowed)
                {
                    return CalcGameReport(gameReport.MoveStatus);
                }

                SwitchPlayer();
                return gameReport;
            }

            ResetSkippedTurns();

            return gameReport;
        }

        public GameReport MakeMove(string position = "")
        {
            List<Coordinate> effectedFlipCoins;
            GameReport gameReport = InitializeGameReport();
            
            if (!CurrentPlayer.IsComputer)
            {
                int column = ConvertInputToColumn(position[0]);
                int row = ConvertInputToRow(position[1]);
                if (row == -1 || column == -1)
                {
                    gameReport.MoveStatus = MoveStatus.MoveFailure;
                    return gameReport;
                }

                if (!Board.IsCellEmpty(row, column))
                {
                    gameReport.MoveStatus = MoveStatus.CellIsTaken;
                    return gameReport;
                }

                effectedFlipCoins = _currentPlayerPoolMoves.GetEffectedFlipCoins(row, column);
            }
            else
            {
                Coordinate computerMove = GetComputerRandomMove();
                effectedFlipCoins = _currentPlayerPoolMoves.GetEffectedFlipCoins(computerMove.Row, computerMove.Column);
                Sleep(2);
            }

            gameReport.MoveStatus = SetPlayerMoves(effectedFlipCoins);
            if (gameReport.MoveStatus == MoveStatus.MoveSuccess)
            {
                SwitchPlayer();
            }
            
            return gameReport;
        }

        private bool IsGameOver()
        {
            bool hasOneValue = false;
            bool hasZeroValue = false;

            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (Board.GetCellValue(i, j) == 0)
                    {
                        hasZeroValue = true;
                    }

                    if (Board.GetCellValue(i, j) == 1)
                    {
                        hasOneValue = true;
                    }

                    if (hasZeroValue && hasOneValue)
                    {
                        return false;
                    }
                }
            }

            return hasZeroValue || hasOneValue;
        }

        private GameReport CalcGameReport(MoveStatus moveStatus)
        {
            int one = 0, zero = 0;
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (Board.GetCellValue(i, j) == 0)
                        zero++;
                    if (Board.GetCellValue(i, j) == 1)
                        one++;

                }
            }

            GameReport gameReport = new GameReport
            {
                GameStatus = GameStatus.GameOver,
                MoveStatus = moveStatus,
            };
            
            if (one > zero)
            {
                gameReport.WinnerPoints = one;
                gameReport.LoserPoints = zero;
                gameReport.Winner = _players[1];
                gameReport.Loser = _players[0];
            }
            else
            {
                gameReport.WinnerPoints = zero;
                gameReport.LoserPoints = one;
                gameReport.Winner = _players[0];
                gameReport.Loser = _players[1];
            }

            return gameReport;
        }

        private GameReport InitializeGameReport()
        {
            return new GameReport
            {
                GameStatus = GameStatus.InProgress,
                MoveStatus = MoveStatus.HasMoveToMake,
                LastMovePlayerName = CurrentPlayer.Name
            };
        }

        private MoveStatus SetPlayerMoves(List<Coordinate> moves)
        {
            if (moves.Count == 0)
            {
                return MoveStatus.MoveFailure;
            }

            foreach (var move in moves)
            {
                Board.SetCellValue(_playerValue, move.Row, move.Column);
            }

            return MoveStatus.MoveSuccess;
        }

        private Coordinate GetComputerRandomMove()
        {
            List<Coordinate> availableMoves = _currentPlayerPoolMoves.GetAllAvailableMoves();
            var random = new Random();
            int index = random.Next(availableMoves.Count);
            return availableMoves[index];
        }

        private int ConvertInputToColumn(char columnChar)
        {
            int column = -1;
            char upperCaseColumn = char.ToUpper(columnChar);
            if (_characterDict.ContainsKey(upperCaseColumn))
            {
                column = _characterDict[upperCaseColumn];
            }

            return column;
        }

        private int ConvertInputToRow(char rowChar)
        {
            int row = -1;

            if (rowChar >= '1' && rowChar <= '8')
            {
                if (Board.Size == 6)
                {
                    if (rowChar >= '1' && rowChar <= '6')
                        row = (int)char.GetNumericValue(rowChar) - 1;
                }
                else
                {
                    row = (int)char.GetNumericValue(rowChar) - 1;
                }
            }
            return row;
        }

        private void SwitchPlayer()
        {
            _playerIndex = _playerIndex == 0 ? 1 : 0;
        }

        private void ResetSkippedTurns()
        {
            _skippedTurns = 0;
        }

        private void InitializeInputDictionary(int matrixSize)
        {
            int maxUnicode = matrixSize == 8 ? 72 : 70;
            int unicode = 65;

            for (int i = unicode; i <= maxUnicode; i++)
            {
                char character = (char)i;
                _characterDict.Add(character, i - unicode);
            }
        }

        private void Sleep(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
