using MvvmUtils.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtils.HttpHandlers
{
    /// <summary>
    /// Http Handler class 
    /// should initialize values in the Base App constands
    /// </summary>
    public abstract class HttpHandler : IDisposable
    {
        private HttpClient Client { get; set; }
        private string RequestUri { get; set; }
        private string AuthorizationScheme { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; private set; }
        protected HttpRequestMessage HttpRequestMessage { get; private set; }
        protected string HttpContent { get; set; }
        protected HttpContent Content { get; set; }
        protected MultipartFormDataContent MultipartContentData;
        protected HttpHandler(string requestUri)
        {
            System.Diagnostics.Debug.Write(requestUri);

            HttpContent = string.Empty;
            HttpRequestMessage = new HttpRequestMessage();
            HttpResponseMessage = new HttpResponseMessage();
            RequestUri = requestUri;
            try
            {
                //the redirect code obtained when updating settings is giving an exception in HeadRequest.
                //Hence setting auto redirect to false using HttpClientHandler.
                HttpClientHandler handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                Client = new HttpClient(handler);
                String accessToken = BaseAppConstants.AccessToken;

                if (accessToken != null)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BaseAppConstants.AuthorizationType, accessToken);
                }
                if (string.IsNullOrEmpty(BaseAppConstants.BaseUrl))
                {
                    throw new BaseUrlNotInitalizedException("Please initalize the base url value in the base constants");
                }
                Client.BaseAddress = new Uri(BaseAppConstants.BaseUrl);
            }
            catch (HttpRequestException ex)
            {
                Dispose();
                try
                {
                    throw ex.InnerException;
                }
                catch (WebException)
                {

                    Client.Dispose();
                    throw new Exception("Check your internet connectivity");
                }
            }
            catch (WebException)
            {
                Dispose();

            }
            catch (TaskCanceledException)
            {
                Dispose();

            }
            catch (Exception)
            {
                Dispose();
                throw;
            }

        }

        public void Dispose()
        {
            if (Client != null) Client.Dispose();
            Client = null;

        }

        public async Task SendHttpGetRequest()
        {
            try
            {

                HttpRequestMessage.Method = HttpMethod.Get;

                HttpResponseMessage = await Client.GetAsync(BaseAppConstants.BaseUrl+RequestUri);

                if (HttpResponseMessage != null)
                {
                    HttpContent = await HttpResponseMessage.Content.ReadAsStringAsync();
                    if (!HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await HttpResponseMessage.Content.ReadAsStringAsync();

                        var responseOutput = JObject.Parse(result);
                        var message = (string)responseOutput["Message"];
                        UnSuccessfullStatusCodeException ex = new UnSuccessfullStatusCodeException(message);
                        ex.StatusCode = (int)HttpResponseMessage.StatusCode;
                        throw ex;
                    }
                }
            }
            catch (UnSuccessfullStatusCodeException ex)
            {
                Client.Dispose();
                throw ex;
            }

            catch (HttpRequestException ex)
            {
                Client.Dispose();

                throw new Exception(ex.Message);


            }

            catch (TaskCanceledException ex)
            {
                Client.Dispose();
                throw ex;
            }
            catch (Exception e)
            {
                Client.Dispose();
                throw e;

            }
        }

        public abstract Task<object> FetchDataAsync();

        public abstract void SetData(object data);

        public async Task<Object> SendHttpPostRequest()
        {
            try
            {
                Client.CancelPendingRequests();

                HttpResponseMessage = await Client.PostAsync(BaseAppConstants.BaseUrl + RequestUri
                    ,
                    new StringContent(HttpContent,
                        Encoding.UTF8,
                        "application/json"));

                if (HttpResponseMessage != null)
                {
                    HttpContent = await HttpResponseMessage.Content.ReadAsStringAsync();
                    if (!HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await HttpResponseMessage.Content.ReadAsStringAsync();

                        var responseOutput = JObject.Parse(result);
                        var message = (string)responseOutput["Message"];
                        UnSuccessfullStatusCodeException ex = new UnSuccessfullStatusCodeException(message);
                        ex.StatusCode = (int)HttpResponseMessage.StatusCode;
                        throw ex;
                    }
                }


            }
            catch (UnSuccessfullStatusCodeException ex)
            {
                Client.Dispose();
                throw ex;
            }
            catch (Exception e)
            {
                Client.Dispose();
                throw e;
            }


            return HttpResponseMessage;
        }

        public async Task<object> SendHttpPutRequest()
        {
            try
            {
                HttpRequestMessage.Method = HttpMethod.Put;

                HttpResponseMessage = await Client.PutAsync(BaseAppConstants.BaseUrl + RequestUri, new StringContent(HttpContent, Encoding.UTF8, "application/json"));

                if (HttpResponseMessage != null)
                {
                    HttpContent = await HttpResponseMessage.Content.ReadAsStringAsync();
                    if (!HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await HttpResponseMessage.Content.ReadAsStringAsync();

                        var responseOutput = JObject.Parse(result);
                        var message = (string)responseOutput["Message"];
                        UnSuccessfullStatusCodeException ex = new UnSuccessfullStatusCodeException(message);
                        ex.StatusCode = (int)HttpResponseMessage.StatusCode;
                        throw ex;
                    }
                }

            }
            catch (UnSuccessfullStatusCodeException ex)
            {
                Client.Dispose();
                throw ex;
            }
            catch (Exception e)
            {
                Client.Dispose();
                if (e.Message == BaseAppConstants.NoInternetConnSystemMessage)
                {
                    UnSuccessfullStatusCodeException exception = new UnSuccessfullStatusCodeException(BaseAppConstants.NoInternetConnUserMessage);
                    exception.StatusCode = BaseAppConstants.NoInternetConnStatusCode;
                    throw exception;

                }
                else
                {
                    throw e;

                }
            }

            return HttpResponseMessage;
        }

        public async Task<Object> SendHttpDeleteRequest()
        {
            try
            {
                Client.CancelPendingRequests();
                HttpResponseMessage = await Client.DeleteAsync(BaseAppConstants.BaseUrl + RequestUri);
                if (HttpResponseMessage != null)
                {
                    HttpContent = await HttpResponseMessage.Content.ReadAsStringAsync();
                    if (!HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await HttpResponseMessage.Content.ReadAsStringAsync();

                        var responseOutput = JObject.Parse(result);
                        var message = (string)responseOutput["Message"];
                        UnSuccessfullStatusCodeException ex = new UnSuccessfullStatusCodeException(message);
                        ex.StatusCode = (int)HttpResponseMessage.StatusCode;
                        throw ex;
                    }
                }
            }
            catch (UnSuccessfullStatusCodeException ex)
            {
                Client.Dispose();
                throw ex;
            }
            catch (Exception e)
            {
                Client.Dispose();
                throw e;
            }

            return HttpResponseMessage;
        }

        public async Task<Object> SendHttpHeadRequest()
        {
            try
            {
                HttpRequestMessage.Method = HttpMethod.Head;
                System.Uri Uri = null;
                System.Uri.TryCreate(BaseAppConstants.BaseUrl + RequestUri, UriKind.Relative, out Uri);

                HttpRequestMessage.RequestUri = Uri;

                HttpResponseMessage = await Client.SendAsync(HttpRequestMessage);
                if (HttpResponseMessage != null)
                {
                    HttpContent = await HttpResponseMessage.Content.ReadAsStringAsync();
                    if (!HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await HttpResponseMessage.Content.ReadAsStringAsync();

                        string message = "";
                        if (!string.IsNullOrEmpty(result))
                        {
                            var responseOutput = JObject.Parse(result);
                            message = (string)responseOutput["Message"];
                        }

                        UnSuccessfullStatusCodeException ex = new UnSuccessfullStatusCodeException(message);
                        ex.StatusCode = (int)HttpResponseMessage.StatusCode;
                        throw ex;
                    }
                }
            }
            catch (UnSuccessfullStatusCodeException e1)
            {
                Client.Dispose();
                throw e1;
            }

            catch (HttpRequestException e2)
            {
                Client.Dispose();

                throw new Exception(e2.Message);


            }

            catch (TaskCanceledException e3)
            {
                Client.Dispose();
                throw e3;
            }
            catch (Exception e4)
            {
                Client.Dispose();
                throw e4;

            }

            return HttpResponseMessage.StatusCode;
        }

        public async Task<Object> SendHttpMultipartPostRequest()
        {
            try
            {

                Client.CancelPendingRequests();

                HttpResponseMessage = await Client.PostAsync(BaseAppConstants.BaseUrl + RequestUri
                    ,
                    MultipartContentData);

                if (HttpResponseMessage != null)
                {

                    if (!HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = await HttpResponseMessage.Content.ReadAsStringAsync();

                        var responseOutput = JObject.Parse(result);
                        var message = (string)responseOutput["Message"];
                        UnSuccessfullStatusCodeException ex = new UnSuccessfullStatusCodeException(message);
                        ex.StatusCode = (int)HttpResponseMessage.StatusCode;
                        throw ex;
                    }
                }


            }
            catch (UnSuccessfullStatusCodeException ex)
            {
                Client.Dispose();
                throw ex;
            }
            catch (Exception)
            {
                Client.Dispose();
                throw;
            }


            return HttpResponseMessage;
        }

    }
}
