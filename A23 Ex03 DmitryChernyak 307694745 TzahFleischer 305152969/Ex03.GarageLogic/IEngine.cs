using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    interface IEngine
    {
        float _currenEnergy { get; set; }
        float _maxEnergy { get; set; }

        void AddEnergy(float quantity, EnergyType energy);

        string ToString();
      

    }
}
