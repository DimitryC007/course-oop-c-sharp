using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        public string _model;
        public string _numberPlate;
        public float _remainingEnergy;
        public List<Tire> _tires;
        public IEngine _vehicelEngine;

    }
}
