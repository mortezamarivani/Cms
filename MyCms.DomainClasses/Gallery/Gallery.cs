using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Gallery
{
    public class Gallery
    {
        public Gallery()
        {
        }

        [Key]
        [Display(Name = "GalleryID")]
        public int GalleryID { get; set; }

        [Display(Name = "GalleryDesc")]
        [MaxLength(200)]
        public string GalleryDesc { get; set; }

        //[Required(ErrorMessage = "Plase Enter {0} !")]
        [Display(Name = "GalleryName")]
        [MaxLength(200)]
        public string GalleryName { get; set; }


        [Display(Name = "Status")]
        [DataType(DataType.MultilineText)]
        public  bool Status { get; set; }


        [Display(Name = "CreatedDate")]
        public int CreatedDate { get; set; }


        [Display(Name = "CreatorUserID")]
        public int CreatorUserID { get; set; }

        [Display(Name = "PicRow")]
        public int PicRow { get; set; }

        [Display(Name = "SuffixFile")]
        public string SuffixFile { get; set; }

        [Display(Name = "Languge")]
        public int Languge { get; set; }


    }
}
