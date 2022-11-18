using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Tokens
{

    /*JWT ADIM:2 
     * -hangi işlemleri yapacaksak onların tanımlamalarını yazıyoruz
     * -DTO klasörü içinde token modeli oluşturuyoruz
     * -Concrete lerini Infrastructure->Services->Token->TokenHandler
     * -adım 3 TokenHadndler da
     */
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}
