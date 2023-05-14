using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Skills
{
    public class Skills
    {


        public Skills()
        {
            
        }

        [Key]
        [Display(Name = "SkillsID")]
        public int SkillsID { get; set; }

        [Display(Name = "SkillsTitle")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(400)]
        public string SkillsTitle { get; set; }

        [Display(Name = "SkillsDescription")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string SkillsDescription { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [DataType(DataType.MultilineText)]
        public  bool Status { get; set; }


        [Display(Name = "BootstarpClassName")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        [MaxLength(50)]
        public string BootstarpClassName { get; set; }
        

        [Display(Name = "Progress")]
        [Required(ErrorMessage = "Plase Enter {0} !")]
        public int Progress { get; set; }

        [Display(Name = "Languge")]
        public int Languge { get; set; }

    }
}
