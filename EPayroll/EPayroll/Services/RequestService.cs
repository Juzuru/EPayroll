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

        public TResult GetAsync<TResult>(string uri)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return Task.Run(async () =>
                {
                    using (response = httpClient.GetAsync(uri).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }).Result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public TResult PostAsync<TResult>(string uri, object dataModel)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(dataModel));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return Task.Run(async () =>
                {
                    using (response = httpClient.PostAsync(uri, content).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }).Result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public TResult PutAsync<TResult>(string uri, object dataModel)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(dataModel));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return Task.Run(async () =>
                {
                    using (response = httpClient.PutAsync(uri, content).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }).Result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public TResult DeleteAsync<TResult>(string uri, object dataModel)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return Task.Run(async () =>
                {
                    using (var response = httpClient.DeleteAsync(uri).GetAwaiter().GetResult())
                    {
                        return await Task.Run(async () => JsonConvert.DeserializeObject<TResult>(await EnsureSuccessStatusCode(response)));
                    }
                }).Result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Dispose();
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

        private void Dispose()
        {
            if (httpClient != null) httpClient.Dispose();
            if (response != null) response.Dispose();
        }
    }

    public interface IRequestService
    {
        TResult GetAsync<TResult>(string uri);
        TResult PostAsync<TResult>(string uri, object dataModel);
        TResult PutAsync<TResult>(string uri, object dataModel);
        TResult DeleteAsync<TResult>(string uri, object dataModel);
    }
}
