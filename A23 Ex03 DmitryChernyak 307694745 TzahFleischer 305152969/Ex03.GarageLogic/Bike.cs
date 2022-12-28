namespace Ex03.GarageLogic
{
    internal class Bike: Vehicle
    {
        public Bike(IEngine engine)
        {
            Engine = engine;
        }

        public override int NumOfWheels => 2;
        public override int MaxTirePressure => 28;
        public override IEngine Engine { get; set; }
        public BikeLicenceType BikeLicence { get; set; }
        public int cubicCapacity { get; set; }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase vehicle)
        {
            base.SetVehicleInformation(vehicle);
            GarageCustomer.BikeDto bikeDto = vehicle as GarageCustomer.BikeDto;
            BikeLicence = bikeDto.BikeLicence;
            cubicCapacity = bikeDto.CubicCapacity;
        }

        public override void UpdateWheels(string manufacturerName, float tirePressure)
        {
            base.UpdateWheels(manufacturerName, tirePressure);
        }
    }
}
