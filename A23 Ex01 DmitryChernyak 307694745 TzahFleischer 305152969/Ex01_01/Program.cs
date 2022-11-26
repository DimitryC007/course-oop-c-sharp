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
            PrintStatistics(binaryNumbers, base10Numbers);
        }

        public static void PrintArray(int[] i_arr)
        {
            for (int i = 0; i < i_arr.Length; i++)
            {
                Console.WriteLine("arr[{0}] = {1}", i, i_arr[i]);
            }
        }

        public static void PrintStatistics(int[] i_binaryNumbers, int[] i_base10Numbers)
        {
            Console.WriteLine("avarage of digit number: 1 is {0}", GetAvgByDigit(i_binaryNumbers, 1));
            Console.WriteLine("avarage of digit number: 0 is {0}", GetAvgByDigit(i_binaryNumbers, 0));
            Console.WriteLine("num of numbers divided by four is {0}", numOfDividedByFour(i_base10Numbers));

            int numOfPolyndroms = 0, numOfDescendingSeries = 0;

            for (int i = 0; i < i_base10Numbers.Length; i++)
            {
                if (IsPolyndrom(i_base10Numbers[i]))
                {
                    numOfPolyndroms++;
                }
                if (IsDescendingSeries(i_base10Numbers[i]))
                {
                    numOfDescendingSeries++;
                }
            }
            Console.WriteLine("number of polyndroms are: {0}", numOfPolyndroms);
            Console.WriteLine("number of descending series are: {0}", numOfDescendingSeries);
        }

        public static bool IsDescendingSeries(int i_number)
        {
            int lastDigit = i_number % 10;

            i_number /= 10;

            while (i_number > 0)
            {
                int currentLastDigit = i_number % 10;
                if (currentLastDigit >= lastDigit)
                {
                    return false;
                }
                i_number /= 10;
            }

            return true;
        }

        public static bool IsPolyndrom(int i_number)
        {
            int reversedNumber = ReverseNumber(i_number);

            while (i_number > 0)
            {
                if (i_number % 10 != reversedNumber % 10)
                {
                    return false;
                }
                reversedNumber /= 10;
                i_number /= 10;
            }

            return true;
        }


        public static int GetNumOfDigitsInNumber(int i_number)
        {
            int power = 0;

            while (i_number > 0)
            {
                i_number /= 10;
                power++;
            }

            return power;
        }

        public static int ReverseNumber(int i_number)
        {
            int reversedNumber = 0;
            double baseNumber = 10, power = GetNumOfDigitsInNumber(i_number) - 1;

            while (i_number > 0)
            {
                int lastDigit = i_number % 10;
                reversedNumber += lastDigit * (int)Math.Pow(baseNumber, power--);
                i_number /= 10;
            }

            return reversedNumber;
        }

        public static int numOfDividedByFour(int[] i_base10Numbers)
        {
            int counter = 0;

            for (int i = 0; i < i_base10Numbers.Length; i++)
            {
                if (i_base10Numbers[i] % 4 == 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static int GetAvgByDigit(int[] i_binaryNumbers, int i_digit)
        {
            int countOfDigit = 0;

            for (int i = 0; i < i_binaryNumbers.Length; i++)
            {
                int binaryNumber = i_binaryNumbers[i];
                int size = 8;

                while (binaryNumber > 0)
                {
                    int lastDigit = binaryNumber % 10;

                    if (lastDigit == i_digit)
                    {
                        countOfDigit++;
                    }

                    binaryNumber /= 10;
                    size--;
                }

                if (i_digit == 0 && size != 0)
                {
                    countOfDigit += size;
                }
            }

            return countOfDigit / i_binaryNumbers.Length;
        }

        public static int ConvertTo10BaseNumber(int i_number)
        {
            int Base10Number = 0;
            double power = 0, baseNumber = 2;

            while (i_number > 0)
            {
                int lastDigit = i_number % 10;
                Base10Number += (int)(lastDigit * Math.Pow(baseNumber, power++));
                i_number /= 10;
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

        public static bool IsValidBinaryNumber(string i_inputNumber)
        {
            if (i_inputNumber.Length != 8)
            {
                return false;
            }

            bool isValidNumber = int.TryParse(i_inputNumber, out int number);

            if (!isValidNumber)
            {
                return false;
            }

            while (number > 0)
            {
                int lastDigit = number % 10;

                if (lastDigit != 1 && lastDigit != 0)
                {
                    return false;
                }
                number = number / 10;
            }

            return true;
        }
    }
}
