using System;

namespace Ex01_01
{
    public class Program
    {
        public static void Main()
        {
            int[] binaryNumbers = GetBinaryNumbers();
            int[] base10Numbers = new int[binaryNumbers.Length];

            for (int i = 0; i < base10Numbers.Length; i++)
            {
                base10Numbers[i] = ConvertTo10BaseNumber(binaryNumbers[i]);
            }

            Array.Reverse(base10Numbers);
            PrintArray(base10Numbers);
            PrintStatistics(binaryNumbers , base10Numbers);

        }

        public static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("arr[{0}] = {1}", i, arr[i]);
            }
        }

        public static void PrintStatistics(int[] binaryNumbers, int[] base10Numbers)
        {
            Console.WriteLine("avarage of digit number: 1 is {0}", GetAvgByDigit(binaryNumbers, 1));
            Console.WriteLine("avarage of digit number: 0 is {0}", GetAvgByDigit(binaryNumbers, 0));
            Console.WriteLine("num of numbers divided by four is {0}", numOfDividedByFour(base10Numbers));
            int numOfPolyndroms = 0, numOfDescendingSeries = 0;
            for (int i = 0; i < base10Numbers.Length; i++)
            {
                if (IsPolyndrom(base10Numbers[i]))
                    numOfPolyndroms++;

                if (IsDescendingSeries(base10Numbers[i]))
                    numOfDescendingSeries++;

            }
            Console.WriteLine("number of polyndroms are: {0}", numOfPolyndroms);
            Console.WriteLine("number of descending series are: {0}", numOfDescendingSeries);
        }

        public static bool IsDescendingSeries(int number)
        {
            int lastDigit = number % 10;
            number /= 10;
            while (number > 0)
            {
                int currentLastDigit = number % 10;
                if (currentLastDigit >= lastDigit)
                    return false;
                number /= 10;
            }
            return true;
        }

        public static bool IsPolyndrom(int number)
        {
            int reversedNumber = ReverseNumber(number);
            while (number > 0)
            {
                if (number % 10 != reversedNumber % 10)
                    return false;

                reversedNumber /= 10;
                number /= 10;
            }
            return true;
        }


        public static int GetNumOfDigitsInNumber(int number)
        {
            int power = 0;
            while (number > 0)
            {
                number /= 10;
                power++;
            }
            return power;
        }

        public static int ReverseNumber(int number)
        {
            int reversedNumber = 0;
            double baseNumber = 10, power = GetNumOfDigitsInNumber(number) - 1;
            while (number > 0)
            {
                int lastDigit = number % 10;
                reversedNumber += lastDigit * (int)Math.Pow(baseNumber, power--);
                number /= 10;
            }
            return reversedNumber;
        }

        public static int numOfDividedByFour(int[] base10Numbers)
        {
            int counter = 0;
            for (int i = 0; i < base10Numbers.Length; i++)
            {
                if (base10Numbers[i] % 4 == 0)
                    counter++;
            }
            return counter;
        }

        public static int GetAvgByDigit(int[] binaryNumbers, int digit)
        {
            int countOfDigit = 0;
            for (int i = 0; i < binaryNumbers.Length; i++)
            {
                int binaryNumber = binaryNumbers[i];
                int size = 8;
                while (binaryNumber > 0)
                {
                    int lastDigit = binaryNumber % 10;
                    if (lastDigit == digit)
                        countOfDigit++;

                    binaryNumber /= 10;
                    size--;
                }
                if (digit == 0 && size != 0)
                    countOfDigit += size;
            }
            return countOfDigit / binaryNumbers.Length;
        }

        public static int ConvertTo10BaseNumber(int number)
        {
            int Base10Number = 0;
            double power = 0, baseNumber = 2;
            while (number > 0)
            {
                int lastDigit = number % 10;
                Base10Number += (int)(lastDigit * Math.Pow(baseNumber, power++));
                number /= 10;
            }
            return Base10Number;
        }

        public static int[] GetBinaryNumbers()
        {
            int size = 3;
            var binaryNumbers = new int[size];
            int index = 0;

            Console.WriteLine("Hello, we would like to get from you {0} binary numbers", size);
            while (index < size)
            {
                Console.WriteLine("Please enter your {0} binary number", index + 1);
                string input = Console.ReadLine();

                if (!IsValidBinaryNumber(input))
                {
                    Console.WriteLine("Please enter a valid binary number");
                }
                else
                {
                    binaryNumbers[index] = int.Parse(input);
                    index++;
                }
            }
            return binaryNumbers;
        }

        public static bool IsValidBinaryNumber(string inputNumber)
        {
            if (inputNumber.Length != 8)
                return false;

            bool isValidNumber = int.TryParse(inputNumber, out int number);
            if (!isValidNumber)
                return false;

            while (number > 0)
            {
                int lastDigit = number % 10;
                if (lastDigit != 1 && lastDigit != 0)
                    return false;

                number = number / 10;
            }
            return true;
        }
    }
}
