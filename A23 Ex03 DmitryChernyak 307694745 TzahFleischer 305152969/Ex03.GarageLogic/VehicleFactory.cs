using System;

namespace Ex03.GarageLogic
{
    internal class VehicleFactory
    {
        public Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.PetrolCar:
                    {
                        vehicle = new Car(new PetrolEngine(50f, eEnergyType.Octan95));
                        break;

                    }
                case eVehicleType.ElectricCar:
                    {
                        vehicle = new Car(new ElectricEngine(4.7f));
                        break;

                    }
                case eVehicleType.PetrolBike:
                    {
                        vehicle = new Bike(new PetrolEngine(6f, eEnergyType.Octan98));
                        break;

                    }
                case eVehicleType.ElectricBike:
                    {
                        vehicle = new Bike(new ElectricEngine(1.6f));
                        break;

                    }
                case eVehicleType.PetrolTruck:
                    {
                        vehicle = new Truck(new PetrolEngine(120f, eEnergyType.Soler));
                        break;
                    }
                default:
                    throw new NotSupportedException("Vehicle Type is unknown");
            }

            return vehicle;
        }
    }
}
