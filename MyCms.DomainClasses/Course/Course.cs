using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Course
{

    public class Course
    {
        public Course()
        {
        }

        [Key]
        [Display(Name = "CourseID")]
        public int CourseID { get; set; }

        [Display(Name = "CourseDesc")]
        [MaxLength(200)]
        public string CourseDesc { get; set; }

        //[Required(ErrorMessage = "Plase Enter {0} !")]
        [Display(Name = "CourseName")]
        [MaxLength(200)]
        public string CourseName { get; set; }

        [Display(Name = "Tools")]
        [MaxLength(500)]
        public string Tools { get; set; }

        [Display(Name = "Institute")]
        [MaxLength(200)]
        public string Institute { get; set; }


        [Display(Name = "Status")]
        [DataType(DataType.MultilineText)]
        public bool Status { get; set; }


        [Display(Name = "StartDate")]
        public int StartDate { get; set; }

        [Display(Name = "EndDate")]
        public int EndDate { get; set; }

        [Display(Name = "CreatedDate")]
        public int CreatedDate { get; set; }


        [Display(Name = "CreatorUserID")]
        public int CreatorUserID { get; set; }


        [Display(Name = "DocName")]
        [MaxLength(200)]
        public string DocName { get; set; }

        [Display(Name = "SuffixFile")]
        public string SuffixFile { get; set; }

        [Display(Name = "Languge")]
        public int Languge { get; set; }

        [Display(Name = "Period")]
        public int Period { get; set; }


    }
}
