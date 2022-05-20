using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class BookingHelper_OverlappingBookingsExistTests
    {
        private Booking _existingbooking;
        private Mock<IBookingRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingbooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };
            _repository = new Mock<IBookingRepository>();
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
               _existingbooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate =Before(_existingbooking.ArrivalDate,days: 2) ,
                DepartureDate = Before(_existingbooking.ArrivalDate),
            }, _repository.Object);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingRepository()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.ArrivalDate),
            }, _repository.Object);
            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingRepository()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.DepartureDate),
            }, _repository.Object);
            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingRepository()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.ArrivalDate),
                DepartureDate = Before(_existingbooking.DepartureDate),
            }, _repository.Object);
            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleOfAnexitingBookingButFinishesAfter_ReturnExistingBookingRepository()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.DepartureDate),
            }, _repository.Object);
            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEpmtyString()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.DepartureDate),
                DepartureDate = After(_existingbooking.DepartureDate, days:2),
            }, _repository.Object);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.DepartureDate, days: 2),
                Status = "Cancelled"
            }, _repository.Object);
            Assert.That(result, Is.Empty);
        }
        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month,int day)
        {
            return new DateTime(year, month, day);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }
    }
}
