using System;
using FluentAssertions;
using UnitTestsCourse;
using Xunit;

namespace UnitTestsCourseTests
{
    public class DemeteritPointCalculatorTests
    {
        [Fact]
        public void CalculateDemeritPoints_SpeedLessThanZero_ReturnArgumentOutOfRangeException()
        {
            var calculator = new DemeritPointsCalculator();

            Action act = () => calculator.CalculateDemeritPoints(-5);

            act.Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CalculateDemeritPoints_SpeedEqualsToSpeedLimit_ReturnZero()
        {
            var calculator = new DemeritPointsCalculator();

            var act = calculator.CalculateDemeritPoints(65);

            act.Should().Be(0);
        }

        [Fact]
        public void CalculateDemeritPoints_SpeedGrater15KmThanSpeedLimit_Return3DemeritPoints()
        {
            var calculator = new DemeritPointsCalculator();

            var act = calculator.CalculateDemeritPoints(80);

            act.Should().Be(3);
        }
    }
}