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

        public static void PrintStatistics(int[] i_digits)
        {
            Console.Write("The number is: ");
            PrintNumber(i_digits);
            string resultMessege =
                string.Format("The number of digits bigger than the first digit: {0}.{4}The smallest digit in the number is: {1}.{4}{2} digits divide by 3.{4}The average of the digits is {3}.",
                CountOfNumbersBiggerThanFirstDigit(i_digits),
                SmallestDigit(i_digits),
                DigitsDividedByThree(i_digits),
                AverageOfDigits(i_digits),
                Environment.NewLine);

            Console.WriteLine(resultMessege);
        }

        private static void PrintNumber(int[] i_digits)
        {
            StringBuilder digitBuilder = new StringBuilder();

            for (int i = 0; i < i_digits.Length; i++)
            {
                digitBuilder.Append(i_digits[i]);
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

        public static bool IsValidNumber(string i_number)
        {
            if (i_number.Length != 6)
            {
                return false;
            }

            return int.TryParse(i_number, out int placeHolder);
        }

        public static int CountOfNumbersBiggerThanFirstDigit(int[] i_digits)
        {
            int biggerNumbersThanFirstDigit = 0;

            for (int i = 0; i < i_digits.Length - 1; i++)
            {
                if (i_digits[i] > i_digits[i_digits.Length - 1])
                {
                    biggerNumbersThanFirstDigit++;
                }
            }

            return biggerNumbersThanFirstDigit;
        }

        public static int SmallestDigit(int[] i_digits)
        {
            int minimum = int.MaxValue;

            for (int i = 0; i < i_digits.Length; i++)
            {
                if (i_digits[i] < minimum)
                {
                    minimum = i_digits[i];
                }
            }

            return minimum;
        }

        public static int DigitsDividedByThree(int[] i_digits)
        {
            int numberOfDigitsDividedByThree = 0;

            for (int i = 0; i < i_digits.Length; i++)
            {
                if (i_digits[i] % 3 == 0)
                    numberOfDigitsDividedByThree++;
            }

            return numberOfDigitsDividedByThree;
        }

        public static float AverageOfDigits(int[] i_digits)
        {
            float sumOfDigits = 0;

            for (int i = 0; i < i_digits.Length; i++)
            {
                sumOfDigits += i_digits[i];
            }

            return sumOfDigits / i_digits.Length;
        }
    }
}
