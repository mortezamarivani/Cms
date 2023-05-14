using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.UserX
{
    public class UserX
    {
        public UserX()
        {

        }
        [Key]
        [Display(Name ="UserID")]
        public int UserID { get; set; }

        [MaxLength(50)]
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Display(Name = "Family")]
        public string Family { get; set; }

        [MaxLength(50)]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "Status")]
        public bool Status { get; set; }

        [MaxLength(15)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

    }
}
