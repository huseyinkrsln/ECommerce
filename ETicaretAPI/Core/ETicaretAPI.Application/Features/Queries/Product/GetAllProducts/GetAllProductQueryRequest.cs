using ETicaretAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
    //MediatR hangisinin Req,Res old anlamak için kalıtım imkanı sunar ve <(response dönecek sınıf adı)>, hangi sınıfın Req sınıfına karşılık handler  sınıfı olarak bildirimesi
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        //Parametreyi karşılayan proplar eklenmeli
        // public Pagination Pagination { get; set; }

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;

    }

}
