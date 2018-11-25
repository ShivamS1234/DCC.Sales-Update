using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DCC.SalesApp.Models;
using DevExpress.Mobile.DataGrid.Theme;
using DevExpress.Mobile.DataGrid;
using DCC.SalesApp.ViewModels;
using DCC.SalesApp.CustomRenderers;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notes : ContentPage
    {
        Int32 _selectedId = -1;
        NotesViewModel viewModel;
        public Notes()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new NotesViewModel(Navigation);
            //lstNotes.grdNotes.AutoFilterPanelHeight = 30;
            lstNotes.ItemTapped += GrdNotes_RowTap;
            //loading();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetNotesList();
            _selectedId = -1;
        }

        private bool _canClose = true;
        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            if (_canClose)
            {
                ShowExitDialog();
            }
            return _canClose;
        }
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Want to go Dashboard Screen?", "Yes", "No");
            if (answer)
            {
                App.Current.MainPage = new MainPage();
                _canClose = false;
                //OnBackButtonPressed;
            }
        }     
        private void GrdNotes_RowTap(object sender, ItemTappedEventArgs e)
        {
            var list = sender as InfiniteListView;
            list.SelectedItem = null;
            var item = e.Item as NotesInfo;
            if (item != null)
            {

                var selectedItem = viewModel.NoteLists.Where(x => x.IsNotesInfo == true).FirstOrDefault();
                if (selectedItem != null)
                {
                    if (item.ID != selectedItem.ID)
                    {
                        selectedItem.IsNotesInfo = false;
                    }
                }

                if (item.IsNotesInfo)
                    item.IsNotesInfo = false;
                else
                    item.IsNotesInfo = true;

            }
        }
        //private void GrdNotes_RowTap(object sender, RowTapEventArgs e)
        //{
        //    NotesInfo obj = new NotesInfo();
        //    obj = Notes_view.grdNotes.SelectedDataObject as NotesInfo;
        //    if (_selectedId != obj.ID)
        //    {
        //        _selectedId = obj.ID;
        //        this.Navigation.PushAsync(new NotesDetail(_selectedId) { Title = obj.ActivityName });
        //    }
        //}

        public void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NoteNew() { Title = "Add New Note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}