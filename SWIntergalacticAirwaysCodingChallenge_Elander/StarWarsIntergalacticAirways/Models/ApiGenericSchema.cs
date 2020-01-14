using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsIntergalacticAirways.Models
{
    public class ApiGenericSchema<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<T> results {get;set;}
    }
}
