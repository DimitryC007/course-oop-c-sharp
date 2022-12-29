using System;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public Car(IEngine engine)
        {
            Engine = engine;
        }
        private int _numOfDoors;
        public override int NumOfWheels => 5;
        public override float MaxTirePressure => 32;
        public override IEngine Engine { get; set; }
        public CarColor Color { get; set; }
        public int NumOfDoors
        {
            get => _numOfDoors;
            set
            {
                if (value < 2 || value > 5)
                {
                    throw new ArgumentException($"Num of door should be 2 - 5 and not {value}");
                }

                _numOfDoors = value;
            }
        }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase vehicle)
        {
            base.SetVehicleInformation(vehicle);
            GarageCustomer.CarDto carDto = vehicle as GarageCustomer.CarDto;
            Color = carDto.Color;
            NumOfDoors = carDto.NumOfDoors;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}---Car details---{Environment.NewLine}Color: {Color} ,Number of doors: {NumOfDoors}";
        }
    }
}
