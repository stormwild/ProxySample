using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyDomain;
using Xunit;
using Xunit.Abstractions;

namespace Proxy.Tests
{
    public class RepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public RepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Repository_Tests()
        {
            // Arrange
            IRepository<Customer> customerRepository = new Repository<Customer>();

            var customer = new Customer
            {
                Id = 1,
                Name = "Customer 1",
                Address = "Address 1"
            };

            // Act
            customerRepository.Add(customer);
            var sut = customerRepository.GetAll();

            // Assert
            Assert.NotNull(sut);
            Assert.NotEmpty(sut);
            Assert.IsAssignableFrom<IEnumerable<Customer>>(sut);

            // Act 
            customer.Address = "Address 1a";
            customerRepository.Update(customer);
            var sut2 = customerRepository.GetById(customer.Id);

            Assert.Equal(customer.Address, sut2.Address);

            _output.WriteLine($"Customer Address: {sut2.Address}");
        }  
    }
}
