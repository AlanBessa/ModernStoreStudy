using ModernStore.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace ModernStore.Domain.Repositories
{
    public interface IProductRepository
    {
        Product Get(Guid id);

        //IEnumerable<Product> Get(List<Guid> ids);
    }
}
