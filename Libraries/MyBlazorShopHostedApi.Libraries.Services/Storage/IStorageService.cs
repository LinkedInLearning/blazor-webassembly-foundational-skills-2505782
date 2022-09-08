using MyBlazorShopHostedApi.Libraries.Shared.Product.Models;
using MyBlazorShopHostedApi.Libraries.Shared.ShoppingCart.Models;

namespace MyBlazorShopHostedApi.Libraries.Services.Storage
{
    /// <summary>
    /// Stores the data used for the application.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Stores a list of products.
        /// </summary>
        IList<ProductModel> Products { get; }

        /// <summary>
        /// Stores the shopping cart.
        /// </summary>
        ShoppingCartModel ShoppingCart { get; }

    }
}
