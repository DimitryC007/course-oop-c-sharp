using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtheloLogic
{
    class CurrentPlayerMoves
    {
        private Dictionary<Coordinate, List<Coordinate>> _availablePlayerMoves;
        private Board _board;
        public bool HasAnyMove => _availablePlayerMoves.Count > 0;
        //private int _opponentValue = 0;

        public CurrentPlayerMoves(Board board)
        {
            _board = board;
            //_opponentValue = opponent;
        }

        private bool isInsideBoard(int row, int column)
        {
            return row >= 0 && row < _board.Size && column >= 0 && column < _board.Size;
        }

        private List<Coordinate> MoveInDirection(Coordinate location,int opponentValue, int rowDirection, int columnDirection)
        {
            List<Coordinate> movesInDirection = new List<Coordinate>();
            int row = location.Row + rowDirection;
            int column = location.Column + columnDirection;

            while(isInsideBoard(row,column) && _board.GetCellValue(row,column) != null)
            {
                if(_board.GetCellValue(row, column) == opponentValue)
                {
                    movesInDirection.Add(new Coordinate(row, column));
                    row += rowDirection;
                    column += columnDirection;
                }
                else
                {
                    return movesInDirection;
                }
            }
            return new List<Coordinate>();
        }

        private List<Coordinate> AllCoordinatesForFlipping(Coordinate location, int opponentValue)
        {
            List<Coordinate> AllCoordinates = new List<Coordinate>();

            for(int rowDirection = -1; rowDirection <= 1; rowDirection++ )
            {
                for(int colDirection = -1; colDirection <= 1; colDirection++)
                {
                    if(rowDirection == 0 && colDirection == 0)
                    {
                        continue;
                    }
                    AllCoordinates.AddRange(MoveInDirection(location, opponentValue, rowDirection, colDirection));
                }
            }
            return AllCoordinates;
        }

        private bool IsCellEmpty(Coordinate location)
        {
            bool cellEmpty = true;

            if(_board.GetCellValue(location.Row,location.Column) != null)
            {
                cellEmpty = false;
            }

            return cellEmpty;

        }

        public Dictionary<Coordinate,List<Coordinate>> AllCurrentPlayerMoves(int opponentValue)
        {
            _availablePlayerMoves = new Dictionary<Coordinate, List<Coordinate>>();

            for(int row = 0; row < _board.Size; row++)
            {
                for(int column = 0; column < _board.Size; column++)
                {
                    Coordinate location = new Coordinate(row, column);
                    if(IsCellEmpty(location))
                    {
                        List<Coordinate> locationMoves = AllCoordinatesForFlipping(location, opponentValue);
                        if (locationMoves.Count > 0)
                        {
                            _availablePlayerMoves[location] = locationMoves;
                        }
                    }
                }
            }

            return _availablePlayerMoves;
        }

        public bool IsMoveAvailable(Coordinate location)
        {
            if(_availablePlayerMoves.ContainsKey(location))
            {
                return true;
            }

            return false;
        }

        public List<Coordinate> ReturnFlippableList(Coordinate location)
        {
            return _availablePlayerMoves[location];
        }

        public int CountOfMoves()
        {
            return _availablePlayerMoves.Count;
        }
        
        public Coordinate LocationAtIndex(int index)
        {
            return _availablePlayerMoves.Keys.ElementAt(index);
        }

        //public bool 



    }
}
