using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CabInvoiceGenerator;
using System.Collections.Generic;

namespace CabInvoiceGenerator
{
    [TestClass]
    public class UnitTest1
    {
        CabInvoiceGenerator cabInvoiceGenerator = null;
        private object generateNormalFare;

        [TestMethod]
        //Created The Generate Cab Invoice Class For Calculating Fares UC1
        public void GivenDistanceAndTimeShouldRetuenTotalFare()
        {
            //creating instance of InvoiceGenerator for Normal ride
            cabInvoiceGenerator = new CabInvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;
            //calculating fare 
            double fare = cabInvoiceGenerator.CalculateFare(time, distance);
            double expected = 25;
            //Asserting value
            Assert.AreEqual(expected, fare);
        }
        // Given Invalid Time And Distance Return Custom Exception
        [TestMethod]
        [TestCategory("Calculate Fare")]
        public void GivenInvalidTimeAndDistanceReturnCustomException()
        {
            var invalidTimeException = Assert.ThrowsException<CabInvoiceException>(() => generateNormalFare.CalculateFare(0, 5));
            Assert.AreEqual(CabInvoiceException.ExceptionType.INVALID_TIME, invalidTimeException.exceptionType);
            var invalidDistanceException = Assert.ThrowsException<CabInvoiceException>(() => generateNormalFare.CalculateFare(12, -1));
            Assert.AreEqual(CabInvoiceException.ExceptionType.INVALID_DISTANCE, invalidDistanceException.exceptionType);
        }
        // TC2.1, 3.1 - Given multiple rides should return invoice summary
        [TestMethod]
        [TestCategory("Multiple Rides")]
        public void GivenMultipleRidesReturnAggregateFare()
        {
            Ride[] cabRides = { new Ride(10, 15), new Ride(10, 15) };
            InvoiceSummary expected = new InvoiceSummary(cabRides.Length, 320);
            var actual = generateNormalFare.CalculateAggregateFare(cabRides);

            Assert.AreEqual(actual, expected);
        }

        // TC2.2 - given no rides return custom exception
        [TestMethod]
        [TestCategory("Multiple Rides")]
        public void GivenNoRidesReturnCustomException()
        {
            Ride[] cabRides = { };
            var nullRidesException = Assert.ThrowsException<CabInvoiceException>(() => generateNormalFare.CalculateAgreegateFare(cabRides));
            Assert.AreEqual(CabInvoiceException.ExceptionType.NULL_RIDES, nullRidesException.exceptionType);
        }
    }
    //Test for returning invoice service when queried by UserId(UC5-TC-5.1)
    [TestMethod]
    public void GivenUserIdInvoiceServiceShouldReturnListOfRides()
    {
        Ride[] rides =
        {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                
            };
        string userId = "12345";
        RideRepository rideRepository = new RideRepository();
        rideRepository.AddRide(userId, rides);
        Ride[] actual = rideRepository.GetRides(userId);
        Assert.AreEqual(rides, actual);
    }
    // UC5 Return the total fare for premium ride
    [TestMethod]
    [TestCategory("CalculatingFare")]
    public void ReturnTotalFareForPremiumRide()
    {

        CabInvoiceGenerator invoice = new CabInvoiceGenerator(RideType.PREMIUM_RIDE);
        double distance = 15.0;
        int time = 12;
        double fare = invoice.CalculateFare(distance, time);
        double expected = 237.0;
        Assert.AreEqual(expected, fare);
    }
    // UC1 & UC5-Handling the custom exception if distance is negative number or zero.
    [TestMethod]
    [TestCategory("CalculatingFare")]
    public void ReturnTotalFareForRideInvalidDistance()
    {
        string expected = "Distance should be positive integer";
        try
        {
            CabInvoiceGenerator invoice = new CabInvoiceGenerator(RideType.PREMIUM_RIDE);
            double distance = -15;
            int time = 12;
            double fare = invoice.CalculateFare(distance, time);

        }
        catch (CustomException ex)
        {
            Assert.AreEqual(expected, ex.message);
        }
    }
    // UC1 & UC5-Handling the custom exception if time is negative number or zero.
    [TestMethod]
    [TestCategory("CalculatingFare")]
    public void ReturnTotalFareForRideInvalidTime()
    {
        string expected = "Time should be positive integer";
        try
        {
            CabInvoiceGenerator invoice = new CabInvoiceGenerator(RideType.NORMAL_RIDE);
            double distance = 10;
            int time = 0;
            double fare = invoice.CalculateFare(distance, time);

        }
        catch (CustomException ex)
        {
            Assert.AreEqual(expected, ex.message);
        }
    }
}
