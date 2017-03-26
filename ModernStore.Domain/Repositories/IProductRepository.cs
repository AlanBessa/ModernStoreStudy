using ModernStore.Domain.Entidades;
using System;

namespace ModernStore.Domain.Repositories
{
    public interface IProductRepository
    {
        Product Get(Guid id);        
    }
}
