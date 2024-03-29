﻿using System;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        public Truck(IEngine i_Engine)
        {
            Engine = i_Engine;
        }

        public override int NumOfWheels => 14;
        public override float MaxTirePressure => 34;
        public override IEngine Engine { get; set; }
        public bool IsDangerousGoods { get; set; }
        public float CargoVolume { get; set; }

        public override void SetVehicleInformation(GarageCustomer.VehicleBase i_Vehicle)
        {
            base.SetVehicleInformation(i_Vehicle);
            GarageCustomer.TruckDto truckDto = i_Vehicle as GarageCustomer.TruckDto;

            CargoVolume = truckDto.CargoVolume;
            IsDangerousGoods = truckDto.IsDangerousGoods;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}---Truck details---{Environment.NewLine}Trasnporting dangerous goods: {IsDangerousGoods} ,Cargo volume: {CargoVolume}";
        }
    }
}
