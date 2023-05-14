using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.ReciveInfo
{
    public class ReciveInfo
    {


        public ReciveInfo()
        {
            
        }

        [Key]
        [Display(Name = "ReciveInfoID")]
        public int ReciveInfoID { get; set; }


        [Display(Name = "SenderName")]
        [MaxLength(100)]
        public string SenderName { get; set; }

        [EmailAddress]
        [Display(Name = "SenderEmail")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(100)]
        public string SenderEmail { get; set; }

        [Display(Name = "ReciveMessage")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string ReciveMessage { get; set; }

        [Display(Name = "Status")]
        [DataType(DataType.MultilineText)]
        public  bool Status { get; set; }

        [Display(Name = "CreatedDate")]
        public int CreatedDate { get; set; }


    }
}
