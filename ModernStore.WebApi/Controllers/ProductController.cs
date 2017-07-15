using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ModernStore.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("v1/products")]
        [Authorize(Policy = "User")]
        public IActionResult Get()
        {
            return Ok(_productRepository.Get());
        }
    }
}
