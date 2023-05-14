using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Menu
    {
        string[] _options;
        string _menu;
        public Menu(ref string[] options)
        {
            _options = options;
            FunctionMenu();
        }
        public Menu(string commonOptionName,int lengthOfNumberedOptions)
        {
            AddNumberedOptions(commonOptionName,lengthOfNumberedOptions);
            FunctionMenu();
        }
        public int GetOption()
        {
            AddMenuToStack();
            int function;
            do
            {
                Console.Write(_menu);
            }
            while (!IsValidFunction(out function));
            CheckExit(ref function);
            return function;
        }// Return an int to be used for switch case
        private bool IsValidFunction(out int function)
        {
            function = (int)InputManager.GetFloatInput("Function");
            if (function > _options.Length || function < 0)
            {
                ErrorHandler.InvalidInputErrorMessage();
                return false;
            }
            return true;
        }
        private void FunctionMenu()
        {       // Create a menu using options provided
            for (int i = 0; i < _options.Length; i++)
            {
                _menu += $"||{i}:\t{_options[i]}\t||\n";
            }
            _menu += $"||{_options.Length}:\tQuit\t||\n";

        }
        private void AddMenuToStack()
        {
            if(!Program.MenuList.Contains(this))
                Program.AddMenuToStack(this);
        }
        public void GoBackToPreviousMenu()
        {
            Menu previousMenu = Program.GetMenuFromStack();
            if (previousMenu == null)
            {
                Program.Exit();
            }
        }
        public void AddNumberedOptions(string nameOfCommonOption, int length)
        {
            _options = new string[length];
            for (int i = 0; i  < length; i++)
            {
                _options[i] = nameOfCommonOption + i;
            }
        }
        private void CheckExit(ref int function)
        {
            if (function == _options.Length)
            {
                Program.GetMenuFromStack();
            }
        }
    }
}
