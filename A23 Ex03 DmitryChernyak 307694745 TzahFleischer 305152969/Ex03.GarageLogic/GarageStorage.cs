using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class GarageStorage
    {
        private Dictionary<string, AutomobileRepair> _vehicles;

        public GarageStorage()
        {
            _vehicles = new Dictionary<string, AutomobileRepair>();
        }

        public AutomobileRepair GetVehicle(string numberPlate)
        {
            throw new NotImplementedException();
        }

        public void AddVehicle(string numberPlate)
        {
            throw new NotImplementedException();
        }
    }
}
