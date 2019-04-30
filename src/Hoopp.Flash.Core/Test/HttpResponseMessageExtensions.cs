using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OHTN.Runtime.Test.InteractionTests.Helpers
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpResponseMessage response) where T : class
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }
    }
}
