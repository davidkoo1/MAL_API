using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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



public class MainPicture
{
    public string medium { get; set; }
    public string large { get; set; }
}

public class Node
{
    public int id { get; set; }
    public string title { get; set; }
    public MainPicture main_picture { get; set; }
}

public class NodeWrapper
{
    public Node node { get; set; }
}

public class Data
{
    public List<NodeWrapper> data { get; set; }
}

public class Paging
{
    public string next { get; set; }
}

public class Season
{
    public int year { get; set; }
    public string season { get; set; }
}

public class BaseResponse
{
    public List<NodeWrapper> data { get; set; }
    public Paging paging { get; set; }
    public Season season { get; set; }
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

        const int limit = 5; // Max value is 100
        int offset = 0;
        var queryParams = $"/season/2017/summer?limit={limit}&offset={offset}"; // Добавьте или удалите другие параметры по мере необходимости

        /*
        using (var httpClientAnime = new HttpClient(handler))
        {

            var apiUrl = "https://api.myanimelist.net/v2/anime" + queryParams;

            var responseAnime = await httpClientAnime.GetAsync(apiUrl);

            if (responseAnime.IsSuccessStatusCode)
            {
                var tmp = await responseAnime.Content.ReadAsAsync<AnimeData>();
                foreach (var item in tmp.Data)
                {
                    await Console.Out.WriteLineAsync(item.Id + "\t" + item.Title);
                }
            }
        }
        */

        // while (true) // Keep fetching until no more anime are returned
         //{
           //  var queryParams = $"/season/2017/summer?limit={limit}&offset={offset}";
             var response = await httpClient.GetAsync($"anime{queryParams}");

             if (response.IsSuccessStatusCode)
             {
                 var responseContent = await response.Content.ReadAsAsync<BaseResponse>();
                 Console.WriteLine(responseContent);

                 offset += limit; // Move to the next set of anime
             }
             else
             {
                 Console.WriteLine($"Error: {response.StatusCode}");
                 //break;
             }
         //}
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