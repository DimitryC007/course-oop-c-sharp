using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(Exception exception,float minValue, float maxValue, string message) : base (message, exception)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public float MaxValue { get; set; }
        public float MinValue { get; set; }
    }
}
