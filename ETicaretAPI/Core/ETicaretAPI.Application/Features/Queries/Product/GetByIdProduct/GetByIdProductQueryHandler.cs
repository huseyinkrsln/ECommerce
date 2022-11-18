﻿using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        async Task<GetByIdProductQueryResponse> IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>.Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            //namespace içinde Product old için şimdilik böyle yazıldı
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
               Name = product.Name,
               Price = product.Price,
               Stock = product.Stock,
            };
        }
    }
}
