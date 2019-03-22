using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmUtils
{
    public static class BaseAppConstants
    {
        public static string BaseUrl = String.Empty;
        public static string AccessToken { get; set; }
        public static string AuthorizationType { get; set; }
        public static string NoInternetConnSystemMessage = "The Internet connection appears to be offline.";
        public static string NoInternetConnUserMessage = "Unable to connect to server.Please check your internet connection.";
        public static int NoInternetConnStatusCode = 503;
    }
}
