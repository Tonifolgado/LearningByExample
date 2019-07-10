using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class ProgramFlow
    {

        public void complexIFstatement(char input)
        {            
                if (input == 'a'
                    || input == 'e'
                    || input == 'i'
                    || input == 'o'
                    || input == 'u')
                {
                    Console.WriteLine("Input is a vowel");
                }
                else
                {
                    Console.WriteLine("Input is a consonant");
                }
        }

        public void CheckWithSwitch(char input)
        {
            switch (input)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    {
                        Console.WriteLine("Input is a vowel");
                        break;
                    }
                case 'y':
                    {
                        Console.WriteLine("Input is sometimes a vowel.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Input is a consonant");
                        break;
                    }
            }
        }

        public void switchWithGoto()
        {
            int i = 1;
            switch (i)
            {
                case 1:
                    {
                        Console.WriteLine("Case 1");
                        goto case 2;
                    }
                case 2:
                    {
                        Console.WriteLine("Case 2");
                        break;
                    }
            }

            // Displays
            // Case 1
            // Case 2

        }

        public void basicFor()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index++)
            {
                Console.Write(values[index]);
            }
            // Displays
            // 123456
        }

        public void loopWithMultipleVariables()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int x = 0, y = values.Length - 1;
                ((x < values.Length) && (y >= 0));
                x++, y--)
            {
                Console.Write(values[x]);
                Console.Write(values[y]);
            }
            // Displays
            // 162534435261
        }

        public void breakAndContinue()
        {
            Console.WriteLine("Using BREAK");
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) break;
                Console.Write(values[index]);
            }
            //Displays 123
            Console.WriteLine("");
            Console.WriteLine("Using CONTINUE");
            int[] othervalues = { 1, 2, 3, 4, 5, 6 };

            for (int index = 0; index < othervalues.Length; index++)
            {
                if (othervalues[index] == 4) continue;
                Console.Write(othervalues[index]);
            }
            // Displays 12356
        }


    }
}
