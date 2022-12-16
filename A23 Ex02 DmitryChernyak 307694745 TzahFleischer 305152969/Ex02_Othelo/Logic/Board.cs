using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_Othelo.Logic
{
    public class Board
    {
        private int?[,] _matrix;
        public int?[,] Matrix { get { return _matrix; } }
        public bool IsBoardFull { get; }

        public Board(int matrixSize)
        {
            _matrix = new int?[matrixSize, matrixSize];
        }

        public void InitCoinsOnBoard()
        {
            ///init X,O in the center
        }

        public void SetMove(int cellValue, string move)
        {

        }



    }
}
