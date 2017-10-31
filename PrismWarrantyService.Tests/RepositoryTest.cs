using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Concrete;
using System.Linq;

namespace PrismWarrantyService.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void GetOrders()
        {
            // arrange
            IRepository repo = new EFRepository();

            // act 
            var orders = repo.Orders
                .ToList();

            // assert
            Assert.AreEqual(orders.Count, 7);
        }
    }
}
