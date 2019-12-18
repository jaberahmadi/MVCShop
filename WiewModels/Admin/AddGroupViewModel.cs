using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiewModels
{
    public class AddGroupViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public Group Group { get; set; }
    }
}