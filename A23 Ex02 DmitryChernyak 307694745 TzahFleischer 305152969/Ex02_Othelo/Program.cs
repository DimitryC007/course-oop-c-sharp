using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_Othelo
{
    class Program
    {
        static void Main(string[] args)
        {
            OtheloGame otheloGame = new OtheloGame();
            otheloGame.LaunchGame();
            otheloGame.PlayGame();
        }
    }
}
