﻿using System;
using System.Collections.Generic;
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
        private int m_PlayerIndex = 0;
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
        }

        private void OnBoardChanged(int row, int col, eCellState newState)
        {
            BoardChanged?.Invoke(row, col, newState);
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

        public GameReport MakeMove(Coordinate i_Position)
        {
            List<Coordinate> effectedFlipCoins;
            GameReport gameReport = InitializeGameReport();

            if (!m_CurrentPlayer.IsComputer)
            {
                effectedFlipCoins = m_CurrentPlayerMoves.GetFlippableList(i_Position);
            }
            else
            {
                Coordinate computerMove = GetComputerRandomMove();
                effectedFlipCoins = m_CurrentPlayerMoves.GetFlippableList(computerMove);
                Sleep(1);
            }

            gameReport.MoveStatus = SetPlayerMoves(effectedFlipCoins);
            m_Board.EmptyCell();

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
                GameReport.PlayerOneWinGames++;
            }
            else
            {
                gameReport.WinnerPoints = zero;
                gameReport.LoserPoints = one;
                gameReport.Winner = m_Players[0];
                gameReport.Loser = m_Players[1];
                GameReport.PlayerTwoWinGames++;
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

        private void SwitchPlayer()
        {
            m_PlayerIndex = m_PlayerIndex == 0 ? 1 : 0;
        }

        private void ResetSkippedTurns()
        {
            m_SkippedTurns = 0;
        }

        private void Sleep(int i_Seconds)
        {
            Thread.Sleep(i_Seconds * 1000);
        }
    }
}
