using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiewModels
{
    public class PageListViewModel
    {
        public IEnumerable<Reseller> Resellers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }

    }
}