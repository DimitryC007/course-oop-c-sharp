using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        public void GarageMenu()
        {
            ///TODO: print all garage options and send to the right method
        }

        private void AddVehicle()
        {
            ///TODO: prompt user for licence plate, check if exists
            ///if exists change car status to in repair print messege
            ///if doesnt exists prompt user for all info on car

        }

        private void ChangeVehicleStatus()
        {
            ///TODO: check if car licence exists
            ///if yes change car status
            ///if not print messege
        }

        private void ShowVehiclesNumberPlates()
        {
            ///TODO: prompt for status,
            ///send to showvehiclesnumberplates with status
            ///print list
        }

        private void AddAirToTires()
        {
            ///TODO: prompt for licence plate
            ///check if exists
            ///if yes prompt airpressure and call garagefuntcion(needs to throw exception)
            ///if no print messege
        }

        private void AddFuelToVehicle()
        {
            ///TODO: prompt for licence plate
            ///check if exists
            ///if yes prompt fuel and call garagefuntcion(needs to throw exception)
            ///if no print messege
        }

        private void ChargeVehicleBattery()
        {
            ///TODO: prompt for licence plate
            ///check if exists
            ///if yes prompt energy and call garagefuntcion(needs to throw exception)
            ///if no print messege
        }

        private void ShowVehicleDetails()
        {
            ///TODO: prompt for licence plate
            ///check if exists
            ///if yes call garagefuntcion(needs to throw exception)
            ///if no print messege
        }

        private float EnergyMenu()
        {

            throw new NotImplementedException();
        }

        private float PetrolInput()
        {
            throw new NotImplementedException();
        }

        private float BatteryInput()
        {
            throw new NotImplementedException();
        }

        private string BikeLicenceInput()
        {
            throw new NotImplementedException();
        }

        private float TirePressureInput()
        {
            throw new NotImplementedException();
        }

        private string OwnerNameInput()
        {
            throw new NotImplementedException();
        }

        private string OwnerPhoneInput()
        {
            throw new NotImplementedException();
        }





    }
}
