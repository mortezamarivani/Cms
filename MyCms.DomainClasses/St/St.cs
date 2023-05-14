using System.ComponentModel.DataAnnotations;


namespace MyCms.DomainClasses.St
{
    public class St
    {
        public St()
        {

        }

        [Key]
        [Display(Name = "StID")]
        public int StID { get; set; }

        [Display(Name = "StValue")]
        public string StValue { get; set; }

        [Display(Name = "StCode")]
        public int StCode { get; set; }

        [Display(Name = "IsZ")]
        public bool? IsZ { get; set; }

        [Display(Name = "StValueLiner")]
        public string StValueLiner { get; set; }

    }
}
