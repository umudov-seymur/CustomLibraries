using CustomLibrary.Collections;
using System;

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
            popularCities.Add("Baku");

            Console.WriteLine(popularCities.Remove("Baku"));
            popularCities.RemoveAt(1);

            popularCities.AddRange(new string[] { "Berlin", "Logan", "Helena" });
            Console.WriteLine($"Helena index is: {popularCities.IndexOf("Helena")}");

            Console.WriteLine($"Baku in cities ? {popularCities.Contains("Baku")}");

            var findedItems = popularCities.FindAll(city => city.Contains("Baku"));

            foreach (var item in findedItems)
            {
                Console.WriteLine(item);
            }

            popularCities.Clear();

            Console.WriteLine(popularCities.Count);
        }
    }
}
