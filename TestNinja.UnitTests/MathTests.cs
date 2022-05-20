using NUnit.Framework;
using TestNinja.Fundamentals;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }
        [Test]
        [Ignore("Because I want it to!")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {          
            var result = _math.Add(1, 2);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a ,int b ,int expectedResult)
        {
            var result = _math.Max(a, b);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        //testing a method that returns a collection
        [Test]
        public void GetOddNumbers_LimitIsGreatorThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);
           /* Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));*/

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 })); // make sure that the result is an array containing these 3 elements 
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
       /* [Test]
        public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        {
            var result = _math.Max(1, 2);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        {
            var result = _math.Max(1, 1);
            Assert.That(result, Is.EqualTo(1));
        }*/
    }
}
