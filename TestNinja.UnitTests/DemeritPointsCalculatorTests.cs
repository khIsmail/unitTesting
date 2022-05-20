using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class DemeritPointsCalculatorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(350)]
        public void CalculateDemeritPoints_SpeedIsNegativeOrGreatorThanMaxSpeed_ThrowArgumentOutOfRangeException(int speed)
        {
            var calcul = new DemeritPointsCalculator();
            Assert.That(() => calcul.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]

        [TestCase(0, 0)]
        [TestCase(65, 0)]
        [TestCase(1, 0)]
        public void CalculateDemeritPoints_SpeedIsLessThanOrEqualToSpeedLimit_ReturnZero(int speed,int result)
        {
            var calcul = new DemeritPointsCalculator();
             
            Assert.That(calcul.CalculateDemeritPoints(speed), Is.EqualTo(result));
        }


        [Test]
        public void CalculateDemeritPoints_SpeedIsBetweenMaxAndLimit_ReturndemeritPoints()
        {
            var calcul = new DemeritPointsCalculator();
            var result = calcul.CalculateDemeritPoints(100);
            Assert.That(result, Is.EqualTo(7));
        }

    }
}
