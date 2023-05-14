using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Config
{
    public class Config
    {
        public Config()
        {
        }

        [Key]
        [Display(Name = "ConfigID")]
        public int ConfigID { get; set; }

        [Display(Name = "Languge")]
        public int Languge { get; set; }

        [Display(Name = "ShowReciveInfo")]
        public bool ShowReciveInfo { get; set; }

        [Display(Name = "ShowHome")]
        public bool ShowHome { get; set; }

        [Display(Name = "ShowIDO")]
        public bool ShowIDO { get; set; }

        [Display(Name = "ShowGallery")]
        public bool ShowGallery { get; set; }

        [Display(Name = "ShowProject")]
        public bool ShowProject { get; set; }

        [Display(Name = "ShowSkills")]
        public bool ShowSkills { get; set; }

        [Display(Name = "ShowRank")]
        public bool ShowRank { get; set; }

        [Display(Name = "ShowContactMe")]
        public bool ShowContactMe { get; set; }

        [Display(Name = "ShowCours")]
        public bool ShowCours { get; set; }

        [Display(Name = "ShowEducation")]
        public bool ShowEducation { get; set; }

    }
}
