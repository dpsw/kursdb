﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class KursDBEntities : DbContext
    {
        public KursDBEntities()
            : base("name=KursDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Guestbook> Guestbook { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<ToR> ToR { get; set; }
        public virtual DbSet<ToR_Services> ToR_Services { get; set; }
        public virtual DbSet<User_Info> User_Info { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Guestbook_View> Guestbook_View { get; set; }
        public virtual DbSet<News_View> News_View { get; set; }
        public virtual DbSet<Reserve_View> Reserve_View { get; set; }
        public virtual DbSet<Room_View> Room_View { get; set; }
        public virtual DbSet<Rooms_View> Rooms_View { get; set; }
        public virtual DbSet<Services_View> Services_View { get; set; }
        public virtual DbSet<ServiceShow> ServiceShow { get; set; }
        public virtual DbSet<ToR_View> ToR_View { get; set; }
        public virtual DbSet<ToRs_View> ToRs_View { get; set; }
        public virtual DbSet<TypeShow> TypeShow { get; set; }
        public virtual DbSet<Users_View> Users_View { get; set; }
    
        [DbFunction("KursDBEntities", "AllUsers")]
        public virtual IQueryable<AllUsers_Result> AllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<AllUsers_Result>("[KursDBEntities].[AllUsers]()");
        }
    
        [DbFunction("KursDBEntities", "ReserveFromDate")]
        public virtual IQueryable<ReserveFromDate_Result> ReserveFromDate(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ReserveFromDate_Result>("[KursDBEntities].[ReserveFromDate](@DateFrom, @DateTo)", dateFromParameter, dateToParameter);
        }
    
        public virtual int Add_Room(Nullable<int> number, string toR)
        {
            var numberParameter = number.HasValue ?
                new ObjectParameter("Number", number) :
                new ObjectParameter("Number", typeof(int));
    
            var toRParameter = toR != null ?
                new ObjectParameter("ToR", toR) :
                new ObjectParameter("ToR", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Add_Room", numberParameter, toRParameter);
        }
    
        public virtual int AddServiceToToR(Nullable<int> serviceID, Nullable<int> toRID)
        {
            var serviceIDParameter = serviceID.HasValue ?
                new ObjectParameter("ServiceID", serviceID) :
                new ObjectParameter("ServiceID", typeof(int));
    
            var toRIDParameter = toRID.HasValue ?
                new ObjectParameter("ToRID", toRID) :
                new ObjectParameter("ToRID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddServiceToToR", serviceIDParameter, toRIDParameter);
        }
    
        public virtual int AllReservation(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AllReservation", dateFromParameter, dateToParameter);
        }
    
        public virtual int Delete_Comment(Nullable<int> noteID)
        {
            var noteIDParameter = noteID.HasValue ?
                new ObjectParameter("NoteID", noteID) :
                new ObjectParameter("NoteID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Comment", noteIDParameter);
        }
    
        public virtual int Delete_News(Nullable<int> newsID)
        {
            var newsIDParameter = newsID.HasValue ?
                new ObjectParameter("NewsID", newsID) :
                new ObjectParameter("NewsID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_News", newsIDParameter);
        }
    
        public virtual int Delete_Room(Nullable<int> roomID)
        {
            var roomIDParameter = roomID.HasValue ?
                new ObjectParameter("RoomID", roomID) :
                new ObjectParameter("RoomID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Room", roomIDParameter);
        }
    
        public virtual int Delete_Service(Nullable<int> serviceID)
        {
            var serviceIDParameter = serviceID.HasValue ?
                new ObjectParameter("ServiceID", serviceID) :
                new ObjectParameter("ServiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Service", serviceIDParameter);
        }
    
        public virtual int Delete_ToR(Nullable<int> toRID)
        {
            var toRIDParameter = toRID.HasValue ?
                new ObjectParameter("ToRID", toRID) :
                new ObjectParameter("ToRID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_ToR", toRIDParameter);
        }
    
        public virtual int DeleteReserveTable(Nullable<int> go)
        {
            var goParameter = go.HasValue ?
                new ObjectParameter("go", go) :
                new ObjectParameter("go", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteReserveTable", goParameter);
        }
    
        public virtual int DeleteServiceFromTOR(Nullable<int> serviceID, Nullable<int> toRID)
        {
            var serviceIDParameter = serviceID.HasValue ?
                new ObjectParameter("ServiceID", serviceID) :
                new ObjectParameter("ServiceID", typeof(int));
    
            var toRIDParameter = toRID.HasValue ?
                new ObjectParameter("ToRID", toRID) :
                new ObjectParameter("ToRID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteServiceFromTOR", serviceIDParameter, toRIDParameter);
        }
    
        public virtual ObjectResult<PagingNews_Result> PagingNews(Nullable<int> countItemOnPage, Nullable<int> page)
        {
            var countItemOnPageParameter = countItemOnPage.HasValue ?
                new ObjectParameter("CountItemOnPage", countItemOnPage) :
                new ObjectParameter("CountItemOnPage", typeof(int));
    
            var pageParameter = page.HasValue ?
                new ObjectParameter("Page", page) :
                new ObjectParameter("Page", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PagingNews_Result>("PagingNews", countItemOnPageParameter, pageParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int UpdateEntries(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateEntries", userIDParameter);
        }
    
        public virtual int UpdateNews(Nullable<int> newsID, string title, string description)
        {
            var newsIDParameter = newsID.HasValue ?
                new ObjectParameter("NewsID", newsID) :
                new ObjectParameter("NewsID", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateNews", newsIDParameter, titleParameter, descriptionParameter);
        }
    
        public virtual int UpdateNote(Nullable<int> noteID, string comment)
        {
            var noteIDParameter = noteID.HasValue ?
                new ObjectParameter("NoteID", noteID) :
                new ObjectParameter("NoteID", typeof(int));
    
            var commentParameter = comment != null ?
                new ObjectParameter("Comment", comment) :
                new ObjectParameter("Comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateNote", noteIDParameter, commentParameter);
        }
    
        public virtual int UpdatePassword(string password, Nullable<int> userID)
        {
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdatePassword", passwordParameter, userIDParameter);
        }
    
        public virtual int UpdateRoom(Nullable<int> roomID, Nullable<int> number, string typeTitle)
        {
            var roomIDParameter = roomID.HasValue ?
                new ObjectParameter("RoomID", roomID) :
                new ObjectParameter("RoomID", typeof(int));
    
            var numberParameter = number.HasValue ?
                new ObjectParameter("Number", number) :
                new ObjectParameter("Number", typeof(int));
    
            var typeTitleParameter = typeTitle != null ?
                new ObjectParameter("TypeTitle", typeTitle) :
                new ObjectParameter("TypeTitle", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateRoom", roomIDParameter, numberParameter, typeTitleParameter);
        }
    
        public virtual int UpdateService(Nullable<int> serviceID, string title)
        {
            var serviceIDParameter = serviceID.HasValue ?
                new ObjectParameter("ServiceID", serviceID) :
                new ObjectParameter("ServiceID", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateService", serviceIDParameter, titleParameter);
        }
    
        public virtual int UpdateTOR(Nullable<int> tORID, string title, Nullable<int> price, Nullable<int> cOP, Nullable<int> area)
        {
            var tORIDParameter = tORID.HasValue ?
                new ObjectParameter("TORID", tORID) :
                new ObjectParameter("TORID", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(int));
    
            var cOPParameter = cOP.HasValue ?
                new ObjectParameter("COP", cOP) :
                new ObjectParameter("COP", typeof(int));
    
            var areaParameter = area.HasValue ?
                new ObjectParameter("Area", area) :
                new ObjectParameter("Area", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateTOR", tORIDParameter, titleParameter, priceParameter, cOPParameter, areaParameter);
        }
    }
}
