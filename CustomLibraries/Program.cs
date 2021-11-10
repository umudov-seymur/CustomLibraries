using System;
using System.Collections.Generic;
using CustomLibrary.Collections;

namespace CustomLibraries
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomList<string> popularCities = new CustomList<string>();

            popularCities.Add("New york");
            popularCities.Add("London");
            popularCities.Add("Baku");
            popularCities.Add("Istanbul");
            popularCities.Insert(2, "Sydney");

            Console.WriteLine(popularCities.Remove("Baku"));
            popularCities.RemoveAt(1);

            popularCities.AddRange(new string[] { "Berlin", "Logan", "Helena" });
            Console.WriteLine($"Helena index is: {popularCities.IndexOf("Helena")}");

            Console.WriteLine($"Baku in cities ? {popularCities.Contains("Baku")}");

            for (int i = 0; i < popularCities.Count; i++)
            {
                Console.WriteLine(popularCities[i]);
            }

            popularCities.Clear();

            Console.WriteLine(popularCities.Count);
        }
    }
}
