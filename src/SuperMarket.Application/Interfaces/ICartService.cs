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
        void AddCartItem(CartItem cartItem);
        decimal CalculateTotalPrice();
    }
}
