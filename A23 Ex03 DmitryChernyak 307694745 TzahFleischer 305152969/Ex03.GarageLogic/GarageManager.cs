using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private GarageStorage _garageStorage;
        private VehicleFactory _vehicleFactory;

        public GarageManager()
        {
            _garageStorage = new GarageStorage();
            _vehicleFactory = new VehicleFactory();
        }

        public bool TryAddVehicleToGarage(string licensePlate)
        {
            bool isVehicleExists = _garageStorage.CheckIfVehicleExists(licensePlate);

            if (isVehicleExists)
            {
                _garageStorage.ChangeVehicleStatus(licensePlate, VehicleStatus.UnderRepair);
            }

            return isVehicleExists;
        }

        private bool IsVehicleExists(string licensePlate)
        {
            return _garageStorage.CheckIfVehicleExists(licensePlate);
        }

        public void AddVehicle(GarageCustomer garageCustomer)
        {
            Vehicle vehicle = _vehicleFactory.CreateVehicle(garageCustomer.VehicleType);
            vehicle.SetVehicleInformation(garageCustomer.Vehicle);

            AutomobileRepair automobileRepair = new AutomobileRepair
            {
                Vehicle = vehicle,
                OwnerName = garageCustomer.OwnerInfo.Name,
                OwnerPhone = garageCustomer.OwnerInfo.Phone,
                VehicleStatus = VehicleStatus.UnderRepair
            };

            _garageStorage.AddVehicle(vehicle.LicensePlate, automobileRepair);
        }

        public List<string> ShowVehiclesNumberPlatesByStatus(VehicleStatus vehicleStatus)
        {
            return _garageStorage.FindAllLicencePlatesByStatus(vehicleStatus);
        }

        public bool ChangeVehicleStatus(string licensePlate, VehicleStatus vehicleStatus)
        {
            //throw new NotImplementedException();
            bool exist = IsVehicleExists(licensePlate);

            if(exist)
            {
                _garageStorage.ChangeVehicleStatus(licensePlate, vehicleStatus);
            }


            return exist;
        }

        public void AddAirToTires(string licensePlate,float airAmount)
        {
            throw new NotImplementedException();
        }

        public bool AddFuelToVehicle(string licensePlate, EnergyType energyType, float quantity)
        {
            bool isVehicleExists = _garageStorage.CheckIfVehicleExists(licensePlate);
            if (isVehicleExists)
            {
                _garageStorage.AddFuel(licensePlate,energyType, quantity);
            }

            return isVehicleExists;
        }

        public void ChargeVehicle(string licensePlate, float chargingHours)
        {
            throw new NotImplementedException();
        }

        public string GetVehicleDetails(string licensePlate)
        {
            throw new NotImplementedException();
        }
    }
}
