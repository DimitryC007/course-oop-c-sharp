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
                PrintMessage(Messages.GarageMenuMessage());
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
                            break;
                        }
                    case (int)UserInterfaceChoise.Exit:
                        {
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
            PrintMessage(Messages.CarStatusMenu());
            string vehicleStatus = GetInput();
            while (!Validations.IsInputEnumTypeValid<VehicleType>(vehicleStatus))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.VehicleTypeMenu());
                vehicleStatus = GetInput();
            }


            bool isVehicleExists = _garageManager.ChangeVehicleStatus(licencePlate, (VehicleStatus)Enum.Parse(typeof(VehicleStatus), vehicleStatus));
            
            if(!isVehicleExists)
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
            PrintMessage(Messages.CarStatusMenu());
            string vehicleStatus = GetInput();
            
            
            while (!Validations.IsInputEnumTypeValid<VehicleStatus>(vehicleStatus))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.CarStatusMenu());
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
            EnergyType fuelType = GetFuelType();


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
                    PrintMessage(Messages.FuelAddedCorrectlyMessage,2);
                }
            }
            catch(ValueOutOfRangeException outOfRangeEx)
            {
                PrintMessage(outOfRangeEx.Message, 2);
            }
            catch(ArgumentException argumentEx)
            {
                PrintMessage(argumentEx.Message,2);
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
            ///TODO: prompt for licence plate
            ///check if exists
            ///if yes call garagefuntcion(needs to throw exception)
            ///if no print messege
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
                VehicleType vehicleType = GetVehicleTypeInput();
                GarageCustomer garageCustomer = new GarageCustomer();
                garageCustomer.VehicleType = vehicleType;
                garageCustomer.OwnerInfo = new GarageCustomer.OwnerInformation();
                garageCustomer.OwnerInfo.Name = GetStringInput(Messages.OwnerNameMessage);
                garageCustomer.OwnerInfo.Phone = GetStringInput(Messages.OwnerPhoneMessage);

                switch (garageCustomer.VehicleType)
                {
                    case VehicleType.PetrolCar:
                    case VehicleType.ElectricCar:
                        {
                            garageCustomer.Vehicle = GetCarInput(vehicleType, lisencePlate);
                            break;
                        }
                    case VehicleType.PetrolBike:
                    case VehicleType.ElectricBike:
                        {
                            garageCustomer.Vehicle = GetBikeInput(vehicleType, lisencePlate);
                            break;
                        }
                    case VehicleType.PetrolTruck:
                        {
                            garageCustomer.Vehicle = GetTruckInput(vehicleType, lisencePlate);
                            break;
                        }
                }
                _garageManager.AddVehicle(garageCustomer);
            }

            PrintMessage(Messages.VehicleAddedSuccessfullyMessage, 2);
        }

        private GarageCustomer.CarDto GetCarInput(VehicleType vehicleType, string licensePlate)
        {
            return new GarageCustomer.CarDto
            {
                LicensePlate = licensePlate,
                Color = GetCarColorInput(),
                NumOfDoors = GetCarNumOfDoorsInput(),
                Model = GetVehicleModelInput(),
                TirePressure = GetFloatInput(Messages.TirePressureMessage),
                EnergyAmount = GetVehicleEnergyInput(vehicleType)
            };
        }

        private GarageCustomer.BikeDto GetBikeInput(VehicleType vehicleType, string licensePlate)
        {
            return new GarageCustomer.BikeDto
            {
                LicensePlate = licensePlate,
                BikeLicence = GetBikeLicenceInput(),
                CubicCapacity = GetIntInput(Messages.BikeCubicCapacityMessage),
                Model = GetVehicleModelInput(),
                TirePressure = GetFloatInput(Messages.TirePressureMessage),
                EnergyAmount = GetVehicleEnergyInput(vehicleType)
            };
        }

        private GarageCustomer.TruckDto GetTruckInput(VehicleType vehicleType, string licensePlate)
        {
            return new GarageCustomer.TruckDto
            {
                LicensePlate = licensePlate,
                Model = GetVehicleModelInput(),
                TirePressure = GetFloatInput(Messages.TirePressureMessage),
                EnergyAmount = GetVehicleEnergyInput(vehicleType),
                CargoVolume = GetFloatInput(Messages.CargoVolumeMessage),
                IsDangerousGoods = IsDangerousGoodsInput()
            };
        }

        private CarColor GetCarColorInput()
        {
            PrintMessage(Messages.CarColorTypeMenu());
            string carColor = GetInput();

            while (!Validations.IsInputEnumTypeValid<CarColor>(carColor))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.CarColorTypeMenu());
                carColor = GetInput();
            }

            return (CarColor)Enum.Parse(typeof(CarColor), carColor);
        }

        private EnergyType GetFuelType()
        {
            PrintMessage(Messages.FuelTypeMenu());
            string fuelType = GetInput();

            while (!Validations.IsInputEnumTypeValid<EnergyType>(fuelType))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.CarColorTypeMenu());
                fuelType = GetInput();
            }

            return (EnergyType)Enum.Parse(typeof(EnergyType), fuelType);
        }

        private int GetCarNumOfDoorsInput()
        {
            PrintMessage(Messages.NumOfDoorsMessage);
            string numOfDoors = GetInput();

            while (!Validations.IsCarNumOfDoorsValid(numOfDoors) && !Validations.IsPositiveNumberValid(int.Parse(numOfDoors)))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.NumOfDoorsMessage);
                numOfDoors = GetInput();
            }

            return int.Parse(numOfDoors);
        }

        private string GetVehicleModelInput()
        {
            PrintMessage(Messages.VehicleModelMessage);
            string vehicleModel = GetInput();

            while (!Validations.IsInputStringValid(vehicleModel))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.VehicleModelMessage);
                vehicleModel = GetInput();
            }

            return vehicleModel;
        }

        private float GetVehicleEnergyInput(VehicleType vehicleType)
        {
            bool isPetrol = vehicleType.ToString().IndexOf("petrol", StringComparison.OrdinalIgnoreCase) > -1;
            string message = isPetrol ? Messages.FuelStatusMessage : Messages.BatteryStatusMessage;

            PrintMessage(message);
            string energy = GetInput();

            while (!Validations.IsInputFloatValid(energy) || !Validations.IsPositiveNumberValid(int.Parse(energy)))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(message);
                energy = GetInput();
            }

            return float.Parse(energy);
        }

        private bool IsDangerousGoodsInput()
        {
            PrintMessage(Messages.IsDangerousGoods);
            string input = GetInput();

            while (!Validations.IsDangerousGoodsValid(input))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.IsDangerousGoods);
                input = GetInput();
            }

            return bool.Parse(input);
        }

        private BikeLicenceType GetBikeLicenceInput()
        {
            PrintMessage(Messages.BikeLicenseTypeMenu());
            string bikeLicense = GetInput();

            while (!Validations.IsInputEnumTypeValid<BikeLicenceType>(bikeLicense))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.VehicleTypeMenu());
                bikeLicense = GetInput();
            }

            return (BikeLicenceType)Enum.Parse(typeof(BikeLicenceType), bikeLicense);
        }

        private VehicleType GetVehicleTypeInput()
        {
            PrintMessage(Messages.VehicleTypeMenu());
            string vehicleType = GetInput();

            while (!Validations.IsInputEnumTypeValid<VehicleType>(vehicleType))
            {
                PrintMessage(Messages.InvalidInputMessage);
                PrintMessage(Messages.VehicleTypeMenu());
                vehicleType = GetInput();
            }

            return (VehicleType)Enum.Parse(typeof(VehicleType), vehicleType);
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
