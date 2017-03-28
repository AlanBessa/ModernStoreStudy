using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Infra.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernStore.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUow _uow;
        private readonly CustomerCommandHandler _handler;

        public CustomerController(IUow uow, CustomerCommandHandler handler)
        {
            _uow = uow;
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/customers")]
        public IActionResult Post([FromBody]RegisterCustomerCommand command)
        {
            var result = _handler.Handle(command);

            if(_handler.IsValid())
            {
                _uow.Commit();
                return Ok(result);
            }
            else
            {
                return BadRequest(_handler.Notifications);
            }
        }
    }
}
