namespace OtheloLogic
{
    public class Board
    {
        private Cell[,] m_Matrix;
        public int m_Size => m_Matrix.GetLength(0);
        public bool m_IsFull => GetIsFull();

        public Board(int i_MatrixSize)
        {
            m_Matrix = new Cell[i_MatrixSize, i_MatrixSize];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            int matrixSize = m_Matrix.GetLength(0);
            int middleLocation = matrixSize / 2 - 1;

            for(int rows = 0; rows < matrixSize; rows++)
            {
                for(int cols = 0; cols< matrixSize; cols++)
                {
                    m_Matrix[rows, cols] = new Cell();
                }
            }

            m_Matrix[middleLocation, middleLocation].Value = 0;
            m_Matrix[middleLocation + 1 , middleLocation].Value = 1;
            m_Matrix[middleLocation, middleLocation + 1].Value = 1;
            m_Matrix[middleLocation + 1, middleLocation + 1].Value = 0;

        }

        private bool GetIsFull()
        { 
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
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

        internal void SetCellValue(int i_Value, int i_Row, int i_Column)
        {
            m_Matrix[i_Row, i_Column].Value = i_Value;
        }

        public int? GetCellValue(int i_Row, int i_Column)
        {
            return m_Matrix[i_Row, i_Column].Value;
        }

        public bool IsCellEmpty(int i_Row, int i_Column)
        {   
            return !m_Matrix[i_Row, i_Column].IsTaken;
        }
    }
}
