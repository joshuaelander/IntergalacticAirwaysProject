using StarWarsIntergalacticAirways.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsIntergalacticAirways.Data
{
    public interface IStarWarsAPI
    {
        People GetPilot(string url);
        List<Starship> GetAllStarships();
    }

}
