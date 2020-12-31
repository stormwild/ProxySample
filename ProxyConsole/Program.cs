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
            
            _log.Info("***\r\nBegin program - with logging\r\n");
            
            IRepository<Customer> customerRepository = new LoggerRepository<Customer>(new Repository<Customer>(), _log);

            var customer = new Customer
            {
                Id = 1,
                Name = "Customer 1",
                Address = "Address 1"
            };
            
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            
            _log.Info("\r\nEnd program - with logging\r\n***");
        }
    }
}
