using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorShopHosted.Libraries.Shared.Product.Models
{
    public class ProductAddToCartModel
    {
        [Required]
        [Range(1, 400, ErrorMessage = "{0} must be between {1} and {2}")]
        public int? Quantity { get; set; }
    }
}
