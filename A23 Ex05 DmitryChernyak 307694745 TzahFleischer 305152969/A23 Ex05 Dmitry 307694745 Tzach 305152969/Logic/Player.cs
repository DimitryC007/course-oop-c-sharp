using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class Player
    {
        public Player(string i_Name, bool i_IsComputer)
        {
            Name = i_Name;
            IsComputer = i_IsComputer;
        }

        public string Name { get; set; }
        public bool IsComputer { get; set; }
    }
}
