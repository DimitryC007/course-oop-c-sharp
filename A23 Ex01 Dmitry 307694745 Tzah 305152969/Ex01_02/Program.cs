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

        public static void PrintTopTriangle(int rows, int numOfStars)
        {
            int space = rows - 1;
            if (rows == 0)
            {
                return;
            }
            PrintSpace(space);
            PrintStar(numOfStars);
            Console.WriteLine();
            PrintTopTriangle(rows - 1, numOfStars + 2);
        }

        public static void PrintSpace(int space)
        {
            if (space < 1)
            {
                return;
            }
            Console.Write(" ");
            PrintSpace(space - 1);
        }

        public static void PrintStar(int numberOfStars)
        {
            if (numberOfStars < 1)
            {
                return;
            }
            Console.Write("*");
            PrintStar(numberOfStars - 1);
        }

        public static void PrintBottomTriangle(int rows, int numOfStars, int space)
        {
            if (rows == 0)
            {
                return;
            }
            PrintSpace(space);
            PrintStar(numOfStars);
            Console.WriteLine();
            PrintBottomTriangle(rows - 1, numOfStars - 2, space + 1);
        }
    }
}
