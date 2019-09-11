using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_Quizz_API_Vierge.Client
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private async Task<ICollection<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            throw new NotImplementedException();
        }
        private async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Common method for making PUT calls
        /// </summary>
        private async Task<ICollection<T>> PutAsync<T>(Uri requestUrl, T content)
        {
            throw new NotImplementedException();
        }
        private async Task<T1> PutAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Common method for making DELETE calls
        /// </summary>
        private async Task<ICollection<T>> DeleteAsync<T>(Uri requestUrl, T content)
        {
            throw new NotImplementedException();
        }


        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            throw new NotImplementedException();
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            throw new NotImplementedException();
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        
    }
}
