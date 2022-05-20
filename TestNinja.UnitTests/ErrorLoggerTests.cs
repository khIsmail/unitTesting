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
    class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();
            logger.Log("a");
            Assert.That(logger.LastError, Is.EqualTo("a"));
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowAnArgumentNullException(string Error) 
        {
            var logger = new ErrorLogger();
            //logger.Log(Error); this will throw an exception and block the test
            //we use a lambda expression
            Assert.That(() => logger.Log(Error), Throws.ArgumentNullException);
           //  Assert.That(() => logger.Log(Error), Throws.Exception.TypeOf<DivideByZeroException>());
        }
        //test methods that raise an event
        [Test]
        public void Log_ValidateError_RaiseErrorLogEvent()
        {
            var logger = new ErrorLogger();
            var id = Guid.Empty;
            logger.ErrorLogged += (sender, args) => { id = args; };
            logger.Log("a");
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }

    }
}
