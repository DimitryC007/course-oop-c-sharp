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
                        return new Car
                        {
                            Color = carDto.Color,
                            LicensePlate = carDto.LicensePlate,
                            Model = carDto.Model,
                            RemainingEnergy = carDto.EnergyCapacity,
                            NumOfDoors = carDto.NumOfDoors,
                            Tires = new List<Tire>(4) { new Tire { TirePressure = carDto.TirePressure } },
                        };
                    }
                case VehicleType.PetrolBike:
                case VehicleType.ElectricBike:
                    {
                        GarageCustomer.BikeDto bikeDto = vehicle as GarageCustomer.BikeDto;
                        return null;
                       
                    }
                case VehicleType.PetrolTruck:
                    {

                        GarageCustomer.TruckDto truckDto = vehicle as GarageCustomer.TruckDto;
                        return new Truck
                        {
                            CargoVolume = truckDto.CargoVolume,
                            IsDangerousGoods = truckDto.IsDangerousGoods,
                            Model = truckDto.Model,
                            LicensePlate = truckDto.LicensePlate,
                            RemainingEnergy = truckDto.EnergyCapacity,
                            Tires = new List<Tire>(4) { new Tire { TirePressure = truckDto.TirePressure } },
                        };
                    }
                default:
                    throw new NotSupportedException("VehicleType is unknown");
            }
        }

        private object CreateVehicleEnergy(VehicleType vehicleType)
        {
            throw new NotImplementedException();
        }
    }
}
