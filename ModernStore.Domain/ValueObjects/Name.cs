using FluentValidator;

namespace ModernStore.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            //Validações
            new ValidationContract<Name>(this)
                .IsRequired(x => x.FirstName, "Nome é obrigatório")
                .HasMaxLenght(x => x.FirstName, 60, "Nome inválido")
                .HasMinLenght(x => x.FirstName, 3, "Nome inválido")
                .IsRequired(x => x.LastName, "Sobrenome é obrigatório")
                .HasMaxLenght(x => x.LastName, 60, "Sobrenome inválido")
                .HasMinLenght(x => x.LastName, 3, "Sobrenome inválido");
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
