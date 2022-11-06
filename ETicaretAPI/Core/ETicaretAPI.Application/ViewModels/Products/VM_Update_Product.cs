using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.ViewModels.Products
{
    public class VM_Update_Product
    {
        //Normalde put işlermlerinde parametre içinde model ve id gönderilir. ama bu sınıf içinde id yazmamızın sebei id bağlı altındaki bilgiler değişeceğinin söylüyoruz .
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

    }
}