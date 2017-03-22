using FluentValidator;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;
using ModernStore.Domain.Entidades;
using ModernStore.Domain.Services;
using ModernStore.Domain.Resources;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;

namespace ModernStore.Domain.Commands.Handlers
{
    public class CustomerCommandHandler : Notifiable, 
        //ICommandHandler<UpdateCustomerCommand>,
        ICommandHandler<RegisterCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        //public ICommandResult Handle(UpdateCustomerCommand command)
        //{
        //    var customer = _customerRepository.Get(command.Id);

        //    if(customer == null)
        //    {
        //        AddNotification("Customer", "Cliente não encontrado");
        //        return null;
        //    }

        //    var name = new Name(command.FirstName, command.LastName);
        //    customer.Update(name, command.BirthDate);

        //    AddNotifications(customer.Notifications);

        //    if (IsValid())
        //    {
        //        _customerRepository.Update(customer);
        //    }
        //}

        public ICommandResult Handle(RegisterCustomerCommand command)
        {
            if (_customerRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso.");
                return null;
            }

            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var document = new Document(command.Document);
            var user = new User(command.Username, command.Password, command.ConfirmPassword);

            var customer = new Customer(name, email, document, user);

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(user.Notifications);
            AddNotifications(customer.Notifications);
            
            if (IsValid())
            {
                _customerRepository.Save(customer);
            }

            _emailService.Send(
                customer.Name.ToString(), 
                customer.Email.Address, 
                string.Format(EmailTemplate.WelcomeEmailTitle, customer.Name),
                string.Format(EmailTemplate.WelcomEmailBody, customer.Name));

            return new RegisterCustomerCommandResult
            {
                Id = customer.Id,
                Name = customer.Name.ToString()
            };
        }
    }
}
