using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChristiansOsloBySykkelClient
{
    internal class CustomHttpClient
    {
        Uri _endpoint;
        HttpClient _client;
        string _clientKey;

        //todo: put url in config file
        internal CustomHttpClient()
        {
            _endpoint = new Uri("https://oslobysykkel.no/api/v1/stations/");
            CreateClient();
        }

        internal CustomHttpClient(Uri endpoint)
        {
            _endpoint = endpoint;
            CreateClient();
        }

        private void CreateClient()
        {
            _client = new HttpClient();
            _clientKey = SetupClientKey();
        }

        //todo: move from text file to config file.
        private string SetupClientKey()
        {
            var execPath = AppDomain.CurrentDomain.BaseDirectory;
            var key = File.ReadAllText(execPath + "ClientKey.txt");
            return key;
        }

        internal async Task<string> GetBikeShedsAsync()
        {
            return await GetjsonAsync(_endpoint);
        }
        internal async Task<string> GetAvailabilitysAsync()
        {
            return await GetjsonAsync(new Uri(_endpoint.AbsoluteUri + "availability/"));
        }

        internal async Task<string> GetjsonAsync(Uri uri)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri);
            req.Headers.Add("Client-Identifier", _clientKey);

            HttpResponseMessage response = await _client.SendAsync(req);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("The call to " + uri.AbsoluteUri + " failed with code " + response.StatusCode.ToString() + ".");
            Stream content = await response.Content.ReadAsStreamAsync();

            var sr = new StreamReader(content);
            string data = sr.ReadToEnd();
            return data;
        }

    }
}
