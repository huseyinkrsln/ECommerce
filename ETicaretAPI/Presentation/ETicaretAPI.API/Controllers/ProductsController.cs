using System.Net;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        //wwroot taki static dosyalara erişim hızlandırması için
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;

        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }


        [HttpGet]
        public IActionResult Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            await _productReadRepository.GetByIdAsync(id, false);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {


            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        //put metoduna sadece model yazdık id yazmadık. Bunun yerine mdel içindeki id numarasından taratıp ona göre işlem yaptırıyoruz.
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

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
