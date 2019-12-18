using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DomainClass
{
    
    public  class FactorItem
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        [Key]
        public int FactorItemId { get; set; }
        public int FactorId { get; set; }
        public int ProductId { get; set; }
        public byte Qty { get; set; }
    
        public virtual Factor Factor { get; set; }
        public virtual Product Product { get; set; }
        public FactorItem()
        {

        }
    }
}
