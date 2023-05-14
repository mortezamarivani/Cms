using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Polls
{
    public class Polls
    {
        public Polls()
        {

        }
        [Key]
        [Display(Name ="PollsID")]
        public int PollsID { get; set; }

        [Display(Name = "PollsCode")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PollsCode { get; set; }


        [Display(Name = "Ip")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Ip { get; set; }


        [Display(Name = "CreateDate")]
        public int CreateDate { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }


    }
}
