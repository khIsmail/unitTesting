
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    //[TestClass]
    [TestFixture]
    public class ReservationTest
    {

       // [TestMethod]
       [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            //Arrange : we initialize the objects we want to test
            var reservation = new Reservation();
            //Act on the target behavior by covering the main thing to be tested
            //(calling a function or method, calling a REST API, or interacting with a web page).
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            //Assert : verify expected outcomes
           // Assert.IsTrue(result);
           // Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        //[TestMethod]
        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            var user = new User();
            var reservetion = new Reservation { MadeBy = user };
            var result = reservetion.CanBeCancelledBy(user);
            Assert.IsTrue(result);
        }

        // [TestMethod]
        [Test]
        public void CanBeCancelled_AnotherUserCancelling_ReturnsFalse()
        {
            var reservation = new Reservation { MadeBy = new User() };
            var result = reservation.CanBeCancelledBy(new User());
            //Assert.IsFalse(result);
            Assert.That(result, Is.False);
        }   
    }
}
