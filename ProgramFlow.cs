using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class ProgramFlow
    {
        public void commandLineArguments()
        {
            // Step through the command-line arguments.
            //foreach (string s in args)
            //{
            //    Console.WriteLine(s);
            //}

            // Alternatively, access the command-line arguments directly.
            Console.WriteLine(Environment.CommandLine);
            foreach (string s in Environment.GetCommandLineArgs())
            {
                Console.WriteLine(s);
            }
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();

            //execute the example using the following command:
            //commandLineArguments "one \"two\"    three" four 'five    six'

        }

        public void manipulateConsoleAppearance()
        {
            // Display the standard console.
            Console.Title = "Standard Console";
            Console.WriteLine("Press Enter to change the console's appearance.");
            Console.ReadLine();
            // Change the console appearance and redisplay.
            Console.Title = "Colored Text";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Press Enter to change the console's appearance.");
            Console.ReadLine();
            // Change the console appearance and redisplay.
            Console.Title = "Cleared / Colored Console";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine("Press Enter to change the console's appearance.");
            Console.ReadLine();

            // Change the console appearance and redisplay.
            Console.Title = "Resized Console";
            Console.ResetColor();
            Console.Clear();
            Console.SetWindowSize(100, 45);
            Console.BufferHeight = 500;
            Console.BufferWidth = 100;
            Console.CursorLeft = 20;
            Console.CursorSize = 50;
            Console.CursorTop = 20;
            Console.CursorVisible = false;
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

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

        public void multipleConditionswithSwitch()
        {
            // switch handling multiple conditions with a single action
            int number = 5;

            switch (number)
            {
                case 0:
                case 1:
                case 2:
                    Console.WriteLine("Contained in the set of whole numbers.");
                    break;
                case -1:
                case -10:
                    Console.WriteLine("Contained in the set of Integers.");
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

        public void anotherBasicFor()
        {
            // Count up to 10 in increments of 2
            for (int counter = 0; counter <= 10; counter += 2)
            {
                Console.WriteLine(counter);
            }
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

        public void lotteryProgram()
        {
            // used to set up a range of values to choose from
            int[] range = new int[49];
            // used to simulate lottery numbers chosen
            int[] picked = new int[6];
            // set up a random number generator
            Random rnd = new Random();
            // populate the range with values from 1 to 49
            for (int i = 0; i < 49; i++)
            {
                range[i] = i + 1;
            }
            // pick 6 random numbers
            for (int limit = 0; limit < 49; limit++)
            {
                for (int select = 0; select < 6; select++)
                {
                    picked[select] = range[rnd.Next(49)];
                }

            }
            Console.WriteLine("Your lotto numbers are:");
            for (int j = 0; j < 6; j++)
            {
                Console.Write(" " + picked[j] + " ");
            }
            Console.WriteLine();

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

        public void WhileExample()
        {
            // while statement example 1
            int someValue = 0;

            while (someValue < 10)
            {
                Console.WriteLine(someValue);
                someValue++;
            }

            // while statement example 2
            char someOtherValue;
            do
            {
                someOtherValue = (char)Console.Read();
                Console.WriteLine(someOtherValue);
                //if the key introduced is q the program exits
            } while (someOtherValue != 'q');

        }

        public void simpleDoWhile()
        {
            // do-while statement example
            int someValue = 0;

            do
            {
                Console.WriteLine(someValue);
                someValue++;
            } while (someValue < 10);

        }

        public void workingWithLoops()
        {
            // using a for loop to count up by one
            Console.WriteLine("Count up by one");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            // using a for loop to count down by one
            Console.WriteLine("Count down by one");
            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            // using a for loop to count up by 2
            Console.WriteLine("Count up by two");
            for (int i = 0; i < 10; i += 2)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            // using a for loop to increment by multiples of 5
            Console.WriteLine("Count up by multiples of 5");
            for (int i = 5; i < 1000; i *= 5)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            // using a foreach loop with integers
            Console.WriteLine("foeach over an array of integers");
            int[] arrInts = new int[] { 1, 2, 3, 4, 5 };
            foreach (int number in arrInts)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine();

            // using a foreach loop with strings
            Console.WriteLine("foreach over an array of strings");
            string[] arrStrings = new string[] { "First", "Second", "Third",
            "Fourth", "Fifth" };
            foreach (string text in arrStrings)
            {
                Console.WriteLine(text);
            }
            Console.WriteLine();

            // using a while loop
            int whileCounter = 0;
            Console.WriteLine("Counting up by one using a while loop");
            while (whileCounter < 10)
            {
                Console.WriteLine(whileCounter);
                whileCounter++;
            }
            Console.WriteLine();

            // using a do-while loop
            int doCounter = 0;
            Console.WriteLine("Counting up using a do-while loop");
            do
            {
                Console.WriteLine(doCounter);
                doCounter++;
            } while (doCounter < 10);
            Console.WriteLine();

        }


    }
}
