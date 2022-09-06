using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlazorShopHosted.Libraries.Services.Product;
using MyBlazorShopHosted.Libraries.Shared.Product.Models;

namespace MyBlazorShopHosted.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IList<ProductModel> GetAll(int? size = null, int page = 1)
        {
            if (!size.HasValue)
            {
                // Return all products
                return _productService.GetAll();
            }

            // Return partial products
            return _productService.GetAll(size.Value, page);
        }

        [HttpGet("page-count")]
        public ProductPageCountModel GetTotalPageCount(int size)
        {
            // Return page count
            return new ProductPageCountModel { TotalPageCount = _productService.GetTotalPageCount(size) };
        }

        [HttpGet("by-sku/{sku}")]
        public IActionResult GetBySku(string sku)
        {
            var product = _productService.Get(sku);

            if (product == null)
            {
                return NotFound(string.Format("The sku '{0}' could not be found", sku));
            }

            return Ok(product);
        }

        [HttpGet("by-slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var product = _productService.GetBySlug(slug);

            if (product == null)
            {
                return NotFound(string.Format("The slug '{0}' could not be found", slug));
            }

            return Ok(product);
        }


    }
}
