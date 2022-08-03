using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1byte_scheduler_basic_interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte schedule = new byte();
            //Console.WriteLine("{0}", Convert.ToString(schedule,2));

            // compaund assigment   `x &= y`   is equivalent  `x = x & y`

            const byte NUL_MASK = 0b00000000; // used for invalid input - index 0 = empty mask
            const byte MON_MASK = 0b00000001;
            const byte TUE_MASK = 0b00000010;
            const byte WED_MASK = 0b00000100;
            const byte THU_MASK = 0b00001000;
            const byte FRI_MASK = 0b00010000;
            const byte SAT_MASK = 0b00100000;
            const byte SUN_MASK = 0b01000000;



            Console.ForegroundColor = ConsoleColor.Cyan;

            string[] weekDaysNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            while (true)
            {
                byte[] weekDays = new byte[8];

                weekDays[0] = NUL_MASK;
                weekDays[1] = MON_MASK;
                weekDays[2] = TUE_MASK;
                weekDays[3] = WED_MASK;
                weekDays[4] = THU_MASK;
                weekDays[5] = FRI_MASK;
                weekDays[6] = SAT_MASK;
                weekDays[7] = SUN_MASK;

                Console.WriteLine("Select the weekday for schedule the training \n Input number only! \n1.{0}\n2.{1}\n3.{2}\n4.{3}\n5.{4}\n6.{5}\n7.{6}", weekDaysNames);

                string weekDayIndexStr = Console.ReadLine();

                int weekDayIndexInt = 0;

                if (!string.IsNullOrWhiteSpace(weekDayIndexStr))
                {
                    weekDayIndexInt = ParceToInt(weekDayIndexStr);
                }
                else
                {
                    PrintError("Empty or null input!");
                    weekDayIndexInt = 0;
                }

                schedule = SetWeekDay(schedule, weekDays, weekDayIndexInt);

                EndDialog();

                Console.Clear();

                int maxWordSize = 10;

                HeaderColumns(weekDaysNames, maxWordSize);

                EmptyColumns(maxWordSize);

                FilledColumns(schedule, weekDays, maxWordSize);

                EmptyColumns(maxWordSize);

                BottomColumns(maxWordSize);

                EndDialog();

                Console.Clear();
            }
        }

        private static void BottomColumns(int maxWordSize)
        {
            Console.Write("╚");
            string bottomString = new string('═', maxWordSize + 2);
            for (int i = 0; i < 7; i++)
            {
                Console.Write(bottomString);

                if (i < 6)
                {
                    Console.Write("╩");
                }
            }
            Console.Write("╝");
            Console.Write("\n");
        }

        private static void FilledColumns(byte schedule, byte[] weekDays, int maxWordSize)
        {
            string whitespaces2 = new string(' ', maxWordSize / 2);
            Console.Write("║");
            for (int i = 0; i < 7; i++)
            {
                Console.Write(whitespaces2);
                if ((schedule & weekDays[i+1]) > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(" {0}", GetWeekDayFlag(schedule, weekDays, i + 1));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(whitespaces2);
                Console.Write("║");
            }
            Console.Write("\n");
        }

        private static void HeaderColumns(string[] weekDaysNames, int maxWordSize)
        {
            Console.Write("╔");
            for (int i = 0; i < 7; i++)
            {
                int compenseSize = maxWordSize - weekDaysNames[i].Length;
                Console.Write("═{0}═", weekDaysNames[i]);
                int cnt = 0;
                while (cnt != compenseSize)
                {
                    Console.Write("═");
                    cnt += 1;
                }
                if (i < 6)
                {
                    Console.Write("╦");
                }

            }
            Console.Write("╗ \n");
        }

        private static void EmptyColumns(int maxWordSize)
        {
            Console.Write("║");
            string whitespaces = new string(' ', maxWordSize + 2);
            for (int i = 0; i < 7; i++)
            {
                Console.Write(whitespaces);
                Console.Write("║");
            }
            Console.Write("\n");
        }

        private static char GetWeekDayFlag(byte schedule, byte[] weekDays, int i)
        {
            if ((schedule & weekDays[i]) > 0)
            {
                return '✓';
            }
            else
            {
                return 'x';
  
            }
        }

        private static int ParceToInt(string value)
        {
            int number;
            bool success = int.TryParse(value, out number);
            if (success)
            {
                return number;
            }
            else
            {
                Console.WriteLine($"Attempted conversion of '{value ?? "<null>"}' failed.");
            }

            return number;
        }

        private static void EndDialog()
        {
            Console.Write("Press <Enter> to next... Any other key for exit ");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Environment.Exit(0);
            }
        }

        private static void BytePrint(byte schedule)
        {
            Console.WriteLine("schedule: {0,8}", Convert.ToString(schedule, 2));
        }

        private static byte SetWeekDay(byte schedule, byte[] weekDays, int index)
        {
            if ((index < 8) & (index >= 0))
            {
                schedule = (byte)(schedule | weekDays[index]);
            }
            else
            {
                PrintError("Invalid index for weekdays array");
            }
            return schedule;
        }

        

        private static void PrintError(string errorMSG)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMSG);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}

