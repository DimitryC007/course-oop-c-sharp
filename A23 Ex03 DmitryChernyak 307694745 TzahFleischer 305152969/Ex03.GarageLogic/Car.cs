using System;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public Car(IEngine i_Engine)
        {
            Engine = i_Engine;
        }

        private int m_NumOfDoors;
        public override int NumOfWheels => 5;
        public override float MaxTirePressure => 32;
        public override IEngine Engine { get; set; }
        public eCarColor Color { get; set; }
        public int NumOfDoors
        {
            get => m_NumOfDoors;
            set
            {
                if (value < 2 || value > 5)
                {
                    throw new ArgumentException($"Num of door should be 2 - 5 and not {value}");
                }

                m_NumOfDoors = value;
            }
        }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase I_Vehicle)
        {
            base.SetVehicleInformation(I_Vehicle);

            GarageCustomer.CarDto carDto = I_Vehicle as GarageCustomer.CarDto;

            Color = carDto.Color;
            NumOfDoors = carDto.NumOfDoors;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}---Car details---{Environment.NewLine}Color: {Color} ,Number of doors: {NumOfDoors}";
        }
    }
}
