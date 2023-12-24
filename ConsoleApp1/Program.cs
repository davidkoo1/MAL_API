using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ConsoleApp1;

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


public class AnimeList
{
    public List<AnimeDetail> Animes { get; set; } = new List<AnimeDetail>();
}

class Program
{
    static async Task Main(string[] args)
    {
        const string clientId = "16068a9a68e0e82b5318ee9ae9aa8ba5";
        var handler = new MalClientIDHandler(clientId);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.myanimelist.net/v2/")
        };

        // Проверяем наличие файла и загружаем из него данные
        AnimeList animeList;
        if (File.Exists("Anime.json"))
        {
            var existingJson = File.ReadAllText("Anime.json");
            animeList = JsonConvert.DeserializeObject<AnimeList>(existingJson) ?? new AnimeList();
        }
        else
        {
            animeList = new AnimeList();
        }

        const int limit = 500;
        string[] seasons = { "winter", "spring", "summer", "fall" };
        for (int start = 2012; start < 2024; start++)
        {
            for (int i = 0; i < seasons.Length; i++)
            {
                bool isFlag = true;
                while (isFlag)
                {
                    var queryParams = $"/season/{start}/{seasons[i]}?limit={limit}";
                    var response = await httpClient.GetAsync($"anime{queryParams}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseAnime = await response.Content.ReadAsAsync<BaseResponse>();
                        Console.WriteLine(seasons[i] + " - " + responseAnime.data.Count());

                        int j = 0;
                        foreach (var item in responseAnime.data)
                        {
                            var myListStatusEndpoint = $"anime/{item.node.id}?fields=id,title,start_date,end_date,mean,rank,popularity,num_list_users,num_scoring_users,created_at,updated_at,media_type,status,genres,num_episodes,start_season,rating,statistics";

                            var responseDetail = await httpClient.GetAsync(myListStatusEndpoint);
                            try
                            {
                                if (responseDetail.IsSuccessStatusCode)
                                {
                                    var animeDetail = await responseDetail.Content.ReadAsAsync<AnimeDetail>();
                                    animeList.Animes.Add(animeDetail);

                                    if (j == 45)
                                    {
                                        await Console.Out.WriteLineAsync(new string('=', 300));
                                        Thread.Sleep(60000);

                                        var json = JsonConvert.SerializeObject(animeList, Formatting.Indented);
                                        File.WriteAllText("Anime.json", json);

                                        j = 0;
                                    }
                                    j++;
                                }
                                else
                                {
                                    Console.WriteLine($"Error: {responseDetail.StatusCode}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }

                        if (responseAnime.data.Count() < 500)
                            isFlag = false;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        break;
                    }
                }
            }
        }

    }
}






/*
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




        var animeId = 17074;
        var myListStatusEndpoint = $"anime/5114?fields=id,title,main_picture,start_date,end_date,mean,rank,popularity,num_list_users,num_scoring_users,created_at,updated_at,media_type,status,genres,num_episodes,start_season,rating,related_anime,statistics";


        var response = await httpClient.GetAsync(myListStatusEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsAsync<AnimeDetail>();
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
}*/