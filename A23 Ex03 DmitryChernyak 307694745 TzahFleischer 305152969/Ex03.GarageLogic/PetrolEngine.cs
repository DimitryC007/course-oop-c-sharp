using System;


namespace Ex03.GarageLogic
{
    internal class PetrolEngine : IEngine
    {

        public PetrolEngine(float maxEnergy, EnergyType energyType)
        {
            MaxEnergy = maxEnergy;
            CurrentEnergyType = energyType;
        }

        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }
        public EnergyType CurrentEnergyType { get; set; }
        public float EnergyPercentage => CurrentEnergy / MaxEnergy * 100;
        public float MinEnergy => 10;

        public void AddEnergy(float energyAmount, EnergyType energyToAdd)
        {
            if (energyToAdd != CurrentEnergyType)
            {
                throw new ArgumentException($"Fuel is not acceptable current fuel type: {CurrentEnergyType} requested fueld to fill up: {energyToAdd}");
            }
            
            if (CurrentEnergy + energyAmount > MaxEnergy)
            {
                throw new ValueOutOfRangeException(new Exception(nameof(ValueOutOfRangeException)), MinEnergy, MaxEnergy, "Petrol amount was exceeded maxmium capacity");
            }

            CurrentEnergy += energyAmount;
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}---Engine details (Petrol)--- Energy type: {CurrentEnergyType} ,Min energy: {MinEnergy} ,Max energy: {MaxEnergy} ,Fuel percentage: {EnergyPercentage}%";
        }
    }
}
