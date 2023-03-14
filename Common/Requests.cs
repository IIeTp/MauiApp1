using System.Net.Http.Json;
using System.Text.Json;

namespace MauiApp1.Common {
    public static class Requests {
        private static readonly int AppVersion = 2182;
        private static readonly string Session = "32e1f3811863076944_1674148736-563660217FhQRL782hs3nA1tujX48hw";

        public static async Task<T> IviSearch<T>(string searchTerm) =>
            await GetDeserializedResponseAsync<T>(BuildUrl("https://api.ivi.ru/mobileapi/autocomplete/common/v7/", searchTerm, "query", "id,name,object_type,title,year,years,orig_title,has_5_1"));

        public static async Task<T> KinopoiskSearch<T>(string searchTerm) {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 7.1.1; SHIELD Android TV Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.125 Safari/537.36");
            var responseJson = await httpClient.GetStringAsync($"https://api.ott.kinopoisk.ru/v13/suggested-data?serviceId=25&withIntro=true&query={searchTerm}");
            return JsonSerializer.Deserialize<T>(responseJson);
        }

        public static async Task<T> IviTvshow<T>(string searchTerm) =>
            await GetDeserializedResponseAsync<T>(BuildUrl("https://api.ivi.ru/mobileapi/videofromcompilation/v7/", searchTerm, "id", $"fake=true&session={Session}&season=1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25&from=0&to=100"));

        public static async Task<T> IviEpisode<T>(int id) {
            var payload = new {
                @params = new object[] {
                    id,
                    new {
                        app_version = AppVersion,
                        session = Session
                    }
                },
                method = "da.content.get"
            };

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("https://api.ivi.ru/light/", payload);

            if (response == null) {
                Console.WriteLine("Error: The response is null.");
                return default;
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseJson);
        }

        private static async Task<T> GetDeserializedResponseAsync<T>(string url) {
            using var httpClient = new HttpClient();
            var responseJson = await httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<T>(responseJson);
        }

        private static string BuildUrl(string baseUrl, string searchTerm, string queryParam, string fields) {
            if (string.IsNullOrEmpty(baseUrl)) {
                throw new ArgumentException("Error: The base URL is null or empty.", nameof(baseUrl));
            }
            if (string.IsNullOrEmpty(searchTerm)) {
                throw new ArgumentException("Error: The search term is null or empty.", nameof(searchTerm));
            }

            // Set the query string
            var queryString = $"?{queryParam}={searchTerm}&fields={fields}&app_version={AppVersion}";
            return baseUrl + queryString;
        }
    }
}
