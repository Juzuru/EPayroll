using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EPayroll.Services
{
    public class RequestService : IRequestService
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            try
            {
                using (httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (response = httpClient.GetAsync(new Uri(uri)).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<TResult> PostAsync<TResult>(string uri, object dataModel)
        {
            try
            {
                using (httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    StringContent content = new StringContent(JsonConvert.SerializeObject(dataModel));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (response = httpClient.PostAsync(new Uri(uri), content).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TResult> PutAsync<TResult>(string uri, object dataModel)
        {
            try
            {
                using (httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(dataModel));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (response = httpClient.PutAsync(new Uri(uri), content).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TResult> DeleteAsync<TResult>(string uri, object dataModel)
        {
            try
            {
                using (httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = httpClient.DeleteAsync(new Uri(uri)).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
                return content;
            }
            catch (Exception)
            {
                throw new HttpRequestException(content);
            }
        }
    }

    public interface IRequestService
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TResult>(string uri, object dataModel);
        Task<TResult> PutAsync<TResult>(string uri, object dataModel);
        Task<TResult> DeleteAsync<TResult>(string uri, object dataModel);
    }
}
