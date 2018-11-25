using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DCC.SalesApp
{
    public static class Theme
    {
        public static readonly BindableProperty CornerRadiusProperty =
          BindableProperty.CreateAttached("CornerRadius", typeof(double), typeof(Theme), 0.0, propertyChanged: OnChanged<CornerRadiusEffect, double>);
        private static void OnChanged<TEffect, TProp>(BindableObject bindable, object oldValue, object newValue)
                  where TEffect : Effect, new()
        {
            var view = bindable as View;
            if (view == null)
            {
                return;
            }

            if (EqualityComparer<TProp>.Equals(newValue, default(TProp)))
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is TEffect);
                if (toRemove != null)
                {
                    view.Effects.Remove(toRemove);
                }
            }
            else
            {
                view.Effects.Add(new TEffect());
            }

        }
        public static void SetCornerRadius(BindableObject view, double radius)
        {
            view.SetValue(CornerRadiusProperty, radius);
        }

        public static double GetCornerRadius(BindableObject view)
        {
            return (double)view.GetValue(CornerRadiusProperty);
        }

        public static void ApplyGridTheme()
        {
            ThemeManager.ThemeName = Themes.Light;
            ThemeManager.Theme.AndroidRefreshCustomizer.BarColor = Color.FromHex("#036890");
            ThemeManager.Theme.HeaderCustomizer.BorderColor = Color.FromHex("#036890");
            ThemeManager.Theme.HeaderCustomizer.BackgroundColor = Color.FromHex("#f3f2f2");

          //  ThemeManager.Theme.CellCustomizer.HighlightColor = Color.Red;// Color.FromHex("#036890");

            ThemeManager.Theme.CellCustomizer.SelectionColor = Color.FromHex("#f9f9f9");

            ThemeFontAttributes headerFont = new ThemeFontAttributes("Tahoma", ThemeFontAttributes.FontSizeFromNamedSize(NamedSize.Default), FontAttributes.None, Color.FromHex("#036890"));
            ThemeManager.Theme.HeaderCustomizer.Font = headerFont;
            ThemeManager.Theme.FilterPanelCustomizer.Font = headerFont;

            ThemeFontAttributes cellFont = new ThemeFontAttributes("Tahoma", ThemeFontAttributes.FontSizeFromNamedSize(NamedSize.Default), FontAttributes.None, Color.FromHex("#036890"));
            ThemeManager.Theme.CellCustomizer.Font = cellFont;


            ThemeFontAttributes _grpheaderFont = new ThemeFontAttributes("Tahoma", ThemeFontAttributes.FontSizeFromNamedSize(NamedSize.Default), FontAttributes.Bold, Color.FromHex("#036890"));
            ThemeManager.Theme.GroupCustomizer.Font = _grpheaderFont;
            ThemeManager.Theme.CellCustomizer.SelectionColor = Color.FromHex("#85C1E9");


            ThemeManager.RefreshTheme();
        }
        private class CornerRadiusEffect : RoutingEffect
        {
            public CornerRadiusEffect()
                : base("Xamarin.CornerRadiusEffect")
            {
            }
        }
    }

}
