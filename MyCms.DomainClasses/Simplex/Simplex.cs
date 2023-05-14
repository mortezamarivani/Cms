using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Simplex
{
    public class Simplex
    {
        public Simplex()
        {

        }

        [Key]
        [Display(Name = "SimplexID")]
        public int SimplexID { get; set; }

        [Display(Name = "TblNumber")]
        public int TblNumber { get; set; }

        [Display(Name = "VarNumRow")]
        public int VarNumRow { get; set; }

        [Display(Name = "VarNumCol")]
        public int VarNumCol { get; set; }

        [Display(Name = "RowIndex")]
        public int RowIndex { get; set; }

        [Display(Name = "ColIndex")]
        public int ColIndex { get; set; }

        [Display(Name = "InOut")]
        public int InOut { get; set; }

        [Display(Name = "Value")]
        public double Value { get; set; }

        [Display(Name = "TypeVar")]
        public int TypeVar { get; set; }

        [Display(Name ="StCode")]
        public int StCode { get; set; }

        [Display(Name = "Min")]
        public bool Min { get; set; }

        [Display(Name = "Out")]
        public bool Out { get; set; }

    }
}
