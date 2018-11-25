using DCC.SalesApp.ViewModels.RetailerVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DCC.SalesApp.Helpers
{
    public class ItemSelectedToCommandBehavior:Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(ItemSelectedToCommandBehavior)
                );
        private object _CommandParameter;
        public object CommandParameter
        {
            get { return _CommandParameter; }
            set { _CommandParameter = value; }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }     


        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);            
            bindable.ItemSelected += Bindable_ItemSelected;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var lv = sender as ListView;
            BindingContext = lv.BindingContext;
        }

        private void Bindable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
            //var lv = sender as ListView;
            //var vm = lv.BindingContext as RetailerListViewModel;
            //vm?.RetailerDetailCommand.Execute(null);
            Command.Execute(CommandParameter);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);            
            bindable.ItemSelected -= Bindable_ItemSelected;
            bindable.BindingContextChanged -=Bindable_BindingContextChanged;
        }
    }
}
