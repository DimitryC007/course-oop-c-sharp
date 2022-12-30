using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private GarageStorage m_GarageStorage;
        private VehicleFactory m_VehicleFactory;

        public GarageManager()
        {
            m_GarageStorage = new GarageStorage();
            m_VehicleFactory = new VehicleFactory();
        }

        public bool TryAddVehicleToGarage(string i_LicensePlate)
        {
            bool isVehicleExists = m_GarageStorage.CheckIfVehicleExists(i_LicensePlate);

            if (isVehicleExists)
            {
                m_GarageStorage.ChangeVehicleStatus(i_LicensePlate, eVehicleStatus.UnderRepair);
            }

            return isVehicleExists;
        }

        public void AddVehicle(GarageCustomer i_GarageCustomer)
        {

            Vehicle vehicle = m_VehicleFactory.CreateVehicle(i_GarageCustomer.VehicleType);

            vehicle.SetVehicleInformation(i_GarageCustomer.Vehicle);

            AutomobileRepair automobileRepair = new AutomobileRepair
            {
                Vehicle = vehicle,
                OwnerName = i_GarageCustomer.OwnerInfo.Name,
                OwnerPhone = i_GarageCustomer.OwnerInfo.Phone,
                VehicleStatus = eVehicleStatus.UnderRepair
            };

            m_GarageStorage.AddVehicle(vehicle.LicensePlate, automobileRepair);
        }

        public string ShowVehiclesNumberPlatesByStatus(eVehicleStatus i_VehicleStatus)
        {
            string licensePlates = "";
            List<string> vehiclesLicencePlate = m_GarageStorage.FindAllLicencePlatesByStatus(i_VehicleStatus);
            foreach (string carLicence in vehiclesLicencePlate)
            {
                licensePlates += (carLicence + Environment.NewLine);
            }

            return licensePlates;

        }

        public bool ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_VehicleStatus)
        {

            bool isExists = m_GarageStorage.CheckIfVehicleExists(i_LicensePlate);

            if (isExists)
            {
                m_GarageStorage.ChangeVehicleStatus(i_LicensePlate, i_VehicleStatus);
            }

            return isExists;
        }

        public string AddAirToTires(string i_LicensePlate)
        {
            if (!m_GarageStorage.CheckIfVehicleExists(i_LicensePlate))
            {
                return "Vehicle not exists";
            }

            m_GarageStorage.GetVehicle(i_LicensePlate).Vehicle.AddAirToTire();
            return "Tires updated successfully";
        }

        public bool AddEnergyToVehicle(string i_LicensePlate, eEnergyType I_EnergyType, float i_Quantity)
        {
            bool isVehicleExists = m_GarageStorage.CheckIfVehicleExists(i_LicensePlate);
            if (isVehicleExists)
            {
                m_GarageStorage.AddFuel(i_LicensePlate, I_EnergyType, i_Quantity);
            }

            return isVehicleExists;
        }

        public string GetVehicleDetails(string i_LicensePlate)
        {
            AutomobileRepair automobileRepair = m_GarageStorage.GetVehicle(i_LicensePlate);

            if (automobileRepair == null)
            {
                return "We didn't find your vehicle sir";
            }
            string vehicleGeneralInfo = $"--- Customer details---{Environment.NewLine}owner name: {automobileRepair.OwnerName} ,owner phone: {automobileRepair.OwnerPhone} ,vehicle status: {automobileRepair.VehicleStatus}";
            return vehicleGeneralInfo + automobileRepair.Vehicle.ToString();
        }

        public bool IsGarageEmpty()
        {
            return m_GarageStorage.CheckIfGarageIsEmpty();
        }
    }
}
