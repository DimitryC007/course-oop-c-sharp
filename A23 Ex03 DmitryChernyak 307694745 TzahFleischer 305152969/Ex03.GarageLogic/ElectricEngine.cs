using System;

namespace Ex03.GarageLogic
{
    class ElectricEngine : IEngine
    {
        public ElectricEngine(float i_MaxEnergy)
        {
            MaxEnergy = i_MaxEnergy;
        }

        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }
        public eEnergyType CurrentEnergyType => eEnergyType.Electric;
        public float MinEnergy => 10;
        public float EnergyPercentage => CurrentEnergy / MaxEnergy * 100;



        public void AddEnergy(float i_EnergyAmount, eEnergyType i_Energy)
        {
            if (i_Energy != CurrentEnergyType)
            {
                throw new ArgumentException($"Fuel is not acceptable for electric engine ,current battery type: {CurrentEnergyType}");
            }

            if (CurrentEnergy + i_EnergyAmount > MaxEnergy)
            {
                throw new ValueOutOfRangeException(new Exception(nameof(ValueOutOfRangeException)), MinEnergy, MaxEnergy, "Electric amount was exceeded maxmium capacity");
            }

            CurrentEnergy += i_EnergyAmount;
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}---Engine details (Electric)--- Energy type: {CurrentEnergyType} ,Min energy: {MinEnergy} ,Max energy: {MaxEnergy} ,Battery percentage: {EnergyPercentage}%";
        }
    }
}
