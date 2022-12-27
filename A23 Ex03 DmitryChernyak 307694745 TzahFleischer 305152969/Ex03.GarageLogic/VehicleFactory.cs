using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    ///TODO: should be a singletone class (design pattern)
    internal class VehicleFactory
    {

        public Vehicle CreateVehicle(VehicleType vehicleType, GarageCustomer.VehicleBase vehicle)
        {
            
            switch (vehicleType)
            {
                case VehicleType.PetrolCar:
                case VehicleType.ElectricCar:
                    {
                        GarageCustomer.CarDto carDto = vehicle as GarageCustomer.CarDto;
                        return new Car();
                        
                    }
                case VehicleType.PetrolBike:
                case VehicleType.ElectricBike:
                    {
                        GarageCustomer.BikeDto bikeDto = vehicle as GarageCustomer.BikeDto;
                        return new Motorcycle();
                       
                    }
                case VehicleType.PetrolTruck:
                    {

                        GarageCustomer.TruckDto truckDto = vehicle as GarageCustomer.TruckDto;
                        return new Truck();
                        
                    }
                default:
                    throw new NotSupportedException("Vehicle Type is unknown");
            }
        }

        private IEngine CreateVehicleEnergy(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.PetrolCar:
                case VehicleType.PetrolBike:
                case VehicleType.PetrolTruck:
                    {
                        return new PetrolEngine();
                        
                    }
                  
                case VehicleType.ElectricCar:
                case VehicleType.ElectricBike:
                    {
                        return new ElectricEngine();
                    }
                default:
                    throw new NotSupportedException("Engine Type is unknown");

            }
        }
    }
}
