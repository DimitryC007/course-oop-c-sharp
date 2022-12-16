using Ex02_Othelo.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_Othelo
{
    public class UserInterface
    {
        private GameLogic _gameLogic;
        // In this class we will have a while loop
        public void LaunchGame()
        {
            GameSettings gameSettings = Menu();
            _gameLogic = new GameLogic(gameSettings);
        }
                
        private GameSettings Menu()
        {
            ///TODO: Get input (matrixSize,Player1,Player2)
            return null;
        }

        public void PrintMatrix(int?[,] matrix)
        { 
            ///TODO: Print matrix
        }
    }
}
