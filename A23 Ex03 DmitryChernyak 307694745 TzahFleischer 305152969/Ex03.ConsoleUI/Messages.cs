using Ex03.GarageLogic;
using System;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class Messages
    {
        public const string k_InvalidInputMessage = "Invalid input, please try again";
        public const string k_AddCarMessage = "For Adding new vehicle to garage, enter license plate";
        public const string k_VehicleAddedSuccessfullyMessage = "Vehicle added successfully";
        public const string k_OwnerNameMessage = "Owner name:";
        public const string k_OwnerPhoneMessage = "Owner phone:";
        public const string k_TirePressureMessage = "Tire pressure:";
        public const string k_BatteryStatusMessage = "Battery status:";
        public const string k_FuelStatusMessage = "Fuel status:";
        public const string k_NumOfDoorsMessage = "Num of doors:";
        public const string k_VehicleModelMessage = "Vehicle model:";
        public const string k_WheelManufactareMessage = "Wheel manufactare:";
        public const string k_BikeCubicCapacityMessage = "Bike cubic capacity:";
        public const string k_CargoVolumeMessage = "Cargo volume:";
        public const string k_IsDangerousGoods = "Is Dangerous Goods: type only true or false";
        public const string k_ChangeVehicleStatusMessage = "To change car status please enter car licence:";
        public const string k_EnterVehicleStatusMessage = "Pleas Choose the status you want to change to";
        public const string k_EnterCarStatustToFilterByMessage = "Please Choose the status you want to filter by";
        public const string k_VehicleDoesntExistMessage = "The Vehicle Doesnt Exist";
        public const string k_VehicleStatusChangedSuccefullyMessage = "The Vehicle status changed succesfully";
        public const string k_VehicleLicenseNumberMessage = "Vehicle license number:";
        public const string k_AddFuelMessage = "Please enter the licence plate of the car you want to refuel";
        public const string k_EnterAmountOfFuel = "Please enter the amount of fuel";
        public const string k_EnterFuelType = "please enter the type of fuel";
        public const string k_FuelAddedCorrectlyMessage = "Fuel added succesfully";
        public const string k_ExitMessage = "Program exited";
        public const string k_NoCarsInGarageMessage = "No Vehicles in the garage";
        public const string k_AddAirToTiresMessage = "For updating tire pressure, enter license plate";
        public const string k_AddEnergyMessage = "Please enter the licence plate of the car you want to recharge";
        public const string k_EnterAmountOfBattery = "Please enter the amount of charge(in hours)";
        public const string k_BatteryrechargedCorrectlyMessage = "Battery recharged succesfully";
        public const string k_VehicleListMessage = "Please Choose from these Vehicles License plates:\n";

        public static string s_GarageMenu => getGarageMenuMessage();
        public static string s_VehicleTypeMenu => getVehicleTypeMenu();
        public static string s_CarColorMenu => getCarColorMenu();
        public static string s_BikeLicenseMenu => getBikeLicenseMenu();
        public static string s_CarStatusMenu => getCarStatusMenu();
        public static string s_FuelTypeMenu => getFuelTypeMenu();

        private static string getGarageMenuMessage()
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

        private static string getCarStatusMenu()
        {
            StringBuilder statusMenu = new StringBuilder();

            statusMenu.Append(string.Format("- Press 1 for under rapair status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for filter by fixed status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 3 for filter by paid status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 4 for view all cars{0}", Environment.NewLine));

            return statusMenu.ToString();
        }

        private static string getFuelTypeMenu()
        {
            StringBuilder statusMenu = new StringBuilder();

            statusMenu.Append(string.Format("- Press 1 for under soler{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 95{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 3 for octan 96{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 4 for octan 98{0}", Environment.NewLine));

            return statusMenu.ToString();
        }

        private static string getVehicleTypeMenu()
        {
            StringBuilder vehicleTypesMenu = new StringBuilder();
            Array vehicleTypeValues = Enum.GetValues(typeof(eVehicleType));
            
            foreach (eVehicleType vehicleType in vehicleTypeValues)
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

        private static string getCarColorMenu()
        {
            StringBuilder menuBuilder = new StringBuilder();
            Array CarColors = Enum.GetValues(typeof(eCarColor));
            
            foreach (eCarColor CarColor in CarColors)
            {

                menuBuilder.Append(string.Format("- for Car color: {0}  press {1}{2}", CarColor.ToString(), (int)CarColor, Environment.NewLine));
            }

            return menuBuilder.ToString();
        }

        private static string getBikeLicenseMenu()
        {
            StringBuilder menuBuilder = new StringBuilder();
            Array bikeLisences = Enum.GetValues(typeof(eBikeLicenceType));
            
            foreach (eBikeLicenceType bikeLisence in bikeLisences)
            {

                menuBuilder.Append(string.Format("- for Bike license: {0}  press {2}{3}", bikeLisence.ToString(), (int)bikeLisence, Environment.NewLine));
            }

            return menuBuilder.ToString();
        }

        public static string getVehicleEnergyMessage(eVehicleType i_VehicleType)
        {
            bool isPetrol = i_VehicleType.ToString().IndexOf("petrol", StringComparison.OrdinalIgnoreCase) > -1;

            return isPetrol ? k_FuelStatusMessage : k_BatteryStatusMessage;
        }
    }
}