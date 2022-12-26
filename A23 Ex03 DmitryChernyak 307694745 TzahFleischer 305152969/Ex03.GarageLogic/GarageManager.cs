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

        public bool IsVehicleExists(string numberPlate)
        {
            return _garageStorage.CheckIfCarExists(numberPlate);
        }

        public void AddVehicle(object VehicleDto)
        {
            throw new NotImplementedException();
        }

        public List<string> ShowVehiclesNumberPlatesByStatus(VehicleStatus vehicleStatus)
        {
            return _garageStorage.FindAllLicencePlatesByStatus(vehicleStatus);
        }

        public void ChangeVehicleStatus(string numberPlate, VehicleStatus vehicleStatus)
        {
            _garageStorage.ChangeVehicleStatus(numberPlate, vehicleStatus);
        }

        public void AddAirToTires(string numberPlate)
        {
            throw new NotImplementedException();
        }

        public void AddFuelToVehicle(string numberPlate, EnergyType energyType, float quantity)
        {
            throw new NotImplementedException();
        }

        public void ChargeVehicle(string numberPlate, float chargingHours)
        {
            throw new NotImplementedException();
        }

        public string GetVehicleDetails(string numberPlate)
        {
            throw new NotImplementedException();
        }
    }
}
