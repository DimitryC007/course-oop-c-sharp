using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Tire
    {
        public string ManufacturerName { get; set; }
        public float TirePressure { get; set; }
        public float MaxTirePressure { get; set; }

        public void AddAirToTire(float airAmount)
        {
            throw new NotImplementedException();
        }
    }
}
