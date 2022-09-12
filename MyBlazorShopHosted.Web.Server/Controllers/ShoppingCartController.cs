using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlazorShopHosted.Libraries.Services.Product;
using MyBlazorShopHosted.Libraries.Shared.Product.Models;
using MyBlazorShopHosted.Libraries.Services.ShoppingCart;
using MyBlazorShopHosted.Libraries.Shared.ShoppingCart.Models;
using MyBlazorShopHosted.Web.Server.Attributes;

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

        [HttpGet("has-product/{sku}")]
        public bool HasProduct(string sku)
        {
            return _shoppingCartService.HasProduct(sku);
        }

        [HttpPost, RequiresAuthorizationHeader]
        public IActionResult AddProduct(ShoppingCartAddModel shoppingCartAddModel)
        {
            var product = !string.IsNullOrWhiteSpace(shoppingCartAddModel.ProductSku) ? _productService.Get(shoppingCartAddModel.ProductSku) : null;

            if (product == null)
            {
                return NotFound(string.Format("The sku '{0}' could not be found", shoppingCartAddModel.ProductSku));
            }

            _shoppingCartService.AddProduct(product, shoppingCartAddModel.Quantity);

            return Ok(new { Success = true });
        }

        [HttpPut, RequiresAuthorizationHeader]
        public IActionResult UpdateProduct(ShoppingCartAddModel shoppingCartAddModel)
        {
            var product = !string.IsNullOrWhiteSpace(shoppingCartAddModel.ProductSku) ? _productService.Get(shoppingCartAddModel.ProductSku) : null;

            if (product == null)
            {
                return NotFound(string.Format("The sku '{0}' could not be found", shoppingCartAddModel.ProductSku));
            }

            _shoppingCartService.AddProduct(product, shoppingCartAddModel.Quantity);

            return Ok(new { Success = true });
        }

        [HttpDelete("{sku}")]
        public IActionResult DeleteProduct(string sku)
        {
            var cart = _shoppingCartService.Get();

            if (cart?.Items == null || !cart.Items.Any(s => s.Product.Sku == sku))
            {
                return NotFound(string.Format("The sku '{0}' could not be found", sku));
            }

            var skuItem = cart.Items.First(s => s.Product.Sku == sku);
            _shoppingCartService.DeleteProduct(skuItem);

            return Ok(new { Success = true });
        }


    }
}
