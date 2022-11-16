using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Lütfen ürün adını boş bırakmayınız")
                .MaximumLength(150).MinimumLength(5).WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz");

            RuleFor(p => p.Stock).NotEmpty().NotNull().WithMessage("Stok bilgisini boş bırakmayınız")
                .Must(s => s >= 0).WithMessage("stok negatif olamaz");

            RuleFor(p => p.Price).NotEmpty().NotNull().WithMessage("Fiyat bilgisini boş bırakmayınız")
                .Must(s => s >= 0).WithMessage("Fiyat negatif olamaz");



        }
    }
}
