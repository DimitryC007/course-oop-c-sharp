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
        //private int _playerIndex = 1;
        private int _playerIndex = 0;
        private int _playerValue => _playerIndex;
        private int _oponentValue => _playerIndex == 0 ? 1 : 0;
        private Dictionary<char, int> _characterDict = new Dictionary<char, int>();
        //private CurrentPlayerPoolMoves _currentPlayerPoolMoves;
        private CurrentPlayerMoves _currentPlayerMoves;
        public GameLogic(GameSettings gameSettings)
        {
            Board = new Board(gameSettings.MatrixSize);

            //_currentPlayerPoolMoves = new CurrentPlayerPoolMoves(Board);
            _currentPlayerMoves = new CurrentPlayerMoves(Board);
            _players = gameSettings.Players;
            InitializeDict(gameSettings.MatrixSize);
            _currentPlayerMoves.AllCurrentPlayerMoves(_oponentValue);

        }

        public string GetPlayerName(int playerNum)
        {
            return _players[playerNum].Name;
        }

        public GameReport FindAllAvailableMoves()
        {
            GameReport gameReport = new GameReport();
            _currentPlayerMoves.AllCurrentPlayerMoves(_oponentValue);

            if(Board.IsFull)
            {
                gameReport.GameStatus = GameStatus.GameOver;                
            }

            else if(!_currentPlayerMoves.HasAnyMove)
            {
                _skipCount++;
                gameReport.MoveStatus = MoveStatus.MoveSkipped;
            }

            else
            {
                _skipCount = 0;
                gameReport.GameStatus = GameStatus.InProgress;
                gameReport.MoveStatus = MoveStatus.MoveSuccess;
            }

            if(_skipCount == 2)
            {
                gameReport.GameStatus = GameStatus.GameOver;
            }

            return gameReport;
        }
        public GameReport MakeMove(string position = "")
        {
            GameReport gameReport = new GameReport();
            gameReport.GameStatus = GameStatus.InProgress;

            //List<Coordinate> effectedFlipCoins;

            //if (Board.IsFull)
            //{
            //    gameReport.GameStatus = GameStatus.GameOver;
            //    return gameReport;
            //}

            //_currentPlayerMoves.AllCurrentPlayerMoves(_oponentValue);
            //_currentPlayerPoolMoves.InitializeAvailablePlayerMoves(_oponentValue);

            //if (!_currentPlayerPoolMoves.HasAnyMove)
            //if (!_currentPlayerMoves.HasAnyMove)
            //{
            //    SwitchPlayer();
            //    gameReport.MoveStatus = MoveStatus.MoveSkipped;
            //    return gameReport;
            //}

            ///TODO: check if both players in the last moves didn't have moves - return game over  
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

                if(!_currentPlayerMoves.IsMoveAvailable(playerChosenMove))
                {
                    gameReport.MoveStatus = MoveStatus.MoveFailure;
                    return gameReport;
                }
               
                FlipCoins(playerChosenMove, _currentPlayerMoves.ReturnFlippableList(playerChosenMove));
                gameReport.MoveStatus = MoveStatus.MoveSuccess;

                //if (!Board.IsCellEmpty(row, column))
                //{
                //    gameReport.MoveStatus = MoveStatus.CellIsTaken;
                //    return gameReport;
                //}

                //effectedFlipCoins = _currentPlayerPoolMoves.GetEffectedFlipCoins(row, column);
            }

            else
            {
                //if(_currentPlayerMoves.HasAnyMove)
                SetComputerRandomMove();
                gameReport.MoveStatus = MoveStatus.MoveSuccess;
                //else 
                //{
                //    gameReport.MoveStatus = MoveStatus.MoveSkipped;
                //    return gameReport;
                //}

                //effectedFlipCoins = _currentPlayerPoolMoves.GetEffectedFlipCoins(computerMove.Row, computerMove.Column);
            }

            
            //gameReport.MoveStatus = SetPlayerMoves(effectedFlipCoins);

            ///TODO: check if game is over - if really game is over return fill the GameReport and return it
            return gameReport;
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

        public bool IsThereMoves()
        {
            return _currentPlayerMoves.CountOfMoves() > 0;
        }

        //private Coordinate GetComputerRandomMove()
        //{
            
        //    List<Coordinate> availableMoves = _currentPlayerPoolMoves.GetAllAvailableMoves();

        //    var random = new Random();
            
        //    int index = random.Next(availableMoves.Count);
           
        //    return availableMoves[index];
        //}

        private void SetComputerRandomMove()
        {

            //List<Coordinate> availableMoves = _currentPlayerPoolMoves.GetAllAvailableMoves();

            var random = new Random();
            int index = random.Next(_currentPlayerMoves.CountOfMoves());
            //int index = random.Next(availableMoves.Count);
            Coordinate move = _currentPlayerMoves.LocationAtIndex(index);
            FlipCoins(move, _currentPlayerMoves.ReturnFlippableList(move));

        }

        private GameStatus GetGameStatus()
        {
            ///TODO: Check if avialbe moves remains on board for the oppsite player 
            throw new NotImplementedException();
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
