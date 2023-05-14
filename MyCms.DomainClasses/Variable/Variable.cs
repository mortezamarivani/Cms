using System;
using System.ComponentModel.DataAnnotations;

namespace MyCms.DomainClasses.Variable
{
    public class Variable
    {
        public Variable()
        {
        }

        [Key]
        [Display(Name = "VariableID")]
        public int VariableID { get; set; }

        [Display(Name = "VarName")]
        public string VarName { get; set; }

        [Display(Name = "VarCode")]
        public int VarCode { get; set; }

        [Display(Name = "VarType")]
        public Int16 VarType { get; set; }

    }
}
