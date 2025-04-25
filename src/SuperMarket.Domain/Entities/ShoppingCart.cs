using SuperMarket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Domain.Entities
{
    public class ShoppingCart(IPricingStrategy pricingStrategy)
    {
        public List<CartItem> Items { get; private set; } = new List<CartItem>();

        public void AddItem(CartItem cartItem) => Items.Add(cartItem);

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var cartItemGroup in Items.GroupBy(c => c.Sku))
            {
                totalPrice += pricingStrategy.CalculatePrice(cartItemGroup.Key, cartItemGroup.Count());
            }
            return totalPrice;
        }
    }
}
