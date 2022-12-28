namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public Car(IEngine engine)
        {
            Engine = engine;
        }

        public override int NumOfWheels => 5;
        public override int MaxTirePressure => 32;
        public override IEngine Engine { get; set; }
        public CarColor Color { get; set; }
        public int NumOfDoors { get; set; }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase vehicle)
        {
            base.SetVehicleInformation(vehicle);
            GarageCustomer.CarDto carDto = vehicle as GarageCustomer.CarDto;
            Color = carDto.Color;
            NumOfDoors = carDto.NumOfDoors;
        }

        public override void UpdateWheels(string manufacturerName, float tirePressure)
        {
            base.UpdateWheels(manufacturerName, tirePressure);
        }
    }
}
