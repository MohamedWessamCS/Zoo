using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal static class InputManager
    {
        public static string GetStringInput(string prompt)
        {
            ShowInputText(ref prompt);
            return Console.ReadLine();
        }
        public static float GetFloatInput(string prompt)
        {
            ShowInputText(ref prompt);
            return ParseFloat(ref prompt);
        }
        private static float ParseFloat(ref string prompt)
        {
            float value;
            while (!float.TryParse(Console.ReadLine(), out value))
            {
                ErrorHandler.ParsingErrorMessage();
                ShowInputText(ref prompt);
            }
            return value;
        }
        private static void ShowInputText(ref string prompt)
        {
            Console.Write($"|| {prompt} =>\t");
        }
    }
}
