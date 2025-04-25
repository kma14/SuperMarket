using SuperMarket.Application.Interfaces;
using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Application.Services
{
    public class CartService(ShoppingCart cart) : ICartService
    {
        public void AddCartItem(CartItem cartItem)
        {
            cart.AddItem(cartItem);
        }

        public decimal CalculateTotalPrice()
        {
            return cart.CalculateTotalPrice();
        }
    }
}
