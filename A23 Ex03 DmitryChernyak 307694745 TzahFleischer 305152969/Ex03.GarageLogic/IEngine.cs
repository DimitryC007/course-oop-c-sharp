using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    interface IEngine
    {
        float CurrentEnergy { get; set; }
        float MaxEnergy { get; set; }

        void AddEnergy(float quantity, EnergyType energy);

        string ToString();
      

    }
}
