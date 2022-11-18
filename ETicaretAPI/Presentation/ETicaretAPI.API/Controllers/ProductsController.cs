using System.Net;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Features.Commands.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProducts;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (AuthenticationSchemes="Admin")]
    public class ProductsController : ControllerBase
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        //wwroot taki static dosyalara erişim hızlandırması için
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;


        //mediatR kullanımı ServisRegistration sayesinde DI yapıldı
        readonly IMediator _mediator;



        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;


            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        //Buradaki id → Id oldu içindeki propa göre değişti
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);

        }

        //put metoduna sadece model yazdık id yazmadık. Bunun yerine mdel içindeki id numarasından taratıp ona göre işlem yaptırıyoruz.
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        //■■■  Burada  değişim yapılacak ■■■
        //file-upload için oluşturuldu
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            /*Test için yazılmıştır
             * wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            **Dosyları yakalama işlemi iiçin yani client taki  "const fileData: FormData = new FormData();" FormData daki veriyiyakalar. --> Request.Form.Files

            **Dizin var mı yok mu 
            if (!Directory.Exists(uploadPath)){
                Directory.CreateDirectory(uploadPath);
            }
              

            **Şİmdiliik random oluşturuyoruz file extension'ı için yani a.jpg dosyamız var a= name .jpeg = extension 'dır(denemeamaçlıdır)
            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.Name)}");
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,useAsync: false);
                **hedef path'e basıyoruz
                await file.CopyToAsync(fileStream);
                **Stream'i boşaltıyoruz
                await fileStream.FlushAsync();
            */

            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();

        }

    }
}
