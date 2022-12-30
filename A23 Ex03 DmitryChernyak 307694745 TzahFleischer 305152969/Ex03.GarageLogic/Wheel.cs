namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        public string ManufacturerName { get; set; }
        public float TirePressure { get; set; }
        public float MaxTirePressure { get; set; }

        public void AddAirToTire()
        {
            TirePressure = MaxTirePressure;
        }
    }
}
