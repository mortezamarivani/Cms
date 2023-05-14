using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.About
{
    public class About
    {


        public About()
        {
            
        }

        [Key]
        [Display(Name = "AboutID")]
        public int AboutID { get; set; }

        [Display(Name = "Name")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        public string Name { get; set; }


        [Display(Name = "HomeDesc")]
        [MaxLength(500)]
        public string HomeDesc { get; set; }


        [Display(Name = "Mobile")]
        [MaxLength(100)]
        public string Mobile { get; set; }

        [Display(Name = "TelPhon")]
        [MaxLength(100)]
        public string TelPhon { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(100)]
        public string Email { get; set; }


        [Display(Name = "Address")]
        [MaxLength(400)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "AboutHeader")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(100)]
        public string AboutHeader { get; set; }

        [Display(Name = "AboutTitle")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(100)]
        public string AboutTitle { get; set; }

        [Display(Name = "AboutDescription")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string AboutDescription { get; set; }


        [Display(Name = "InstagramID")]
        [MaxLength(50)]
        public string InstagramID { get; set; }


        [Display(Name = "LinkdinAddress")]
        [MaxLength(100)]
        public string LinkdinAddress { get; set; }


        [Display(Name = "TwitterAddress")]
        [MaxLength(100)]
        public string TwitterAddress { get; set; }


        [Display(Name = "FacebookAddress")]
        [MaxLength(100)]
        public string FacebookAddress { get; set; }

        [Display(Name = "Status")]
        [DataType(DataType.MultilineText)]
        public  bool Status { get; set; }

        [Display(Name = "CreatedDate")]
        public int CreatedDate { get; set; }

        [Display(Name = "CreatorUserID")]
        public int CreatorUserID { get; set; }

        [Display(Name = "Languge")]
        public int Languge { get; set; }

    }
}
