using System;
using System.Threading.Tasks;
using MvvmUtils.HttpHandlers;
using MvvmUtilsExample.BusinessLayer.Models;
using Newtonsoft.Json;

namespace MvvmUtilsExample.ServiceAccessLayer
{
    public class ImagesHandler: HttpHandler
    {
        public Images imageListObject { get; set; }

        public ImagesHandler(string requestUri) : base(requestUri)
        {
            System.Diagnostics.Debug.Write(requestUri);
        }

        public override async Task<object> FetchDataAsync()
        {
            if (HttpResponseMessage.IsSuccessStatusCode)
            {
                imageListObject = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Images>(HttpContent));
            }

            return imageListObject as object;
        }

        public override void SetData(object data)
        {
            HttpContent = JsonConvert.SerializeObject(data);
        }
    }
}
