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
        }

        public void InitializeBoard()
        {            
            ///TODO: init mat with with new cell each col,row
            ///TODO: calc center and init X,O in the center of the matrix
        }

        public void SetMove(int value, int row, int column)
        {   
            ///TODO: set move on the board with value
        }

        public bool IsCellEmpty(int row, int column)
        {
            return !_matrix[row, column].IsTaken;
        }
    }
}
