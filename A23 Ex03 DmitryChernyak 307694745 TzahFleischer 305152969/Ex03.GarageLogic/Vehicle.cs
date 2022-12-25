using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        public string Model;
        public string NumberPlate;
        public float RemainingEnergy;
        public List<Tire> Tires;
        public IEngine VehicelEngine;

    }
}
