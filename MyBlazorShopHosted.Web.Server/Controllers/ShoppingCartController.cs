using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlazorShopHosted.Libraries.Services.Product;
using MyBlazorShopHosted.Libraries.Services.Product.Models;
using MyBlazorShopHosted.Libraries.Services.ShoppingCart;
using MyBlazorShopHosted.Libraries.Services.ShoppingCart.Models;

namespace MyBlazorShopHosted.Web.Server.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,
            IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        [HttpGet]
        public ShoppingCartModel GetCart()
        {
            return _shoppingCartService.Get();
        }

        [HttpGet("count")]
        public int GetCount()
        {
            return _shoppingCartService.Count();
        }

        [HttpGet("has-product")]
        public bool HasProduct(string sku)
        {
            return _shoppingCartService.HasProduct(sku);
        }

        [HttpPost]
        public IActionResult AddProduct(string sku, int quantity)
        {
            var product = _productService.Get(sku);

            if (product == null)
            {
                return NotFound(string.Format("The sku '{0}' could not be found", sku));
            }

            _shoppingCartService.AddProduct(product, quantity);

            return Ok(new { Success = true });
        }

        [HttpDelete("{sku}")]
        public IActionResult DeleteProduct(string sku)
        {
            var cart = _shoppingCartService.Get();

            if (cart == null || !cart.Items.Any(s => s.Product.Sku == sku))
            {
                return NotFound(string.Format("The sku '{0}' could not be found", sku));
            }

            var skuItem = cart.Items.First(s => s.Product.Sku == sku);
            _shoppingCartService.DeleteProduct(skuItem);

            return Ok(new { Success = true });
        }


    }
}
