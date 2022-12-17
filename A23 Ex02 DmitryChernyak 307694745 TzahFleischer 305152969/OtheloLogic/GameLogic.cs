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

    public enum MoveDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        UpRightDiagonal = 4,
        DownRightDiagonal = 5,
        UpLeftDiagonal = 6,
        DownLeftDiagonal = 7,
    }

    public class GameLogic
    {
        public Board _board;
        private Player[] _players;
        public Player CurrentPlayer => _players[_playerIndex];
        private int _playerIndex = 0;
        private Dictionary<char, int> _characterDict = new Dictionary<char,int>();
        public GameLogic(GameSettings gameSettings)
        {
            _board = new Board(gameSettings.MatrixSize);
            _players = gameSettings.Players;
            InitializeDict(gameSettings.MatrixSize);
        }


        public GameReport MakeMove(string position = "")
        {
            GameReport gameReport;
            ///TODO: check if board is Full
            ///TODO: check currentUser has any move to make - if not return MoveStatus.MoveSkipped 
            ///TODO: check if both players in the last moves didn't have moves - return game over  
            if (!CurrentPlayer.IsComputer)
            {
                int column = ConvertInputToColumn(position[0]);
                int row = ConvertInputToRow(position[1]);
                if (row == -1 || column == -1)
                {
                    ///TODO: return MoveStatus.Failure
                    gameReport = new GameReport { };
                    return gameReport;
                }

                //int row = 1, column = 3;
                UserMove(row, column);
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

        private MoveStatus UserMove(int row, int column)
        {
            if (_board.Matrix[row, column].IsTaken)
            {
                return MoveStatus.CellIsTaken;
            }

            var availableMovesDirections = GetAvailableMovesDirectionsCheck(row, column);
            List<Coordinate> flipCoordinates = GetFlipCoordinates(availableMovesDirections, row, column);
            ///TODO: check if the move is legit - if not legit return MoveStatus.Failure
            ///TODO: flip all available oponent coins
            throw new NotImplementedException();
        }

        private List<Coordinate> GetFlipCoordinates(List<MoveDirection> moveDirections, int row, int column)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            int currentPlayerValue = _playerIndex;
            int oponentValue = GetOponentValue();

            foreach (var direction in moveDirections)
            {
                switch (direction)
                {
                    case MoveDirection.Up:
                        {
                            if (_board.Matrix[row - 1, column].IsTaken && _board.Matrix[row - 1, column].Value == oponentValue)
                            {
                                List<Coordinate> innerCoordinate = new List<Coordinate>();
                                bool isFlippable = false;
                                for (int i = row - 1; i >= 0; i--)
                                {
                                    if (!_board.Matrix[i, column].IsTaken)
                                        break;

                                    if (_board.Matrix[i, column].Value == oponentValue)
                                    {
                                        innerCoordinate.Add(new Coordinate(i, column));
                                    }
                                    else
                                    {
                                        isFlippable = true;
                                        break;
                                    }

                                }

                                if (isFlippable)
                                {
                                    coordinates.AddRange(innerCoordinate);
                                }
                            }
                            break;
                        }
                    case MoveDirection.Down:
                        {
                            if (_board.Matrix[row + 1, column].IsTaken && _board.Matrix[row + 1, column].Value == oponentValue)
                            {
                                int matrixRows = _board.Matrix.GetLength(0);
                                List<Coordinate> innerCoordinate = new List<Coordinate>();
                                bool isFlippable = false;

                                for (int i = row + 1; i < matrixRows; i--)
                                {
                                    if (!_board.Matrix[i, column].IsTaken)
                                        break;

                                    if (_board.Matrix[i, column].Value == oponentValue)
                                    {
                                        innerCoordinate.Add(new Coordinate(i, column));
                                    }
                                    else
                                    {
                                        isFlippable = true;
                                        break;
                                    }

                                }

                                if (isFlippable)
                                {
                                    coordinates.AddRange(innerCoordinate);
                                }
                            }
                            break;
                        }
                    case MoveDirection.Left:
                        {
                            break;
                        }
                    case MoveDirection.Right:
                        {
                            break;
                        }
                    case MoveDirection.UpRightDiagonal:
                        {
                            break;
                        }
                    case MoveDirection.DownRightDiagonal:
                        {
                            break;
                        }
                    case MoveDirection.UpLeftDiagonal:
                        {
                            break;
                        }
                    case MoveDirection.DownLeftDiagonal:
                        {
                            break;
                        }
                    default:
                        throw new NotSupportedException();
                }
            }

            if (coordinates.Count > 0)
            {
                coordinates.Add(new Coordinate(row, column));
            }

            return coordinates;
        }

        private List<MoveDirection> GetAvailableMovesDirectionsCheck(int row, int column)
        {
            int matrixRows = _board.Matrix.GetLength(0);
            int matrixCols = _board.Matrix.GetLength(1);
            List<MoveDirection> availableMovesToCheck = new List<MoveDirection>();

            if (row == 0 || row == 1)
            {
                availableMovesToCheck.Add(MoveDirection.Down);
                if (column == 0 || column == 1)
                {
                    availableMovesToCheck.AddRange(new List<MoveDirection>
                    {
                        MoveDirection.Right,
                        MoveDirection.DownRightDiagonal,
                    });
                }
                else if (column == matrixCols - 1 || column == matrixCols - 2)
                {
                    availableMovesToCheck.AddRange(new List<MoveDirection>
                    {
                        MoveDirection.Left,
                        MoveDirection.DownLeftDiagonal,
                    });
                }
                else
                {
                    availableMovesToCheck.AddRange(new List<MoveDirection>
                    {
                        MoveDirection.Left,
                        MoveDirection.Right,
                        MoveDirection.DownLeftDiagonal,
                        MoveDirection.DownRightDiagonal,
                    });
                }
            }
            else if (row == matrixRows - 1 || row == matrixRows - 2)
            {
                availableMovesToCheck.Add(MoveDirection.Up);
                if (column == 0 || column == 1)
                {
                    availableMovesToCheck.AddRange(new List<MoveDirection>
                    {
                        MoveDirection.Right,
                        MoveDirection.UpRightDiagonal,
                    });
                }
                else if (column == matrixCols - 1 || column == matrixCols - 2)
                {
                    availableMovesToCheck.AddRange(new List<MoveDirection>
                    {
                        MoveDirection.Left,
                        MoveDirection.UpLeftDiagonal,
                    });
                }
                else
                {
                    availableMovesToCheck.AddRange(new List<MoveDirection>
                    {
                        MoveDirection.Left,
                        MoveDirection.Right,
                        MoveDirection.UpLeftDiagonal,
                        MoveDirection.UpRightDiagonal,
                    });
                }
            }
            else if (row != 0 && row == 1 && row != matrixRows - 1 && row != matrixRows - 2 && (column == 0 || column == 1))
            {
                availableMovesToCheck.AddRange(new List<MoveDirection>
                {
                        MoveDirection.Up,
                        MoveDirection.Down,
                        MoveDirection.Right,
                        MoveDirection.UpRightDiagonal,
                        MoveDirection.DownRightDiagonal,
                });
            }
            else if (row != 0 && row == 1 && row != matrixRows - 1 && row != matrixRows - 2 && (column == matrixCols - 1 || column == matrixCols - 2))
            {
                availableMovesToCheck.AddRange(new List<MoveDirection>
                {
                        MoveDirection.Up,
                        MoveDirection.Down,
                        MoveDirection.Left,
                        MoveDirection.UpLeftDiagonal,
                        MoveDirection.DownLeftDiagonal,
                });
            }
            else
            {
                availableMovesToCheck.AddRange(new List<MoveDirection>
                {
                    MoveDirection.Up,
                    MoveDirection.Down,
                    MoveDirection.Left,
                    MoveDirection.Right,
                    MoveDirection.UpRightDiagonal,
                    MoveDirection.DownRightDiagonal,
                    MoveDirection.UpLeftDiagonal,
                    MoveDirection.DownLeftDiagonal,
                });
            }

            return availableMovesToCheck;
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
            int column = -1;
            char upperCaseColumn = char.ToUpper(columnChar);
            if(_characterDict.ContainsKey(upperCaseColumn))
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
                if(this._board.Matrix.GetLength(0) == 6)
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

        private int GetOponentValue()
        {
            return _playerIndex == 0 ? 1 : 0;
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
    }


}
