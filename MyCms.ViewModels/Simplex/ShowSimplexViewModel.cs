using System;
using System.ComponentModel.DataAnnotations;

namespace MyCms.ViewModels.Simplex
{
    public class ShowSimplexViewModel
    {
        public int SimplexID { get; set; }
        public int TblNumber { get; set; }
        public int VarNumRow { get; set; }
        public int VarNumCol { get; set; }
        public double Value { get; set; }
        public int InOut { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public string ColVarName { get; set; }
        public string RowVarName { get; set; }
        public int TypeVar { get; set; }
        public int StCode { get; set; }
        public string StValue { get; set; }
        public bool Min { get; set; }
        public bool Out { get; set; }

    }
}
