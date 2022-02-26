using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class CabInvoiceService
    {
        private List<Ride> list;
        private InvoiceSummary userInvoice;

        //Parametrized constructor to set the user cab ride list aling with invoice summary UC4
        public CabInvoiceService(List<Ride> rides, InvoiceSummary invoiceSummary)
        {
            Rides = rides;
            InvoiceSummary = invoiceSummary;
        }

        public CabInvoiceService(List<Ride> list, InvoiceSummary userInvoice)
        {
            this.list = list;
            this.userInvoice = userInvoice;
        }

        //Properties to get the ride list and user invoice details UC5
        public List<Ride> Rides { get; }
        public InvoiceSummary InvoiceSummary { get; }

        //Method to determine the specified object is equal to the instance UC5
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is CabInvoiceService))
                return false;
            CabInvoiceService userCabService = (CabInvoiceService)obj;
            return this.Rides.Count == userCabService.Rides.Count && this.InvoiceSummary.totalFare == userCabService.InvoiceSummary.totalFare && this.InvoiceSummary.average == userCabService.InvoiceSummary.average;
        }
    }
}

