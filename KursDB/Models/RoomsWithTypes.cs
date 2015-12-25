using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursDB.Models
{
    public class RoomsWithTypes
    {
        public int Number { get; set; }
        public int Room_ID { get; set; }
        public List<ToRs_View> AllTypes { get; set; }
    }
}