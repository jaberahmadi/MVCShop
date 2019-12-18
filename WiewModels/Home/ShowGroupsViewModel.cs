using DomainClass;
using System.Collections.Generic;

namespace WiewModels
{
    public class ShowGroupsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}