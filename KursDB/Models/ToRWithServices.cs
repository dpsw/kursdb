using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursDB.Models
{
    public class ToRWithServices
    {
        public int ToR_ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Area { get; set; }
        public int CoP { get; set; }
        public List<ServiceShow> services { get; set; }
        public String Services { get; set; }
        public List<Services_View> AllOtherServices { get; set; }

        public void ToDoList(List<Services_View> allServices)
        {
            foreach (var item in services)
            {
                Services += item.Title + " ";
                allServices.RemoveAll(x => x.Title == item.Title);
            }
            AllOtherServices = allServices;
        }
    }
}