using Default;

namespace DCC.SalesApp.Data
{
    
    
    public class DefaultOfflineContext : Microsoft.Synchronization.ClientServices.SQLite.SQLiteContext {
        
        private const string SyncScopeName = "Default";
        
        public DefaultOfflineContext(string cachePath, System.Uri serviceUri) : 
                base(DefaultOfflineContext .GetSchema(), SyncScopeName, cachePath, serviceUri) {
        }
        
        private static Microsoft.Synchronization.ClientServices.Common.OfflineSchema GetSchema() {
            Microsoft.Synchronization.ClientServices.Common.OfflineSchema schema = new Microsoft.Synchronization.ClientServices.Common.OfflineSchema();
            schema.AddCollection<Users>();
            schema.AddCollection<UserAttendance>();
            schema.AddCollection<UserSafety>();
            schema.AddCollection<ItemMovements>();
            schema.AddCollection<States>();
            schema.AddCollection<PermissionGroupH>();
            schema.AddCollection<PermissionGroupL>();
            schema.AddCollection<UserPerm>();
            schema.AddCollection<Items>();
            schema.AddCollection<Countries>();
            schema.AddCollection<Reasons>();
            schema.AddCollection<Districts>();
            schema.AddCollection<Cities>();
            schema.AddCollection<ActivitySubjects>();
            schema.AddCollection<ItemPrices>();
            schema.AddCollection<Warehouses>();
            schema.AddCollection<ITMSubCat>();
            schema.AddCollection<PriceList>();
            schema.AddCollection<CompanyDetails>();
            schema.AddCollection<ItemWhs>();
            schema.AddCollection<Retailers>();
            schema.AddCollection<Currencies>();
            schema.AddCollection<ITMCategory>();
            schema.AddCollection<Areas>();
            schema.AddCollection<AreaUserMapping>();
            schema.AddCollection<OrderAttachments>();
            schema.AddCollection<OrderItems>();
            schema.AddCollection<Orders>();
            schema.AddCollection<ActivityTypes>();
            schema.AddCollection<ActivityStatus>();
            schema.AddCollection<Activities>();
            schema.AddCollection<Outlets>(); 
            schema.AddCollection<VATD>();
            schema.AddCollection<BPGroup>();
            schema.AddCollection<ActivityActions>();
            schema.AddCollection<BPClass>();
            schema.AddCollection<MeetingLocations>();
            schema.AddCollection<Positions>();
            schema.AddCollection<Permission>();
            schema.AddCollection<RetailerContacts>();
            schema.AddCollection<OutletContacts>();
            schema.AddCollection<RetailerAddress>();
            schema.AddCollection<VATH>();
            schema.AddCollection<Delivery>();
            schema.AddCollection<DeliveryItems>();
            schema.AddCollection<DeliveryAttachments>();
            schema.AddCollection<SalesSchemesPromos>();
            schema.AddCollection<Quotations>();
            schema.AddCollection<UserLocationTracking>();
            schema.AddCollection<Invoices>();
            schema.AddCollection<QuotationItems>();
            schema.AddCollection<InvoiceItems>();
            schema.AddCollection<InvoiceAttachments>();
            schema.AddCollection<QuotationAttachments>();
            schema.AddCollection<PayMeans>();
            schema.AddCollection<CashReceipts>();
            return schema;
        }
    }
}
