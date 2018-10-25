using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    [Serializable]//tuan tu ra nhi phan de gan' vao session
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
}