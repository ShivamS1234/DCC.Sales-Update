namespace DCC.SalesApp.Models
{
    public class SaleProduct
    {
        public int QuotationItemId { get; set; }
        public int OrderItemId { get; set; }        
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double OnHand { get; set; }
        public double SaleQty { get; set; }
        public double OpenQty { get; set; }
        public double Quantity { get; set; }
        public string UoM { get; set; }
        public string CGuid { get; set; }
        public double TaxPer { get; set; }

        public int SaleVat { get; set; }


        public int LineId { get; set; }

        public string product
        {
            get { return code+" - "+name;}
        }

        private byte[] _ItemImage;
        public byte[] ItemImage
        {
            get
            {
                return _ItemImage;
            }
            set
            {                
                _ItemImage = value;               
            }
        }
        public Xamarin.Forms.ImageSource StreamImage
        {
            get
            {
                return
               Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(_ItemImage));
            }
        }
    }
}
