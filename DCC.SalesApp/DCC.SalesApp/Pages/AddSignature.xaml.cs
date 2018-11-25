using System;
using Xamarin.Forms;
using Default;
using System.IO;
using SignaturePad.Forms;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;

namespace DCC.SalesApp.Pages
{
    public partial class AddSignature : PopupPage
    {
        Users oUser = new Users();
        Delivery oDelivery = null;
        CashReceipts ocash = null;
        Image iimg = null;
        public AddSignature(Delivery _oDelivery, CashReceipts _ocash)
        {
            try
            {
                InitializeComponent();
                oDelivery = _oDelivery;
                ocash = _ocash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override Task OnAppearingAnimationEnd()
        {
            Content.Margin = 40;
            return Content.FadeTo(0.9);

        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {

            try
            {
                Stream Image = await PadView.GetImageStreamAsync(SignatureImageFormat.Jpeg);
                using (BinaryReader binaryReader = new BinaryReader(Image))
                {
                    binaryReader.BaseStream.Position = 0;
                    if (oDelivery != null)
                    {
                        oDelivery.SignatureImg = binaryReader.ReadBytes((int)Image.Length);
                    }
                    else if (ocash != null)
                    {
                        ocash.SignatureImg = binaryReader.ReadBytes((int)Image.Length);                      
                    }
                }
                // call();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
          await this.Navigation.PopAsync();

        }

        private async void call()
        {
            Stream img = await PadView.GetImageStreamAsync(SignatureImageFormat.Jpg);
            BinaryReader br = new BinaryReader(img);
            br.BaseStream.Position = 0;
            Byte[] All = br.ReadBytes((int)img.Length);
            //display the signature
            byte[] image = (byte[])All;
            ImageSource imageSource = null;
            imageSource = ImageSource.FromStream(() => new MemoryStream(image));
            iimg.Source = imageSource;

        }
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            PadView.Clear();
        }

    }
}



