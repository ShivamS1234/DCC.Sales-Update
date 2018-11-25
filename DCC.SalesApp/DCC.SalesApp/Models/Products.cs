using Default;
using System.Collections.Generic;

namespace DCC.SalesApp.Models
{
    public class Products : Items
    {
        public int SaleQty { get; set; }
    }
    public class imp
    {
        public imp()
        {
            List<Products> p = new List<Products>();
            IEnumerable<Default.Items> lst = App.Database.GetAllProduct_Cat_SubCat(1, 1);
            foreach (Default.Items i in lst)
            {
                p.Add(new Products() { ID = i.ID, Name = i.Name, SaleQty = 10, OnHand = i.OnHand });
            }
        }
    }
}
