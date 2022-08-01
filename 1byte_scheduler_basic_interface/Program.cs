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

            const byte MON_MASK = 0b00000001;
            const byte TUE_MASK = 0b00000010;
            const byte WED_MASK = 0b00000100;
            const byte THU_MASK = 0b00001000;
            const byte FRI_MASK = 0b00010000;
            const byte SAT_MASK = 0b00100000;
            const byte SUN_MASK = 0b01000000;

            byte[] weekDays = new byte[7];

            weekDays[0] = MON_MASK;
            weekDays[1] = TUE_MASK;
            weekDays[2] = WED_MASK;
            weekDays[3] = THU_MASK;
            weekDays[4] = FRI_MASK;
            weekDays[5] = SAT_MASK;
            weekDays[6] = SUN_MASK;

            int index = 0;

            string leftCornerBox = "";

            int weekDayIndexInt = 0;

            Console.WriteLine("Select the weekday for schedule the training \n Input number only! \n1.Monday\n2.Tuesday\n3.Wednesday\n4.Thursday\n5.Friday\nSaturday\nSunday");
            
            string weekDayIndexStr = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(weekDayIndexStr))
            {
                weekDayIndexInt = ParceToInt(weekDayIndexStr);
            }
            else
            {
                PrintError("Empty or null input!");
            }

            schedule = SetWeekDay(schedule, weekDays, 0);


            BytePrint(schedule);

            EndDialog();
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
            if ((index < 8) & (index > 0))
            {
                index--;
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
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

