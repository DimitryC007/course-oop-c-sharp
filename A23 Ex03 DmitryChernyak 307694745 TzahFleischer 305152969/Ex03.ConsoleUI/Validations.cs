using System;

namespace Ex03.ConsoleUI
{
    public class Validations
    {
        public static bool IsInputStringValid(string i_Input)
        {
            return !string.IsNullOrWhiteSpace(i_Input);
        }

        public static bool IsInputFloatValid(string i_Input)
        {
            return float.TryParse(i_Input, out float f);
        }

        public static bool IsInputIntValid(string i_Input)
        {
            return int.TryParse(i_Input, out int i);
        }

        public static bool IsDangerousGoodsValid(string i_Input)
        {
            return bool.TryParse(i_Input, out bool result);
        }

        public static bool IsPositiveNumberValid(int i_Number)
        {
            return i_Number > 0;
        }

        public static bool IsInputEnumTypeValid<T>(string i_Input) where T : struct, IConvertible
        {
            return Enum.TryParse(i_Input, true, out T t) && Enum.IsDefined(typeof(T), t);
        }

        public static bool IsCarNumOfDoorsValid(string i_Input)
        {
            bool isParsed = int.TryParse(i_Input, out int numOfDoors);
            
            return isParsed && numOfDoors >= 2 && numOfDoors <= 5;
        }
    }
}
