﻿namespace OtheloLogic
{
    public  struct Coordinate
    {
        public Coordinate(int row,int column)
        {
            Row = row;
            Column = column;
        }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
