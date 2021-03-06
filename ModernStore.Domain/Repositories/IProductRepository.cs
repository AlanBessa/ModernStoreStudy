﻿using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace ModernStore.Domain.Repositories
{
    public interface IProductRepository
    {
        Product Get(Guid id);

        IEnumerable<GetProductListCommandResult> Get();
    }
}
