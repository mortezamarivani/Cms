using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.ViewModels.Skills
{
    public class ShowSkillsViewModel
    {
        public int SkillsID { get; set; }

        public string SkillsTitle { get; set; }

        public string SkillsDescription { get; set; }
        public string BootstrapClassName { get; set; }
        public int Progress { get; set; }

        public int Languge { get; set; }
        public bool Status { get; set; }


    }
}
