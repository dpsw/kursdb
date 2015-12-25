using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursDB.Models
{
    public class RoomsWithServices
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public int Room_ID { get; set; }
        public int Price { get; set; }
        public int Area { get; set; }
        public int CoP { get; set; }
        public List<ServiceShow> services {get; set;}
        public String Services { get; set; }
        public void ToDoList()
        {
            foreach (var item in services)
            {
                Services += item.Title + " ";
            }
        }
    }
}