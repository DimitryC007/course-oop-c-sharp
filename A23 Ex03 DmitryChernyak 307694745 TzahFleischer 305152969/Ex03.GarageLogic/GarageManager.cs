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

        public string ShowVehiclesNumberPlatesByStatus(VehicleStatus vehicleStatus)
        {
            string licensePlates = "";
            List<string> vehiclesLicencePlate = _garageStorage.FindAllLicencePlatesByStatus(vehicleStatus);
            foreach (string carLicence in vehiclesLicencePlate)
            {
                licensePlates += (carLicence + Environment.NewLine);
            }

            return licensePlates;

        }

        public bool ChangeVehicleStatus(string licensePlate, VehicleStatus vehicleStatus)
        {
            bool isExists = IsVehicleExists(licensePlate);

            if (isExists)
            {
                _garageStorage.ChangeVehicleStatus(licensePlate, vehicleStatus);
            }

            return isExists;
        }

        public string AddAirToTires(string licensePlate)
        {
            if (!IsVehicleExists(licensePlate))
            {
                return "Vehicle not exists";
            }

            _garageStorage.GetVehicle(licensePlate).Vehicle.AddAirToTire();
            return "Tires updated successfully";
        }

        public bool AddFuelToVehicle(string licensePlate, EnergyType energyType, float quantity)
        {
            bool isVehicleExists = _garageStorage.CheckIfVehicleExists(licensePlate);
            if (isVehicleExists)
            {
                _garageStorage.AddFuel(licensePlate, energyType, quantity);
            }

            return isVehicleExists;
        }

   

        public string GetVehicleDetails(string licensePlate)
        {
            AutomobileRepair automobileRepair = _garageStorage.GetVehicle(licensePlate);

            if (automobileRepair == null)
            {
                return "We didn't find your vehicle sir";
            }
            string vehicleGeneralInfo = $"--- Customer details---{Environment.NewLine}owner name: {automobileRepair.OwnerName} ,owner phone: {automobileRepair.OwnerPhone} ,vehicle status: {automobileRepair.VehicleStatus}";
            return vehicleGeneralInfo + automobileRepair.Vehicle.ToString();
        }

        public bool IsGarageEmpty()
        {
            return _garageStorage.CheckIfGarageIsEmpty();
        }
    }
}
