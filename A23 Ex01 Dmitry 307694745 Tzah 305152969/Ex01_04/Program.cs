using System;

namespace Ex01_04
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("please enter your input to check if polyndrom");
            string input = Console.ReadLine();
            bool isParsed = int.TryParse(input, out int inputNumber);
            if (isParsed)
            {
                string isDivided = inputNumber % 3 == 0 ? "is divided by 3" : "is not divided by 3";
                string textToDisplay = string.Format("the input you gave it's a number, the number: {0} {1}", inputNumber, isDivided);
                Console.WriteLine(textToDisplay);
                return;
            }
            Console.WriteLine("Is polyndrom: {0}", IsPolyndromRecursive(input, 0, input.Length - 1));

            int lowerCaseCounter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLower(input[i]))
                    lowerCaseCounter++;
            }
            Console.WriteLine("num of chars with lower case are: {0}", lowerCaseCounter);
        }


        public static bool IsPolyndromRecursive(string text, int start, int end)
        {
            if (text[start] != text[end])
                return false;

            if (start == end)
                return true;

            return IsPolyndromRecursive(text, start + 1, end - 1);
        }
    }
}
