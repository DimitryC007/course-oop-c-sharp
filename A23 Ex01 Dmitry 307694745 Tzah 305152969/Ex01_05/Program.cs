using System;


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
                string.Format(
@"
The number of digits bigger than the first digit: {0}.
The smallest digit in the number is: {1}.
{2} digits divide by 3.
The average of the digits is {3}.",
                CountOfNumbersBiggerThanFirstDigit(digits), SmallestDigit(digits),
                DigitsDividedByThree(digits),AverageOfDigits(digits));
            Console.WriteLine(resultMessege);
        }

        private static void PrintNumber(int[] digits)
        {
            for(int i = 0; i < digits.Length; i++)
            {
                Console.Write(digits[i]);
            }
        }

        //get input of number
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
            for(int i = 0; i < number.Length; i++)
            {
                digitsFromNumber[i] = (int)char.GetNumericValue(numberBrokenToChar[i]); 
            }

            return digitsFromNumber;


        }

        //check input is 6 digit number
        public static bool IsValidNumber(string number)
        {
            if (number.Length != 6)
                return false;
            return int.TryParse(number, out int placeHolder);
        }

        //check how many digits are bigger than the first digit
        public static int CountOfNumbersBiggerThanFirstDigit(int[] digits)
        {
            int biggerNumbersThanFirstDigit = 0;
            for(int i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] > digits[digits.Length - 1])
                    biggerNumbersThanFirstDigit++;
            }

            return biggerNumbersThanFirstDigit;
        }
       
        //minimal digit
        public static int SmallestDigit(int[] digits)
        {
            int minimum = int.MaxValue;
            for(int i = 0; i < digits.Length; i++)
            {
                if (digits[i] < minimum)
                {
                    minimum = digits[i];
                }
            }
            return minimum;
       
        }

        //how many digits divide by 3
        public static int DigitsDividedByThree(int[] digits)
        {
            int numberOfDigitsDividedByThree = 0;

            for(int i = 0; i < digits.Length; i++)
            {
                if (digits[i] % 3 == 0)
                    numberOfDigitsDividedByThree++;
            }
            return numberOfDigitsDividedByThree;
        }


        //mean of digits
        public static float AverageOfDigits(int[] digits)
        {
            float sumOfDigits = 0;
            for(int i = 0; i < digits.Length; i++)
            {
                sumOfDigits += digits[i];
            }

            return sumOfDigits/digits.Length;
        }



    }
}
