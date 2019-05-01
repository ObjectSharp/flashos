using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hoopp.Flash.Core.Test
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, string pathString,
            Action<HttpRequestMessage> replay = null) 
        {
            return await SendWithoutBodyAsync(client, HttpMethods.Get, pathString, replay);
        }

        public static async Task<HttpResponseMessage> OptionsAsync(this HttpClient client, string pathString,
            Action<HttpRequestMessage> replay = null) 
        {
            return await SendWithoutBodyAsync(client, HttpMethods.Options, pathString, replay);
        }
        
        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string pathString, T body,
            Action<HttpRequestMessage> replay = null) where T : class
        {
            return await SendWithBodyAsync(client, HttpMethods.Post, pathString, body, replay);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string pathString, T body,
            Action<HttpRequestMessage> replay = null) where T : class
        {
            return await SendWithBodyAsync(client, HttpMethods.Put, pathString, body, replay);
        }

        private static async Task<HttpResponseMessage> SendWithoutBodyAsync(HttpClient client, string method,
            string pathString, Action<HttpRequestMessage> replay = null)
        {
            return await SendWithoutBodyAsync(client, method, Normalize(pathString), replay);
        }

        private static async Task<HttpResponseMessage> SendWithBodyAsync<T>(HttpClient client, string method,
            string pathString, T body, Action<HttpRequestMessage> replay = null) where T : class
        {
            return await SendWithBodyAsync(client, method, Normalize(pathString), body, replay);
        }

        private static async Task<HttpResponseMessage> SendWithoutBodyAsync(HttpClient client, string method,
            PathString pathString, Action<HttpRequestMessage> replay = null)
        {
            var requestUri = pathString.ToUriComponent();
            var request = new HttpRequestMessage(new HttpMethod(method), requestUri);
            replay?.Invoke(request);

            return await client.SendAsync(request);
        }

        private static async Task<HttpResponseMessage> SendWithBodyAsync<T>(HttpClient client, string method,
            PathString pathString, T body, Action<HttpRequestMessage> replay = null) where T : class
        {
            var requestUri = pathString.ToUriComponent();
            var request = new HttpRequestMessage(new HttpMethod(method), requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            };
            replay?.Invoke(request);

            return await client.SendAsync(request);
        }

        private static PathString Normalize(string pathString)
        {
            Contract.Assert(pathString != null);
            if (!pathString.StartsWith("/"))
                pathString = $"/{pathString}";

            if (!pathString.EndsWith("/"))
                pathString = $"{pathString}/";

            return pathString;
        }
    }
}
