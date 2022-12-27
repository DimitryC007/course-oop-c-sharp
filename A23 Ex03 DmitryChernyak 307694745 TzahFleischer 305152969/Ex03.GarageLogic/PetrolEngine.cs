using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class PetrolEngine : IEngine
    {
        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }

        public void AddEnergy(float quantity, EnergyType energy)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
