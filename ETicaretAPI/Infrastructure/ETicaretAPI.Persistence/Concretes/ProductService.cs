﻿using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concrete
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new() 
            {
                new (){Id = Guid.NewGuid(),Name="Product1",Price=100,Stock=50},
                new (){Id = Guid.NewGuid(),Name="Product2",Price=200,Stock=150},
                new (){Id = Guid.NewGuid(),Name="Product3",Price=200,Stock=120},
                new (){Id = Guid.NewGuid(),Name="Product4",Price=300,Stock=250},
            };
    }
}
