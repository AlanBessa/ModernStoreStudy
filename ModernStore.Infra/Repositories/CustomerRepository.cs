using ModernStore.Domain.Repositories;
using System;
using System.Linq;
using System.Data.Entity;
using ModernStore.Domain.Entidades;
using ModernStore.Infra.Contexts;
using ModernStore.Domain.Commands.Results;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public bool DocumentExists(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }

        public Customer Get(Guid id)
        {
            return _context.Customers.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public GetCustomerCommandResult Get(string username)
        {
            var query = @"SELECT [Name],[Document],[Email],[Username],[Password],[Active] FROM [GetCustomerInfoView] WHERE [Active] AND [Username]=@username";

            using (var conn = new SqlConnection(@"Data Source=ALAN-PC;Initial Catalog=ModernStoreData;Integrated Security=True"))
            {      
                conn.Open();
                return conn.Query<GetCustomerCommandResult>(query, new { username = username }).FirstOrDefault();
            }

            //Usando Entity Framework
            //return _context.Customers.Include(x => x.User).AsNoTracking().Select(x => new GetCustomerCommandResult {
            //    Name = x.Name.ToString(),
            //    Document = x.Document.Number,
            //    Email = x.Email.Address,
            //    Active = x.User.Active,
            //    Username = x.User.Username,
            //    Password = x.User.Password

            //}).FirstOrDefault(x => x.Username == username);
        }

        public Customer GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
    }
}
