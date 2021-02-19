using System;

namespace Inlämning_Crud
{
    internal static class Utilitys
    {
        /// <summary>
        /// Sriver ut strängar i rött
        /// </summary>
        /// <param name="input"></param>
        public static void PrintRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("" + input);
            Console.ResetColor();
        }

        /// <summary>
        /// Sriver ut strängar i Grönt
        /// </summary>
        /// <param name="input"></param>
        public static void PrintGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("" + input);
            Console.ResetColor();
        }

        /// <summary>
        /// Sriver ut strängar i Blått
        /// </summary>
        /// <param name="input"></param>
        public static void PrintDarcyan(string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("" + input);
            Console.ResetColor();
        }

        /// <summary>
        /// Sriver ut strängar i gult
        /// </summary>
        /// <param name="input"></param>
        public static void PrintYellow(string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("" + input);
            Console.ResetColor();
        }

        /// <summary>
        /// Ascii art logo för huvudmenyn
        /// </summary>
        public static void LogoMenu()
        {
            PrintDarcyan(@"    ____        __        __
   / __ \____ _/ /_____ _/ /_  ____ _________
  / / / / __ `/ __/ __ `/ __ \/ __ `/ ___/ _ \
 / /_/ / /_/ / /_/ /_/ / /_/ / /_/ (__  )  __/
/_____/\__,_/\__/\__,_/_.___/\__,_/____/\___/
");
        }

        /// <summary>
        /// Ascii art logo för Addperson
        /// </summary>
        public static void LogoAddPerson()
        {
            Utilitys.PrintGreen(@"
    ___       __    __
   /   | ____/ /___/ /  ____  ___  ______________  ____
  / /| |/ __  / __  /  / __ \/ _ \/ ___/ ___/ __ \/ __ \
 / ___ / /_/ / /_/ /  / /_/ /  __/ /  (__  ) /_/ / / / /
/_/  |_\__,_/\__,_/  / .___/\___/_/  /____/\____/_/ /_/
                    /_/

");
        }

        /// <summary>
        /// Ascii art logo för sök
        /// </summary>
        public static void LogoSearch()
        {
            Utilitys.PrintYellow(@"
   _____                      __
  / ___/___  ____ ___________/ /_
  \__ \/ _ \/ __ `/ ___/ ___/ __ \
 ___/ /  __/ /_/ / /  / /__/ / / /
/____/\___/\__,_/_/   \___/_/ /_/
");
        }

        /// <summary>
        /// Ascii art logo för update
        /// </summary>
        public static void LogoUpdate()
        {
            PrintDarcyan(@"
   __  __          __      __       __
  / / / /___  ____/ /___ _/ /____  / /
 / / / / __ \/ __  / __ `/ __/ _ \/ /
/ /_/ / /_/ / /_/ / /_/ / /_/  __/_/
\____/ .___/\__,_/\__,_/\__/\___(_)
    /_/
");
        }

        /// <summary>
        /// Ascii art logo för delete
        /// </summary>
        public static void LogoDelete()
        {
            PrintRed(@"
    ____       __     __       __
   / __ \___  / /__  / /____  / /
  / / / / _ \/ / _ \/ __/ _ \/ /
 / /_/ /  __/ /  __/ /_/  __/_/
/_____/\___/_/\___/\__/\___(_)
");
        }
    }
}