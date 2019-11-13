using System;
using LevenshteinLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
//            var source = "saturday";
//            var target = "sunday";
            var source = "meilenstein";
            var target = "levenshtein";

            var calculator = new LevenshteinCalculator();
            var result = calculator.Calculate(source, target);

            Console.WriteLine($"The distance between '{source}' and '{target}' is {result.Distance}.");
            Console.WriteLine();
            Console.WriteLine("Path:");
            Console.WriteLine();
            Console.WriteLine(result);
        }
    }
}