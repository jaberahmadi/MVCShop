using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DomainClass
{

    public  class City
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        [Key]
        public int CityId { get; set; }
        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ResellersId { get; set; }

        [Required(ErrorMessage = "نام شهر را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("نام شهر")]
        [Display(Name = "نام شهر")]
        [StringLength(50, ErrorMessage = "این فیلد باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "استان را انتخاب کنید کنید", AllowEmptyStrings = false)]
        [DisplayName("استان")]
        [Display(Name = "استان")]
        [ScaffoldColumn(false)]
        public int OstanId { get; set; }
        public virtual Ostan Ostan { get; set; }
        public virtual ICollection<Reseller> Resellers { get; set; }
        public City()
        {
        }


    }
}
