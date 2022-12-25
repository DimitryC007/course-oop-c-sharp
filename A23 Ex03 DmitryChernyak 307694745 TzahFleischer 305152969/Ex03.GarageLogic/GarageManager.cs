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

        public bool AddVehicle(string numberPlate)
        {
            throw new ValueOutOfRangeException(2, 4, "not good");
            throw new NotImplementedException();
        }

        public List<string> ShowVehiclesNumberPlates(VehicleStatus vehicleStatus)
        {
            throw new NotImplementedException();
        }

        public bool ChangeVehicleStatus(string numberPlate, VehicleStatus vehicleStatus)
        {
            throw new NotImplementedException();
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
