using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        //anonim gönderdiğimiz için önceden  şimdilik objcect yazıyoruz
        public object Products { get; set; }
    }
}
