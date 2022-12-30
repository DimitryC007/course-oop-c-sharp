namespace Ex03.GarageLogic
{
    interface IEngine
    {
        float CurrentEnergy { get; set; }
        float MaxEnergy { get; set; }
        float MinEnergy { get; }
        eEnergyType CurrentEnergyType { get; }
        float EnergyPercentage { get; }

        void AddEnergy(float i_EnergyAmount, eEnergyType i_Energy);

        string ToString();
    }
}
