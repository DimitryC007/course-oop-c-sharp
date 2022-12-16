using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_Othelo.Logic
{
    public class GameLogic
    {
        public Board _board;
        public GameLogic(GameSettings gameSettings)
        {
            _board = new Board(gameSettings.MatrixSize);
        }
    }
}
