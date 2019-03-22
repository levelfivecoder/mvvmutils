using MvvmUtils;
using MvvmUtils.NavigationUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

namespace MvvmUtilsExample.Application_Layer
{
    public class LoginViewModel : BaseViewModel
    {
        ICommand loginCommand;
        public ICommand LoginCommand
        {
            get => this.loginCommand;
            set => SetProperty(ref this.loginCommand, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(loginAction);
        }

        private void loginAction()
        {
            // throw new Exception("Custom app center exception");
            Analytics.TrackEvent("user logged in", new Dictionary<string, string> {
            { "Ajith", "11001" }
            });
            Navigation.Push(ViewFactory.CreatePage(new ImageBrowserViewModel()));
        }
    }
}
