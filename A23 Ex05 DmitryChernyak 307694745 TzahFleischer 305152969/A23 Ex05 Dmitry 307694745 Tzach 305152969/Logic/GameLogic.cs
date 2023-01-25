using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Logic
{
    public delegate void BoardChangedEventHandler(int row, int col, eCellState newState);

    public enum eGameStatuses
    {
        GameOver,
        InProgress
    }

    public enum eMoveStatuses
    {
        CellIsTaken,
        MoveSuccess,
        MoveFailure,
        MoveSkipped,
        HasMoveToMake
    }

    public class GameLogic
    {
        private GameSettings _gameSettings;

        public Board m_Board { get; }
        private Player[] m_Players;
        public Player m_CurrentPlayer => m_Players[m_PlayerIndex];
        private int m_PlayerIndex = 1;
        private eCellState m_PlayerValue => m_PlayerIndex == 0 ? eCellState.Black : eCellState.White;
        private eCellState m_OponentValue => m_PlayerIndex == 0 ? eCellState.White : eCellState.Black;
        private Dictionary<char, int> m_CharacterDict = new Dictionary<char, int>();
        private CurrentPlayerMoves m_CurrentPlayerMoves;
        private int m_SkippedTurns = 0;
        private const int k_MaxSkippedTurnsAllowed = 2;

        public event BoardChangedEventHandler BoardChanged;

        public GameLogic(GameSettings i_GameSettings)
        {
            _gameSettings = i_GameSettings;
            m_Board = new Board(i_GameSettings.MatrixSize);
            m_Board.BoardChanged += OnBoardChanged;
            
        }

        public void InitGame()
        {
            m_Board.InitializeBoard();
            m_CurrentPlayerMoves = new CurrentPlayerMoves(m_Board);
            m_Players = _gameSettings.Players;
            InitializeInputDictionary(_gameSettings.MatrixSize);
        }

        private void OnBoardChanged(int row, int col, eCellState newState)
        {
            if(BoardChanged != null)
            {
                BoardChanged.Invoke(row, col, newState);
            }
        }

        public GameReport CheckHasAnyMove()
        {
            m_CurrentPlayerMoves.AllCurrentPlayerMoves(m_OponentValue);

            GameReport gameReport = InitializeGameReport();

            if (m_Board.m_IsFull || IsGameOver())
            {
                return CalcGameReport(eMoveStatuses.MoveFailure);
            }

            if (!m_CurrentPlayerMoves.HasAnyMove)
            {
                gameReport.MoveStatus = eMoveStatuses.MoveSkipped;
                m_SkippedTurns++;
                Sleep(2);

                if (m_SkippedTurns == k_MaxSkippedTurnsAllowed)
                {
                    return CalcGameReport(gameReport.MoveStatus);
                }

                SwitchPlayer();
                return gameReport;
            }

            ResetSkippedTurns();

            return gameReport;
        }

        public GameReport MakeMove(string i_Position)
        {
            List<Coordinate> effectedFlipCoins;
            GameReport gameReport = InitializeGameReport();

            if (!m_CurrentPlayer.IsComputer)
            {
                int column = ConvertInputToColumn(i_Position[0]);
                int row = ConvertInputToRow(i_Position[1]);

                if (row == -1 || column == -1)
                {
                    gameReport.MoveStatus = eMoveStatuses.MoveFailure;
                    return gameReport;
                }

                if (!m_Board.IsCellEmpty(row, column))
                {
                    gameReport.MoveStatus = eMoveStatuses.CellIsTaken;
                    return gameReport;
                }

                Coordinate coordinate = new Coordinate(row, column);
                effectedFlipCoins = m_CurrentPlayerMoves.GetFlippableList(coordinate);
            }
            else
            {
                Coordinate computerMove = GetComputerRandomMove();
                effectedFlipCoins = m_CurrentPlayerMoves.GetFlippableList(computerMove);
                Sleep(2);
            }

            gameReport.MoveStatus = SetPlayerMoves(effectedFlipCoins);

            if (gameReport.MoveStatus == eMoveStatuses.MoveSuccess)
            {
                SwitchPlayer();
            }

            return gameReport;
        }

        private bool IsGameOver()
        {
            bool hasOneValue = false;
            bool hasZeroValue = false;

            for (int i = 0; i < m_Board.m_Size; i++)
            {
                for (int j = 0; j < m_Board.m_Size; j++)
                {
                    if (m_Board.GetCellValue(i, j) == eCellState.Black)
                    {
                        hasZeroValue = true;
                    }

                    if (m_Board.GetCellValue(i, j) == eCellState.White)
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

        private GameReport CalcGameReport(eMoveStatuses i_MoveStatus)
        {
            int one = 0, zero = 0;

            for (int i = 0; i < m_Board.m_Size; i++)
            {
                for (int j = 0; j < m_Board.m_Size; j++)
                {
                    if (m_Board.GetCellValue(i, j) == eCellState.Black)
                    {
                        zero++;
                    }

                    if (m_Board.GetCellValue(i, j) == eCellState.White)
                    {
                        one++;
                    }
                }
            }

            GameReport gameReport = new GameReport
            {
                GameStatus = eGameStatuses.GameOver,
                MoveStatus = i_MoveStatus,
            };

            if (one > zero)
            {
                gameReport.WinnerPoints = one;
                gameReport.LoserPoints = zero;
                gameReport.Winner = m_Players[1];
                gameReport.Loser = m_Players[0];
            }
            else
            {
                gameReport.WinnerPoints = zero;
                gameReport.LoserPoints = one;
                gameReport.Winner = m_Players[0];
                gameReport.Loser = m_Players[1];
            }

            return gameReport;
        }

        private GameReport InitializeGameReport()
        {
            return new GameReport
            {
                GameStatus = eGameStatuses.InProgress,
                MoveStatus = eMoveStatuses.HasMoveToMake,
                LastMovePlayerName = m_CurrentPlayer.Name
            };
        }

        private eMoveStatuses SetPlayerMoves(List<Coordinate> i_Moves)
        {
            if (i_Moves.Count == 0)
            {
                return eMoveStatuses.MoveFailure;
            }

            foreach (var move in i_Moves)
            {
                m_Board.SetCellValue(m_PlayerValue, move.Row, move.Column);
            }

            return eMoveStatuses.MoveSuccess;
        }

        private Coordinate GetComputerRandomMove()
        {
            List<Coordinate> availableMoves = m_CurrentPlayerMoves.GetAvailableComputerMoves();
            var random = new Random();
            int index = random.Next(availableMoves.Count);
            return availableMoves[index];
        }

        private int ConvertInputToColumn(char i_ColumnChar)
        {
            int column = -1;
            char upperCaseColumn = char.ToUpper(i_ColumnChar);

            if (m_CharacterDict.ContainsKey(upperCaseColumn))
            {
                column = m_CharacterDict[upperCaseColumn];
            }

            return column;
        }

        private int ConvertInputToRow(char i_RowChar)
        {
            int row = -1;

            if (i_RowChar >= '1' && i_RowChar <= '8')
            {
                if (m_Board.m_Size == 6)
                {
                    if (i_RowChar >= '1' && i_RowChar <= '6')
                    {
                        row = (int)char.GetNumericValue(i_RowChar) - 1;
                    }
                }
                else
                {
                    row = (int)char.GetNumericValue(i_RowChar) - 1;
                }
            }

            return row;
        }

        private void SwitchPlayer()
        {
            m_PlayerIndex = m_PlayerIndex == 0 ? 1 : 0;
        }

        private void ResetSkippedTurns()
        {
            m_SkippedTurns = 0;
        }

        private void InitializeInputDictionary(int i_MatrixSize)
        {
            int maxUnicode = i_MatrixSize == 8 ? 72 : 70;
            int unicode = 65;

            for (int i = unicode; i <= maxUnicode; i++)
            {
                char character = (char)i;
                m_CharacterDict.Add(character, i - unicode);
            }
        }

        private void Sleep(int i_Seconds)
        {
            Thread.Sleep(i_Seconds * 1000);
        }
    }
}
