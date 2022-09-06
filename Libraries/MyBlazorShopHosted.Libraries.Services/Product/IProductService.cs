using MyBlazorShopHosted.Libraries.Shared.Product.Models;

namespace MyBlazorShopHosted.Libraries.Services.Product
{
    /// <summary>
    /// Used for product methods.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets a product.
        /// </summary>
        /// <param name="sku">The unique sku reference.</param>
        /// <returns>A <see cref="ProductModel"/> type.</returns>
        ProductModel? Get(string sku);

        /// <summary>
        /// Get a product by slug.
        /// </summary>
        /// <param name="slug">The slug of the product</param>
        /// <returns></returns>
        ProductModel? GetBySlug(string slug);

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>A <see cref="IList<ProductModel>"/> type.</returns>
        IList<ProductModel> GetAll();

        /// <summary>
        /// Gets all products, limiting to listing size
        /// </summary>
        /// <param name="size">The number of items to return</param>
        /// <param name="page">The page number</param>
        /// <returns>A <see cref="IList<ProductModel>"/> type.</returns>
        IList<ProductModel> GetAll(int size, int page = 1);

        /// <summary>
        /// Gets the total page count for the products
        /// </summary>
        /// <param name="size">The number of items</param>
        /// <returns>The total number of pages</returns>
        int GetTotalPageCount(int size);
    }
}
