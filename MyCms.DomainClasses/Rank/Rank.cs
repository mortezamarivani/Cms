using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Rank
{

    public class Rank
    {
        public Rank()
        {
        }

        [Key]
        [Display(Name = "RankID")]
        public int RankID { get; set; }

        [Display(Name = "RankDesc")]
        [MaxLength(200)]
        public string RankDesc { get; set; }

        //[Required(ErrorMessage = "Plase Enter {0} !")]
        [Display(Name = "RankName")]
        [MaxLength(200)]
        public string RankName { get; set; }

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

        [Display(Name = "Type")]
        public int Type { get; set; }


        

    }
}
