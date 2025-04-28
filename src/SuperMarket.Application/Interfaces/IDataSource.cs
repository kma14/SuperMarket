using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Application.Interfaces
{
    public interface IDataSource
    {
        T Load<T>(string? key = null);
    }
}

