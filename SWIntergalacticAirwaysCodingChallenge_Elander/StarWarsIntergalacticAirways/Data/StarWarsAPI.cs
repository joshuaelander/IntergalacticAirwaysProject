using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using StarWarsIntergalacticAirways.Models;

namespace StarWarsIntergalacticAirways.Data
{

    //class to hold the API calls
    public class StarWarsAPI : IStarWarsAPI
    {
        private static string apiBaseUrl = "https://swapi.co/api/";
        public People GetPilot(string url)
        {
            return GetItemByUrl<People>(url);
        }
        private T GetItemByUrl<T>(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse itemResponse = client.Execute(request);
            T item = RestSharp.SimpleJson.DeserializeObject<T>(itemResponse.Content);
            return item;
        }
        public List<Starship> GetAllStarships()
        {
            return GetAll<Starship>("starships/");
        }
        private List<T> GetAll<T>(string apiUrlSegment, int pageNumber = 1)
        {
            var localPageNumber = pageNumber;
            var pageUrl = "?page=" + localPageNumber;
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest(apiUrlSegment + pageUrl, Method.GET);
            IRestResponse arrayOfItems = client.Execute(request);
            ApiGenericSchema<T> allItems = RestSharp.SimpleJson.DeserializeObject<ApiGenericSchema<T>>(arrayOfItems.Content);
            List<T> listOfAllItems = allItems.results;
            if(allItems.next != null)
            {
                localPageNumber+=1;
                listOfAllItems.AddRange(GetAll<T>(apiUrlSegment, localPageNumber));
            }
            return listOfAllItems;
        }
    }
}
