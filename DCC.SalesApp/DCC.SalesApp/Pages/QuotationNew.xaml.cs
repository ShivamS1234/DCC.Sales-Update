using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuotationNew : ContentPage
    {
        ObservableCollection<Default.RetailerAddress> customerAddress = new ObservableCollection<Default.RetailerAddress>();
        List<Models.Customer> Customerslist = null;
        Default.Quotations _Quotation = null;
        Int32 QuotationID = -1;

        public static Models.Customer _retailers { get; set; }

        public QuotationNew()
        {
            InitializeComponent();
        }

        public QuotationNew(Int32 _quotationID)
        {
            InitializeComponent();
            QuotationProducts.itmCat = null;
            QuotationProducts.itmSubCat = null;
            QuotationID = _quotationID;
            initializeCustomer();
            ApplyDetails();
            //Navigation.PushAsync(new ProductSelectionNew());
        }

        public void getCustomerAddress(int custId)
        {
            customerAddress = App.Database.getCustomerAddress(custId);

        }
        public void initializeCustomer()
        {
            Customerslist = App.Database.GetAllCustomerDetails().ToList();

        }

        public void ApplyDetails()
        {
            if (QuotationID > 0)
            {
                _Quotation = App.Database.GetQuotation(QuotationID);
                pkrCustomer.Text = Customerslist.Find(a => a.Code == _Quotation.CustCode).nameCode;
                pkrShipping.SelectedIndex = customerAddress.IndexOf(customerAddress.Where(X => X.ID == _Quotation.ShiptoID).FirstOrDefault());
                pkrBilling.SelectedIndex = customerAddress.IndexOf(customerAddress.Where(X => X.ID == _Quotation.BilltoID).FirstOrDefault());

            }
        }

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

            if (QuotationID <= 0)
                _Quotation = new Default.Quotations();

            if (_retailers != null)
            {
                _Quotation.CustCode = (_retailers as Models.Customer).Code;
                _Quotation.CustName = (_retailers as Models.Customer).Name;
            }
            if (pkrBilling.SelectedIndex > -1)
                _Quotation.BilltoID = Convert.ToInt32(pkrBilling.SelectedItem.ToString().Split('-').Last());
            if (pkrShipping.SelectedIndex > -1)
                _Quotation.ShiptoID = Convert.ToInt32(pkrShipping.SelectedItem.ToString().Split('-').Last());

            _Quotation.PostDate = CalStartDate.Date;
            _Quotation.DueDate = CalDelDate.Date;
            _Quotation.TaxDate = CalStartDate.Date;
            _Quotation.CustRefNo = txtCustRefNo.Text;
            _Quotation.UserId = (int)Application.Current.Properties["ID"];
            _retailers = null;
            Navigation.PushAsync(new QuotationProducts(_Quotation) { Title = "Quotation Products" });
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Demo() { Title = "Customers" });
        }

        protected override void OnAppearing()
        {
            if (_retailers != null)
            {
                try
                {
                    var customer = _retailers;
                    Int32 _AreaId = Convert.ToInt32(customer.AreaID);
                    if (_AreaId > 0)
                    {
                        // txtArea.Text = Convert.ToString(customer.AreaID);
                        lblAreaText.Text = App.database.GetAreaName(_AreaId);
                    }
                    else
                    {
                        //txtArea.Text = "0";
                        lblAreaText.Text = "";
                    }
                    IEnumerable<Models.CustomerContact> customerContact = App.Database.GetCustomerContact(customer.ID);
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
                        lblContactPerson.Text = customerContact.First(c => c.ID == customer.ID).contactPerson;
                    else
                    {
                        lblContactPerson.Text = "";
                        txtBilling.Text = "";
                        txtShipping.Text = "";

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

