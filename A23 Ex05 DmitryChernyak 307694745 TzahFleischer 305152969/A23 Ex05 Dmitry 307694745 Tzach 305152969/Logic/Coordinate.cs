using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class Coordinate
    {
        public Coordinate(int i_Row, int i_Column)
        {
            Row = i_Row;
            Column = i_Column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
