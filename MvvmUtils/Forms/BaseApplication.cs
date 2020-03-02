using System;
using MvvmUtils.NavigationUtils;
using MvvmUtils.ViewModel;
using Xamarin.Forms;

namespace MvvmUtils.Forms
{
    /// <summary>
    /// Base class for application
    /// </summary>
    public abstract class BaseApplication : Application
    {
        #region App Lifecycle events
        /// <summary>
        /// On Start
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
        }
        /// <summary>
        /// On Sleep
        /// </summary>
        protected override void OnSleep()
		{
            base.OnSleep();
		}
        /// <summary>
        /// On Resume
        /// </summary>
		protected override void OnResume()
		{
            base.OnResume();
		}
        #endregion
        /// <summary>
        /// Sets up the main page for the given View Model type and parameter.
        /// </summary>
        protected void RegisterAppStart<TViewModel, TNavigationParameter>(TNavigationParameter navigationParameter) where TViewModel : ViewModelBase<TNavigationParameter>
        {
            Page page = ViewFactory.CreatePage<TViewModel,TNavigationParameter>(navigationParameter);
            MainPage = new NavigationPage(page);
        }
        /// <summary>
        /// Sets up the main page for the given View Model type and parameter.
        /// </summary>
        protected void RegisterAppStart<TViewModel>() where TViewModel : ViewModelBase
        {
            Page page = ViewFactory.CreatePage<TViewModel>();
            MainPage = new NavigationPage(page);
        }
    }
}
