namespace Ex03.GarageLogic
{
    public class GarageCustomer
    {
        public VehicleBase Vehicle { get; set; }
        public OwnerInformation OwnerInfo { get; set; }
        public VehicleType VehicleType { get; set; }


        public class OwnerInformation
        {
            public string Name { get; set; }
            public string Phone { get; set; }
        }

        public class VehicleBase
        {
            public string LicensePlate { get; set; }
            public float TirePressure { get; set; }
            public string ManufactareName { get; set; }
            public float EnergyAmount { get; set; }
            public string Model { get; set; }
        }

        public class CarDto : VehicleBase
        {
            public CarColor Color { get; set; }
            public int NumOfDoors { get; set; }
        }

        public class BikeDto : VehicleBase
        {
            public BikeLicenceType BikeLicence { get; set; }
            public int CubicCapacity { get; set; }
        }

        public class TruckDto : VehicleBase
        {
            public bool IsDangerousGoods { get; set; }
            public float CargoVolume { get; set; }
        }
    }
}
