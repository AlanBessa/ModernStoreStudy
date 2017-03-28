using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entidades;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class CustomerTests
    {
        private readonly User user = new User("andrebaltieri", "andrebaltieri", "andrebaltieri");

        private Name _completeName;
        private Name _onlyFirstName;
        private Name _onlyLastName;
        private Document _document;
        private Email _email;
        private Email _invalidEmail;

        [TestInitialize]
        public void Initialize()
        {
            _completeName = new Name("André", "Baltieri");
            _onlyFirstName = new Name("André", "");
            _onlyLastName = new Name("", "Baltieri");
            _document = new Document("09420483770");
            _email = new Email("andrebaltieri@hotmail.com");
            _invalidEmail = new Email("a");
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidFirstNameShouldReturnmANotification()
        {
            var customer = new Customer(_onlyFirstName, _email, _document, user);
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidLastNameShouldReturnmANotification()
        {
            var customer = new Customer(_onlyLastName, _email, _document, user);
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidEmailNameShouldReturnmANotification()
        {
            var customer = new Customer(_completeName, _invalidEmail, _document, user);
            Assert.IsFalse(customer.IsValid());
        }
    }
}
