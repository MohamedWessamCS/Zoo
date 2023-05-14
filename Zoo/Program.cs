using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Program
    {
        static Stack<Menu> menuList = new Stack<Menu>();
        public static Stack<Menu> MenuList { get {  return menuList; } }
        
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            while(menuList.Count > 0)
            {
                zoo.OpenZooMenu();
            }
            Console.WriteLine("DEBUG");
        }
        public static void AddMenuToStack(Menu menu)
        {
            menuList.Push(menu);
        }
        public static Menu GetMenuFromStack()
        {
            return menuList.Count == 0 ? null : menuList.Pop();
        }
        public static void Exit()
        {
            Console.WriteLine("Thank you for visiting the zoo! We hope you enjoyed your stay there :D");
            Environment.Exit(0);
        }
    }
}
