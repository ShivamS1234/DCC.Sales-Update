using DCC.SalesApp.Models;
using Default;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockItems : ContentPage
    {
        int SelectedID = 0;
        public StockItems(int _ID)
        {
            InitializeComponent();
            SelectedID = _ID;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Theme.ApplyGridTheme();
            try
            {
                List<ItemStocks> objwarehouse = App.Database.GetAll_ItemsStock(SelectedID).ToList();
                _grdStockItems.ItemsSource = objwarehouse;
                _grdStockItems.SortMode = GridSortMode.Multiple;
                _grdStockItems.AutoFilterPanelHeight = 30;
                ThemeManager.RefreshTheme();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        #region Commentted
        public void CreateWarehouseCategories(int _WhsID)
        {
            try
            {
                ListView lstwarehouse = new ListView();
                List<ITMCategory> obstorck = App.Database.GetAllCategoriesforWarehouse(_WhsID).ToList();

                foreach (var _Catogery in obstorck)
                {
                    ContentPage _ContentPage = new ContentPage();
                    _ContentPage.Title = _Catogery.Name;           
                    ScrollView _ScrollView = new ScrollView();


                    StackLayout _stack = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        BackgroundColor = Color.Transparent,
                        StyleClass = new List<string>() { "Stack" }

                    };

                    Grid _Grid = new Grid
                    {
                        ColumnDefinitions = {new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) }, new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) },
                        }
                    };

                    var objstock = App.Database.ShowCategoryWiseItems(_WhsID, _Catogery.ID);
                    int ctr = 0;
                    int ctrrow = 0;
                    List<string> a = new List<string>() { "LabelClass" };
                    foreach (var item in objstock)
                    {
                        int ctrColumn = 0;
                        Grid _Grid1 = new Grid
                        {
                            RowDefinitions = { new RowDefinition { Height = new GridLength(60, GridUnitType.Star) }, new RowDefinition { Height = new GridLength(60, GridUnitType.Star) }, new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) } },
                            ColumnDefinitions = {new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) }, new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) },
                        }
                        };
                        _Grid1.VerticalOptions = LayoutOptions.Start;
                        _Grid1.HorizontalOptions = LayoutOptions.Start;
                        _Grid1.Children.Add(new Label { Text = item.Name, StyleClass = a }, 0, ctrColumn);
                        _Grid1.Children.Add(new Label { Text = "Sales Price " + item.SalesPrice.ToString(), StyleClass = a }, 1, ctrColumn);
                        ctrColumn++;
                        _Grid1.Children.Add(new Label { Text = "Qty in Cases " + item.OnHand.ToString(), StyleClass = a }, 0, ctrColumn);
                        _Grid1.Children.Add(new Label { Text = "Whole Sale Price " + item.WholePrice.ToString(), StyleClass = a }, 1, ctrColumn);
                        ctrColumn++;
                        BoxView _Bxview = new BoxView { HeightRequest = 1, StyleClass = new List<string>() { "HorizontalRule" } };
                        _Grid1.Children.Add(_Bxview, 0, ctrColumn);
                        Grid.SetColumnSpan(_Bxview, 2);

                        ctrColumn++;


                        int C = ctr % 2;
                        _Grid.Children.Add(_Grid1, C, ctrrow);
                        if (C == 1)
                            ctrrow++;
                        ctr++;
                    }
                    _stack.Children.Add(_Grid);

                    _ScrollView.Content = _stack;
                    _ContentPage.Content = _ScrollView;
                    //Children.Add(_ContentPage);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion  
    }

}