using System.Net.Http;
using System.Threading.Tasks;

namespace Cresce.Api.Tests
{
    public static class ResponseExtensions
    {
        public static Task<T> GetContent<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<T>();
        }
    }
}