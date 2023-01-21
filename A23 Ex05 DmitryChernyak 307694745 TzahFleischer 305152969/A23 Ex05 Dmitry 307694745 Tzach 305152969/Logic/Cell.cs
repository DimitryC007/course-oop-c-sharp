using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class Cell
    {
        public bool IsTaken => Value != null;
        public int? Value { get; internal set; }
    }
}
