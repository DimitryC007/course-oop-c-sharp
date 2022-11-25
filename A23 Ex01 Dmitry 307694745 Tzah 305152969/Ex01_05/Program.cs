using System;
using System.Text;

namespace Ex01_05
{
    class Program
    {
        public static void Main()
        {
            int[] digitArray = GetNumber();
            PrintStatistics(digitArray);
        }

        public static void PrintStatistics(int[] digits)
        {
            Console.Write("The number is: ");
            PrintNumber(digits);
            string resultMessege =
                string.Format("The number of digits bigger than the first digit: {0}.{4}The smallest digit in the number is: {1}.{4}{2} digits divide by 3.{4}The average of the digits is {3}.",
                CountOfNumbersBiggerThanFirstDigit(digits),
                SmallestDigit(digits),
                DigitsDividedByThree(digits),
                AverageOfDigits(digits),
                Environment.NewLine);
            Console.WriteLine(resultMessege);
        }

        private static void PrintNumber(int[] digits)
        {
            StringBuilder digitBuilder = new StringBuilder();
            for (int i = 0; i < digits.Length; i++)
            {
                digitBuilder.Append(digits[i]);
            }
            Console.WriteLine(digitBuilder.ToString());
        }

        public static int[] GetNumber()
        {
            Console.Write("Enter a 6 digit number: ");
            string number = Console.ReadLine();
            int[] digitsFromNumber = new int[6];

            while (!IsValidNumber(number))
            {
                Console.Write("Invalid number! it must be a 6 digit number\nEnter another number: ");
                number = Console.ReadLine();
            }

            char[] numberBrokenToChar = number.ToCharArray();
            for (int i = 0; i < number.Length; i++)
            {
                digitsFromNumber[i] = (int)char.GetNumericValue(numberBrokenToChar[i]);
            }
            return digitsFromNumber;
        }

        public static bool IsValidNumber(string number)
        {
            if (number.Length != 6)
            {
                return false;
            }
            return int.TryParse(number, out int placeHolder);
        }

        public static int CountOfNumbersBiggerThanFirstDigit(int[] digits)
        {
            int biggerNumbersThanFirstDigit = 0;
            for (int i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] > digits[digits.Length - 1])
                {
                    biggerNumbersThanFirstDigit++;
                }
            }
            return biggerNumbersThanFirstDigit;
        }

        public static int SmallestDigit(int[] digits)
        {
            int minimum = int.MaxValue;
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i] < minimum)
                {
                    minimum = digits[i];
                }
            }
            return minimum;
        }

        public static int DigitsDividedByThree(int[] digits)
        {
            int numberOfDigitsDividedByThree = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i] % 3 == 0)
                    numberOfDigitsDividedByThree++;
            }
            return numberOfDigitsDividedByThree;
        }

        public static float AverageOfDigits(int[] digits)
        {
            float sumOfDigits = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                sumOfDigits += digits[i];
            }
            return sumOfDigits / digits.Length;
        }
    }
}
