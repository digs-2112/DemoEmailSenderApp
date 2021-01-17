using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoEmailSender.Models
{
    public class SendEmailViewModel
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }


        public bool IsCSV { get; set; }
        public bool IsXLS { get; set; }
        public bool IsPDF { get; set; }
    }
}