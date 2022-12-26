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

        public AutomobileRepair GetVehicle(string numberPlate)
        {
            throw new NotImplementedException();
        }

        public void AddVehicle(string numberPlate)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfCarExists(string licencePlate)
        {
            return _vehicles.ContainsKey(licencePlate);
        }

        public List<string> FindAllLicencePlatesByStatus(VehicleStatus vehicleStatus)
        {
            List<string> vehiclesNumberPlatesByStatus = new List<string>();

            if (vehicleStatus == VehicleStatus.AllStatus)
            {
                vehiclesNumberPlatesByStatus = new List<string>(_vehicles.Keys);
            }
            else
            {
                foreach(string licencePlate in _vehicles.Keys)
                {
                    if(_vehicles[licencePlate]._vehicleStatus == vehicleStatus)
                    {
                        vehiclesNumberPlatesByStatus.Add(licencePlate);
                    }
                }
            }

            return vehiclesNumberPlatesByStatus;
        }

        public void ChangeVehicleStatus(string numberPlate, VehicleStatus vehicleStatus)
        {
            _vehicles[numberPlate]._vehicleStatus = vehicleStatus;
        }

    }
}
