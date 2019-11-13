namespace LevenshteinLibrary
{
    public class LevenshteinCalculator : ILevenshteinCalculator
    {
        public LevenshteinResult Calculate(string source, string target)
        {
            return new LevenshteinResult(source ?? string.Empty, target ?? string.Empty);
        }
    }
}