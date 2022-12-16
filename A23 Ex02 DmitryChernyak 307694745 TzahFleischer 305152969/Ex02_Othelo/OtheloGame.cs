using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_Othelo
{
    public class OtheloGame
    {
        private UserInterface _userInterface;
        ///In the future we can add GameStats
        public OtheloGame()
        {
            _userInterface = new UserInterface();
        }

        public void LaunchGame()
        {
            _userInterface.LaunchGame();
        }
    }
}
