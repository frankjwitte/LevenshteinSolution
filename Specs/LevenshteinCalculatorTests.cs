using LevenshteinLibrary;
using Xunit;
using Xunit.Abstractions;

namespace Specs
{
    public class LevenshteinCalculatorTests
    {
        private readonly ILevenshteinCalculator _sut;

        public LevenshteinCalculatorTests()
        {
            _sut = new LevenshteinCalculator();
        }

        [Theory]
        [InlineData(null, null, 0)]
        [InlineData("", "target", 6)]
        [InlineData("source", "", 6)]
        [InlineData("source", "target", 5)]
        [InlineData("kitten", "sitting", 3)]
        [InlineData("sunday", "sunday", 0)]
        [InlineData("saturday", "sunday", 3)]
        [InlineData("bright", "bright", 0)]
        [InlineData("bright", "freight", 2)]
        [InlineData("bright", "sleight", 3)]
        [InlineData("bright", "bride", 3)]
        [InlineData("bright", "plight", 2)]
        [InlineData("bright", "pride", 4)]
        [InlineData("drink", "drunk", 1)]
        [InlineData("drink", "dr?nk", 0)]
        [InlineData("drink", "dr?ck", 1)]
        [InlineData("drink", "d?k", 2)]
        [InlineData("drank", "d*k", 0)]
        [InlineData("drank", "dran*k", 0)]
        [InlineData("dran*k", "dran*k", 0)]
        [InlineData("dran*k", "drank", 0)]
        [InlineData("drank", "d*rk", 1)]
        [InlineData("drank", "dr*k", 0)]
        [InlineData("blank", "d*k", 1)]
        [InlineData("doctor", "ra*or", 2)]
        [InlineData("kermit", "gonzo", 6)]
        [InlineData("kermit", "t*", 1)]
        [InlineData("meilenstein", "levenshtein", 4)]
        public void ExamplesWithExpectedResult(string source, string target, int expectedResult)
        {
            Assert.Equal(expectedResult, _sut.Calculate(source, target).Distance);
        }
    }
}