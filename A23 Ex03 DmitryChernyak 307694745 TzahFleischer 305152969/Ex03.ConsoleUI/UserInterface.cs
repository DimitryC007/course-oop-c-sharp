using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public enum UserInterfaceChoice
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
                Clear();
                PrintMessage(Messages.GarageMenu);
                int input = GetMenuInput();

                switch (input)
                {
                    case (int)UserInterfaceChoice.AddNewVehicle:
                        {
                            TryAddVehicle();
                            break;
                        }
                    case (int)UserInterfaceChoice.ShowFilteredVehicle:
                        {
                            ShowVehiclesLicensePlates();
                            break;
                        }
                    case (int)UserInterfaceChoice.ChangeVehicleStatus:
                        {
                            ChangeVehicleStatus();
                            break;
                        }
                    case (int)UserInterfaceChoice.AddAirToVehicleTires:
                        {
                            AddAirToTires();
                            break;
                        }
                    case (int)UserInterfaceChoice.FillUpFuel:
                        {
                            AddFuelToVehicle();
                            break;
                        }
                    case (int)UserInterfaceChoice.ChargeBattery:
                        {
                            ChargeVehicleBattery();
                            break;
                        }
                    case (int)UserInterfaceChoice.ShowVehicleDetails:
                        {
                            ShowVehicleDetails();
                            break;
                        }
                    case (int)UserInterfaceChoice.Exit:
                        {
                            PrintMessage(Messages.ExitMessage, 2);
                            return;
                        }
                    default:
                        {
                            PrintMessage(Messages.InvalidInputMessage, 2);
                            break;
                        }
                }
            }
        }

        private void ChangeVehicleStatus()
        {
            Clear();
            if (!_garageManager.IsGarageEmpty())
            {
                PrintVehicleLicense();
                string licencePlate = GetStringInput(Messages.ChangeCarStatusMessage);
                PrintMessage(Messages.EnterCarStatusMessage);
                VehicleStatus vehicleStatus = GetEnumInput<VehicleStatus>(Messages.CarStatusMenu);


                bool isVehicleExists = _garageManager.ChangeVehicleStatus(licencePlate, vehicleStatus);

                if (!isVehicleExists)
                {
                    PrintMessage(Messages.VehicleDoesntExistMessage, 2);
                }
                else
                {
                    PrintMessage(Messages.VehicleStatusChangedSuccefullyMessage, 2);
                }
            }
            else
            {
                PrintMessage(Messages.NoCarsInGarageMessage, 2);
            }


        }

        private void ShowVehiclesLicensePlates()
        {
            Clear();
            if (!_garageManager.IsGarageEmpty())
            {
                List<string> carLicencePlates = new List<string>();
                PrintMessage(Messages.EnterCarStatustToFilterByMessage);
                PrintMessage(Messages.CarStatusMenu);
                VehicleStatus vehicleStatus = GetEnumInput<VehicleStatus>(Messages.CarStatusMenu);


                Clear();
                PrintMessage(_garageManager.ShowVehiclesNumberPlatesByStatus(vehicleStatus));
                PrintMessage("", 10);

            }
            else
            {
                PrintMessage(Messages.NoCarsInGarageMessage, 2);
            }


        }

        private void AddAirToTires()
        {
            string licensePlate = GetStringInput(Messages.AddAirToTiresMessage);
            PrintMessage(_garageManager.AddAirToTires(licensePlate), 2);
        }

        private void AddFuelToVehicle()
        {
            Clear();
            if (!_garageManager.IsGarageEmpty())
            {
                PrintVehicleLicense();
                string licencePlate = GetStringInput(Messages.AddFuelMessage);
                
                float fuelAmount = GetFloatInput(Messages.EnterAmountOfFuel);
                
                PrintMessage(Messages.EnterFuelType);
                EnergyType fuelType = GetEnumInput<EnergyType>(Messages.FuelTypeMenu);


                try
                {
                    bool isVehicleExists = _garageManager.AddEnergyToVehicle(licencePlate, fuelType, fuelAmount);
                    if (!isVehicleExists)
                    {
                        PrintMessage(Messages.VehicleDoesntExistMessage, 2);
                    }
                    else
                    {
                        PrintMessage(Messages.FuelAddedCorrectlyMessage, 2);
                    }
                }
                catch (ValueOutOfRangeException outOfRangeEx)
                {
                    PrintMessage(outOfRangeEx.Message, 2);
                }
                catch (ArgumentException argumentEx)
                {
                    PrintMessage(argumentEx.Message, 2);
                }
            }
            else
            {
                PrintMessage(Messages.NoCarsInGarageMessage, 2);
            }

        }


        private void ChargeVehicleBattery()
        {

            Clear();
            if (!_garageManager.IsGarageEmpty())
            {

                PrintVehicleLicense();

                string licencePlate = GetStringInput(Messages.AddEnergyMessage);

                float fuelAmount = GetFloatInput(Messages.EnterAmountOfBattery);

                try
                {
                    bool isVehicleExists = _garageManager.AddEnergyToVehicle(licencePlate, EnergyType.Electric, fuelAmount);
                    if (!isVehicleExists)
                    {
                        PrintMessage(Messages.VehicleDoesntExistMessage, 2);
                    }
                    else
                    {
                        PrintMessage(Messages.BatteryrechargedCorrectlyMessage, 2);
                    }
                }
                catch (ValueOutOfRangeException outOfRangeEx)
                {
                    PrintMessage(outOfRangeEx.Message, 2);
                }
                catch (ArgumentException argumentEx)
                {
                    PrintMessage(argumentEx.Message, 2);
                }
            }
            else
            {
                PrintMessage(Messages.NoCarsInGarageMessage, 2);
            }
        }

        private void ShowVehicleDetails()
        {
            string userInput = GetStringInput(Messages.VehicleLicenseNumberMessage);
            PrintMessage(_garageManager.GetVehicleDetails(userInput), 5);
        }

        private int GetMenuInput()
        {
            string userInput = Console.ReadLine();
            return int.TryParse(userInput, out int input) ? input : 0;
        }

        private string GetInput()
        {
            return Console.ReadLine();
        }

        private void TryAddVehicle()
        {
            PrintMessage(Messages.AddCarMessage);
            string lisencePlate = GetInput();
            bool isVehicleExists = _garageManager.TryAddVehicleToGarage(lisencePlate);

            if (!isVehicleExists)
            {
                VehicleType vehicleType = GetEnumInput<VehicleType>(Messages.VehicleTypeMenu);
                GarageCustomer garageCustomer = new GarageCustomer();
                garageCustomer.VehicleType = vehicleType;
                garageCustomer.OwnerInfo = new GarageCustomer.OwnerInformation();
                garageCustomer.OwnerInfo.Name = GetStringInput(Messages.OwnerNameMessage);
                garageCustomer.OwnerInfo.Phone = GetStringInput(Messages.OwnerPhoneMessage);
                string vehicleEnergyMessage = Messages.GetVehicleEnergyMessage(vehicleType);

                switch (garageCustomer.VehicleType)
                {
                    case VehicleType.PetrolCar:
                    case VehicleType.ElectricCar:
                        {
                            garageCustomer.Vehicle = GetCarInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                    case VehicleType.PetrolBike:
                    case VehicleType.ElectricBike:
                        {
                            garageCustomer.Vehicle = GetBikeInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                    case VehicleType.PetrolTruck:
                        {
                            garageCustomer.Vehicle = GetTruckInput(lisencePlate, vehicleEnergyMessage);
                            break;
                        }
                }
                _garageManager.AddVehicle(garageCustomer);
            }

            PrintMessage(Messages.VehicleAddedSuccessfullyMessage, 2);
        }

        private GarageCustomer.CarDto GetCarInput(string licensePlate, string vehicleEnergyMessage)
        {
            return new GarageCustomer.CarDto
            {
                LicensePlate = licensePlate,
                Model = GetStringInput(Messages.VehicleModelMessage),
                ManufactareName = GetStringInput(Messages.WheelManufactareMessage),
                TirePressure = GetFloatInput(Messages.TirePressureMessage),
                EnergyAmount = GetFloatInput(vehicleEnergyMessage),
                Color = GetEnumInput<CarColor>(Messages.CarColorMenu),
                NumOfDoors = GetIntInput(Messages.NumOfDoorsMessage),
            };
        }

        private GarageCustomer.BikeDto GetBikeInput(string licensePlate, string vehicleEnergyMessage)
        {
            return new GarageCustomer.BikeDto
            {
                LicensePlate = licensePlate,
                Model = GetStringInput(Messages.VehicleModelMessage),
                ManufactareName = GetStringInput(Messages.WheelManufactareMessage),
                TirePressure = GetFloatInput(Messages.TirePressureMessage),
                EnergyAmount = GetFloatInput(vehicleEnergyMessage),
                BikeLicence = GetEnumInput<BikeLicenceType>(Messages.BikeLicenseMenu),
                CubicCapacity = GetIntInput(Messages.BikeCubicCapacityMessage),
            };
        }

        private GarageCustomer.TruckDto GetTruckInput(string licensePlate, string vehicleEnergyMessage)
        {
            return new GarageCustomer.TruckDto
            {
                LicensePlate = licensePlate,
                Model = GetStringInput(Messages.VehicleModelMessage),
                ManufactareName = GetStringInput(Messages.WheelManufactareMessage),
                TirePressure = GetFloatInput(Messages.TirePressureMessage),
                EnergyAmount = GetFloatInput(vehicleEnergyMessage),
                CargoVolume = GetFloatInput(Messages.CargoVolumeMessage),
                IsDangerousGoods = GetBoolInput(Messages.IsDangerousGoods)
            };
        }

        private T GetEnumInput<T>(string message) where T : struct, IConvertible
        {
            PrintMessage(message);
            string enumType = GetInput();

            while (!Validations.IsInputEnumTypeValid<T>(enumType))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(message);
                enumType = GetInput();
            }

            return (T)Enum.Parse(typeof(T), enumType);
        }

        private bool GetBoolInput(string message)
        {
            PrintMessage(message);
            string input = GetInput();

            while (!Validations.IsDangerousGoodsValid(input))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(message);
                input = GetInput();
            }

            return bool.Parse(input);
        }

        private int GetIntInput(string message)
        {
            PrintMessage(message);
            string input = GetInput();

            while (!Validations.IsInputIntValid(input) || !Validations.IsPositiveNumberValid(int.Parse(input)))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(message);
                input = GetInput();
            }

            return int.Parse(input);
        }

        private string GetStringInput(string message)
        {
            PrintMessage(message);
            string input = GetInput();

            while (!Validations.IsInputStringValid(input))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(message);
                input = GetInput();
            }

            return input;
        }

        private float GetFloatInput(string message)
        {
            PrintMessage(message);
            string input = GetInput();

            while (!Validations.IsInputFloatValid(input) || !Validations.IsPositiveNumberValid(int.Parse(input)))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(message);
                input = GetInput();
            }

            return float.Parse(input);
        }

        private void PrintVehicleLicense()
        {
            PrintMessage(Messages.VehicleListMessage);
            PrintMessage(_garageManager.ShowVehiclesNumberPlatesByStatus(VehicleStatus.AllStatus));
        }

        private void PrintMessage(string message, int delayInSecondes = 0)
        {
            Console.WriteLine(message);
            Delay(delayInSecondes);
        }

        private void Delay(int delayInSecondes)
        {
            Thread.Sleep(delayInSecondes * 1000);
        }

        private void Clear()
        {
            Console.Clear();
        }
    }
}
