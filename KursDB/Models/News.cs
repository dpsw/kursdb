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
    
    public partial class News
    {
        public int News_ID { get; set; }
        public int User_ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
