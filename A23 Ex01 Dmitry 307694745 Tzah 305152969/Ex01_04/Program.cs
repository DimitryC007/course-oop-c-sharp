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

            if (!IsValidString(input))
            {
                Console.WriteLine("your text is incorrect");

                return;
            }

            Console.WriteLine("Is polyndrom: {0}", IsPolyndromRecursive(input, 0, input.Length - 1));

            int lowerCaseCounter = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLower(input[i]))
                {
                    lowerCaseCounter++;
                }
            }

            Console.WriteLine("num of chars with lower case are: {0}", lowerCaseCounter);
        }


        public static bool IsValidString(string i_text)
        {
            for (int i = 0; i < i_text.Length; i++)
            {
                if (int.TryParse(i_text[i].ToString(), out int res))
                {
                    return false;
                }
            }

            return true;
        }


        public static bool IsPolyndromRecursive(string i_text, int i_start, int i_end)
        {
            if (i_text[i_start] != i_text[i_end])
            {
                return false;
            }

            if (i_start >= i_end)
            {
                return true;
            }

            return IsPolyndromRecursive(i_text, i_start + 1, i_end - 1);
        }
    }
}
