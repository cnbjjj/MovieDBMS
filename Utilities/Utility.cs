using System.Text.RegularExpressions;

namespace Utilities
{
    public static class Utility
    {
        const string WARNING = "Invalid input. Please try again!";
        const string QUIT = "quit";

        // Inputs
        public static int ReadInputAsInt(string question, string quit = QUIT, string waring = WARNING, int min = int.MinValue, int max = int.MaxValue)
        {
            string res = ReadInputAsString(question, "", waring, str => int.TryParse(str, out int num) && num >= min && num <= max);
            if (res == quit || res == "") return int.MinValue;
            return int.Parse(res);
        }

        public static decimal ReadInputAsDecimal(string question, string quit = QUIT, string warning = WARNING, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            string res = ReadInputAsString(question, "", warning, str => decimal.TryParse(str, out decimal num) && num >= min && num <= max);
            if (res == quit || res == "") return decimal.MinValue;
            return decimal.Parse(res);
        }

        public static DateTime ReadInputAsDate(string question, string quit = QUIT, string warning = WARNING)
        {
            string res = ReadInputAsString(question, "", warning, str => DateTime.TryParse(str, out DateTime date));
            if (res == quit || res == "") return DateTime.MinValue;
            return DateTime.Parse(res);
        }

        public static string ReadInputAsString(string question, string quit = QUIT, string warning = WARNING, Func<string, bool> validator = null, Func<string, string> Prompt = null)
        {
            if (validator == null) validator = (str) => !string.IsNullOrEmpty(str);
            if (Prompt == null) Prompt = ReadRawInput;
            string str = Prompt(question).ToLower();
            while (str != quit && !validator(str))
            {
                Console.WriteLine(warning);
                str = Prompt(question).ToLower();
            }
            return str;
        }

        public static string ReadRawInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        // Outputs
        public static void PrintLn(string message)
        {
            Console.WriteLine(message);
        }

        public static void Print(string message)
        {
            Console.Write(message);
        }

        public static void PrintTitle(string title)
        {
            PrintLn($"\n\n -------------------------------------{title}-----------------------------------------------------\n");
        }

        public static void PrintEnd()
        {
            PrintLn("\n -------------------------------------======-----------------------------------------------------\n");
        }

        public static void PrintMessageBlock(string title, string message)
        {
            PrintTitle(title);
            PrintLn($"\n {message}");
            PrintEnd();
        }

        public static string[] Split(string str, string delimeter)
        {
            return Regex.Split(str, delimeter);
        }
    }
}
