using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeturAssessment.Services
{
    public static class HttpClientExtension
    {
        public static async Task<TInput> SendAsync<TInput>(this HttpClient client, HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            using var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;

            var byteArray = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var encodingResult = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

            if (!response.IsSuccessStatusCode)
                throw new Exception(encodingResult);

            if (response.Content == null)
                return default;
            var data = JsonConvert.DeserializeObject<TInput>(encodingResult);
            return data;
        }
    }
}
