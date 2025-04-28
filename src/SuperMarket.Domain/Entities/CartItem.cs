using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Domain.Entities
{
    public class CartItem(string sku)
    {
        public string Sku => sku;
        public int Quantity { get; private set; } = 1;

        public void Increment() => Quantity++;
    }
}
