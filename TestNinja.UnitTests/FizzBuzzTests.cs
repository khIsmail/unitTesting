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
    class FizzBuzzTests
    {
       [Test]      
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz()
        {
           
            string result = FizzBuzz.GetOutput(15);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        [Test]
        public void GetOutput_InputIsDivisibleBy3_ReturnFizz()
        {
            string result = FizzBuzz.GetOutput(3);
            Assert.That(result, Is.EqualTo("Fizz"));
        }
        [Test]
        public void GetOutput_InputIsDivisibleBy5_ReturnBuzz()
        {
            ;
            string result = FizzBuzz.GetOutput(5);
            Assert.That(result, Is.EqualTo("Buzz"));
        }
        [Test]
        public void GetOutput_InputNotDiviblebyAny_ReturnInput()
        {
            
            string result = FizzBuzz.GetOutput(7);
            Assert.That(result, Is.EqualTo("7"));
        }
    }
}
