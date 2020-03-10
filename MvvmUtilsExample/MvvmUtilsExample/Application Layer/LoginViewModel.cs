using MvvmUtils;
using MvvmUtils.NavigationUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using MvvmUtils.ViewModel;

namespace MvvmUtilsExample.Application_Layer
{
    public class LoginViewModel : ViewModelBase
    {
        ICommand loginCommand;
        public ICommand LoginCommand
        {
            get => this.loginCommand;
            set => SetProperty(ref this.loginCommand, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(LoginAction);
        }

        private void LoginAction()
        {
            // throw new Exception("Custom app center exception");
            Analytics.TrackEvent("user logged in", new Dictionary<string, string> {
            { "Ajith", "11001" }
            });
            Navigation.Push<ImageBrowserViewModel, int>(3);
        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();
        }
        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }
        public override void ViewCreated()
        {
            base.ViewCreated();
        }
        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
        }
        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
        }
        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
        }
        public override void Initialize()
        {
            base.Initialize();
        }

    }
}
