using System;
using MvvmUtils.ViewModel;
using Xamarin.Forms;

namespace MvvmUtils.Forms
{
   
    /// <summary>
    /// Base master detail page
    /// </summary>
    public class BaseMasterDetailPage : MasterDetailPage
    {
        public BaseMasterDetailPage() : base()
        {

        }
        private ViewModelBase viewmodel;
        /// <summary>
        /// On binding context changed event.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            viewmodel = BindingContext as ViewModelBase;
        }
        /// <summary>
        /// On Appearing event
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewmodel?.ViewAppearing();
            viewmodel?.ViewAppeared();
            viewmodel?.ViewCreated();
        }
        /// <summary>
        /// On disappearing event
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewmodel?.ViewDisappearing();
            viewmodel?.ViewDisappeared();
            viewmodel?.ViewDestroy();
        }
    }
}
