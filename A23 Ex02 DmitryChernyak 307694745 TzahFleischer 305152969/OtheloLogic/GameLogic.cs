using System;
using System.Collections.Generic;

namespace OtheloLogic
{
    public enum GameStatus
    {
        GameOver = 0, // no moves left
        InProgress = 1, // game in progress
        BoardIsFull = 2 // board is full
    }

    public enum MoveStatus
    {
        CellIsTaken = 0, // we need to know cell is taken to ask input from the user again 
        MoveSuccess = 1, // continue playing
        MoveFailure = 2, // input not valid from --> User
        MoveSkipped = 3 // doesn't have a move to make
    }

    //public enum MoveDirection
    //{
    //    Up = 0,
    //    Down = 1,
    //    Left = 2,
    //    Right = 3,
    //    UpRightDiagonal = 4,
    //    DownRightDiagonal = 5,
    //    UpLeftDiagonal = 6,
    //    DownLeftDiagonal = 7,
    //}

    public class GameLogic
    {
        public Board Board { get; }
        private Player[] _players;
        private int _skipCount = 0;
        public Player CurrentPlayer => _players[_playerIndex];
        
        private int _playerIndex = 0;
        private int _playerValue => _playerIndex;
        private int _oponentValue => _playerIndex == 0 ? 1 : 0;
        private Dictionary<char, int> _characterDict = new Dictionary<char, int>();
        
        private CurrentPlayerMoves _currentPlayerMoves;
        public GameLogic(GameSettings gameSettings)
        {
            Board = new Board(gameSettings.MatrixSize);

            
            _currentPlayerMoves = new CurrentPlayerMoves(Board);
            _players = gameSettings.Players;
            InitializeDict(gameSettings.MatrixSize);
            

        }

        public string GetPlayerName(int playerNum)
        {
            return _players[playerNum].Name;
        }

        public GameReport FindAllAvailableMoves()
        {
            GameReport gameReport = new GameReport();
            _currentPlayerMoves.AllCurrentPlayerMoves(_oponentValue);

            if (Board.IsFull)
            {
                gameReport.GameStatus = GameStatus.GameOver;
            }

            else if (!_currentPlayerMoves.HasAnyMove)
            {
                _skipCount++;
                gameReport.MoveStatus = MoveStatus.MoveSkipped;
                gameReport.GameStatus = GameStatus.InProgress;
         
            }

            else
            {
                _skipCount = 0;
                gameReport.GameStatus = GameStatus.InProgress;
                gameReport.MoveStatus = MoveStatus.MoveSuccess;
            }

            if (_skipCount == 2)
            {
                gameReport.GameStatus = GameStatus.GameOver;
            }

            return gameReport;
        }

        public GameReport MakeMove(string position = "")
        {
            GameReport gameReport = new GameReport();
            gameReport.GameStatus = GameStatus.InProgress;


            if (!CurrentPlayer.IsComputer)
            {
                int column = ConvertInputToColumn(position[0]);
                int row = ConvertInputToRow(position[1]);
                if (row == -1 || column == -1)
                {
                    gameReport.MoveStatus = MoveStatus.MoveFailure;
                    return gameReport;
                }

                Coordinate playerChosenMove = new Coordinate(row, column);

                if (!_currentPlayerMoves.IsMoveAvailable(playerChosenMove))
                {
                    gameReport.MoveStatus = MoveStatus.MoveFailure;
                    return gameReport;
                }

                FlipCoins(playerChosenMove, _currentPlayerMoves.ReturnFlippableList(playerChosenMove));
                gameReport.MoveStatus = MoveStatus.MoveSuccess;


            }

            else
            {

                SetComputerRandomMove();
                gameReport.MoveStatus = MoveStatus.MoveSuccess;

            }

            CheckCount();

            return gameReport;
        }


        public bool IsThereMoves()
        {
            return _currentPlayerMoves.CountOfMoves() > 0;
        }

        public void CheckCount()
        {
            int xCount = 0;
            int oCount = 0;
            for (int rows = 0; rows < Board.Size; rows++)
            {
                for (int cols = 0; cols < Board.Size; cols++)
                {
                    if (!Board.IsCellEmpty(rows, cols))
                    {
                        if (Board.GetCellValue(rows, cols) == 0)
                            oCount++;
                        else
                            xCount++;
                    }
                }
            }

            _players[0].Count = xCount;
            _players[1].Count = oCount;
            //Console.WriteLine("X Count: {0}, O Count: {1}", oCount, xCount);
            //return oCount >= xCount ? 0 : 1;
        }

        public int GetPlayerCount(int playerIndex)
        {
            return _players[playerIndex].Count;
        }

        public int GetWinner()
        {
            return _players[0].Count >= _players[1].Count ? 0 : 1;
        }

     
        private void SetComputerRandomMove()
        {

            var random = new Random();
            int index = random.Next(_currentPlayerMoves.CountOfMoves());
          
            Coordinate move = _currentPlayerMoves.LocationAtIndex(index);
            FlipCoins(move, _currentPlayerMoves.ReturnFlippableList(move));

        }

        //private GameStatus GetGameStatus()
        //{
        //    ///TODO: Check if avialbe moves remains on board for the oppsite player 
        //    throw new NotImplementedException();
        //}

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

        public void SwitchPlayer()
        {
            _playerIndex = _playerIndex == 0 ? 1 : 0;
        }

        public void InitializeDict(int matrixSize)
        {
            int maxUnicode = matrixSize == 8 ? 72 : 70;
            int unicode = 65;

            for (int i = unicode; i <= maxUnicode; i++)
            {
                char character = (char)i;
                _characterDict.Add(character, i - unicode);
            }
        }

        public void FlipCoins(Coordinate location, List<Coordinate> flippableCoins)
        {
            Board.SetCellValue(_playerValue, location.Row, location.Column);
            foreach(Coordinate coin in flippableCoins)
            {
                Board.SetCellValue(_playerValue, coin.Row, coin.Column);
            }
        }
    }

    


}
