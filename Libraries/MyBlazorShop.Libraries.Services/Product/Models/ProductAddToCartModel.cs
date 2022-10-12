using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorShop.Libraries.Services.Product.Models
{
    public class ProductAddToCartModel
    {
        [Required]
        public int? Quantity { get; set; }
    }
}
