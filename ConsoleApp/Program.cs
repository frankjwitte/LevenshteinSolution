using System;
using LevenshteinLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
//            var source = "meilenstein";
//            var target = "levenshtein";
//            var source = "saturday";
//            var target = "sunday";
//            var source = "kitten";
//            var target = "sitting";
            var source = "drank";
            var target = "dr?nk";

            var calculator = new LevenshteinCalculator();
            var result = calculator.Calculate(source, target);
            
            Console.WriteLine($"The distance is {result.Distance}.");
            Console.WriteLine("Result:");
            Console.WriteLine(result);
        }
    }
}