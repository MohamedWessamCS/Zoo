using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal static class ErrorHandler
    {
        public static void ParsingErrorMessage()
        {
            Console.Write("Error, you must provide a valid number! Please try again.\n");
        }
        public static void WrongSpeciesErrorMessage(string species)
        {
            Console.Write($"Error, this animal is not a {species} so it can't be allowed in this enclosure!\n");
        }
        public static void NoSpaceErrorMessage()
        {
            Console.Write("Error, no space available in current enclosure!\n");
        }
        public static void NoAnimalReferenceErrorMessage()
        {
            Console.Write("Error, couldn't find animal in current enclosure!\n");
        }
        public static void InvalidInputErrorMessage()
        {
            Console.Write("Error, invalid input!\n");
        }
        public static void SameAnimalErrorMessage()
        {
            Console.Write("Error, can't perform operation on itself!\n");
        }
    }
}
