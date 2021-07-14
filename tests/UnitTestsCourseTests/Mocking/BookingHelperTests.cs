using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using UnitTestsCourse.Mocking;
using Xunit;

namespace UnitTestsCourseTests.Mocking
{
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _mockRepository;

        public BookingHelperTests()
        {
            _existingBooking = new Booking() { Id = 1, Status = "Active", ArrivalDate = new DateTime(2021, 06, 23, 6, 15, 00), DepartureDate = new DateTime(2021, 06, 26, 15, 50, 00), Reference = "Booking 1" };

            _mockRepository = new Mock<IBookingRepository>();
            _mockRepository.Setup(r => r.GetActiveBookings(2)).Returns(GetExistingBookings());
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate, days: 1),
                Reference = "Booking 2"
            }, _mockRepository.Object);

            result.Should().BeEmpty("Because no one booking returned from method.");
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesIntTheMiddleAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.ArrivalDate, days: 1),
                Reference = "Booking 2"
            }, _mockRepository.Object);

            result.Should().Be(_existingBooking.Reference);
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 1),
                Reference = "Booking 2"
            }, _mockRepository.Object);

            result.Should().Be(_existingBooking.Reference);
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesInTheMiddleAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = Before(_existingBooking.DepartureDate, days: 1),
                Reference = "Booking 2"
            }, _mockRepository.Object);

            result.Should().Be(_existingBooking.Reference);
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingStartsInTheMiddleAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 1),
                Reference = "Booking 2"
            }, _mockRepository.Object);

            result.Should().Be(_existingBooking.Reference);
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.DepartureDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2),
                Reference = "Booking 2"
            }, _mockRepository.Object);

            result.Should().BeEmpty("Because no one booking returned from method.");
        }

        [Fact]
        public void OverlappingBookingsExist_WhenBookingOverlapButNewBookingIsCancelled_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.ArrivalDate, days: 1),
                Reference = "Booking 2",
                Status = "Cancelled"
            }, _mockRepository.Object);

            result.Should().BeEmpty("Because the new booking is cancelled.");
        }

        private DateTime Before(DateTime arrivalDate, int days)
            => arrivalDate.AddDays(-days);

        private DateTime After(DateTime departureDate, int days)
            => departureDate.AddDays(days);

        private IQueryable<Booking> GetExistingBookings()
            => new List<Booking>() { _existingBooking }.AsQueryable();
    }
}