using System;

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

    public class GameLogic
    {
        public Board _board;
        private Player[] _players;
        public Player CurrentPlayer => _players[_playerIndex];
        private int _playerIndex = 0;
        public GameLogic(GameSettings gameSettings)
        {
            _board = new Board(gameSettings.MatrixSize);
            _players = gameSettings.Players;
        }


        public GameReport MakeMove(string position = "")
        {   
            GameReport gameReport;
            ///TODO: check if board is Full
            ///TODO: check currentUser has any move to make - if not return MoveStatus.MoveSkipped
            if (!CurrentPlayer.IsComputer)
            {
                ///TODO: convert position to row,col --> User
                int row = ConvertInputToColumn(position[0]);
                int column = ConvertInputToRow(position[1]);
                if (row == -1 || column == -1)
                {   
                    ///TODO: return MoveStatus.Failure
                    gameReport = new GameReport { };
                    return gameReport;
                }

                ///TODO: call UserMove with row and column if returned status is MoveStatus.MoveSuccess
                ///TODO:
            }
            else
            {
                ///TODO: get computer move random row,col
                ///TODO: call to ComputerMove()
            }

            ///TODO: check if game is over - if really game is over return fill the GameReport and return it

            ///TODO: call to SwitchPlayer at the end of the move
            SwitchPlayer();

            ///TODO: always return GameReport
            throw new NotImplementedException();
        }

        private MoveStatus UserMove(int row, int col)
        {   
            ///TODO: check if the cell it taken - if so return MoveStatus.CellIsTaken
            ///TODO: check if the move is legit - if not legit return MoveStatus.Failure
            throw new NotImplementedException();
        }

        private int[] GetComputerRandomMove()
        {   ///TODO: return random move arr[0] = row, arr[1] = column
            throw new NotImplementedException();
        }

        private MoveStatus ComputerMove(int row, int col)
        {   
            throw new NotImplementedException();
        }

        private GameStatus GetGameStatus()
        {   
            ///TODO: Check if avialbe moves remains on board for the oppsite player 
            throw new NotImplementedException();
        }

        private int ConvertInputToColumn(char columnChar)
        {
            ///TODO: convert the input to position using dictionary
            ///TODO: if not exists in dictionary it means that the input is not valid - if it is not valid return -1

            throw new NotImplementedException();
        }

        private int ConvertInputToRow(char rowChar)
        {
            ///TODO: check if row char is valid and between matrix[row][0]:start - matrix[row][0]:end - if it is not valid return -1
            throw new NotImplementedException();
        }

        private void SwitchPlayer()
        {
            _playerIndex = _playerIndex == 0 ? 1 : 0;
        }
    }
}
