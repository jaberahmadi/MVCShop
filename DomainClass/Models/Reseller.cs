using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomainClass
{
    public  class Reseller
    {


        [ScaffoldColumn(false)]
        [Bindable(false)]
        [Key]
        public int ResellerId { get; set; }

        [Required(ErrorMessage = "نام نمایندگی را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("نام نمایندگی")]
        [Display(Name = "نام نمایندگی")]
        [StringLength(50, ErrorMessage = "این فیلد باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }


        [DisplayName("آدرس نمایندگی")]
        [Display(Name = "آدرس نمایندگی")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "این فیلد باید حداکثر 500 کاراکتر باشد")]
        public string Address { get; set; }

        public virtual City City { get; set; }
         public virtual Ostan Ostan { get; set; }
        public Reseller()
        {

        }
    }
}
