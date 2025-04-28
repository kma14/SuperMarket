using SuperMarket.Application.Interfaces;
using SuperMarket.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Application.Providers;

public class PricingRulesProvider
{
    public IReadOnlyDictionary<string, List<Pack>> PackRules { get; }


    /// <summary>
    /// Provides pricing rules for the application by loading configuration data from IDataSource
    /// 
    /// It knows the expected structure (a dictionary mapping SKUs to available packs)
    /// 
    /// It hides(does not care) the underlying storage format (JSON, database, etc.) behind an IDataSource abstraction.
    /// </summary>
    public PricingRulesProvider(IDataSource dataSource)
    {
        PackRules = dataSource.Load<Dictionary<string, List<Pack>>>("PricingRules:PackRules");
    }
}