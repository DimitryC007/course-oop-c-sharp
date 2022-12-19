using System;
using System.Collections.Generic;

namespace OtheloLogic
{
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

    public class CurrentPlayerPoolMoves
    {
        private Dictionary<string, List<Coordinate>> _availablePlayerMoves;
        private readonly Dictionary<string, List<MoveDirection>> _boardMoveDirections;
        private Board _board;
        public bool HasAnyMove => _availablePlayerMoves.Count > 0;

        public CurrentPlayerPoolMoves(Board board)
        {
            _board = board;
            _boardMoveDirections = InitializeBoardMoveDirections();
        }

        private string CreateKey(int row, int column)
        {
            return $"{row}_{column}";
        }

        private Dictionary<string, List<MoveDirection>> InitializeBoardMoveDirections()
        {
            Dictionary<string, List<MoveDirection>> boardMoveDirections = new Dictionary<string, List<MoveDirection>>();

            for (int i = 0; i < _board.Size; i++)
            {
                for (int j = 0; j < _board.Size; j++)
                {
                    boardMoveDirections.Add(CreateKey(i, j), GetAvailableMovesDirections(i, j));
                }
            }

            return boardMoveDirections;
        }

        private List<Coordinate> GetMoveEffectedFlipCoins(int row, int column, int oponentValue)
        {
            var availableMovesDirections = _boardMoveDirections[CreateKey(row, column)];
            return GetFlipCoordinates(availableMovesDirections, oponentValue, row, column);
        }

        private List<MoveDirection> GetAvailableMovesDirections(int row, int column)
        {
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
                else if (column == _board.Size - 1 || column == _board.Size - 2)
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
            else if (row == _board.Size - 1 || row == _board.Size - 2)
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
                else if (column == _board.Size - 1 || column == _board.Size - 2)
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
            else if (column == 0 || column == 1)
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
            else if (column == _board.Size - 1 || column == _board.Size - 2)
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

        private List<Coordinate> GetFlipCoordinates(List<MoveDirection> moveDirections, int oponentValue, int row, int column)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            if (_board.IsCellEmpty(row, column))
            {
                foreach (var direction in moveDirections)
                {
                    switch (direction)
                    {
                        case MoveDirection.Up:
                            {
                                if (_board.GetCellValue(row - 1, column) == oponentValue)
                                {
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;
                                    for (int i = row - 1; i >= 0; i--)
                                    {
                                        if (_board.IsCellEmpty(i, column))
                                            break;

                                        if (_board.GetCellValue(i, column) == oponentValue)
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
                                if (_board.GetCellValue(row + 1, column) == oponentValue)
                                {
                                    int matrixRows = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;

                                    for (int i = row + 1; i < matrixRows; i++)
                                    {
                                        if (_board.IsCellEmpty(i, column))
                                            break;

                                        if (_board.GetCellValue(i, column) == oponentValue)
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
                                if (_board.GetCellValue(row, column - 1) == oponentValue)
                                {
                                    int matrixColumns = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;

                                    for (int i = column - 1; i >= 0; i--)
                                    {
                                        if (_board.IsCellEmpty(row, i))
                                            break;

                                        if (_board.GetCellValue(row, i) == oponentValue)
                                        {
                                            innerCoordinate.Add(new Coordinate(row, i));
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
                        case MoveDirection.Right:
                            {
                                if (_board.GetCellValue(row, column + 1) == oponentValue)
                                {
                                    int matrixColumns = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;

                                    for (int i = column + 1; i < matrixColumns; i++)
                                    {
                                        if (_board.IsCellEmpty(row, i))
                                            break;

                                        if (_board.GetCellValue(row, i) == oponentValue)
                                        {
                                            innerCoordinate.Add(new Coordinate(row, i));
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
                        case MoveDirection.UpRightDiagonal:
                            {
                                if (_board.GetCellValue(row - 1, column + 1) == oponentValue)
                                {
                                    int matrixColumns = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;
                                    for (int i = row - 1, j = column + 1; i >= 0 && j < matrixColumns; i--, j++)
                                    {
                                        if (_board.IsCellEmpty(i, j))
                                            break;

                                        if (_board.GetCellValue(i, j) == oponentValue)
                                        {
                                            innerCoordinate.Add(new Coordinate(i, j));
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
                        case MoveDirection.DownRightDiagonal:
                            {
                                if (_board.GetCellValue(row + 1, column + 1) == oponentValue)
                                {
                                    int matrixColumns = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;
                                    for (int i = row + 1, j = column + 1; i < matrixColumns && j < matrixColumns; i++, j++)
                                    {
                                        if (_board.IsCellEmpty(i, j))
                                            break;

                                        if (_board.GetCellValue(i, j) == oponentValue)
                                        {
                                            innerCoordinate.Add(new Coordinate(i, j));
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
                        case MoveDirection.UpLeftDiagonal:
                            {
                                if (_board.GetCellValue(row - 1, column - 1) == oponentValue)
                                {
                                    int matrixColumns = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;
                                    for (int i = row - 1, j = column - 1; i >= 0 && j >= 0; i--, j--)
                                    {

                                        if (_board.IsCellEmpty(i, j))
                                            break;

                                        if (_board.GetCellValue(i, j) == oponentValue)
                                        {
                                            innerCoordinate.Add(new Coordinate(i, j));
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
                        case MoveDirection.DownLeftDiagonal:
                            {
                                if (_board.GetCellValue(row + 1, column - 1) == oponentValue)
                                {
                                    int matrixColumns = _board.Size;
                                    List<Coordinate> innerCoordinate = new List<Coordinate>();
                                    bool isFlippable = false;
                                    for (int i = row + 1, j = column - 1; i < matrixColumns && j >= 0; i++, j--)
                                    {
                                        if (_board.IsCellEmpty(i, j))
                                            break;

                                        if (_board.GetCellValue(i, j) == oponentValue)
                                        {
                                            innerCoordinate.Add(new Coordinate(i, j));
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
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
            if (coordinates.Count > 0)
            {
                coordinates.Add(new Coordinate(row, column));
            }

            return coordinates;
        }

        public void InitializeAvailablePlayerMoves(int oponentValue)
        {
            _availablePlayerMoves = new Dictionary<string, List<Coordinate>>();
            for (int i = 0; i < _board.Size; i++)
            {
                for (int j = 0; j < _board.Size; j++)
                {
                    List<Coordinate> effectedFlipCoins = GetMoveEffectedFlipCoins(i, j, oponentValue);
                    if (effectedFlipCoins.Count > 0)
                    {
                        _availablePlayerMoves.Add(CreateKey(i, j), effectedFlipCoins);
                    }
                }
            }
        }

        public List<Coordinate> GetEffectedFlipCoins(int row, int column)
        {
            string key = CreateKey(row, column);
            if (_availablePlayerMoves.TryGetValue(key, out List<Coordinate> effectedFlipCoins))
            {
                return effectedFlipCoins;
            }
            return new List<Coordinate>();
        }

        public List<Coordinate> GetAllAvailableMoves()
        {
            List<Coordinate> allAvailableMoves = new List<Coordinate>();

            foreach (KeyValuePair<string, List<Coordinate>> move in _availablePlayerMoves)
            {
                int row, column;
                string[] rowAndCol = move.Key.Split('_');
                row = int.Parse(rowAndCol[0].ToString());
                column = int.Parse(rowAndCol[1].ToString());
                Coordinate coordinate = new Coordinate(row, column);
                allAvailableMoves.Add(coordinate);
            }

            return allAvailableMoves;
        }
    }
}
