namespace OtheloLogic
{
    public class Board
    {
        private Cell[,] _matrix;
        public int Size => _matrix.GetLength(0);
        public bool IsFull => GetIsFull();

        public Board(int matrixSize)
        {
            _matrix = new Cell[matrixSize, matrixSize];
            InitializeBoard();
        }

        private void InitializeBoard()
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
            _matrix[middleLocation, middleLocation].Value = 1;
            _matrix[middleLocation + 1 , middleLocation].Value = 0;
            _matrix[middleLocation, middleLocation + 1].Value = 0;
            _matrix[middleLocation + 1, middleLocation + 1].Value = 1;

        }

        private bool GetIsFull()
        { 
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int? cellValue = GetCellValue(i, j);
                    if (cellValue == null)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        internal void SetCellValue(int value, int row, int column)
        {
            _matrix[row, column].Value = value;
        }

        public int? GetCellValue(int row, int column)
        {
            return _matrix[row, column].Value;
        }

        public bool IsCellEmpty(int row, int column)
        {
            return !_matrix[row, column].IsTaken;
        }

        
    }
}
