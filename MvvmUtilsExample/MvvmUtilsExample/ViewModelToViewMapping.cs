using MvvmUtils.NavigationUtils;
using MvvmUtilsExample.Application_Layer;
using MvvmUtilsExample.User_Interface_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmUtilsExample
{
    public static class ViewModelToViewMapping
    {
        public static void Map()
        {
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<ImageBrowserPage, ImageBrowserViewModel>();
        }
    }
}
