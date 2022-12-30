using System;

namespace Ex03.GarageLogic
{
    internal class VehicleFactory
    {
        public Vehicle CreateVehicle(VehicleType vehicleType)
        {
            Vehicle vehicle = null;

            switch (vehicleType)
            {
                case VehicleType.PetrolCar:
                    {
                        vehicle = new Car(new PetrolEngine(50f, EnergyType.Octan95));
                        break;

                    }
                case VehicleType.ElectricCar:
                    {
                        vehicle = new Car(new ElectricEngine(4.7f));
                        break;

                    }
                case VehicleType.PetrolBike:
                    {
                        vehicle = new Bike(new PetrolEngine(6f, EnergyType.Octan98));
                        break;

                    }
                case VehicleType.ElectricBike:
                    {
                        vehicle = new Bike(new ElectricEngine(1.6f));
                        break;

                    }
                case VehicleType.PetrolTruck:
                    {
                        vehicle = new Truck(new PetrolEngine(120f, EnergyType.Soler));
                        break;
                    }
                default:
                    throw new NotSupportedException("Vehicle Type is unknown");
            }

            return vehicle;
        }
    }
}
