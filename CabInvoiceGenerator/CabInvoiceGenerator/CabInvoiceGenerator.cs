using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class CabInvoiceGenerator
    {
        //Constants
        public readonly int MINIMUM_FARE;
        public readonly int COST_PER_KM;
        public readonly int COST_PER_MINUTE;
        private object rideRepository;

        // Parameterized constructor
        public CabInvoiceGenerator(RideType type)
        {
            if (type.Equals(RideType.NORMAL))
            {
                COST_PER_KM = 10;
                COST_PER_MINUTE = 1;
                MINIMUM_FARE = 5;
            }
        }
        //  Method to calculate fare for single ride UC1
        public double CalculateFare(int time, double distance)
        {
            double totalFare = 0;
            try
            {
                if (time <= 0)
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                if (distance <= 0)
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                //Calculating total fare
                totalFare = (distance * COST_PER_KM) + (time * COST_PER_MINUTE);
                //Comparing minimum fare and calculated fare to return the maximum fare
                return Math.Max(totalFare, MINIMUM_FARE);
            }
            catch (CabInvoiceException ex)
            {
                throw ex;
            }
        }
        // Method to calculate aggregate fare for multiple rides and return Invoice summary UC2
        public InvoiceSummary CalculateAggregateFare(Ride[] rides)
        {
            double totalFare = 0;
            if (rides.Length == 0)
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "No Rides Found");
            foreach (var ride in rides)
            {
                totalFare += CalculateFare(ride.time, ride.distance);
            }
            //double agreegateFare = Math.Max(totalFare, MINIMUM_FARE);
            return new InvoiceSummary(rides.Length, totalFare);
        }
        //UC4
        public RideRepository rideRepository;
        public double CalculateFare(double distance, int time)
        {
            int costPerKilometer = 10;
            int costPerMinute = 1;
            int minimumFare = 5;
            double totalFare = distance * costPerKilometer + time * costPerMinute;
            if (totalFare < minimumFare)
                return minimumFare;
            return totalFare;
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            foreach (Ride ride in rides)
            {
                totalFare += this.CalculateFare(ride.distance, ride.time);
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        public void AddRides(string userId, Ride[] rides)
        {
            rideRepository.AddRide(userId, rides);
        }
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            return this.CalculateFare(rideRepository.GetRides(userId));
        }
    }
}
       
   


