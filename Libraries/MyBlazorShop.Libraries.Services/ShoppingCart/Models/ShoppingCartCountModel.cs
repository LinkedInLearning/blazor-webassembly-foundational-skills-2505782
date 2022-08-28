using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorShop.Libraries.Services.ShoppingCart.Models
{
    public class ShoppingCartCountModel
    {
        public int Count { get; set; }

        public Action? OnCountChange { get; set; }
    }
}
