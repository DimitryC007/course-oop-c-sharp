using Ex03.GarageLogic;
using System;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public enum eUserInterfaceChoice
    {
        AddNewVehicle = 1,
        ShowFilteredVehicle = 2,
        ChangeVehicleStatus = 3,
        AddAirToVehicleTires = 4,
        FillUpFuel = 5,
        ChargeBattery = 6,
        ShowVehicleDetails = 7,
        Exit = 8
    }

    public class UserInterface
    {
        private GarageManager _garageManager;
        public UserInterface()
        {
            _garageManager = new GarageManager();
        }

        public void GarageMenu()
        {
            while (true)
            {
                clear();
                printMessage(Messages.s_GarageMenu);
                int input = getMenuInput();
                clear();

                switch (input)
                {
                    case (int)eUserInterfaceChoice.AddNewVehicle:
                        {
                            tryAddVehicle();
                            break;
                        }
                    case (int)eUserInterfaceChoice.ShowFilteredVehicle:
                        {
                            showVehiclesLicensePlates();
                            break;
                        }
                    case (int)eUserInterfaceChoice.ChangeVehicleStatus:
                        {
                            changeVehicleStatus();
                            break;
                        }
                    case (int)eUserInterfaceChoice.AddAirToVehicleTires:
                        {
                            addAirToTires();
                            break;
                        }
                    case (int)eUserInterfaceChoice.FillUpFuel:
                        {
                            addFuelToVehicle();
                            break;
                        }
                    case (int)eUserInterfaceChoice.ChargeBattery:
                        {
                            chargeVehicleBattery();
                            break;
                        }
                    case (int)eUserInterfaceChoice.ShowVehicleDetails:
                        {
                            showVehicleDetails();
                            break;
                        }
                    case (int)eUserInterfaceChoice.Exit:
                        {
                            printMessage(Messages.k_ExitMessage, 2);
                            return;
                        }
                    default:
                        {
                            printMessage(Messages.k_InvalidInputMessage, 2);
                            break;
                        }
                }
            }
        }

        private void changeVehicleStatus()
        {
            if (!_garageManager.IsGarageEmpty())
            {
                printVehicleLicense();

                string licencePlate = getStringInput(Messages.k_ChangeVehicleStatusMessage);
                eVehicleStatus vehicleStatus = getEnumInput<eVehicleStatus>(Messages.s_CarStatusMenu);
                bool isVehicleExists = _garageManager.ChangeVehicleStatus(licencePlate, vehicleStatus);

                if (!isVehicleExists)
                {
                    printMessage(Messages.k_VehicleDoesntExistMessage, 2);
                }
                else
                {
                    printMessage(Messages.k_VehicleStatusChangedSuccefullyMessage, 2);
                }

            }
            else
            {
                printMessage(Messages.k_NoCarsInGarageMessage, 2);
            }

        }

        private void showVehiclesLicensePlates()
        {
            if (!_garageManager.IsGarageEmpty())
            {
                printMessage(Messages.k_EnterCarStatustToFilterByMessage);
                eVehicleStatus vehicleStatus = getEnumInput<eVehicleStatus>(Messages.s_CarStatusMenu);

                clear();

                printMessage(_garageManager.ShowVehiclesNumberPlatesByStatus(vehicleStatus));
                printMessage("", 5);
            }
            else
            {
                printMessage(Messages.k_NoCarsInGarageMessage, 2);
            }

        }

        private void addAirToTires()
        {
            if (_garageManager.IsGarageEmpty())
            {
                printMessage(Messages.k_NoCarsInGarageMessage, 2);
                return;
            }

            string licensePlate = getStringInput(Messages.k_AddAirToTiresMessage);

            printMessage(_garageManager.AddAirToTires(licensePlate), 2);
        }

        private void addFuelToVehicle()
        {
            if (!_garageManager.IsGarageEmpty())
            {
                printVehicleLicense();

                string licencePlate = getStringInput(Messages.k_AddFuelMessage);
                float fuelAmount = getFloatInput(Messages.k_EnterAmountOfFuel);

                printMessage(Messages.k_EnterFuelType);
                eEnergyType fuelType = getEnumInput<eEnergyType>(Messages.s_FuelTypeMenu);

                try
                {
                    bool isVehicleExists = _garageManager.AddEnergyToVehicle(licencePlate, fuelType, fuelAmount);
                    
                    if (!isVehicleExists)
                    {
                        printMessage(Messages.k_VehicleDoesntExistMessage, 2);
                    }
                    else
                    {
                        printMessage(Messages.k_FuelAddedCorrectlyMessage, 2);
                    }

                }
                catch (ValueOutOfRangeException outOfRangeEx)
                {
                    printMessage(outOfRangeEx.Message, 4);
                }
                catch (ArgumentException argumentEx)
                {
                    printMessage(argumentEx.Message, 4);
                }

            }
            else
            {
                printMessage(Messages.k_NoCarsInGarageMessage, 2);
            }
        }

        private void chargeVehicleBattery()
        {
            if (!_garageManager.IsGarageEmpty())
            {
                printVehicleLicense();

                string licencePlate = getStringInput(Messages.k_AddEnergyMessage);
                float fuelAmount = getFloatInput(Messages.k_EnterAmountOfBattery);

                try
                {
                    bool isVehicleExists = _garageManager.AddEnergyToVehicle(licencePlate, eEnergyType.Electric, fuelAmount);

                    if (!isVehicleExists)
                    {
                        printMessage(Messages.k_VehicleDoesntExistMessage, 2);
                    }
                    else
                    {
                        printMessage(Messages.k_BatteryrechargedCorrectlyMessage, 2);
                    }

                }
                catch (ValueOutOfRangeException outOfRangeEx)
                {
                    printMessage(outOfRangeEx.Message, 4);
                }
                catch (ArgumentException argumentEx)
                {
                    printMessage(argumentEx.Message, 4);
                }
            }
            else
            {
                printMessage(Messages.k_NoCarsInGarageMessage, 2);
            }
        }

        private void showVehicleDetails()
        {
            if (_garageManager.IsGarageEmpty())
            {
                printMessage(Messages.k_NoCarsInGarageMessage, 2);
                return;
            }
            
            string userInput = getStringInput(Messages.k_VehicleLicenseNumberMessage);

            printMessage(_garageManager.GetVehicleDetails(userInput), 5);
        }

        private int getMenuInput()
        {
            string userInput = Console.ReadLine();

            return int.TryParse(userInput, out int input) ? input : 0;
        }

        private string getInput()
        {
            return Console.ReadLine();
        }

        private void tryAddVehicle()
        {
            printMessage(Messages.k_AddCarMessage);

            string lisencePlate = getInput();
            bool isVehicleExists = _garageManager.TryAddVehicleToGarage(lisencePlate);

            if (!isVehicleExists)
            {
                eVehicleType vehicleType = getEnumInput<eVehicleType>(Messages.s_VehicleTypeMenu);
                GarageCustomer garageCustomer = new GarageCustomer();

                garageCustomer.VehicleType = vehicleType;
                garageCustomer.OwnerInfo = new GarageCustomer.OwnerInformation();
                garageCustomer.OwnerInfo.Name = getStringInput(Messages.k_OwnerNameMessage);
                garageCustomer.OwnerInfo.Phone = getStringInput(Messages.k_OwnerPhoneMessage);

                string vehicleEnergyMessage = Messages.getVehicleEnergyMessage(vehicleType);

                switch (garageCustomer.VehicleType)
                {
                    case eVehicleType.PetrolCar:
                    case eVehicleType.ElectricCar:
                        {
                            garageCustomer.Vehicle = getCarInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                    case eVehicleType.PetrolBike:
                    case eVehicleType.ElectricBike:
                        {
                            garageCustomer.Vehicle = getBikeInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                    case eVehicleType.PetrolTruck:
                        {
                            garageCustomer.Vehicle = getTruckInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                    default:
                        {
                            garageCustomer.Vehicle = getVeihcleInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                }

                try
                {
                    _garageManager.AddVehicle(garageCustomer);
                    printMessage(Messages.k_VehicleAddedSuccessfullyMessage, 2);
                }
                catch (NotSupportedException ex)
                {
                    printMessage(ex.Message, 4);
                }
                catch (ValueOutOfRangeException ex)
                {
                    printMessage(ex.Message, 4);
                }
            }
        }

        private GarageCustomer.CarDto getCarInput(string i_LicensePlate, string i_VehicleEnergyMessage)
        {
            return new GarageCustomer.CarDto
            {
                LicensePlate = i_LicensePlate,
                Model = getStringInput(Messages.k_VehicleModelMessage),
                ManufactareName = getStringInput(Messages.k_WheelManufactareMessage),
                TirePressure = getFloatInput(Messages.k_TirePressureMessage),
                EnergyAmount = getFloatInput(i_VehicleEnergyMessage),
                Color = getEnumInput<eCarColor>(Messages.s_CarColorMenu),
                NumOfDoors = getIntInput(Messages.k_NumOfDoorsMessage),
            };
        }

        private GarageCustomer.BikeDto getBikeInput(string i_LicensePlate, string i_VehicleEnergyMessage)
        {
            return new GarageCustomer.BikeDto
            {
                LicensePlate = i_LicensePlate,
                Model = getStringInput(Messages.k_VehicleModelMessage),
                ManufactareName = getStringInput(Messages.k_WheelManufactareMessage),
                TirePressure = getFloatInput(Messages.k_TirePressureMessage),
                EnergyAmount = getFloatInput(i_VehicleEnergyMessage),
                BikeLicence = getEnumInput<eBikeLicenceType>(Messages.s_BikeLicenseMenu),
                CubicCapacity = getIntInput(Messages.k_BikeCubicCapacityMessage),
            };
        }

        private GarageCustomer.VehicleBase getVeihcleInput(string i_LicensePlate, string i_VehicleEnergyMessage)
        {
            return new GarageCustomer.VehicleBase
            {
                LicensePlate = i_LicensePlate,
                Model = getStringInput(Messages.k_VehicleModelMessage),
                ManufactareName = getStringInput(Messages.k_WheelManufactareMessage),
                TirePressure = getFloatInput(Messages.k_TirePressureMessage),
                EnergyAmount = getFloatInput(i_VehicleEnergyMessage),
            };
        }

        private GarageCustomer.TruckDto getTruckInput(string i_LicensePlate, string i_VehicleEnergyMessage)
        {
            return new GarageCustomer.TruckDto
            {
                LicensePlate = i_LicensePlate,
                Model = getStringInput(Messages.k_VehicleModelMessage),
                ManufactareName = getStringInput(Messages.k_WheelManufactareMessage),
                TirePressure = getFloatInput(Messages.k_TirePressureMessage),
                EnergyAmount = getFloatInput(i_VehicleEnergyMessage),
                CargoVolume = getFloatInput(Messages.k_CargoVolumeMessage),
                IsDangerousGoods = getBoolInput(Messages.k_IsDangerousGoods)
            };
        }

        private T getEnumInput<T>(string i_Message) where T : struct, IConvertible
        {
            printMessage(i_Message);

            string enumType = getInput();

            while (!Validations.IsInputEnumTypeValid<T>(enumType))
            {
                printMessage(Messages.k_InvalidInputMessage);
                printMessage(i_Message);
                enumType = getInput();
            }

            return (T)Enum.Parse(typeof(T), enumType);
        }

        private bool getBoolInput(string i_Message)
        {
            printMessage(i_Message);

            string input = getInput();

            while (!Validations.IsDangerousGoodsValid(input))
            {
                printMessage(Messages.k_InvalidInputMessage);
                printMessage(i_Message);
                input = getInput();
            }

            return bool.Parse(input);
        }

        private int getIntInput(string i_Message)
        {
            printMessage(i_Message);

            string input = getInput();

            while (!Validations.IsInputIntValid(input) || !Validations.IsPositiveNumberValid(int.Parse(input)))
            {
                printMessage(Messages.k_InvalidInputMessage);
                printMessage(i_Message);
                input = getInput();
            }

            return int.Parse(input);
        }

        private string getStringInput(string i_Message)
        {
            printMessage(i_Message);

            string input = getInput();

            while (!Validations.IsInputStringValid(input))
            {
                printMessage(Messages.k_InvalidInputMessage);
                printMessage(i_Message);
                input = getInput();
            }

            return input;
        }

        private float getFloatInput(string i_Message)
        {
            printMessage(i_Message);

            string input = getInput();

            while (!Validations.IsInputFloatValid(input) || !Validations.IsPositiveNumberValid((int)float.Parse(input)))
            {
                printMessage(Messages.k_InvalidInputMessage);
                printMessage(i_Message);
                input = getInput();
            }

            return float.Parse(input);
        }

        private void printVehicleLicense()
        {
            printMessage(Messages.k_VehicleListMessage);
            printMessage(_garageManager.ShowVehiclesNumberPlatesByStatus(eVehicleStatus.AllStatus));
        }

        private void printMessage(string i_Message, int i_DelayInSecondes = 0)
        {
            Console.WriteLine(i_Message);
            delay(i_DelayInSecondes);
        }

        private void delay(int i_DelayInSecondes)
        {
            Thread.Sleep(i_DelayInSecondes * 1000);
        }

        private void clear()
        {
            Console.Clear();
        }
    }
}
