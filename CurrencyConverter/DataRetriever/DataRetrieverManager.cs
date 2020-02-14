using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace DataRetriever
{
    public class DataRetrieverManager
    {
        /// <summary>
        /// Retrieves JSON data from API and maps it to .NET class asynchronous 
        /// </summary>
        /// <typeparam name="T"> .NET class to map JSON data </typeparam>
        /// <param name="url"> Url to the API </param>
        /// <returns> Mapped JSON data to .NET object </returns>
        public async Task<T> DownloadSerializedJsonDataAsync<T>(string url) where T : new()
        {
            using (var webClient = new WebClient())
            {
                var jsonData = string.Empty;
                // try to download JSON data as a string 
                try
                {
                    jsonData = await webClient.DownloadStringTaskAsync(url);
                }
                catch (Exception)
                {
                    string messageBoxText = "There is no internet connection or API does not respond.";
                    MessageBox.Show(messageBoxText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // if string with JSON data is not empty, deserialize it to class and return its instance
                // for JSON deserialization I used Json.NET library because it provides an easy and efficient way to serialize/deserialize data from JSON to .NET and back
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
            }
        }

    }
}
