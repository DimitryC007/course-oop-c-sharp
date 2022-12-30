using System;

namespace Ex03.GarageLogic
{
    internal class Bike: Vehicle
    {
        public Bike(IEngine i_Engine)
        {
            Engine = i_Engine;
        }

        public override int NumOfWheels => 2;
        public override float MaxTirePressure => 28;
        public override IEngine Engine { get; set; }
        public eBikeLicenceType BikeLicence { get; set; }
        public int CubicCapacity { get; set; }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase i_Vehicle)
        {
            base.SetVehicleInformation(i_Vehicle);
            GarageCustomer.BikeDto bikeDto = i_Vehicle as GarageCustomer.BikeDto;
            BikeLicence = bikeDto.BikeLicence;
            CubicCapacity = bikeDto.CubicCapacity;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}---Bike details---{Environment.NewLine}Bike licence: {BikeLicence} ,Cubic capacity: {CubicCapacity}";
        }
    }
}
