# MvvmUtils #

MvvmUtils provides a way to quickly integrate MVVM and viewmodel navigation.

[![Build Status](https://dev.azure.com/levelfiveteam-xamarin/Mvvm%20Utils/_apis/build/status/ci-release-pipeline?branchName=master)](https://dev.azure.com/levelfiveteam-xamarin/Mvvm%20Utils/_build/latest?definitionId=7&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/Xamarin.Forms.MvvmUtils)](https://www.nuget.org/packages/Xamarin.Forms.MvvmUtils/)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/Xamarin.Forms.MvvmUtils)](https://www.nuget.org/packages/Xamarin.Forms.MvvmUtils/)

Mvvmutils is a mvvm framework. It enables developers to create apps using the MVVM pattern on *Xamarin.Forms*. This allows for better code sharing by allowing you to share behavior and business logic between platforms.

Among the features Mvvmutils provides are:

- ViewModel to Page bindings the developers has to follow the naming convention. 
- Every page has to end with *Page* and its curresponding viewmodel has to end with *ViewModel*
- Naming convention is case sensitive.
- Eg: Viewmodel for *HomePage* should be *HomeViewModel*. The library will pick the curresponding Page on navigation based on the convention.
- ViewModel to ViewModel navigation

Grab the latest [MvvmUtils NuGet](https://www.nuget.org/packages/Xamarin.Forms.MvvmUtils/) package and install in your solution.

> Install-Package Xamarin.Forms.MvvmUtils

Make sure that both the shared core project and your application projects include the NuGet. 

### The "App" class

This is what it would typically look like:

```c#
using MvvmUtilsExample.Application_Layer;
using System;
using MvvmUtils;
using MvvmUtils.Forms;

namespace MvvmUtilsExample
{
    public partial class App : BaseApplication
    {
        public App()
        {
	    InitializeComponent();
        }
        /// <summary>
        /// On Start
        /// </summary>
        protected override void OnStart()
	{
            RegisterAppStart<LoginViewModel>();
        }
    }
}

```
RegisterAppStart method will start the application.

This is how a typical `ViewModel` might look like:
```c#
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }

```
If any parameters are passed in the navigation, The type has to mention in the viewmodel definition. Please check the example project for more. Also the pages has to be inherited from the base pages in the MvvmUtils library.
