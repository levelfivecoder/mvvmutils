using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtils.HttpHandlers
{
    public class ServiceProvider
    {
        public HttpHandler Handler { get; private set; }

        private object Data { get; set; }

        public ServiceProvider(HttpHandler ob)
        {
            System.Diagnostics.Debug.Write("Hanfler");
            Handler = ob;
        }

        public async Task<object> GetData()
        {
            await Handler.SendHttpGetRequest();
            Data = await Handler.FetchDataAsync();
            return Data;
        }

        public async Task<object> PostData(object data)
        {
            Handler.SetData(data);
            return await Handler.SendHttpPostRequest();

        }

        public async Task<object> PostMultipartFormData(object data)
        {
            Handler.SetData(data);
            return await Handler.SendHttpMultipartPostRequest();

        }

        public async Task<Object> DeleteData()
        {
            return await Handler.SendHttpDeleteRequest();
        }

        public async Task<object> PutData(object data)
        {
            Handler.SetData(data);
            return await Handler.SendHttpPutRequest();

        }

        public async Task<Object> HeadData()
        {
            return await Handler.SendHttpHeadRequest();
        }
    }
}
