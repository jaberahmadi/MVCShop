using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Product> BestSellersProducts { get; set; }
        public IEnumerable<Product> OffProducts { get; set; }
    }
}