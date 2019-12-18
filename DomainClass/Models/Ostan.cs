using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DomainClass
{
    
    public  class Ostan
    {


        [ScaffoldColumn(false)]
        [Bindable(false)]
        [Key]
        public int OstanId { get; set; }
        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ResellersId { get; set; }

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int CityId { get; set; }
        [Required(ErrorMessage = "نام استان را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("نام استان")]
        [Display(Name = "نام استان")]
        [StringLength(50, ErrorMessage = "این فیلد باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Reseller> Resellers { get; set; }
        public Ostan()
        {
        }
    }
}
