using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CabInvoiceGenerator;

namespace CabInvoiceGenerator
{
    [TestClass]
    public class UnitTest1
    {
        CabInvoiceGenerator cabInvoiceGenerator = null;
        [TestMethod]
        //Created The Generate Cab Invoice Class For Calculating Fares UC1
        public void GivenDistanceAndTimeShouldRetuenTotalFare()
        {
            //creating instance of InvoiceGenerator for Normal ride
            cabInvoiceGenerator = new CabInvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;
            //calculating fare 
            double fare = cabInvoiceGenerator.CalculateFare( time, distance);
            double expected = 25;
            //Asserting value
            Assert.AreEqual(expected, fare);
        }
    }
}
