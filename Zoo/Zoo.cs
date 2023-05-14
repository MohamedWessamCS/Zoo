using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Zoo
    {
        Enclosure[] _enclosures;
        public static Animal[] DefaultAnimals = new Animal[3];
        private Menu _zooMenu;

        public Zoo()
        {
            _enclosures = new Enclosure[(int)InputManager.GetFloatInput("Enclosure Count")];
            InitializeEnclosures();
            InitializeZooMenu();
            OpenZooMenu();
        }

        private void InitializeEnclosures()
        {
            for(int i = 0; i < _enclosures.Length; i++)
            {
                _enclosures[i] = CreateEnclosure();
                if (_enclosures[i] == null)
                {
                    break;
                }
            }
        }
        private Enclosure CreateEnclosure()
        {
            return new Enclosure();
        }
        private void InitializeZooMenu()
        {
            if (_zooMenu == null) 
            {
                _zooMenu = new Menu("Enclosures", _enclosures.Length);
            }
        }
        public void OpenZooMenu()
        {
            StartEnclosureMenu(GetEnclosureFromMenu(ref _zooMenu));
        }
        private int GetEnclosureFromMenu(ref Menu enclosureMenu)
        {
            return enclosureMenu.GetOption();
        }

        private void StartEnclosureMenu(int function)
        {
            if(function == _enclosures.Length)
                _zooMenu.GoBackToPreviousMenu();
            else
                _enclosures[function].AnimalMenu()?.AnimalOptions(ref _enclosures[function]);
        }

    }
}
