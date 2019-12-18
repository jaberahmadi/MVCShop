using DomainClass;
using System.Collections.Generic;

namespace WiewModels
{
    public class AddProductViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public Product Product { get; set; }
    }
}