using SuperMarket.Application.Models;
using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Application.Interfaces
{
    public interface ICartService
    {
        List<CartItemWithLineTotal> GetCartItems();
        CartItemWithLineTotal? GetCartItem(string sku);
        void AddCartItem(string sku);
        decimal CalculateTotalPrice();
    }
}
