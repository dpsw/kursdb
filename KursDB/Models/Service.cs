//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KursDB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        public Service()
        {
            this.ToR_Services = new HashSet<ToR_Services>();
        }
    
        public int Service_ID { get; set; }
        public string Title { get; set; }
    
        public virtual ICollection<ToR_Services> ToR_Services { get; set; }
    }
}
