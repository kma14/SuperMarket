using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Application.Models
{
    public class CartItemWithLineTotal 
    {
        public required string Sku {  get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    } 
}
