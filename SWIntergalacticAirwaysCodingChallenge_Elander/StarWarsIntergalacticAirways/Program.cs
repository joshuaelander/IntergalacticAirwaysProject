using StarWarsIntergalacticAirways.Data;
using System;
using System.Linq;

namespace StarWarsIntergalacticAirways
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Intergalactic Airways");
            //Calls the main functionality of the program to get a list of ships for passenger requirements
            var starWarsIntergalacticAirwaysProgram = new FindingStarshipService();
            starWarsIntergalacticAirwaysProgram.FindStarshipForClient();
        }


    }
}
