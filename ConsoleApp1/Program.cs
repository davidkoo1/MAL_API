using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MalClientIDHandler : DelegatingHandler
{
    private readonly string _clientId;

    public MalClientIDHandler(string clientId, HttpMessageHandler innerHandler = null)
        : base(innerHandler ?? new HttpClientHandler())
    {
        _clientId = clientId;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-MAL-CLIENT-ID", _clientId);
        return await base.SendAsync(request, cancellationToken);
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        const string clientId = "d98a83e56b08ea0c546ac5b1393687d5";
        var handler = new MalClientIDHandler(clientId);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.myanimelist.net/v2/")
        };




        // Set query parameters
        const string query = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var queryParams = $"?q={query}&limit=10"; // Добавьте или удалите другие параметры по мере необходимости

        var response = await httpClient.GetAsync($"anime{queryParams}");



        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
}

/*class Program
{
    static async Task Main(string[] args)
    {
        const string clientId = "d98a83e56b08ea0c546ac5b1393687d5";
        var handler = new MalClientIDHandler(clientId);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.myanimelist.net/v2/")
        };




        var animeId = 17074;
        var myListStatusEndpoint = $"anime/{animeId}/my_list_status";

        var requestBody = new
        {
            status = "watching",
            num_episodes_watched = 73,
            score = 8,
            comments = "You wa shock!",
            start_date = "2022-02-20",
            finish_date = "" // пустая строка для удаления даты
        };

        var json = JsonSerializer.Serialize(requestBody);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PatchAsync(myListStatusEndpoint, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
}*/