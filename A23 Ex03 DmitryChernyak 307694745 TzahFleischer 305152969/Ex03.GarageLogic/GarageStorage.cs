using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class GarageStorage
    {
        private Dictionary<string, AutomobileRepair> _vehicles;

        public GarageStorage()
        {
            _vehicles = new Dictionary<string, AutomobileRepair>();
        }

        public AutomobileRepair GetVehicle(string licensePlate)
        {
            bool isVehicleExists = CheckIfVehicleExists(licensePlate);

            if (!isVehicleExists)
            {
                return null;
            }

            return _vehicles[licensePlate];
        }

        public void AddVehicle(string licensePlate, AutomobileRepair automobileRepair)
        {
            if (!_vehicles.ContainsKey(licensePlate))
            {
                _vehicles.Add(licensePlate, automobileRepair);
            }
        }

        public bool CheckIfVehicleExists(string licencePlate)
        {
            return _vehicles.ContainsKey(licencePlate);
        }

        public List<string> FindAllLicencePlatesByStatus(VehicleStatus vehicleStatus)
        {
            List<string> vehiclesLicensePlatesByStatus = new List<string>();

            if (vehicleStatus == VehicleStatus.AllStatus)
            {
                vehiclesLicensePlatesByStatus = new List<string>(_vehicles.Keys);
            }
            else
            {
                foreach (string licensePlate in _vehicles.Keys)
                {
                    if (_vehicles[licensePlate].VehicleStatus == vehicleStatus)
                    {
                        vehiclesLicensePlatesByStatus.Add(licensePlate);
                    }
                }
            }

            return vehiclesLicensePlatesByStatus;
        }

        public void ChangeVehicleStatus(string licensePlate, VehicleStatus vehicleStatus)
        {
            _vehicles[licensePlate].VehicleStatus = vehicleStatus;
        }

        internal void AddFuel(string licensePlate, EnergyType energyType, float quantity)
        {
            _vehicles[licensePlate].Vehicle.Engine.AddEnergy(quantity, energyType);
        }

        internal bool CheckIfGarageIsEmpty()
        {
            return _vehicles.Count == 0;
        }
    }
}
