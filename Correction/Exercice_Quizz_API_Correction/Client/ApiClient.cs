using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_Quizz_API.Client
{
    public partial class ApiClient
    {

        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; set; }

        public ApiClient(Uri baseEndpoint)
        {
            BaseEndpoint = baseEndpoint ?? throw new ArgumentNullException("baseEndpoint");
            _httpClient = new HttpClient();
        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            //addHeaders();
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private async Task<ICollection<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            //addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ICollection<T>>(data);
        }
        private async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            //addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }

        /// <summary>
        /// Common method for making PUT calls
        /// </summary>
        private async Task<ICollection<T>> PutAsync<T>(Uri requestUrl, T content)
        {
            //addHeaders();
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            string[] dataStringArrayLeft = data.Split('[');
            string[] dataStringFinal = dataStringArrayLeft[1].Split(']');
            return JsonConvert.DeserializeObject<ICollection<T>>("[" + dataStringFinal.First() + "]");
        }
        private async Task<T1> PutAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            //addHeaders();
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }

        /// <summary>
        /// Common method for making DELETE calls
        /// </summary>
        private async Task<ICollection<T>> DeleteAsync<T>(Uri requestUrl, T content)
        {
            //addHeaders();
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ICollection<T>>(data);
        }


        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

    }
}
