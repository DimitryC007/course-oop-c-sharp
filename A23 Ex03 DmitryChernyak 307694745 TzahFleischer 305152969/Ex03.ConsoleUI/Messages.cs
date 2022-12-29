using Ex03.GarageLogic;
using System;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class Messages
    {
        public const string InvalidInputMessage = "Invalid input, please try again";
        public const string AddCarMessage = "For Adding new vehicle to garage, enter license plate";
        public const string VehicleAddedSuccessfullyMessage = "Vehicle added successfully";
        public const string OwnerNameMessage = "Owner name:";
        public const string OwnerPhoneMessage = "Owner phone";
        public const string TirePressureMessage = "Tire pressure";
        public const string BatteryStatusMessage = "Battery status:";
        public const string FuelStatusMessage = "Fuel status:";
        public const string NumOfDoorsMessage = "Num of doors:";
        public const string VehicleModelMessage = "Vehicle model:";
        public const string WheelManufactareMessage = "Wheel manufactare:";
        public const string BikeCubicCapacityMessage = "Bike cubic capacity:";
        public const string CargoVolumeMessage = "Cargo volume:";
        public const string IsDangerousGoods = "Is Dangerous Goods: type only true or false";
        public const string ChangeCarStatusMessage = "To change car status please enter car licence:";
        public const string EnterCarStatusMessage = "Pleas Choose the status you want to change to";
        public const string EnterCarStatustToFilterByMessage = "Please Choose the status you want to filter by";
        public const string VehicleDoesntExistMessage = "The Vehicle Doesnt Exist";
        public const string VehicleStatusChangedSuccefullyMessage = "The Vehicle status changed succesfully";
        public const string VehicleLicenseNumberMessage = "Vehicle license number:";
        public const string AddFuelMessage = "Please enter the licence plate of the car you want to refuel";
        public const string EnterAmountOfFuel = "Please enter the amount of fuel";
        public const string EnterFuelType = "please enter the type of fuel";
        public const string FuelAddedCorrectlyMessage = "Fuel added succesfully";
        public const string ExitMessage = "Program exited";
        public const string NoCarsInGarageMessage = "No Vehicles in the garage";
        public const string AddEnergyMessage = "Please enter the licence plate of the car you want to recharge";
        public const string EnterAmountOfBattery = "Please enter the amount of charge(in hours)";
        public const string BatteryrechargedCorrectlyMessage = "Battery recharged succesfully";
        public const string VehicleListMessage = "Please Choose from these Vehicles License plates:\n";

        public static string GarageMenu => GetGarageMenuMessage();
        public static string VehicleTypeMenu => GetVehicleTypeMenu();
        public static string CarColorMenu => GetCarColorMenu();
        public static string BikeLicenseMenu => GetBikeLicenseMenu();
        public static string CarStatusMenu => GetCarStatusMenu();
        public static string FuelTypeMenu => GetFuelTypeMenu();

        private static string GetGarageMenuMessage()
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

        private static string GetCarStatusMenu()
        {
            StringBuilder statusMenu = new StringBuilder();

            statusMenu.Append(string.Format("- Press 1 for under rapair status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for filter by fixed status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 3 for filter by paid status{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 4 for view all cars{0}", Environment.NewLine));

            return statusMenu.ToString();
        }

        private static string GetFuelTypeMenu()
        {
            StringBuilder statusMenu = new StringBuilder();

            statusMenu.Append(string.Format("- Press 1 for under soler{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 95{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 96{0}", Environment.NewLine));
            statusMenu.Append(string.Format("- Press 2 for octan 98{0}", Environment.NewLine));

            return statusMenu.ToString();
        }

        private static string GetVehicleTypeMenu()
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

        private static string GetCarColorMenu()
        {
            StringBuilder menuBuilder = new StringBuilder();
            Array CarColors = Enum.GetValues(typeof(CarColor));
            foreach (CarColor CarColor in CarColors)
            {

                menuBuilder.Append(string.Format("- for Car color: {0}  press {1}{2}", CarColor.ToString(), (int)CarColor, Environment.NewLine));
            }

            return menuBuilder.ToString();
        }

        private static string GetBikeLicenseMenu()
        {
            StringBuilder menuBuilder = new StringBuilder();
            Array bikeLisences = Enum.GetValues(typeof(BikeLicenceType));
            foreach (BikeLicenceType bikeLisence in bikeLisences)
            {

                menuBuilder.Append(string.Format("- for Bike license: {0}  press {2}{3}", bikeLisence.ToString(), (int)bikeLisence, Environment.NewLine));
            }

            return menuBuilder.ToString();
        }

        public static string GetVehicleEnergyMessage(VehicleType vehicleType)
        {
            bool isPetrol = vehicleType.ToString().IndexOf("petrol", StringComparison.OrdinalIgnoreCase) > -1;
            return isPetrol ? FuelStatusMessage : BatteryStatusMessage;
        }
    }
}