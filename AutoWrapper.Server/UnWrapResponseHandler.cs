using AutoWrapper.Server.Helpers;
using AutoWrapper.Server.Wrapper;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AutoWrapper.Server
{
    public class UnWrapResponseHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            return await UnWrapResponse(request, response);
        }

        private static async Task<HttpResponseMessage> UnWrapResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return response;
            }

            var contentResult = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ApiResponse>(contentResult, JsonSettings.JsonDeserializerSettings());

            var unwrappedResponse = new HttpResponseMessage(response.StatusCode)
            {
                Content = new StringContent(JsonSerializer.Serialize(data.Result), Encoding.UTF8, "application/json")
            };

            foreach (var header in response.Headers)
            {
                unwrappedResponse.Headers.Add(header.Key, header.Value);
            }

            return unwrappedResponse;
        }
    }
}
