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
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
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

        [HttpGet("total-page-count")]
        public int GetTotalPageCount(int size)
        {
            // Return page count
            return _productService.GetTotalPageCount(size);
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
