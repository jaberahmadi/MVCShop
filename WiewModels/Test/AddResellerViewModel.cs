using DomainClass;
using System.Collections.Generic;

namespace WiewModels
{
    public class AddResellerViewModel
    {
        public Reseller Reseller { get; set; }

        public IEnumerable<Ostan> Proviences { get; set; }
    }
}