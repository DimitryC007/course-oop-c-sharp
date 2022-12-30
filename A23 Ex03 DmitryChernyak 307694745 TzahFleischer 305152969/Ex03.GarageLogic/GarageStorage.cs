using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class GarageStorage
    {
        private Dictionary<string, AutomobileRepair> m_Vehicles;

        public GarageStorage()
        {
            m_Vehicles = new Dictionary<string, AutomobileRepair>();
        }

        public AutomobileRepair GetVehicle(string i_LicensePlate)
        {
            bool isVehicleExists = CheckIfVehicleExists(i_LicensePlate);

            if (!isVehicleExists)
            {
                return null;
            }

            return m_Vehicles[i_LicensePlate];
        }

        public void AddVehicle(string i_LicensePlate, AutomobileRepair i_AutomobileRepair)
        {
            if (!m_Vehicles.ContainsKey(i_LicensePlate))
            {
                m_Vehicles.Add(i_LicensePlate, i_AutomobileRepair);
            }
        }

        public bool CheckIfVehicleExists(string i_LicensePlate)
        {
            return m_Vehicles.ContainsKey(i_LicensePlate);
        }

        public List<string> FindAllLicencePlatesByStatus(eVehicleStatus i_VehicleStatus)
        {
            List<string> vehiclesLicensePlatesByStatus = new List<string>();

            if (i_VehicleStatus == eVehicleStatus.AllStatus)
            {
                vehiclesLicensePlatesByStatus = new List<string>(m_Vehicles.Keys);
            }
            else
            {
                foreach (string licensePlate in m_Vehicles.Keys)
                {
                    if (m_Vehicles[licensePlate].VehicleStatus == i_VehicleStatus)
                    {
                        vehiclesLicensePlatesByStatus.Add(licensePlate);
                    }
                }
            }

            return vehiclesLicensePlatesByStatus;
        }

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus I_VehicleStatus)
        {
            m_Vehicles[i_LicensePlate].VehicleStatus = I_VehicleStatus;
        }

        internal void AddFuel(string I_LicensePlate, eEnergyType i_EnergyType, float i_Quantity)
        {
            m_Vehicles[I_LicensePlate].Vehicle.Engine.AddEnergy(i_Quantity, i_EnergyType);
        }

        internal bool CheckIfGarageIsEmpty()
        {
            return m_Vehicles.Count == 0;
        }
    }
}
