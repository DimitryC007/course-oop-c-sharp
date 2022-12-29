using System;

namespace Ex03.GarageLogic
{
    internal class Bike: Vehicle
    {
        public Bike(IEngine engine)
        {
            Engine = engine;
        }

        public override int NumOfWheels => 2;
        public override float MaxTirePressure => 28;
        public override IEngine Engine { get; set; }
        public BikeLicenceType BikeLicence { get; set; }
        public int CubicCapacity { get; set; }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase vehicle)
        {
            base.SetVehicleInformation(vehicle);
            GarageCustomer.BikeDto bikeDto = vehicle as GarageCustomer.BikeDto;
            BikeLicence = bikeDto.BikeLicence;
            CubicCapacity = bikeDto.CubicCapacity;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}---Bike details---{Environment.NewLine}Bike licence: {BikeLicence} ,Cubic capacity: {CubicCapacity}";
        }
    }
}
