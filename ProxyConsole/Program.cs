using System;
using ProxyDomain;

namespace ProxyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***\r\nBegin program - no logging\r\n");
            
            IRepository<Customer> customerRepository = new Repository<Customer>();

            var customer = new Customer
            {
                Id = 1,
                Name = "Customer 1",
                Address = "Address 1"
            };
            
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            
            Console.WriteLine("\r\nEnd program - no logging\r\n***");
            Console.ReadLine();
        }
    }
}
