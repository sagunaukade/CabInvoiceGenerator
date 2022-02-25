using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Cab Invoice Generator");
            CabInvoiceGenerator cabInvoiceGenerator = new CabInvoiceGenerator(RideType.NORMAL);
            Console.WriteLine(cabInvoiceGenerator.CalculateFare(10, 15));
            Console.ReadLine();
        }
    }
}
