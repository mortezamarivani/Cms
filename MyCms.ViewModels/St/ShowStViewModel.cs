using System;
using System.ComponentModel.DataAnnotations;

namespace MyCms.ViewModels.St
{
    public class ShowStViewModel
    { 
        public int StID { get; set; }
        public string StValue { get; set; }
        public int StCode { get; set; }
        public bool? IsZ { get; set; }
        public string StValueLiner { get; set; }
    }
}
