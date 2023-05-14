using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Enclosure
    {
        Animal[] _animals;
        Menu _menu;
        string[] _strAnimals;
        public int EnclosureSize
        {
            get { return _animals.Length; }
        }
        public Enclosure()
        {
            _animals = new Animal[(int)InputManager.GetFloatInput("Enclosure Size")];
            _strAnimals = new string[EnclosureSize];
            Console.Write($"||\tSuccessfly created an enclosure of size {EnclosureSize}!\t||\n");
            EnclosureInitialization();
        }

        private void EnclosureInitialization()
        {
            CreateAnimalPrompt();
        }
        private void CreateAnimalPrompt()
        {
            for (int i = 0; i < EnclosureSize && CanCreate(); i++)
            {
                CreateAnimals();
            }
        }
        private bool CanCreate()
        {
            string userChoice = InputManager.GetStringInput("Y/N do you want to create an animal?");
            userChoice = userChoice.ToLower();
            switch (userChoice)
            {
                case ("y"):
                case ("yes"):
                    return true;
                default:
                    return false;
            }
        }
        private void CreateAnimals()
        {
            Animal animal = new Animal();
            AddAnimal(ref animal);
        }

        public void AddAnimal(ref Animal animal)
        {
            try
            {
                _animals[CheckEmptySpace()] = animal;
            }
            catch (InsufficientMemoryException)
            {
                ErrorHandler.NoSpaceErrorMessage();
            }
        }
        public void RemoveAnimal(ref Animal animal)
        {
            try
            {
                _animals[CheckForAnimal(ref animal)] = null;
            }
            catch (IndexOutOfRangeException)
            {
                ErrorHandler.NoAnimalReferenceErrorMessage();
            }
        }
        public void RemoveAnimal(string animalName)
        {
            try
            {
                _animals[CheckForAnimal(ref animalName)] = null;
            }
            catch (IndexOutOfRangeException)
            {
                ErrorHandler.NoAnimalReferenceErrorMessage();
            }
        }
        private int CheckEmptySpace()
        {
            for(int i = 0; i < _animals.Length; i++)
            {
                if (_animals[i] == null)
                    return i;
            }
            throw new InsufficientMemoryException("No space available");
        }
        private int CheckForAnimal(ref Animal animal)
        {
            for(int i = 0; i < _animals.Length; i++)
            {
                if (_animals[i] == animal)
                {
                    return i;
                }
            }
            return -1;
        }
        private int CheckForAnimal(ref string name)
        {
            for (int i = 0; i < _animals.Length; i++)
            {
                if (_animals[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        public virtual Animal AnimalMenu()
        {
            GetAnimals();
            _menu = new Menu(ref _strAnimals);
            Animal animal = ReturnAnimal();
            UpdateAll();
            return animal;
        }
        public virtual Animal ReturnAnimal()
        {
            int animalIndex = _menu.GetOption();
            if (animalIndex != _animals.Length)
                return _animals[animalIndex];
            return null;
        }
        private void GetAnimals()
        {
            for(int i = 0; i < _animals.Length; i++)
            {
                if (_animals[i] == null)
                    _strAnimals[i] = "None";
                else
                    _strAnimals[i] = _animals[i].Name + $"\tHunger = {_animals[i].Hunger} Thirst = {_animals[i].Thirst}";
            }
        }
        public void UpdateAll()
        {
            Enclosure enclosure = this;
            foreach (Animal animal in _animals)
            {
                animal?.Update(ref enclosure);
            }
        }
        public virtual int ShowAnimals()
        {
            int animalCount = 0;
            int i = 0;
            foreach (Animal animal in _animals)
            {
                if(animal != null)
                {
                    animalCount++;
                    Console.Write($"||{i} {animal.Name}\t{animal.Shape}\t Thirst:{animal.Thirst} Hunger:{animal.Hunger}||\n");
                    
                    i++;
                }
            }
            return animalCount;
        }
        public virtual void Print()
        {
            int animalCount = ShowAnimals();
            Console.Write($"||\tThis enclosure has {animalCount} / {_animals.Length} animals\t||\n");
        }
        public virtual Animal GetAnimal(int id)
        {
            return _animals[id];
        }
    }
}
