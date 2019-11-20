using AutoWrapper.Server.Helpers;
using AutoWrapper.Server.Wrapper;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NS = Newtonsoft.Json;
using DN = System.Text.Json;

namespace AutoWrapper.Server
{
    public class UnwrappingResponseHandler : DelegatingHandler
    {
        private readonly string _propertyToUnwrap = string.Empty;
        public UnwrappingResponseHandler(string propertyToUnwrap)
        {
            _propertyToUnwrap = propertyToUnwrap;
        }

        public UnwrappingResponseHandler(){}

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            return await UnWrapResponse(_propertyToUnwrap, response);
        }

        private static async Task<HttpResponseMessage> UnWrapResponse(string propertyToUnwrap, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return response;
            }

            var contentResult = await response.Content.ReadAsStringAsync();

            AutoWrapperResponse data = new AutoWrapperResponse();
            string content = string.Empty;
            if (string.IsNullOrEmpty(propertyToUnwrap))
            {
                data = DN.JsonSerializer.Deserialize<AutoWrapperResponse>(contentResult, JsonSettings.DotNetJsonSettings());
                content = DN.JsonSerializer.Serialize(data.Result);
            }

            else
            {
                data = NS.JsonConvert.DeserializeObject<AutoWrapperResponse>(contentResult, JsonSettings.NewtonsoftJsonSettings(propertyToUnwrap));
                content = NS.JsonConvert.SerializeObject(data.Result);
            }
       

            var unwrappedResponse = new HttpResponseMessage(response.StatusCode)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            foreach (var header in response.Headers)
            {
                unwrappedResponse.Headers.Add(header.Key, header.Value);
            }

            return unwrappedResponse;
        }
    }
}
