using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Gallery
{
    public class PeriodicElement
    {
        public PeriodicElement()
        {
        }

        [Key]
        public int position { get; set; }

        public int weight { get; set; }

        public string name { get; set; }

        public string symbol { get; set; }


    }
}
