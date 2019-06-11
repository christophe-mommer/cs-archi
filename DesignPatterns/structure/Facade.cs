using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    struct Coordinates
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
    class GeoLookupService
    {
        public string GetCityForZipCode(string zipCode) { return "Paris"; }
        public Coordinates GetCoordinatesForZipCode(string zipCode)
        {
            return new Coordinates
            {
                Latitude = 48.866667m,
                Longitude = 2.333333m
            };
        }
        public string GetStateForZipCode(string zipCode) { return "Ile-de-France"};
    }
    class WeatherService
    {
        public decimal GetTemperature(Coordinates coordinates, DateTime date)
        {
            return 15.0m;
        }
    }
    class Facade
    {
        public void DisplayTemperature()
        {
            var geoLookup = new GeoLookupService();
            var weatherService = new WeatherService();
            var zipCode = "75000";
            var city = geoLookup.GetCityForZipCode(zipCode);
            var state = geoLookup.GetStateForZipCode(zipCode);
            var coordinates = geoLookup.GetCoordinatesForZipCode(zipCode);

            var weather = weatherService.GetTemperature(coordinates, DateTime.Today);

            Console.WriteLine($"At {city}, in {state} state, it's currently {weather}°");
        }
    }
}
