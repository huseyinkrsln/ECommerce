using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.ViewModels.Products
{
    public class VM_Create_Product
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price {get; set;}
    }
}