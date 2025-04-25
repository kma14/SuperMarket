using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Domain.Interfaces
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(string sku, int quantity);
    }
}
