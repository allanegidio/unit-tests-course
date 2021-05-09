using FluentAssertions;
using UnitTestsCourse;
using Xunit;

namespace UnitTestsCourseTests
{
    public class FizzBuzzTests
    {
        [Fact]
        public void GetOutput_WhenCalled_ReturnFizzBuzz()
        {
            //Act
            var result = FizzBuzz.GetOutput(15);

            //Asserts
            result.Should().Be("FizzBuzz");
        }
    }
}