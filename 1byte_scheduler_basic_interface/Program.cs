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
            const ushort PWD_MASK = 0x1F2F;



            Console.ForegroundColor = ConsoleColor.Cyan;

            string[] weekDaysNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string userSecret = "";


            while (true)
            {
                byte[] weekDays = new byte[8];
                int maxWordSize = 10;

                weekDays[0] = NUL_MASK;
                weekDays[1] = MON_MASK;
                weekDays[2] = TUE_MASK;
                weekDays[3] = WED_MASK;
                weekDays[4] = THU_MASK;
                weekDays[5] = FRI_MASK;
                weekDays[6] = SAT_MASK;
                weekDays[7] = SUN_MASK;

                printArt();

                FirstDialog(ref schedule, PWD_MASK, weekDaysNames, ref userSecret, weekDays);

                EndDialog();

                Console.Clear();

                printArt();

                HeaderColumns(weekDaysNames, maxWordSize);

                EmptyColumns(maxWordSize);

                FilledColumns(schedule, weekDays, maxWordSize);

                EmptyColumns(maxWordSize);

                BottomColumns(maxWordSize);

                MoreOptionsMenu(ref schedule, NUL_MASK, PWD_MASK, ref userSecret);

                EndDialog();

                Console.Clear();
            }
        }

        private static void FirstDialog(ref byte schedule, ushort PWD_MASK, string[] weekDaysNames, ref string userSecret, byte[] weekDays)
        {
            int userInputInt = 1;
            Console.WriteLine("Select option:\n1. Show scheduller \n2. Change scheduler \n3. Set password\n 0. Exit");
            string userInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                userInputInt = ParceToInt(userInput);
            }
            else
            {
                PrintError("Empty or null input!");
                userInputInt = 1;
            }

            if ((userInputInt == 1) | (userInputInt == 2) | (userInputInt == 3) | (userInputInt == 0))

            {
                if (userInputInt == 2)
                {
                    Console.Clear();
                    schedule = GetInput(schedule, weekDaysNames, weekDays);
                }

                if (userInputInt == 3)
                {
                    Console.Clear();
                    userSecret = GetAndCryptPassword(PWD_MASK);
                }

                if (userInputInt == 0)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                PrintError("Incorrect option!");
            }
        }

        private static void MoreOptionsMenu(ref byte schedule, byte NUL_MASK, ushort PWD_MASK, ref string userSecret)
        {
            Console.WriteLine("Choose next option: \n 1.Return \n 2. Set password \n 3. Clear scheduler (Password needed)");
            string optionsDialog = Console.ReadLine();
            int optionsDialogInt = 1;

            if (!string.IsNullOrWhiteSpace(optionsDialog))
            {
                optionsDialogInt = ParceToInt(optionsDialog);
            }
            else
            {
                PrintError("Empty or null input!");
                optionsDialogInt = 1;
            }

            if ((optionsDialogInt == 1) | (optionsDialogInt == 2) | (optionsDialogInt == 3))

            {
                if (optionsDialogInt == 2)
                {
                    userSecret = GetAndCryptPassword(PWD_MASK);
                }

                if (optionsDialogInt == 3)
                {
                    schedule = ClearScheduler(schedule, NUL_MASK, PWD_MASK, userSecret);
                }
            }
            else
            {
                PrintError("Incorrect option!");
            }
        }

        private static byte ClearScheduler(byte schedule, byte NUL_MASK, ushort PWD_MASK, string userSecret)
        {
            if (userSecret == "")
            {
                Console.WriteLine("Set the password first");
            }
            else
            {
                Console.WriteLine("Input password for clear Scheduller:");
                string tryPassword = Console.ReadLine();
                if (tryPassword == Crypt(PWD_MASK, userSecret))
                {
                    schedule = NUL_MASK;
                    Console.WriteLine("Schedule cleared!");
                }
                else
                {
                    PrintError("Incorrect password");
                }
            }

            return schedule;
        }

        private static string GetAndCryptPassword(ushort PWD_MASK)
        {
            string userSecret;
            Console.WriteLine("Input password:");
            string userPasswordUnmasked = Console.ReadLine();
            userSecret = Crypt(PWD_MASK, userPasswordUnmasked);
            Console.WriteLine("Password saved");
            return userSecret;
        }

        private static string Crypt(ushort PWD_MASK, string password)
        {
            string userPasswordMasked = "";
            for (int i = 0; i < password.Length; i++)
            {
                userPasswordMasked = userPasswordMasked + (char)((ushort)password[i] ^ PWD_MASK);
            }
            return userPasswordMasked;
        }

        private static byte GetInput(byte schedule, string[] weekDaysNames, byte[] weekDays)
        {
            printArt();

            Console.WriteLine("Select the weekday for schedule the training \n !!! Input number only !!! ");
            Console.WriteLine("\n1.{0}\n2.{1}\n3.{2}\n4.{3}\n5.{4}\n6.{5}\n7.{6}", weekDaysNames);

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
            return schedule;
        }

        private static void printArt()
        {
            Console.WriteLine(
@" o   \ o /  _ o         __|    \ /     |__        o _  \ o /   o
/|\    |     /\   ___\o   \o    |    o/    o/__   /\     |    /|\
/ \   / \   | \  /)  |    ( \  /o\  / )    |  (\  / |   / \   / \ " + "\n");
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
               // Console.WriteLine($"Attempted conversion of '{value ?? "<null>"}' failed.");
            }

            return number;
        }

        private static void EndDialog()
        {
            Console.Write("Press <Esc> to exit... Any other key for continue");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
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
                PrintError("Invalid weekdays number");
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

