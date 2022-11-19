using System;
using Ex01_02;


namespace Ex01_03
{
    class Program
    {
        public static void Main()
        {
            int sizeOfDiamond = GetSizeOfDiamond();
            Ex01_02.Program.PrintDiamond(sizeOfDiamond);

        }

        public static int GetSizeOfDiamond()
        {
            Console.Write("Please enter the size of the diamond(number): ");
            string input = Console.ReadLine();
            while (!IsValidSizeOfDiamond(input))
            {
                Console.Write("Please enter a valid number:");
                input = Console.ReadLine();
            }
            int sizeOfDiamond = int.Parse(input);


            return FixedSize(sizeOfDiamond);

        }

        public static bool IsValidSizeOfDiamond(string input)
        {
            return int.TryParse(input, out int sizeOfDiamond);
        }

        public static int FixedSize(int number)
        {
            if (number % 2 == 0)
                return number + 1;
            else
                return number;
        }
    }
}
