using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Animal
    {
        string _name;
        string _shape;
        string _gender;
        float _age = 0;
        float _thirst = 1;
        float _hunger = 1;
        static string[] _options;
        static Menu _menu;

        public string Name { get { return _name; } set { _name = value; } }
        public string Shape { get { return _shape; } }
        public string Gender { get { return _gender; }
            private set
            {
                string tempGender = value;
                if (tempGender.ToLower() != "male" && tempGender.ToLower() != "female")
                    _gender = "Male";
                else
                    _gender = value;
            } }
        public float Age { get { return _age; } }
        public float Thirst { get { return _thirst; }
            private set
            {
                _thirst = value;
                if (_thirst < 0)
                    _thirst = 0;
                if (_thirst > 1)
                    _thirst = 1;
            }
        }
        public float Hunger { get { return _hunger; }
            private set
            {
                _hunger = value;
                if (_hunger < 0)
                    _hunger = 0;
                if (_hunger > 1)
                    _hunger = 1;
            }
        }

        static Animal()
        {
            _options = new string[6] { "Play", "Feed with an Animal", "Feed with Food", "Breed", "Hydrate Animal", "Print" };
            _menu = new Menu(ref _options);
        }

        public Animal()
        {
            _name = InputManager.GetStringInput("Name");
            _shape = InputManager.GetStringInput("Shape");
            Gender = InputManager.GetStringInput("Gender");
            _age = InputManager.GetFloatInput("Age");
        }
        protected Animal(Animal parent1, Animal parent2)
        {
            _name = parent1.Name.Substring(0, parent1.Name.Length / 2) + parent2.Name.Substring(0, parent2.Name.Length / 2);
            Gender = parent1.Gender.ToLower() == "male" ? "Female" : "Male";
            _shape = parent2.Shape.Substring(0, parent2.Shape.Length / 2) + parent1.Shape.Substring(parent1.Shape.Length / 2);
        }
        public void Update(ref Enclosure enclosure)
        {
            AgeUp(0.1f);
            GetHungry(0.1f);
            GetThirsty(0.2f);
            if (_hunger <= 0 || _thirst <= 0)
            {
                Die();
                Animal animal = this;
                enclosure.RemoveAnimal(ref animal);
            }
        }

        ~Animal()
        {
            //Die(); 
        }
        public void AgeUp(float age)
        {
            _age += age;
        }
        public void GetHungry(float hunger)
        {
            _hunger -= hunger;
        }
        public void GetThirsty(float thirst)
        {
            _thirst -= thirst;
        }
        public void Die()
        {
            Console.WriteLine($"You couldn't take good care of {_name} :( it has died!");
        }


        public virtual void Print()
        {
            Console.Write($"\n|| Name => {_name}\t|| \n");
            Console.Write($"|| Age => {_age}\t|| \n");
            Console.Write($"|| Gender => {_gender}\t|| \n");
            Console.Write($"|| Thirst => {_thirst}\t|| \n");
            Console.Write($"|| Hunger => {_hunger}\t|| \n");
            Console.Write($"\n|| Shape\n " +
                $"||\t{_shape}\t|| \n");
        }
        public virtual void Play()
        {
            Console.Write($"||\tYou have played with {_name} :D\t||\n");
            Console.Write($"||\t<3<3<3 {_shape} <3<3<3<3\t||\n");
        }
        public virtual void Feed(float food)
        {
            Hunger += food / 100;
            Console.WriteLine($"||\t{_name} is happy! Thank you for feeding it :)\t||\n");
        }
        public virtual void Feed(Animal animal, ref Enclosure currentEnclosure)
        {
            if (animal != this)
            {
                Hunger += animal.Hunger;
                currentEnclosure.RemoveAnimal(ref animal);
                Console.WriteLine($"||\t You have killed {animal.Name} :( but {_name} is full and happy now :)\t||\n");
            }
            else
                ErrorHandler.SameAnimalErrorMessage();
        }
        public virtual void GiveWater(float water)
        {
            Thirst += water / 100;
            Console.WriteLine($"||\t{_name} is happy! Thank you for hydrating it :)\t||\n");
        }
        public Animal Breed(Animal animal2)
        {
            if (animal2 == null || this == null)
            {
                ErrorHandler.NoAnimalReferenceErrorMessage();
                return null;
            }
            if (animal2 != this && animal2.Gender != Gender)
            {
                Animal child = new Animal(this, animal2);
                Console.WriteLine($"||\t The miracle of birth! welcome your new born {child.Name}!\t||\n"+
                    $"||\t it looks just like its parents! {child.Shape}\t||\n");
                return child;
            }
            return null;
        }
        private int GetAnimalOption()
        {
            return _menu.GetOption();
        }
        public void AnimalOptions(ref Enclosure currentEnclosure)
        {
            int function = GetAnimalOption();
            switch (function)
            {
                case 0:
                    Play();
                    break;
                case 1:
                    Feed(GetAnimal(ref currentEnclosure), ref currentEnclosure);
                    break;
                case 2:
                    Feed(InputManager.GetFloatInput("Food"));
                    break;
                case 3:
                    Animal child = Breed(GetAnimal(ref currentEnclosure));
                    currentEnclosure.AddAnimal(ref child);
                    break;
                case 4:
                    GiveWater(InputManager.GetFloatInput("Water"));
                    break;
                case 5:
                    Print();
                    break;
            }
            _menu.GoBackToPreviousMenu();
        }
        private Animal GetAnimal(ref Enclosure currentEnclosure)
        {
            return currentEnclosure.AnimalMenu();
        }
    }

}
