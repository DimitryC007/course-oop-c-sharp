using System;

namespace Ex04.Menus.Interfaces
{
    public class ShowVersion : IAction
    {
        public void Execute()
        {
            Console.WriteLine("Version: 23.1.4.8859");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public class CountUppercase : IAction
    {
        public void Execute()
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
    }

    public class ShowDate : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Date: {DateTime.Now.ToShortDateString()}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public class ShowTime : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Time: {DateTime.Now.ToShortTimeString()}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
