using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModernStore.Infra.Transactions;
using ModernStore.Domain.Commands.Handlers;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Commands.Inputs;

namespace ModernStore.Api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderCommandHandler _handler;

        public OrderController(IUow uow, OrderCommandHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/orders")]
        public async Task<IActionResult> Post([FromBody]RegisterOrderCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}
