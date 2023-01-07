using System;

namespace Ex04.Menus.Test
{
    public static class Methods
    {
        public static void ShowVersion()
        {
            Console.WriteLine("Version: 23.1.4.8859");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void CountUppercase()
        {
            string userInput;
            int index = 0;

            Console.WriteLine("insert a sentence:");
            userInput = Console.ReadLine();

            foreach (char c in userInput)
            {
                if (char.IsUpper(c))
                {
                    index++;
                }
            }

            Console.WriteLine($"Count of uppercase: {index}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void ShowDate()
        {
            Console.WriteLine($"Date: {DateTime.Now.ToShortDateString()}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void ShowTime()
        {
            Console.WriteLine($"Time: {DateTime.Now.ToShortTimeString()}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
