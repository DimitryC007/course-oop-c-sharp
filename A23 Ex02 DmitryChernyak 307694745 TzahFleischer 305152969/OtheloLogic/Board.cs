namespace OtheloLogic
{
    public class Board
    {
        private Cell[,] _matrix;
        public Cell[,] Matrix => _matrix;
        public bool IsBoardFull { get; }

        public Board(int matrixSize)
        {
            _matrix = new Cell[matrixSize, matrixSize];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            int matrixSize = _matrix.GetLength(0);
            int middleLocation = matrixSize / 2 - 1;

            for(int rows = 0; rows < matrixSize; rows++)
            {
                for(int cols = 0; cols< matrixSize; cols++)
                {
                    _matrix[rows, cols] = new Cell();
                }
            }
            _matrix[middleLocation, middleLocation].Value = 0;
            _matrix[middleLocation + 1 , middleLocation].Value = 1;
            _matrix[middleLocation, middleLocation + 1].Value = 1;
            _matrix[middleLocation + 1, middleLocation + 1].Value = 0;

        }

        public void SetMove(int value, int row, int column)
        {
            _matrix[row, column].Value = value;
        }

        public bool IsCellEmpty(int row, int column)
        {
            return !_matrix[row, column].IsTaken;
        }
    }
}
