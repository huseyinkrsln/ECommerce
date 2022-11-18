using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application
{
    //Application katmanındaki gerekli handler sınıflarını  Presantation katmanında Program.cs üzerinden Ioc container'a eklememeizi sağlayacak olan sınıftır
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            //ait olduğu gömülü sınıf ne ise tüm handlerlara Ioc işlemini uygulayabilecek
            collection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
