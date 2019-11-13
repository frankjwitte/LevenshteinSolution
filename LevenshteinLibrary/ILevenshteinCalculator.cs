using System.Text;

namespace LevenshteinLibrary
{
    public interface ILevenshteinCalculator
    {
        LevenshteinResult Calculate(string source, string target);
    }
}