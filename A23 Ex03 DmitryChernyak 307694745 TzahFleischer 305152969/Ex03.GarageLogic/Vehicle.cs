using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        public Vehicle()
        {
            CreateWheels();
        }

        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public List<Wheel> Wheels { get; set; }
        public abstract int NumOfWheels { get; }
        public abstract int MaxTirePressure { get; }
        public abstract IEngine Engine { get; set; }

        public virtual void SetVehicleInformation(GarageCustomer.VehicleBase vehicle)
        {
            Model = vehicle.Model;
            LicensePlate = vehicle.LicensePlate;
            Engine.AddEnergy(vehicle.EnergyAmount, Engine.CurrentEnergyType);
            UpdateWheels(vehicle.ManufactareName, vehicle.TirePressure);
        }

        private void CreateWheels()
        {
            Wheels = new List<Wheel>();
            for (int i = 0; i < NumOfWheels; i++)
            {
                Wheels.Add(new Wheel
                {
                    MaxTirePressure = MaxTirePressure
                });
            }
        }

        public virtual void UpdateWheels(string manufacturerName, float tirePressure)
        {
            foreach (var wheel in Wheels)
            {
                if (tirePressure > wheel.MaxTirePressure)
                {
                    throw new ValueOutOfRangeException(new Exception(nameof(UpdateWheels)), 0, wheel.MaxTirePressure, "tire pressure excceded the maximum");
                }

                wheel.TirePressure = tirePressure;
                wheel.ManufacturerName = manufacturerName;
            }
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}vehicle details: {Environment.NewLine}Lisence plate: {LicensePlate}{Environment.NewLine}Model: {Model}{Environment.NewLine}Wheel details: manufactare: {Wheels[0].ManufacturerName} ,wheels: {NumOfWheels} ,max tire pressure: {Wheels[0].MaxTirePressure} ,current tire pressure: {Wheels[0].TirePressure}{Environment.NewLine}Engine details:{Engine.ToString()}";
        }
    }
}
