using Ex03.GarageLogic;
using System;

namespace Ex03.ConsoleUI
{
    public class Validations
    {
        public static bool IsInputStringValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsInputFloatValid(string input)
        {
            return float.TryParse(input, out float f);
        }

        public static bool IsInputIntValid(string input)
        {
            return int.TryParse(input, out int i);
        }

        public static bool IsDangerousGoodsValid(string input)
        {
            return bool.TryParse(input, out bool result);
        }

        public static bool IsPositiveNumberValid(int number)
        {
            return number > 0;
        }

        public static bool IsInputEnumTypeValid<T>(string input) where T : struct, IConvertible
        {
            return Enum.TryParse(input, true, out T t) && Enum.IsDefined(typeof(T), t);
        }

        public static bool IsCarNumOfDoorsValid(string input)
        {
            bool isParsed = int.TryParse(input, out int numOfDoors);
            
            return isParsed && numOfDoors >= 2 && numOfDoors <= 5;
        }
    }
}
