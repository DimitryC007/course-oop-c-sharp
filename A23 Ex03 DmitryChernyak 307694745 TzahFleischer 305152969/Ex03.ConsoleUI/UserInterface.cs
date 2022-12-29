using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public enum UserInterfaceChoise
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
                    case (int)UserInterfaceChoise.AddNewVehicle:
                        {
                            TryAddVehicle();
                            break;
                        }
                    case (int)UserInterfaceChoise.ShowFilteredVehicle:
                        {
                            ShowVehiclesLicensePlates();
                            break;
                        }
                    case (int)UserInterfaceChoise.ChangeVehicleStatus:
                        {
                            break;
                        }
                    case (int)UserInterfaceChoise.AddAirToVehicleTires:
                        {
                            break;
                        }
                    case (int)UserInterfaceChoise.FillUpFuel:
                        {
                            break;
                        }
                    case (int)UserInterfaceChoise.ChargeBattery:
                        {
                            break;
                        }
                    case (int)UserInterfaceChoise.ShowVehicleDetails:
                        {
                            ShowVehicleDetails();
                            break;
                        }
                    case (int)UserInterfaceChoise.Exit:
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
            PrintMessage(Messages.ChangeCarStatusMessage);
            string licencePlate = GetInput();
            PrintMessage(Messages.EnterCarStatusMessage);
            PrintMessage(Messages.CarStatusMenu);
            string vehicleStatus = GetInput();
            while (!Validations.IsInputEnumTypeValid<VehicleType>(vehicleStatus))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.VehicleTypeMenu);
                vehicleStatus = GetInput();
            }


            bool isVehicleExists = _garageManager.ChangeVehicleStatus(licencePlate, (VehicleStatus)Enum.Parse(typeof(VehicleStatus), vehicleStatus));

            if (!isVehicleExists)
            {
                PrintMessage(Messages.VehicleDoesntExistMessage, 2);
            }
            else
            {
                PrintMessage(Messages.VehicleStatusChangedSuccefullyMessage, 2);
            }


        }

        private void ShowVehiclesLicensePlates()
        {
            Clear();
            List<string> carLicencePlates = new List<string>();
            PrintMessage(Messages.EnterCarStatustToFilterByMessage);
            PrintMessage(Messages.CarStatusMenu);
            string vehicleStatus = GetInput();


            while (!Validations.IsInputEnumTypeValid<VehicleStatus>(vehicleStatus))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.CarStatusMenu);
                vehicleStatus = GetInput();
            }

            carLicencePlates = _garageManager.ShowVehiclesNumberPlatesByStatus((VehicleStatus)Enum.Parse(typeof(VehicleStatus), vehicleStatus));

            foreach (string carLicence in carLicencePlates)
            {
                Console.WriteLine(carLicence);
            }

            PrintMessage("", 10);

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
            Clear();
            PrintMessage(Messages.AddFuelMessage);
            string licencePlate = GetInput();
            PrintMessage(Messages.EnterAmountOfFuel);
            string fuel = GetInput();


            while (!Validations.IsInputFloatValid(fuel) || !Validations.IsPositiveNumberValid(int.Parse(fuel)))
            {
                PrintMessage(Messages.InvalidInputMessage);
                fuel = GetInput();
            }
            PrintMessage(Messages.EnterFuelType);
            EnergyType fuelType = GetEnumInput<EnergyType>(Messages.FuelTypeMenu);


            float fuelAmount = float.Parse(fuel);
            try
            {
                bool isVehicleExists = _garageManager.AddFuelToVehicle(licencePlate, fuelType, fuelAmount);
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
            string userInput = GetStringInput(Messages.VehicleLicenseNumberMessage);
            PrintMessage(_garageManager.GetVehicleDetails(userInput), 3);
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
