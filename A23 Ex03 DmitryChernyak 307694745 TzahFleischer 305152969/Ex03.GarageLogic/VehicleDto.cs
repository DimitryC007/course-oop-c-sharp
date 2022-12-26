using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleDto
    {
    

        public int VehicleType { get; set; }
        public int VehicleStatus { get; set; }
        public float EnergyCapacity { get; set; }
        public float TirePressure { get; set; }
        public int CarColor { get; set; }
        public int Doors { get; set; }
        public BikeLicenceType BikeLicence { get; set; }
        public int CubicCapacity { get; set; }
        public bool IsDangerousGoods { get; set; }
        public int CargoVolume { get; set; }

        //might need to add owner name,model, ownerphone

        public VehicleDto(int vehicleType,int vehicleStatus,float energyCapacity,float tirePressure,int carColor,int doors, BikeLicenceType bikeLicence,int cubicCapacity,bool isDangerousGoods,int cargoVolume)
        {
            VehicleType = vehicleType;
            VehicleStatus = vehicleStatus;
            EnergyCapacity = energyCapacity;
            TirePressure = tirePressure;
            CarColor = carColor;
            Doors = doors;
            BikeLicence = bikeLicence;
            CubicCapacity = cubicCapacity;
            IsDangerousGoods = isDangerousGoods;
            CargoVolume = cargoVolume;

        }
    }
}
