using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Domain.Exceptions;

public class InvalidSkuException(string sku):Exception($"Item with SKU '{sku}' is not available.")
{
    public string Sku { get; } = sku;
}
