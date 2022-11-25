using System;

namespace Ex01_02
{
    public class Program
    {
        public static void Main()
        {
            int row = 9;

            PrintDiamond(row);
        }

        public static void PrintDiamond(int i_SizeOfDiamond)
        {
            if (i_SizeOfDiamond < 1)
            {
                return;
            }

            if (i_SizeOfDiamond == 1)
            {
                Console.WriteLine("*");

                return;
            }

            if (i_SizeOfDiamond == 2)
            {
                Console.WriteLine("*\n*");

                return;
            }

            int top = (i_SizeOfDiamond / 2) + 1;
            int bottom = i_SizeOfDiamond - top;

            PrintTopTriangle(top, 1);
            PrintBottomTriangle(bottom, i_SizeOfDiamond - 2, 1);
        }

        public static void PrintTopTriangle(int i_rows, int i_numOfStars)
        {
            int space = i_rows - 1;

            if (i_rows == 0)
            {
                return;
            }

            PrintSpace(space);
            PrintStar(i_numOfStars);
            Console.WriteLine();
            PrintTopTriangle(i_rows - 1, i_numOfStars + 2);
        }

        public static void PrintSpace(int i_space)
        {
            if (i_space < 1)
            {
                return;
            }

            Console.Write(" ");
            PrintSpace(i_space - 1);
        }

        public static void PrintStar(int i_numberOfStars)
        {
            if (i_numberOfStars < 1)
            {
                return;
            }

            Console.Write("*");
            PrintStar(i_numberOfStars - 1);
        }

        public static void PrintBottomTriangle(int i_rows, int i_numOfStars, int i_space)
        {
            if (i_rows == 0)
            {
                return;
            }

            PrintSpace(i_space);
            PrintStar(i_numOfStars);
            Console.WriteLine();
            PrintBottomTriangle(i_rows - 1, i_numOfStars - 2, i_space + 1);
        }
    }
}
