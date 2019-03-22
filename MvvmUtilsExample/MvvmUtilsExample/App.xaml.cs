using MvvmUtilsExample.Application_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmUtils;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using MvvmUtils.NavigationUtils;
using Microsoft.AppCenter.Distribute;
using System.Threading.Tasks;

namespace MvvmUtilsExample
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            BaseAppConstants.BaseUrl = "http://www.splashbase.co/api/v1/";
            ViewModelToViewMapping.Map();
            Page page = ViewFactory.CreatePage<LoginViewModel>();
            MainPage = new NavigationPage(page);

        }

        protected override void OnStart ()
		{
            Distribute.ReleaseAvailable = OnReleaseAvailable;
            // Handle when your app starts
            AppCenter.Start("android=e34d7234-ca88-436d-b0f1-d2f9c69f2c52;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
            AppCenter.Start("ios={Your Xamarin iOS App Secret};android={e34d7234-ca88-436d-b0f1-d2f9c69f2c52}", typeof(Distribute));
        }
        bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            string versionName = releaseDetails.ShortVersion;
            string versionCodeOrBuildNumber = releaseDetails.Version;
            string releaseNotes = releaseDetails.ReleaseNotes;
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;

            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user cannot postpone
            if (releaseDetails.MandatoryUpdate)
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // Note that this method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you are using your own dialog, false otherwise
            return true;
        }
        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
