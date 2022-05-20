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
    class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var customer = new CustomerController();
            var result = customer.GetCustomer(0);
            //type of result is NotFound object
            Assert.That(result, Is.TypeOf<NotFound>());
            //type of result is NotFound object or one of its derivatives
            Assert.That(result, Is.InstanceOf<NotFound>());
        }
    }
}
