using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class AutomobileRepair
    {
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
