using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Gallery
{
    public class GithubIssue
    {
        public GithubIssue()
        {
        }

        [Key]
        public int id { get; set; }

        public string created { get; set; }

        public string state { get; set; }

        public string number { get; set; }

        public string title { get; set; }

    }
}
