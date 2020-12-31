using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using ProxyDomain;

namespace ProxyConsole
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            
            _log.Info("Log4Net working");
            
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
