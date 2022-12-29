using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class Messages
    {
        public static readonly string InvalidInputMessage = "Invalid input, please try again";
        public static readonly string AddCarMessage = "For Adding new vehicle to garage, enter license plate";
        public static readonly string VehicleAddedSuccessfullyMessage = "Vehicle added successfully";
        public static readonly string OwnerNameMessage = "Owner name:";
        public static readonly string OwnerPhoneMessage = "Owner phone";
        public static readonly string TirePressureMessage = "Tire pressure";
        public static readonly string BatteryStatusMessage = "Battery status:";
        public static readonly string FuelStatusMessage = "Fuel status:";
        public static readonly string NumOfDoorsMessage = "Num of doors:";
        public static readonly string VehicleModelMessage = "Vehicle model:";
        public static readonly string WheelManufactareMessage = "Wheel manufactare:";
        public static readonly string BikeCubicCapacityMessage = "Bike cubic capacity:";
        public static readonly string CargoVolumeMessage = "Cargo volume:";
        public static readonly string IsDangerousGoods = "Is Dangerous Goods: type only true or false";
        public static readonly string ChangeCarStatusMessage = "To change car status please enter car licence:";
        public static readonly string EnterCarStatusMessage = "Pleas Choose the status you want to change to";
        public static readonly string EnterCarStatustToFilterByMessage = "Please Choose the status you want to filter by";
        public static readonly string VehicleDoesntExistMessage = "The Vehicle Doesnt Exist";
        public static readonly string VehicleStatusChangedSuccefullyMessage = "The Vehicle status changed succesfully";
        public static readonly string VehicleLicenseNumberMessage = "Vehicle license number:";
        public static readonly string AddFuelMessage = "Please enter the licence plate of the car you want to refuel";
        public static readonly string EnterAmountOfFuel = "Please enter the amount of fuel";
        public static readonly string EnterFuelType = "please enter the type of fuel";
        public static readonly string FuelAddedCorrectlyMessage = "Fuel added succesfully";
        public static readonly string NoCarsInGarageMessage = "No Vehicles in the garage";

        public static string GarageMenuMessage()
        {
            StringBuilder garageMenu = new StringBuilder();
            garageMenu.Append("=================================");
            garageMenu.Append(" Welcome to our Garage Repair ");
            garageMenu.Append(string.Format("================================={0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 1 for adding a new car to garage{0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 2 for filtering all vehicles based on thier status{0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 3 for changing vehicle status{0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 4 for adding air to vehicle{0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 5 for fill up fuel to vehicle{0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 6 for charge vehicle{0}", Environment.NewLine));
            garageMenu.Append(string.Format("- Press 7 for display vehicle details{0}", Environment.NewLine));
            garageMenu.Append("* For exit press 8");
            return garageMenu.ToString();
        }

        public static string CarStatusMenu()
        {
            StringBuilder statusMenu = new StringBuilder();
            
                    
            statusMenu.Append(string.Format("- Press 1 for under rapair status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for filter by fixed status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 3 for filter by paid status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 4 for view all cars{0}", Environment.NewLine));
            return statusMenu.ToString();
        }

        public static string FuelTypeMenu()
        {
            StringBuilder statusMenu = new StringBuilder();


            statusMenu.Append(string.Format("- Press 1 for under soler{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 95{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 96{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 98{0}", Environment.NewLine));
            return statusMenu.ToString();
        }


        public static string VehicleTypeMenu()
        {
            StringBuilder vehicleTypesMenu = new StringBuilder();
            Array vehicleTypeValues = Enum.GetValues(typeof(VehicleType));
            foreach (VehicleType vehicleType in vehicleTypeValues)
            {
                string[] splited = null;

                if (vehicleType.ToString().IndexOf("petrol", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    string[] seperator = new string[] { "Petrol" };
                    splited = vehicleType.ToString().Split(seperator, StringSplitOptions.None);
                    splited[0] = "Petrol";
                }

                if (vehicleType.ToString().IndexOf("electric", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    string[] seperator = new string[] { "Electric" };
                    splited = vehicleType.ToString().Split(seperator, StringSplitOptions.None);
                    splited[0] = "Electric";
                }

                vehicleTypesMenu.Append(string.Format("- for {0} {1} press {2}{3}", splited[0], splited[1], (int)vehicleType, Environment.NewLine));
            }

            return vehicleTypesMenu.ToString();
        }

        public static string CarColorTypeMenu()
        {
            StringBuilder menuBuilder = new StringBuilder();
            Array CarColors = Enum.GetValues(typeof(CarColor));
            foreach (CarColor CarColor in CarColors)
            {

                menuBuilder.Append(string.Format("- for Car color: {0}  press {1}{2}", CarColor.ToString(), (int)CarColor, Environment.NewLine));
            }

            return menuBuilder.ToString();
        }

        public static string BikeLicenseTypeMenu()
        {
            StringBuilder menuBuilder = new StringBuilder();
            Array bikeLisences = Enum.GetValues(typeof(BikeLicenceType));
            foreach (BikeLicenceType bikeLisence in bikeLisences)
            {

                menuBuilder.Append(string.Format("- for Bike license: {0}  press {2}{3}", bikeLisence.ToString(), (int)bikeLisence, Environment.NewLine));
            }

            return menuBuilder.ToString();
        }
    }
}
