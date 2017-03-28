using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entidades;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]    
    public class OrderTests
    {
        private Customer _customer;

        [TestInitialize]
        public void Initialize()
        {
            var name = new Name("André", "Baltieri");
            var document = new Document("09420483770");
            var email = new Email("andrebaltieri@hotmail.com");

            _customer = new Customer(name, email, document, new User("andrebaltieri", "andrebaltieri", "andrebaltieri"));
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnOutOfStockProductItShouldReturnAnError()
        {
            var mouse = new Product("Mouse", 299, "mouse.jpg", 0);

            var order = new Order(_customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsFalse(order.IsValid());
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnInStockProductItShouldReturnUpdateQuantityOnHand()
        {
            var mouse = new Product("Mouse", 299, "mouse.jpg", 20);

            var order = new Order(_customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsTrue(mouse.QuantityOnHand == 18);
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAValidOrderTheTotalShouldBe310()
        {
            var mouse = new Product("Mouse", 300, "mouse.jpg", 20);

            var order = new Order(_customer, 12, 2);
            order.AddItem(new OrderItem(mouse, 1));

            Assert.IsTrue(order.Total() == 310);
        }
    }
}
