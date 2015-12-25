using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursDB.Models
{
    public class OrderModel
    {
        public int Number { get; set; }
        public String Date { get; set; }
        public String Date_From { get; set; }
        public String Date_To { get; set; }
    }
}