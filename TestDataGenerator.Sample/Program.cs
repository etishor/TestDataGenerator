using System;
using Newtonsoft.Json;

namespace TestDataGenerator.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Catalog catalog = new Catalog();

            Customer customer = catalog.CreateInstance<Customer>();

            Console.WriteLine(JsonConvert.SerializeObject(customer, Formatting.Indented));

            Console.ReadKey();
        }
    }
}
