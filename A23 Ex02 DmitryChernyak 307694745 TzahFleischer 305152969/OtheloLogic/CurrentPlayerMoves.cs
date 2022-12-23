using System.Collections.Generic;

namespace OtheloLogic
{
    class CurrentPlayerMoves
    {
        private Dictionary<string, List<Coordinate>> _availablePlayerMoves;
        private Board _board;
        public bool HasAnyMove => _availablePlayerMoves.Count > 0;

        public CurrentPlayerMoves(Board board)
        {
            _board = board;
        }

        private string CreateKey(int row, int column)
        {
            return $"{row}_{column}";
        }

        private bool isInsideBoard(int row, int column)
        {
            return row >= 0 && row < _board.Size && column >= 0 && column < _board.Size;
        }

        private List<Coordinate> MoveInDirection(Coordinate location, int opponentValue, int rowDirection, int columnDirection)
        {
            List<Coordinate> movesInDirection = new List<Coordinate>();
            int row = location.Row + rowDirection;
            int column = location.Column + columnDirection;

            while (isInsideBoard(row, column) && _board.GetCellValue(row, column) != null)
            {
                if (_board.GetCellValue(row, column) == opponentValue)
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

            for (int rowDirection = -1; rowDirection <= 1; rowDirection++)
            {
                for (int colDirection = -1; colDirection <= 1; colDirection++)
                {
                    if (rowDirection == 0 && colDirection == 0)
                    {
                        continue;
                    }
                    AllCoordinates.AddRange(MoveInDirection(location, opponentValue, rowDirection, colDirection));
                }
            }
            return AllCoordinates;
        }

        public Dictionary<string, List<Coordinate>> AllCurrentPlayerMoves(int opponentValue)
        {
            _availablePlayerMoves = new Dictionary<string, List<Coordinate>>();

            for (int row = 0; row < _board.Size; row++)
            {
                for (int column = 0; column < _board.Size; column++)
                {
                    Coordinate location = new Coordinate(row, column);
                    if (_board.IsCellEmpty(location.Row, location.Column))
                    {
                        List<Coordinate> locationMoves = AllCoordinatesForFlipping(location, opponentValue);
                        if (locationMoves.Count > 0)
                        {
                            string key = CreateKey(location.Row, location.Column);
                            locationMoves.Add(location);
                            _availablePlayerMoves[key] = locationMoves;
                        }
                    }
                }
            }

            return _availablePlayerMoves;
        }

        public List<Coordinate> GetFlippableList(Coordinate location)
        {
            string key = CreateKey(location.Row, location.Column);
            return _availablePlayerMoves.ContainsKey(key) ? _availablePlayerMoves[key] : new List<Coordinate>();
        }

        public List<Coordinate> GetAvailableComputerMoves()
        {
            List<Coordinate> moves = new List<Coordinate>();
            
            foreach (var key in _availablePlayerMoves.Keys)
            {
                int row, column;
                string[] rowAndCol = key.Split('_');
                row = int.Parse(rowAndCol[0]);
                column = int.Parse(rowAndCol[1]);
                moves.Add(new Coordinate(row,column));
            }
            
            return moves;
        }
    }
}
