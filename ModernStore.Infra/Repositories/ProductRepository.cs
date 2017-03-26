using ModernStore.Domain.Repositories;
using System;
using System.Linq;
using ModernStore.Domain.Entidades;
using ModernStore.Infra.Contexts;

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
    }
}
