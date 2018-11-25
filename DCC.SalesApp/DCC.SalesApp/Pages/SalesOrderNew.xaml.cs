using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesOrderNew : ContentPage
    {
        ObservableCollection<Default.RetailerAddress> customerAddress = new ObservableCollection<Default.RetailerAddress>();
        List<Models.Customer> Customerslist = null;
        Default.Orders _Order = null;
        Int32 SalesOrderID = -1;
        Int32 ContID = 0;
        IEnumerable<Models.CustomerContact> customerContact;
        public static Models.Customer _retailers { get; set; }
        //int ContactPerson;
        public SalesOrderNew()
        { }
        public SalesOrderNew(Int32 _salesOrderID)
        {
            InitializeComponent();
            SalesOrderProducts.itmCat = null;
            SalesOrderProducts.itmSubCat = null;
            SalesOrderID = _salesOrderID;
            initializeCustomer();
            ApplyDetails();
        }


        public void getCustomerAddress(int custId)
        {
            customerAddress = App.Database.getCustomerAddress(custId);
        }
        public void initializeCustomer()
        {
            Customerslist = App.Database.GetAllCustomerDetails().ToList();
            //pkrCustomer.ItemsSource = Customerslist;

        }

        public void ApplyDetails()
        {
            if (SalesOrderID > 0)
            {
                _Order = App.Database.GetOrder(SalesOrderID);
                //pkrCustomer.SelectedIndex = Customerslist.FindIndex(a => a.Code == _Order.CustCode);
                pkrCustomer.Text = Customerslist.Find(a => a.Code == _Order.CustCode).nameCode;
                pkrShipping.SelectedIndex = customerAddress.IndexOf(customerAddress.Where(X => X.ID == _Order.ShiptoID).FirstOrDefault());
                pkrBilling.SelectedIndex = customerAddress.IndexOf(customerAddress.Where(X => X.ID == _Order.BilltoID).FirstOrDefault());

            }
        }
        //private void pkrCustomer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var customer = (Models.Customer)pkrCustomer.SelectedItem;
        //        txtArea.Text = Convert.ToString(customer.AreaID);
        //        customerContact = App.Database.GetCustomerContact(customer.ID);
        //        //ContactPerson = customerContact.ToList()[0].ID;
        //        customerAddress = App.Database.getCustomerAddress(customer.ID);

        //        try
        //        {

        //            pkrBilling.Items.Clear();
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Diagnostics.Debug.WriteLine(ex.Message);
        //        }

        //        foreach (var it in customerAddress)
        //        {
        //            string _item = it.AddressCode + "-" + it.ID;
        //            this.pkrBilling.Items.Add(_item);
        //        }
        //        try
        //        {
        //            pkrShipping.Items.Clear();
        //        }
        //        catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        //        foreach (var it in customerAddress)
        //        {
        //            string _item = it.AddressCode + "-" + it.ID;
        //            this.pkrShipping.Items.Add(_item);
        //        }
        //        if (customerContact.Count() != 0)
        //        {
        //            lblContactPerson.Text = customerContact.First(c => c.ID == customer.ID).contactPerson;
        //            ContID = customerContact.First(c => c.ID == customer.ID).contId;
        //        }
        //        else
        //        {
        //            lblContactPerson.Text = "";
        //            txtBilling.Text = "";
        //            txtShipping.Text = "";

        //        }

        //        if (pkrBilling.HeightRequest == 30)
        //        {
        //            pkrBilling.HeightRequest = 31;
        //            pkrShipping.HeightRequest = 31;
        //        }
        //        else
        //        {
        //            pkrBilling.HeightRequest = 30;
        //            pkrShipping.HeightRequest = 30;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Default.RetailerAddress vm = BindingContext as Default.RetailerAddress;

            if (vm != null)
            {
                this.pkrBilling.Items.Clear();
                foreach (var it in vm.AddressCode)
                {
                    this.pkrBilling.Items.Add(it.ToString());
                }
            }
        }
        private void pkrShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string shipping = pkrShipping.SelectedItem.ToString();
                string _id = shipping.Split('-').Last();
                Default.RetailerAddress _add = customerAddress.FirstOrDefault(x => x.ID == (_id == "" ? 0 : Convert.ToInt32(_id)));
                string address = _add.Building + ", " + _add.Street + ", " + _add.Block;
                txtShipping.Text = address;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void pkrBilling_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string billing = pkrBilling.SelectedItem.ToString();
                string _id = billing.Split('-').Last();
                Default.RetailerAddress _add = customerAddress.FirstOrDefault(x => x.ID == (_id == "" ? 0 : Convert.ToInt32(_id)));
                string address = _add.Building + ", " + _add.Street + ", " + _add.Block;
                txtBilling.Text = address;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void toolbarSave_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["CustomerDetails"] = _retailers;

            if (SalesOrderID <= 0)
                _Order = new Default.Orders();

            if (_retailers!=null)
            {
                _Order.CustCode = (_retailers as Models.Customer).Code;
                _Order.CustName = (_retailers as Models.Customer).Name;
            }
            if (pkrBilling.SelectedIndex > -1)
                _Order.BilltoID = Convert.ToInt32(pkrBilling.SelectedItem.ToString().Split('-').Last());
            if (pkrShipping.SelectedIndex > -1)
                _Order.ShiptoID = Convert.ToInt32(pkrShipping.SelectedItem.ToString().Split('-').Last());

            _Order.PostDate = CalStartDate.Date;
            _Order.DueDate = CalDelDate.Date;
            _Order.TaxDate = CalStartDate.Date;
            _Order.CustRefNo = txtCustRefNo.Text;
            _Order.ContPerson = ContID;
            _Order.UserId = (int)Application.Current.Properties["ID"];
            _retailers = null;
            Navigation.PushAsync(new SalesOrderProducts(_Order) { Title = "Sale Products" });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Demo() { Title = "Customers" });
        }
        protected override void OnAppearing()
        {
            //base.OnAppearing();
            if (_retailers != null)
            {
                try
                {
                    var customer = _retailers;
                    txtArea.Text = Convert.ToString(customer.AreaID);
                    customerContact = App.Database.GetCustomerContact(customer.ID);
                    //ContactPerson = customerContact.ToList()[0].ID;
                    customerAddress = App.Database.getCustomerAddress(customer.ID);
                    pkrCustomer.Text = _retailers.nameCode;

                    try
                    {

                        pkrBilling.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    foreach (var it in customerAddress)
                    {
                        string _item = it.AddressCode + "-" + it.ID;
                        this.pkrBilling.Items.Add(_item);
                    }
                    try
                    {
                        pkrShipping.Items.Clear();
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
                    foreach (var it in customerAddress)
                    {
                        string _item = it.AddressCode + "-" + it.ID;
                        this.pkrShipping.Items.Add(_item);
                    }
                    if (customerContact.Count() != 0)
                    {
                        lblContactPerson.Text = customerContact.First(c => c.ID == customer.ID).contactPerson;
                        ContID = customerContact.First(c => c.ID == customer.ID).contId;
                    }
                    else
                    {
                        lblContactPerson.Text = "";
                        txtBilling.Text = "";
                        txtShipping.Text = "";

                    }

                    if (pkrBilling.HeightRequest == 30)
                    {
                        pkrBilling.HeightRequest = 31;
                        pkrShipping.HeightRequest = 31;
                    }
                    else
                    {
                        pkrBilling.HeightRequest = 30;
                        pkrShipping.HeightRequest = 30;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
