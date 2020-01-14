using StarWarsIntergalacticAirways.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsIntergalacticAirways
{
    public class FindingStarshipService
    {
        //Hard coded for now. Ideally this would be setup in the startup services
        private IStarWarsAPI starWarsAPI = new StarWarsAPI();
        public FindingStarshipService()
        {

        }
        public void FindStarshipForClient()
        {
            var numberOfPassengers = 0;
            Console.WriteLine("How many passengers do you have?");
            bool inputWasANumber = int.TryParse(Console.ReadLine(), out numberOfPassengers);
            if (inputWasANumber && numberOfPassengers >= 0)
            {
                Console.WriteLine("Processing request...");
                //function call with number of passengers to find the compatible ships and drivers
                var possibleShips = GetPossibleShips(numberOfPassengers);
                if(possibleShips != null && possibleShips.Count > 0)
                {
                    Console.WriteLine("Ships that meet the desired paramters of " + numberOfPassengers + " passengers are: ");
                    DisplayItemsToConsole(possibleShips);
                }
                else
                {
                    Console.WriteLine("Sorry, no ships could hold " + numberOfPassengers + ".");
                }
                //Check if the client wishes to search more
                SearchAgainCheck();


            }
            else
            {
                Console.WriteLine("Invalid response, please type the number of passengers.");
                FindStarshipForClient();
                
            }


        }

        //Lets the user have the option to search again
        public void SearchAgainCheck()
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Would you like to search again? Press 'Y' to search again." + Environment.NewLine + "Or press any other key to exit.");
            var continueInput = Console.ReadLine();
            if (continueInput == "Y" || continueInput == "y")
            {
                //Re-call the starship searching service
                FindStarshipForClient();
            }
            else
            {
                Console.WriteLine("Goodbye! We look forward to doing business with you again.");
            }
        }

        //Generic function to loop and display from a list of strings
        public void DisplayItemsToConsole(List<string> itemsToDisplayInConsole)
        {
            foreach (var items in itemsToDisplayInConsole)
            {
                Console.WriteLine(items);
            }
        }

        //Calls the API interface to get the list of all ships from the API then checks paramaters
        public List<string> GetPossibleShips(int numberOfPassengers)
        {
            List<String> shipPilotLogDetails = new List<string>();
            //Call to get the data from the api
            var ships = starWarsAPI.GetAllStarships();
            //use linq to find the ships that have the right passenger size
            var matchingShips = ships.Where(x => (int.TryParse(x.passengers, out int outParam) ? outParam : 0) >= numberOfPassengers);
            matchingShips = matchingShips.Where(x=>x.pilots != null && x.pilots.Length != 0);
            //This is a little costly, with more time consider more efficient/faster ways
            foreach(var ship in matchingShips)
            {
                foreach (var pilot in ship.pilots)
                {
                    var pilotInfo = starWarsAPI.GetPilot(pilot);
                    shipPilotLogDetails.Add(ship.name + " - " + pilotInfo.name);
                }
                
            }
            return shipPilotLogDetails;
        }
    }
}
