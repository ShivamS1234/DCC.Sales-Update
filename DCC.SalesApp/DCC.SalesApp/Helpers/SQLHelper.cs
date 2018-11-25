using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Default;
using DCC.SalesApp.Models;
using DevExpress.Mobile.Core.Containers;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Helpers
{
    public class SQLHelper
    {
        static object locker = new object();
        SQLiteConnection database;
        public SQLHelper()
        {
            //database = GetConnection(); //
            database = Xamarin.Forms.DependencyService.Get<ISQLite>().GetConnection();
            // create the tables

            database.CreateTable<UserSetting>();
            database.CreateTable<Users>();
            database.CreateTable<UserAttendance>();
            database.CreateTable<UserSafety>();
            database.CreateTable<ItemMovements>();
            database.CreateTable<States>();
            database.CreateTable<PermissionGroupH>();
            database.CreateTable<PermissionGroupL>();
            database.CreateTable<UserPerm>();
            database.CreateTable<Items>();
            database.CreateTable<Countries>();
            database.CreateTable<Reasons>();
            database.CreateTable<Districts>();
            database.CreateTable<Cities>();
            database.CreateTable<ActivitySubjects>();
            database.CreateTable<ItemPrices>();
            database.CreateTable<Warehouses>();
            database.CreateTable<ITMSubCat>();
            database.CreateTable<PriceList>();
            database.CreateTable<CompanyDetails>();
            database.CreateTable<ItemWhs>();
            database.CreateTable<Retailers>();
            database.CreateTable<Currencies>();
            database.CreateTable<ITMCategory>();
            database.CreateTable<Areas>();
            database.CreateTable<AreaUserMapping>();
            database.CreateTable<OrderAttachments>();
            database.CreateTable<OrderItems>();
            database.CreateTable<Orders>();
            database.CreateTable<ActivityTypes>();
            database.CreateTable<ActivityStatus>();
            database.CreateTable<Activities>();
            database.CreateTable<Outlets>();
            database.CreateTable<SalesSchemesPromos>();
            database.CreateTable<VATD>();
            database.CreateTable<BPGroup>();
            database.CreateTable<Currencies>();

            database.CreateTable<ActivityActions>();
            database.CreateTable<BPClass>();
            database.CreateTable<MeetingLocations>();
            database.CreateTable<Positions>();
            database.CreateTable<Permission>();
            database.CreateTable<RetailerContacts>();
            database.CreateTable<OutletContacts>();
            database.CreateTable<RetailerAddress>();
            database.CreateTable<VATH>();
            database.CreateTable<Delivery>();
            database.CreateTable<DeliveryItems>();
            database.CreateTable<DeliveryAttachments>();
            database.CreateTable<Quotations>();
            database.CreateTable<UserLocationTracking>();
            database.CreateTable<Invoices>();
            database.CreateTable<QuotationItems>();
            database.CreateTable<InvoiceItems>();
            database.CreateTable<InvoiceAttachments>();
            database.CreateTable<QuotationAttachments>();
            database.CreateTable<PayMeans>();
            database.CreateTable<CashReceipts>();

            database.CreateTable<UserEOD>();

            database.CreateTable<Address>();
        }

        internal  void UpdateRetailerLocation(double lat, double lon, int id)
        {
            //throw new NotImplementedException();
             database.Query<Retailers>("update Retailers set Latitude =? , Longitude =? where Id =? ",lat, lon, id);
        }

        //public SQLite.SQLiteConnection GetConnection()
        //{
        //    //SQLiteConnection sqlitConnection;
        //    //var sqliteFilename = "DCCSalesApp.db3";
        //    //IFolder folder = FileSystem.Current.LocalStorage;
        //    //string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
        //    //sqlitConnection = new SQLiteConnection(path, false);
        //    //return sqlitConnection;



        //}
        public int nextTableID(string TableName)
        {
            lock (locker)
            {
                return database.ExecuteScalar<Int32>("SELECT  ifnull(MAX(Id),0) + 1 FROM " + TableName);
            }
        }
        public int TotalTableRows(string TableName)
        {
            lock (locker)
            {
                return database.ExecuteScalar<Int32>("SELECT  ifnull(Count(1),0) FROM " + TableName);
            }
        }

        #region User functions
        public IEnumerable<Users> GetAllUserDetails()
        {
            lock (locker)
            {
                return (from i in database.Table<Users>() select i).ToList();
            }
        }

        public IEnumerable<UserAttendance> GetAllUserAttendances()
        {
            lock (locker)
            {
                return (from i in database.Table<UserAttendance>() select i).ToList();
            }
        }

        public Users GetUserDetails(Int32 _id)
        { 
            lock (locker)
            {
                return database.Table<Users>().FirstOrDefault(x => x.ID == _id);
            }
        }
        public UserSetting GetUserSetting()
        {
            lock (locker)
            {
                return database.Table<UserSetting>().FirstOrDefault();
            }
        }
        public int UpdateUserSetting(UserSetting oUserSetting, bool isNew)
        {
            lock (locker)
            {
                if (isNew)
                    return database.Insert(oUserSetting);
                else
                    return database.Update(oUserSetting);
            }
        }

        public UserAttendance GetUserLastLogin(Int32 _id)
        {
            lock (locker)
            {
                return database.Table<UserAttendance>().OrderByDescending(x => x.LoginTime).FirstOrDefault(x => x.UserId == _id);
            }
        }

        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<Users>(id);
            }
        }
        public IEnumerable<UserSafety> GetAllUserSafetyDetails()
        {
            lock (locker)
            {
                return (from i in database.Table<UserSafety>() select i).ToList();
            }
        }
        public int SaveUserSafety(UserSafety oUserSafety)
        {
            lock (locker)
            {
                return database.Insert(oUserSafety);
            }
        }
        public int UpdateUserDetail(Users oUser)
        {
            lock (locker)
            {
                return database.Execute(string.Format(@"Update Users SET Email='{0}', FirstName ='{1}', Address ='{2}', MiddleName='{3}',LastName='{4}',MobileNo ='{5}',Password ='{6}', ProfileImg ='{7}' where ID={8}    ", oUser.Email, oUser.FirstName, oUser.Address, oUser.MiddleName, oUser.LastName, oUser.MobileNo, oUser.Password, oUser.ProfileImg, oUser.ID));
            }
        }
        public int SaveUserAttendance(UserAttendance oUserAttendance)
        {
            lock (locker)
            {
                return database.Insert(oUserAttendance);
            }
        }



        #endregion

        #region Retailer functions
        //public IEnumerable<Retailers> GetAllRetailersDetails()
        //{

        //    lock (locker)
        //    {
        //        return database.Query<Customer>(@"select * from Retailers ");
        //    }
        //}
        public Retailers GetRetailer(Int32 ID)
        {
            lock (locker)
            {
                return database.Table<Retailers>().FirstOrDefault(x => x.ID == ID);
            }
        }

        public string GetAreaName(Int32 _ID)
        {
            lock (locker)
            {
                return database.Table<Areas>().FirstOrDefault(x => x.ID == _ID).AreaCode;
            }
        }
        public int updateRetails(Retailers oRetailer)
        {
            lock (locker)
            {
                return database.Update(oRetailer);

            }
        }

        #endregion

        #region warehouse functions
        public BindingList<Items> GetAllItems()
        {
            lock (locker)
            {

                var oItems = new BindingList<Items>();
                var jj = (from i in database.Table<Items>() select i).ToList();
                foreach (var j in jj)
                {
                    oItems.Add(j);
                }
                return oItems;
            }
        }
        public ObservableCollection<StockWarehouse> GetAllWarehouses()
        {

            lock (locker)
            {
                Int32 a = database.Table<Warehouses>().Count();
                return (new ObservableCollection<StockWarehouse>
                    (from info in database.Query<StockWarehouse>(@"SELECT T0.ID, T0.Code, T0.Name, T0.Location,T1.Name as City
                FROM Warehouses T0 INNER JOIN Cities T1 ON T0.City = T1.ID   order by T0.Name") select info));
            }
        }
        public IEnumerable<ITMCategory> GetAllCategoriesforWarehouse(int _WhsID)
        {
            lock (locker)
            {

                return database.Query<ITMCategory>(@"SELECT Distinct T0.*
FROM  [ITMCategory] T0
Inner JOIN Items T1 on T0.ID = T1.CatID
Inner JOIN ItemWhs T2 on T2.ItemID  = T1.ID and T2.WhsID = ?", _WhsID.ToString()).ToList();

            }
        }
        public IEnumerable<ItemStocks> GetAll_ItemsStock(int _WhsID)
        {
            lock (locker)
            {
                return database.Query<ItemStocks>(@" SELECT  T0.Name,T0.Code ,T0.UOM ,T0.ItemImage ,T2.Price as SalesPrice,T3.OnHand,T2.Currency from  Items T0 
                                                         INNER JOIN ITMCategory T1 on T0.CatID = T1.ID
                                                         INNER JOIN ItemPrices T2 on T2.ItemID=T0.Id and T2.PriceListID = 1
                                                         INNER JOIN ItemWhs T3 on T3.ItemID=T0.Id and  T3.WhsID = ?", _WhsID.ToString()).ToList();

            }
        }
        public IEnumerable<ItemsStock> ShowCategoryWiseItems(int _WhsID, int _CatID)
        {
            lock (locker)
            {

                return database.Query<ItemsStock>(@"SELECT T1.ID, T1.Code, T1.Name, SUM(T2.OnHand) OnHand ,SUM(T2.Commited) Commited ,SUM(T2.Ordered) Ordered ,
SUM(T3.Price) SalesPrice ,SUM(T4.Price) WholePrice

FROM[ITMCategory] T0
Inner JOIN Items T1 on T0.ID = T1.CatID and T1.CatID = " + _CatID + @" 
Inner JOIN ItemWhs T2 on T2.ItemID  = T1.ID and T2.WhsID = ?
Left Outer JOIN ItemPrices T3 on T3.ItemID = T1.ID and T3.PriceListID = 2
Left Outer JOIN ItemPrices T4 on T4.ItemID = T1.ID and T4.PriceListID = 1
Group by T1.ID, T1.Code, T1.Name", _WhsID.ToString()).ToList();
            }
        }
        internal IEnumerable<Items> GetAllProduct_Cat_SubCat(int _item_cat, int _item_sub_cat)
        {
            return database.Table<Items>().Where(x => x.CatID == _item_cat && x.SubCatId == _item_sub_cat).ToList();
        }
        internal ObservableCollection<ITMSubCat> GetAllProductSubCatagory(int id)
        {
            lock (locker)
            {
                return new ObservableCollection<ITMSubCat>(database.Table<ITMSubCat>().Where(x => x.ParentCategoryID == id).OrderBy(x => x.ID));
            }
        }
        internal ObservableCollection<ITMCategory> GetAllProductCatagory()
        {
            lock (locker)
            {
                return new ObservableCollection<ITMCategory>(from i in database.Table<ITMCategory>() orderby i.ID select i);
            }
        }

        #endregion

        #region Notes  functions
        public IEnumerable<NotesInfo> GetNotes()
        {
            try
            {
                lock (locker)
                {
                    return database.Query<NotesInfo>(@"SELECT  T0.Details,T0.ID,T0.StartDate ,T0.StartTime ,T0.EndDate ,T0.EndTime ,T0.Status ,T0.Notes,T0.Priority ,T0.BPCode ,T0.Attachment,
   T1.Name as ActivityName, T2.Name as SubjectName,T3.Name as TypeName ,T4.AreaCode  as Location from  Activities T0 
 inner join ActivityTypes T1 on T0.TypeID = T1.ID
 inner join ActivitySubjects T2 on T0.SubjectID=T2.Id 
inner join ActivityActions T3 on T0.ActionID=T3.Id 
   inner join Areas T4 on T0.Location = T4.Id");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ActivitySubjects> GetAllSubject()
        {
            lock (locker)
            {
                return (from i in database.Table<ActivitySubjects>() select i).ToList();
            }
        }
        public IEnumerable<ActivityActions> GetAllActiviStatus()
        {
            lock (locker)
            {
                return (from i in database.Table<ActivityActions>() select i).ToList();
            }
        }
        public IEnumerable<ActivityTypes> GetAllActivityType()
        {
            lock (locker)
            {
                return (from i in database.Table<ActivityTypes>() select i).ToList();
            }
        }
        public int SaveNotes(Activities oActivities)
        {

            lock (locker)
            {
                return database.Insert(oActivities);
            }

            // throw new NotImplementedException();
        }
        public IEnumerable<NotesInfo> GetNotesOnly(int _ID)
        {
            lock (locker)
            {
                IEnumerable<NotesInfo> x;
                x = database.Query<NotesInfo>(@" SELECT  T0.Details,T0.ID,T0.StartDate ,T0.StartTime ,T0.EndDate ,T0.EndTime ,T0.Status ,T0.Notes,T0.Priority,T0.Attachment,
   T1.Name as ActivityName, T2.Name as SubjectName,T3.Name as TypeName ,T4.AreaCode  as Location from  Activities T0  
   inner join ActivityTypes T1 on T0.TypeID = T1.ID 
   inner join ActivitySubjects T2 on T0.SubjectID=T2.Id   
   inner join ActivityActions T3 on T0.ActionID=T3.Id 
   inner join Areas T4 on T0.Location = T4.Id and T0.Id =?", _ID.ToString()).ToList();
                return x;
            }

        }

        public IEnumerable<Activities> GetAllActivities()
        {
            lock (locker)
            {
                return (from i in database.Table<Activities>() select i).ToList();
            }
        }
        #endregion

        #region Customer functions

        public Retailers GetRetailerDetails(Int32 _id)
        {
            lock (locker)
            {
                return database.Table<Retailers>().FirstOrDefault(x => x.ID == _id);
            }
        }

        public int AddRetailer(Retailers _Retailers)
        {            
            lock (locker)
            {                
                return database.Insert(_Retailers);
            }
        }

        public int UpdateRetailer(Retailers oRet)
        {
            lock (locker)
            {
                return database.Update(oRet);
            }
        }
        public ObservableCollection<SaleProduct> getSaleProducts(Int32 _OrderID)
        {
            lock (locker)
            {
                if (_OrderID > 0)
                {
                    return new ObservableCollection<SaleProduct>(database.Query<SaleProduct>(@"
                select I.Id, I.UoM , Code, Name, Price, IW.OnHand, I.ItemImage, I.SaleVat, VD.TaxPer,QI.Quantity SaleQty,ifnull(QI.Id,0) LineId,
                QI.CGuid,QI.ID as OrderItemId 
                from Items I 
                INNER JOIN ItemPrices IP ON i.id=ip.ItemID and IP.PriceListID =1
                INNER JOIN (select ItemID,SUM(OnHand) OnHand FROM ItemWHS GROUP BY  ItemID) IW ON i.id=iw.ItemID  
                INNER JOIN VATH VH ON i.SaleVat=vh.ID 
                INNER JOIN VATD VD  ON vh.id =vd.vatid
                LEFT OUTER JOIN Orders Q ON Q.ID = '" + _OrderID + @"'  
                LEFT OUTER JOIN OrderItems  QI ON   Q.CGuid =  QI.CGuid     and I.Id = QI.ItemID 
                  Order by  ifnull(QI.LineNum,999)  "));
                }
                else
                {

                    return new ObservableCollection<SaleProduct>(database.Query<SaleProduct>(@"
                select I.Id, I.UoM , Code, Name, Price, IW.OnHand, I.ItemImage, I.SaleVat, VD.TaxPer , 0 SaleQty, 0 LineId
                from Items I 
                INNER JOIN ItemPrices IP ON i.id=ip.ItemID and IP.PriceListID =1
                INNER JOIN (select ItemID,SUM(OnHand) OnHand FROM ItemWHS GROUP BY  ItemID) IW ON i.id=iw.ItemID  
                INNER JOIN VATH VH ON i.SaleVat=vh.ID 
                INNER JOIN VATD VD  ON vh.id =vd.vatid                                                  
                                                    "));
                }
            }
        }
        public ObservableCollection<RetailerAddress> getCustomerAddress(int custID)
        {
            lock (locker)
            {
                return (new ObservableCollection<RetailerAddress>
                    (from info in database.Query<RetailerAddress>(@"Select * from RetailerAddress  where RetailerID=?", custID) select info));
            }
        }
        public IEnumerable<Customer> GetAllCustomerDetails()
        {
            lock (locker)
            { 
                return database.Query<Customer>(@"select * from Retailers ");
            }
        }

        public IEnumerable<Retailers> GetAllCustomer()
        {
            lock (locker)
            {
                return (from i in database.Table<Retailers>() orderby i.Name select i).ToList();
            }
        }
        public Customer GetCustomerbyCode(string Code)
        {
            lock (locker)
            {
                return database.Query<Customer>(@"select * from Retailers where Code = '" + Code + "'").FirstOrDefault();
            }
        }
        public IEnumerable<CustomerContact> GetCustomerContact(int customerid)
        {
            lock (locker)
            {
                return database.Query<CustomerContact>(@" select r.id, rc.ID as contId, rc.FirstName, rc.MiddleName, rc.LastName, Phone1 from retailers r
                                                        inner join retailercontacts rc
                                                        on r.id=rc.retailerid
                                                        where r.Id=?", customerid);
            }
        }


        public IEnumerable<Areas> GetAllLocation()
        {
            lock (locker)
            {
                return (from i in database.Table<Areas>() select i).ToList();
            }
        }

        #endregion

#region Customer_Address
        public int AddCustomerAddress(Address _Address)
        {
            lock (locker)
            {
                return database.Insert(_Address);
            }
        }

        public IEnumerable<AddressInfo> GetCustomerAddress()
        {
            try
            {
                lock (locker)
                {
                    return database.Query<AddressInfo>(@"SELECT  CustomerT.Name CustomerName, CustomerT.OwnerEmail Email,  CustomerT.OwnerMobileNo MobileNo, AddressT.Address1 Address from Retailers CustomerT inner join Address AddressT on CustomerT.Code = AddressT.CustomerCode");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
#endregion

        #region Orders functions

        internal ObservableCollection<Orders> GetAllOrders()
        {
            lock (locker)
            {
                return new ObservableCollection<Orders>((from i in database.Table<Orders>() orderby i.PostDate select i));
            }
        }
        public int SaveOrder(Orders objorder)
        {
            lock (locker)
            {
                //List<Orders> o=database.Table<Orders>().ToList();
                return database.Insert(objorder);
            }
        }
        public int insertOrderItems(OrderItems orderItems, Int32 OrderLineID)
        {
            lock (locker)
            {
                if (OrderLineID > 0)
                    return database.Update(orderItems);
                else
                    return database.Insert(orderItems);
            }
        }
        public int insertOrder(Orders order)
        {
            //lock (locker)
            //{
            //    return database.Insert(order);
            //}
            lock (locker)
            {
                if (database.Table<Orders>().Where(x => x.ID == order.ID).Count() > 0)
                    return database.Update(order);
                else
                    return database.Insert(order);
            }
        }
        internal void updateOrderItemsAll(List<OrderFulfillNew> lst)
        { 
        lock (locker)
                {
                    foreach (Models.OrderFulfillNew sp in lst)
                    {
                        database.Query<Models.OrderFulfillNew>(@"update OrderItems set DeliveredQty = OpenQty, OpenQty=0
                                                            where CGuid=?", sp.CGuid);
                    }
                }
        }
        internal void updateOrderItems(List<SaleProduct> lst)
        {
            //throw new NotImplementedException();            
            string CGUID = lst[0].CGuid;
            lock (locker)
            {
                foreach (Models.SaleProduct sp in lst)
                {

                    if (sp.OpenQty > 0)
                    {
                        database.Query<Models.SaleProduct>(@"update OrderItems set OpenQty=OpenQty-?, DeliveredQty=DeliveredQty+?
                                                            where CGuid=? and ItemID=?", sp.SaleQty, sp.SaleQty, sp.CGuid, sp.id);
                    }
                    else if (sp.OpenQty == 0 && sp.SaleQty > 0)
                    {
                        OrderItems orderItems = new OrderItems();
                        orderItems.ID = App.database.nextTableID("OrderItems");
                        orderItems.CGuid = CGUID;
                        orderItems.LineNum = (database.ExecuteScalar<Int32>("SELECT  ifnull(MAX(LineNum),0) + 1 FROM " + "OrderItems")) + 1;
                        orderItems.ItemID = sp.id;
                        orderItems.VATCode = sp.SaleVat;
                        orderItems.ItemName = sp.name;

                        orderItems.LineTax = Convert.ToDecimal(sp.TaxPer * sp.SaleQty);

                        orderItems.LineStatus = "x";
                        orderItems.Quantity = Convert.ToDecimal(sp.SaleQty);
                        orderItems.DeliveredQty = 0;
                        orderItems.Rate = (decimal)sp.price;
                        orderItems.OpenQty = Convert.ToDecimal(sp.SaleQty);
                        App.database.insertOrderItems(orderItems, 0);
                    }
                }
            }

        }

        internal IEnumerable<Orders> GetOrders(string guid)
        {
            lock (locker)
            {
                return database.Query<Orders>("Select * from Orders where CGuid=?", guid);
            }
        }
        internal IEnumerable<OrderItems> GetOrdersItems(string guid)
        {
            lock (locker)
            {
                return database.Query<OrderItems>("Select * from OrderItems where CGuid=?", guid);
            }
        }

        public Orders GetOrder(Int32 _ID)
        {
            lock (locker)
            {

                return database.Table<Orders>().FirstOrDefault(x => x.ID == _ID);
            }
        }
        #endregion

        #region Order Fulfillment
        public IEnumerable<Orders> GetAllorderFullFill()
        {
            lock (locker)
            {
                return (from i in database.Table<Orders>() select i).ToList();
            }
        }
        public IEnumerable<OrdersFullFill> GetfullfillOrderOnly(int _ID)
        {
            lock (locker)
            {
                IEnumerable<OrdersFullFill> x;

                x = database.Query<OrdersFullFill>(@" SELECT 
                                                     T0.CustRefNo,
                                                  T0.CustCode,
                                                  T0.CustName,
                                                  T1.ItemName,
                                                  T1.Quantity,
                                                  T1.UnitPrice,
                                                  T1.OpenQty,
                                                    T2.ItemImage
                                                     FROM  [Orders] T0  
                                           Inner JOIN OrderItems T1 on T0.CGuid = T1.CGuid  
                                           Inner JOIN Items T2 on T2.ID  = T1.ItemID and T0.ID =?", _ID.ToString()).ToList();
                return x;

            }

        }
        public ObservableCollection<Models.SaleProduct> getOrderFulfillmentProduct(string cguid)
        {
            lock (locker)
            {
                return new ObservableCollection<Models.SaleProduct>(database.Query<Models.SaleProduct>(@"select i.ID, ItemImage, name, code, Quantity, UoM, Price, oi.OpenQty, oi.OpenQty as SaleQty, oi.CGuid  from items i
                                                            left join orderitems oi
                                                            on i.ID=oi.ItemID
                                                            left join ItemPrices ip
                                                            on i.id=ip.ItemId
                                                            where oi.CGuid=? OR oi.CGuid is null
                                                            order by oi.OpenQty desc
                                                            ", cguid /*orderId*/));

            }
        }

        public ObservableCollection<Models.OrderFulfillNew> GetAllorderFullFillNew(string CustCode)
        {
            lock (locker)
            {
                if (string.IsNullOrEmpty(CustCode))
                {
                    return new ObservableCollection<OrderFulfillNew>( database.Query<Models.OrderFulfillNew>(@"select Distinct  o.ID CustName,CustCode, ContPerson, rc.FirstName,rc.MiddleName,rc.LastName, a.AreaCode, 
                                                                o.id as OrderId, o.PostDate,
                                                                o.DueDate, o.RoundingAmnt,o.CGuid
                                                                from orders o
																inner join OrderItems oi
																on o.CGUID=oi.CGUID
                                                                inner join Retailers r
                                                                on o.CustCode=r.Code
                                                                inner join RetailerContacts rc
                                                                on rc.id=o.ContPerson
                                                                inner join Areas a
                                                                on r.AreaID=a.ID
																where oi.OpenQty>0"));
                }
                else
                {

                    return new ObservableCollection<OrderFulfillNew>(database.Query<Models.OrderFulfillNew>(@"select o.ID CustName,CustCode, ContPerson, rc.FirstName,rc.MiddleName,rc.LastName, a.AreaCode, 
                                                                o.id as OrderId, o.PostDate,
                                                                o.DueDate, o.RoundingAmnt,o.CGuid
                                                                from orders o
                                                                inner join Retailers r
                                                                on o.CustCode=r.Code and o.CustCode = '" + CustCode + @"'
                                                                inner join RetailerContacts rc
                                                                on rc.id=o.ContPerson
                                                                inner join Areas a
                                                                on r.AreaID=a.ID"));
                }
            }
        }

        #endregion

        #region Delivery functions
        internal int insertDelivery(Delivery delivery)
        {
            lock (locker)
            {
                return database.Insert(delivery);
            }
        }
        internal int insertDeliveryItems(DeliveryItems deliveryItems)
        {
            lock (locker)
            {
                return database.Insert(deliveryItems);
            }
        }

        #endregion

        #region My Account functions
        public IEnumerable<Transaction> GetTransactions(int UserId)
        {

            lock (locker)
            {
                return database.Query<Transaction>(String.Format(@"

SELECT 'Order' as TransactionType, ID as SalesOrder,CustName, DueDate , DocTotal  from Orders
WHERE DATE(CreatedDate) <= DATE('now') AND  DATE(CreatedDate) >= DATE('now') and UserId = {0}

 UNION ALL 

SELECT 'Notes' as TransactionType, Id as SalesOrder ,BPName,StartDate,0 DocTotal from  Activities 
WHERE  DATE(CreatedDate) <= DATE('now') AND  DATE(CreatedDate) >= DATE('now') and  UserId = {0}

 UNION ALL 

SELECT 'Quotation' as TransactionType, ID as SalesOrder,CustName,DueDate, DocTotal from Quotations
WHERE DATE(CreatedDate) <= DATE('now') AND  DATE(CreatedDate) >= DATE('now') and UserId = {0} ", UserId));
            }
        }

        public string GetDesignationName(int key)
        {
            try
            {
                //string value = (from i in mSqlConnection.Table<BrandingInfoModel>() select key).ToString();
                string value = database.Table<Positions>().FirstOrDefault(x => x.ID == key).Name;
                return value;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
        }

        public IEnumerable<Outlets> GetAllOutlets()
        {
            lock (locker)
            {
                return (from i in database.Table<Outlets>() select i).ToList();
            }
        }

        public ObservableCollection<EodOutlets> GetAllEODOutlets(string isPJp)
        {

            lock (locker)
            {
                return (new ObservableCollection<EodOutlets>
                    (from info in database.Query<EodOutlets>(string.Format(@"SELECT ID, Code, Name, 0 Selected, IsPJP  FROM Outlets where IsPjP ='{0}'", isPJp)) select info));
            }
        }



        #endregion

        #region Quotations Functions
        public int insertQuotations(Quotations quotation)
        {
            lock (locker)
            {
                if (database.Table<Quotations>().Where(x => x.ID == quotation.ID).Count() > 0)
                    return database.Update(quotation);
                else
                    return database.Insert(quotation);
            }
        }

        public int insertQuotationsProducts(QuotationItems quotationItems, Int32 QuotationLineID)
        {
            lock (locker)
            {
                if (QuotationLineID > 0)
                    return database.Update(quotationItems);
                else
                    return database.Insert(quotationItems);
            }
        }
        public ObservableCollection<SaleProduct> getQuotationProductsByCatagory(int cat = 0, int subcat = 0)
        {
            lock (locker)
            {
                return new ObservableCollection<SaleProduct>(database.Query<SaleProduct>(@"select i.id, code, i.CatId, i.SubCatId, name, price, iw.OnHand, i.ItemImage
                                                     from Items i 
                                                     inner join itemPrices ip
                                                     on i.id=ip.ItemID
                                                     inner join ItemWHS iw
                                                     on i.id=iw.ItemID
              where i.CatId= case when ? >0 then ? else  i.CatId end 
              and i.SubCatId= case when ? >0 then ? else  i.SubCatId end                                                    
                                                    ", cat, cat, subcat, subcat));

            }
        }

        public ObservableCollection<SaleProduct> getQuotationProducts(Int32 _QuotationID)
        {
            lock (locker)
            {
                if (_QuotationID > 0)
                {
                    return new ObservableCollection<SaleProduct>(database.Query<SaleProduct>(@"
                select I.Id, I.UoM , Code, Name, Price, IW.OnHand, I.ItemImage, I.SaleVat, VD.TaxPer,QI.Quantity SaleQty,ifnull(QI.Id,0) LineId,
                QI.CGuid,QI.ID as QuotationItemId
                from Items I 
                INNER JOIN ItemPrices IP ON i.id=ip.ItemID and IP.PriceListID =1
                INNER JOIN (select ItemID,SUM(OnHand) OnHand FROM ItemWHS GROUP BY  ItemID) IW ON i.id=iw.ItemID  
                INNER JOIN VATH VH ON i.SaleVat=vh.ID 
                INNER JOIN VATD VD  ON vh.id =vd.vatid
                LEFT OUTER JOIN Quotations Q ON Q.ID = '" + _QuotationID + @"'  
                LEFT OUTER JOIN QuotationItems  QI ON   Q.CGuid =  QI.CGuid     and I.Id = QI.ItemID 
                  Order by  ifnull(QI.LineNum,999)  "));
                }
                else
                {

                    return new ObservableCollection<SaleProduct>(database.Query<SaleProduct>(@"
                select I.Id, I.UoM , Code, Name, Price, IW.OnHand, I.ItemImage, I.SaleVat, VD.TaxPer , 0 SaleQty, 0 LineId
                from Items I 
                INNER JOIN ItemPrices IP ON i.id=ip.ItemID and IP.PriceListID =1
                INNER JOIN (select ItemID,SUM(OnHand) OnHand FROM ItemWHS GROUP BY  ItemID) IW ON i.id=iw.ItemID  
                INNER JOIN VATH VH ON i.SaleVat=vh.ID 
                INNER JOIN VATD VD  ON vh.id =vd.vatid                                                  
                                                    "));
                }
            }
        }

       

        #endregion

        #region Invoice Functions
        public IEnumerable<Invoices> GetOpenInvoices(string CustCode)
        {
            lock (locker)
            {
                return (from i in database.Table<Invoices>() select i).Where(x => x.Status == "1" && x.CustCode == CustCode).ToList();
            }
        }
        public IEnumerable<Models.InvoiceDetails> GetInvoiceDetail()
        {
            lock (locker)
            {
                return database.Query<InvoiceDetails>(@"SELECT  T0.Name,T0.Code ,T0.UOM ,T0.ItemImage,T1.Quantity,T1.UnitPrice,T1.VATPer,T1.LineTotalLC  
from  Items T0 
join InvoiceItems T1 on T0.ID =T1.ID ");
            }
        }

        public IEnumerable<OrdersFullFill> GetInvoicebyID(int _ID)
        {
            lock (locker)
            {
                IEnumerable<OrdersFullFill> x;

                x = database.Query<OrdersFullFill>(@" SELECT 
                                                     T0.CustRefNo,
                                                  T0.CustCode,
                                                  T0.CustName,
                                                  T1.ItemName,
                                                  T1.Quantity,
                                                  T1.UnitPrice,
                                                  T1.OpenQty,
                                                    T2.ItemImage
                                                     FROM  [invoices] T0  
                                           Inner JOIN InvoiceItems T1 on T0.CGuid = T1.CGuid  
                                           Inner JOIN Items T2 on T2.ID  = T1.ItemID and T0.ID =?", _ID.ToString()).ToList();
                return x;

            }
        }

        public Invoices GetInvoice(Int32 _ID)
        {
            lock (locker)
            {

                return database.Table<Invoices>().FirstOrDefault(x => x.ID == _ID);
            }
        }

        #endregion

        #region Schemes
        public ObservableCollection<Schemes> GetAllScheme()
        {

            lock (locker)
            {
                return (new ObservableCollection<Schemes>
                    (from info in database.Query<Schemes>(@"SELECT  T0.Name,T0.Code ,T0.ItemImage,T1.Quantity ,T1.FreeQty,T1.SchemeName,T1.SchemeCode,T1.DiscountPer as Discount,T1.ApplicableTo from  Items T0 
inner join SalesSchemesPromos T1 on T0.ID=T1.ID  order by T0.Name") select info));
            }
        }
        #endregion

        #region Quotations
        internal ObservableCollection<Quotations> GetAllQuotations()
        {
            lock (locker)
            {
                return new ObservableCollection<Quotations>((from i in database.Table<Quotations>() orderby i.PostDate select i));
            }
        }

        public Quotations GetQuotation(Int32 _id)
        {
            lock (locker)
            {
                return database.Table<Quotations>().FirstOrDefault(x => x.ID == _id);
            }
        }
        #endregion

        #region Cash From Customer
        public IEnumerable<Currencies> GetAllCurrencies()
        {
            lock (locker)
            {
                return (from i in database.Table<Currencies>() select i).ToList();
            }
        }


        public IEnumerable<PayMeans> GetAllPayment()
        {
            lock (locker)
            {
                return (from i in database.Table<PayMeans>() select i).ToList();
            }
        }


        public int SaveCustomerCash(CashReceipts oCashReceipt)
        {

            lock (locker)
            {
                return database.Insert(oCashReceipt);
            }

            // throw new NotImplementedException();
        }

        #endregion

        #region UserPerformance
        internal Users getUser(int id)
        {
            lock (locker)
            {
                return database.Get<Users>(id);
            }
        }
        internal UserEOD outletVisited(int userID, DateTime date)
        {
            lock (locker)
            {
                return database.FindWithQuery<UserEOD>("select * from UserEOD where EODDate =? and UserID = ?",date,userID);
            }
        }

        internal int deliveryMadeToday(int ID)
        {
            lock (locker)
            {
                return database.Query<int>("Select * from Delivery d inner join Users u on d.UserID=u.ID where UserId=? and DueDate=date('now') ", ID).Count;
            }
        }
        internal int openOrderToday(int ID)
        {
            lock (locker)
            {
                return database.Query<int>("Select * from Orders o inner join Users u on o.UserID=u.ID where UserId=? and DueDate=date('now') ", ID).Count;
            }
        }

        internal int openOrderYesterday(int ID)
        {
            lock (locker)
            {
                return database.Query<int>("Select * from Orders o inner join Users u on o.UserID=u.ID where UserId=? and DueDate=date('now') - 1 ", ID).Count;
            }
        }

        internal IEnumerable <CashReceipts> paymentReceivedToday(int ID)
        {
            lock (locker)
            {
                return database.Query<CashReceipts>("select Amount from CashReceipts where UserID = ? and CreatedDate = date('now'); ", ID);
            }
        }

        internal int closedOrderToday(int ID)
        {
            lock (locker)
            {
                return database.Query<int>("select * from Users u inner join Orders o on u.ID = o.UserID inner join OrderItems oi on o.CGuid = oi.CGUID where o.DueDate = date('now') and OpenQty = 0", ID).Count;
            }
        }

        #endregion

        #region DSRReport
        internal ObservableCollection<Models.User> getUsers()
        {
            lock (locker)
            {
                return new ObservableCollection<Models.User>( database.Query<Models.User>("Select * from users")); 
            }
        }

        internal ObservableCollection<Models.Warehouse> getWarehouses()
        {
            lock (locker)
            {
                return new ObservableCollection<Models.Warehouse>(database.Query<Models.Warehouse>("select * from Warehouses"));
            }
        }

        internal ObservableCollection<Models.Area> getAreas()
        {
            lock (locker)
            {
                return new ObservableCollection<Models.Area>(database.Query<Models.Area>("select * from Areas"));
            }
        }

        internal ObservableCollection<Models.DSRReport> getOrders()
        {
            lock (locker)
            {
                return new ObservableCollection<Models.DSRReport>(database.Query<Models.DSRReport>(@"select a.areaCode as region,  o.custName, o.PostDate, o.UserID, o.id as orderNo, oi.Itemname, oi.Quantity, oi.DeliveredQty, oi.WhsCode,oi.OpenQty, oi.Rate, oi.Quantity*oi.Rate as TotalAmount from
                                                                                                    orders o inner join orderItems oi
                                                                                                    on o.cguid=oi.cguid
                                                                                                    left join Retailers r
                                                                                                    on r.Code=o.custCode
                                                                                                    left join Areas a
                                                                                                    on r.AreaID=a.id"));
            }
        }
        #endregion

        #region Detail Order Report
        internal ObservableCollection<Retailers> getCustomers()
        {
            lock (locker)
            {
                return new ObservableCollection<Retailers>(database.Query<Retailers>(@"select * from Retailers"));
            }
        }

        internal ObservableCollection<DetailOrderReport> getDetailOrderReport()
        {
            lock (locker)
            {
                return new ObservableCollection<DetailOrderReport>(database.Query<DetailOrderReport>(@"Select	u.id as UserID,	a.ID AreaID, a.AreaCode, u.FirstName, u.MiddleName, u.LastName, o.CustName, o.PostDate, o.ID, oi.ItemName, oi.Quantity, oi.DeliveredQty, oi.OpenQty, oi.Rate  
                                                                                                        from		Orders o
                                                                                                        inner join	OrderItems oi
                                                                                                        on			o.CGUID=oi.CGUID
                                                                                                        inner join	Users u
                                                                                                        on			o.UserId=u.id
																										inner join	Retailers r
																										on r.Code=o.CustCode
																										inner join	areas a
																										on r.AreaID= a.ID"));
            }
        }
        #endregion

        #region Regionwise Report

        internal ObservableCollection<RegionWiseReport> getRegionWiseReport()
        {
            lock (locker)
            {
                return new ObservableCollection<RegionWiseReport>(database.Query<RegionWiseReport>(@"Select		a.AreaCode, o.PostDate, o.CustName, o.UserId, o.CustCode, count(*) as NumberOfOrders , o.RoundingAmnt
                                                                                                    from		Areas a
                                                                                                    inner join	Retailers r
                                                                                                    on			a.ID=r.AreaID
                                                                                                    inner join	Orders o
                                                                                                    on			r.Code=o.CustCode
                                                                                                    group by	o.PostDate, a.AreaCode, o.CustName, o.UserId, o.CustCode, o.RoundingAmnt
                                                                                                    "));
            }
        }
        #endregion

        #region  Daily Activity Report
        internal ObservableCollection<ActivityStatus> getActivityStatus()
        {
            return new ObservableCollection<ActivityStatus>(database.Query<ActivityStatus>(@"Select * from ActivityStatus"));
        }

        internal ObservableCollection<Models.DailyActivitiesReport> getDailyActivityReport()
        {
            return new ObservableCollection<DailyActivitiesReport>(database.Query<DailyActivitiesReport>(@"Select		a.AreaCode, _as.Name as Status, r.Name, ac.Details as Description, ac.CreatedDate
                                                                                                            from		Areas a
                                                                                                            inner join	Retailers r
                                                                                                            on			a.ID=r.AreaID
                                                                                                            inner join	Activities ac
                                                                                                            on			r.Code=ac.BPCode
                                                                                                            inner join	ActivityStatus _as
                                                                                                            on			ac.status=_as.id"));
        }
        #endregion

        #region BPGroups

        public ObservableCollection<BPGroup> GetBPGroups()
        {
            lock (locker)
            {
                return new ObservableCollection<BPGroup>( database.Query<BPGroup>(@"select * from BPGroup "));
            }
        }
        #endregion

        #region BPClass
        public ObservableCollection<BPClass> GetBPClasses()
        {
            lock (locker)
            {
                return new ObservableCollection<BPClass>(database.Query<BPClass>(@"select * from BPClass "));
            }
        }
        #endregion
    }

}
