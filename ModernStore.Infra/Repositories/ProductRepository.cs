using ModernStore.Domain.Repositories;
using System;
using System.Linq;
using ModernStore.Domain.Entidades;
using ModernStore.Infra.Contexts;
using ModernStore.Domain.Commands.Results;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreDataContext _context;

        public ProductRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Product Get(Guid id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            var query = @"SELECT [Id], [Title], [Price], [Image] FROM [Product]";

            using (var conn = new SqlConnection(@"Data Source=ALAN-PC;Initial Catalog=ModernStoreData;Integrated Security=True"))
            {
                conn.Open();
                return conn.Query<GetProductListCommandResult>(query);
            }
        }
    }
}
