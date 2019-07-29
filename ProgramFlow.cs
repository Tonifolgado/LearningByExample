using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class ProgramFlow
    {
        public void ifStatements()
        {
            // declare some variables for use in the code and assign initial values
            int first = 2;
            int second = 0;
            // use a single if statement to evaluate a condition and output
            Console.WriteLine("Single if statement");
            if (first == 2)
            {
                Console.WriteLine("The if statement evaluated to true");
            }
            Console.WriteLine("This line outputs regardless of the if condition");
            Console.WriteLine();

            // if statement that evaluates two conditions and executes
            // statements only if both are true
            Console.WriteLine("An if statement using && operator.");
            if (first == 2 && second == 0)
            {
                Console.WriteLine("The if statement evaluated to true");
            }
            Console.WriteLine("This line outputs regardless of the if condition");
            Console.WriteLine();

            // create nested if statements
            Console.WriteLine("Nested if statements.");
            if (first == 2)
            {
                if (second == 0)
                {
                    Console.WriteLine("Both outer and inner conditions are true.");
                }
                Console.WriteLine("Outer condition is true, inner may be true.");
            }
            Console.WriteLine("This line outputs regardless of the if condition");
            Console.WriteLine();
        }

        public void beyondBasicIfStatements()
        {
            bool condition1;
            bool condition2;
            bool condition3;
            // single if statement
            condition1 = true;
            if (condition1)
            {
                Console.WriteLine("This statement prints if condition is true");
            }
            Console.WriteLine("This statement executes regardless of condition.");
            Console.WriteLine();
            //nested if statement
            condition1 = true;
            condition2 = true;
            if (condition1)
            {
                if (condition2)
                {
                    Console.WriteLine("This only prints if both conditions are true.");
                }
            }
            Console.WriteLine();
            // if statement with logical operator
            condition1 = true;
            condition2 = true;
            if (condition1 && condition2)
            {
                Console.WriteLine("This only prints if both conditions are true.");
            }
            Console.WriteLine();
            // if-else statement
            condition1 = true;
            if (condition1)
            {
                Console.WriteLine("This statement prints if condition is true.");
            }
            else
            {
                Console.WriteLine("This statement prints if condition is false.");
            }
            Console.WriteLine("This statement executes regardless of condition.");
            Console.WriteLine();
            // if-else if statement
            condition1 = true;
            condition2 = false;
            condition3 = false;

            if (condition1)
            {
                Console.WriteLine("This statement prints if condition1 is true.");
            }
            else if (condition2)
            {
                Console.WriteLine("This statement prints if condition2 is true.");
            }
            else if (condition3)
            {
                Console.WriteLine("This statement prints if condition3 is true.");
            }
            else
            {
                Console.WriteLine("This statement prints if previous conditions are false.");
            }
            Console.WriteLine("This statement executes regardless of condition.");
            Console.WriteLine();
        }

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

        public void switchStatement()
        {
            // sample switch statement using a string comparison
            string condition = "Hello";
            switch (condition)
            {
                case "Good Morning":
                    Console.WriteLine("Good morning to you");
                    break;
                case "Hello":
                    Console.WriteLine("Hello");
                    break;
                case "Good Evening":
                    Console.WriteLine("Wonderful evening");
                    break;
                default:
                    Console.WriteLine("So long");
                    break;
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
