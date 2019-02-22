using System;
using PolicyMaturity.BusinessLayer;
using PolicyMaturity.Contracts;

namespace PolicyMaturity.App
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { "../../MaturityData.csv" };
         
            /* WE CAN USE IOC CONTAINERS LIKE STRUCTUREMAP TO MANAGE CLASS DEPENDENCIES */

            IWriter writer = new XmlWriter();
            ICalculator calculator = new XmlCalculator();

            IReader reader = new CsvReader();
            IValidator validator = new CsvValidator();

            IPolicyManager policyManager = new PolicyManager(writer, reader, calculator, validator);
            bool isSuccess = policyManager.Execute(args[0], true);

            if (isSuccess)
            {
                Console.WriteLine("Process Completed");
            }
            else
            {
                Console.WriteLine("File doesn't exists or Incorrect file");
            }

            Console.Read();
        }
    }
}
