using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
    //REq sınıfına karşılık res sıfını döndüreceğini bildiriyoruz bunun için kalıtım veriyoruz
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        async Task<GetAllProductQueryResponse> IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>.Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            /*NOT√
             * GEt metodundaki kodları artık buraya aktarıyoruz ve request parameteresi üzerinden hareket edilecek
            Önceden anonim tip göndermiştik ama artık response türünde gidecek
            */


            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return new()
            {
                Products = products,
                TotalCount = totalCount

            };
        }
    }
}
