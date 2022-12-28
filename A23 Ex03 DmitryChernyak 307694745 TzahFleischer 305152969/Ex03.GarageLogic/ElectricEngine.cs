using System;

namespace Ex03.GarageLogic
{
    class ElectricEngine : IEngine
    {
        public ElectricEngine(float maxEnergy)
        {
            MaxEnergy = maxEnergy;
        }

        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }
        public EnergyType CurrentEnergyType => EnergyType.Electric;
        public int MinEnergy => 10;
        public float EnergyPercentage => CurrentEnergy / MaxEnergy * 100;



        public void AddEnergy(float energyAmount, EnergyType energy = EnergyType.Electric)
        {
            if (CurrentEnergy + energyAmount > MaxEnergy)
            {
                throw new ValueOutOfRangeException(new Exception(nameof(ValueOutOfRangeException)), MinEnergy, MaxEnergy, "Electric amount was exceeded maxmium capacity");
            }

            CurrentEnergy += energyAmount;
        }

        public override string ToString()
        {
            return string.Format(
            "{0}Electric Engine: {0}Battery status: {1}",
            Environment.NewLine,
            EnergyPercentage);
        }
    }
}
