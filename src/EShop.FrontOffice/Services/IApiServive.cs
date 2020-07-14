using System.Net.Http;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public interface IApiServive
    {
        public Task<TResult> SendAsync<TResult>(HttpRequestMessage request);
    }
}
