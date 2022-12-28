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
        int MinEnergy { get; }
        EnergyType CurrentEnergyType { get; }
        float EnergyPercentage { get; }

        void AddEnergy(float energyAmount, EnergyType energy);

        string ToString();
    }
}
