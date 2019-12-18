using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DomainClass
{

    public  class Group
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        [Key]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "نام گروه را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("نام گروه")]
        [Display(Name = "نام گروه")]
        [StringLength(50, ErrorMessage = "این فیلد باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [DisplayName("گروه پدر")]
        [Display(Name = "گروه پدر")]
        [ScaffoldColumn(false)]
        public Nullable<int> ParentId { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual Group Group1 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public int Id { get; set; }

        public Group()
        {
        }
    }
}
