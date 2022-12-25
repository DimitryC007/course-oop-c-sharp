using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class GarageStorage
    {
        private Dictionary<string, object> _vehicles;

        public GarageStorage()
        {
            _vehicles = new Dictionary<string, object>();
        }

        public object GetVehicle(string numberPlate)
        {
            throw new NotImplementedException();
        }

        public object AddVehicle(string numberPlate)
        {
            throw new NotImplementedException();
        }
    }
}
