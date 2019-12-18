using DomainClass;
using System.Collections.Generic;

namespace WiewModels
{
    public class CityDrpViewModel
    {
        public Reseller Reseller { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}