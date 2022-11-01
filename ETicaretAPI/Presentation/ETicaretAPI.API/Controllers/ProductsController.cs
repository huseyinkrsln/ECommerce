using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        readonly private IOrderWriteRepository _orderWriteRepository;

        readonly private ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }


        [HttpGet]
        public async Task Get()
        {
            var customerId = Guid.NewGuid();
            

            await _customerWriteRepository.AddAsync(new Customer() {Name="huseyin",Id=customerId });
            await _orderWriteRepository.AddAsync(new Order() { Description = "test", Address = "ankara",CustomerId=customerId });
            await _orderWriteRepository.SaveAsync();
        }


    }
}
