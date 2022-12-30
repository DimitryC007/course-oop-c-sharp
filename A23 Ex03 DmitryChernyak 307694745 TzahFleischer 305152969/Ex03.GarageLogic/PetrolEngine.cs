using System;


namespace Ex03.GarageLogic
{
    internal class PetrolEngine : IEngine
    {

        public PetrolEngine(float i_MaxEnergy, eEnergyType i_EnergyType)
        {
            MaxEnergy = i_MaxEnergy;
            CurrentEnergyType = i_EnergyType;
        }

        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }
        public eEnergyType CurrentEnergyType { get; set; }
        public float EnergyPercentage => CurrentEnergy / MaxEnergy * 100;
        public float MinEnergy => 10;

        public void AddEnergy(float i_EnergyAmount, eEnergyType i_EnergyToAdd)
        {
            if (i_EnergyToAdd != CurrentEnergyType)
            {
                throw new ArgumentException($"Fuel is not acceptable current fuel type: {CurrentEnergyType} requested fueld to fill up: {i_EnergyToAdd}");
            }
            
            if (CurrentEnergy + i_EnergyAmount > MaxEnergy)
            {
                throw new ValueOutOfRangeException(new Exception(nameof(ValueOutOfRangeException)), MinEnergy, MaxEnergy, "Petrol amount was exceeded maxmium capacity");
            }

            CurrentEnergy += i_EnergyAmount;
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}---Engine details (Petrol)--- Energy type: {CurrentEnergyType} ,Min energy: {MinEnergy} ,Max energy: {MaxEnergy} ,Fuel percentage: {EnergyPercentage}%";
        }
    }
}
