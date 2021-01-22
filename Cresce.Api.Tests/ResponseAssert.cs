using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public static class ResponseAssert
    {
        public static async Task ListAreEquals<T>(T expect, HttpResponseMessage response)
            where T : IEnumerable
        {
            CollectionAssert.AreEqual(expect, await response.GetContent<T>());
        }
    }
}