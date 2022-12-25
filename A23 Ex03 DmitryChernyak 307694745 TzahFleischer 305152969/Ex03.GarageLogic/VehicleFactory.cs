using System;

namespace Ex03.GarageLogic
{
    ///TODO: should be a singletone class (design pattern)
    internal class VehicleFactory
    {   
        
        public object CreateVehicle(VehicleType vehicleType)
        {
            throw new NotImplementedException();
        }

        private object CreateVehicleEnergy(VehicleType vehicleType)
        {
            throw new NotImplementedException();
        }
    }
}
