using FluentValidator;

namespace ModernStore.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            //Validações
            new ValidationContract<Email>(this)
                .IsEmail(x => x.Address, "E-mail é inválido");
        }

        public string Address { get; private set; }
    }
}
