using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.ViewModels.Polls
{
    public class ShowPollsViewModel
    {
        public int PollsCode { get; set; }
        public decimal Ip { get; set; }
        public int Status { get; set; }
        public int CreateDate { get; set; }
        public int CountPollsCode { get; set; }
        public int CountAllPolls { get; set; }
        public decimal Average { get; set; }


    }
}
