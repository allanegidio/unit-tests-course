using FluentAssertions;
using UnitTestsCourse;
using Xunit;

namespace UnitTestsCourseTests
{
    public class FizzBuzzTests
    {
        [Theory]
        [InlineData(465, "FizzBuzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(9, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(1, "1")]

        public void GetOutput_WhenCalled_ReturnFizzBuzz(int number, string expected)
        {
            //Act
            var result = FizzBuzz.GetOutput(number);

            //Asserts
            result.Should().Be(expected);
        }
    }
}