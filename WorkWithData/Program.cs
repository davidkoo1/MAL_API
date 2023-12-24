using Newtonsoft.Json;
using System.Text;

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
Console.WriteLine("Total anime: " + animeList.Animes.Count());

string answer = "";
string[] answerTypes = { "no", "n" };

do
{

    Console.Write("Input needed user score: ");
    int selectVoteNum = int.Parse(Console.ReadLine());
    Console.Write("Input season year: ");
    int selectYear_Num = int.Parse(Console.ReadLine());


    var animesWithFilter = animeList.Animes
        .Where(x => x.num_scoring_users >= selectVoteNum
        && x.media_type == "tv"
        && x.num_episodes < 50
        && x.status == "finished_airing"
        && x.start_season.year == selectYear_Num)
        .DistinctBy(x => x.title) // Удаление повторяющихся элементов
        /*.OrderBy(x => x.num_scoring_users)*/
        .ToList();

    Random random = new Random();
    var randomAnime = animesWithFilter[random.Next(animesWithFilter.Count)];

    Console.WriteLine("Filter anime: " + animesWithFilter.Count());

    

    StringBuilder sb = new StringBuilder();
    sb.AppendLine(new string('=', 50));
    sb.AppendLine($"===================={randomAnime.title}======================");
    sb.AppendLine(new string('=', 50));
    sb.AppendLine($"ID: {randomAnime.id}");
    sb.AppendLine($"Title: {randomAnime.title}");
    sb.AppendLine($"Start Date: {randomAnime.start_date}");
    sb.AppendLine($"End Date: {randomAnime.end_date}");
    sb.AppendLine($"Mean Rating: {randomAnime.mean}");
    sb.AppendLine($"Rank: {randomAnime.rank}");
    sb.AppendLine($"Popularity: {randomAnime.popularity}");
    sb.AppendLine($"Number of List Users: {randomAnime.num_list_users}");
    sb.AppendLine($"Number of Scoring Users: {randomAnime.num_scoring_users}");
    sb.AppendLine($"Created At: {randomAnime.created_at}");
    sb.AppendLine($"Updated At: {randomAnime.updated_at}");
    sb.AppendLine($"Media Type: {randomAnime.media_type}");
    sb.AppendLine($"Status: {randomAnime.status}");
    sb.AppendLine($"Number of Episodes: {randomAnime.num_episodes}");
    sb.AppendLine($"Start Season: {randomAnime.start_season.year} {randomAnime.start_season.season}");
    sb.AppendLine($"Rating: {randomAnime.rating}");

    sb.AppendLine("Genres:");
    foreach (var genre in randomAnime.genres)
    {
        sb.AppendLine($" - {genre.name}");
    }

    sb.AppendLine("Statistics:");
    sb.AppendLine($" - Watching: {randomAnime.statistics.status.watching}");
    sb.AppendLine($" - Completed: {randomAnime.statistics.status.completed}");
    sb.AppendLine($" - On Hold: {randomAnime.statistics.status.on_hold}");
    sb.AppendLine($" - Dropped: {randomAnime.statistics.status.dropped}");
    sb.AppendLine($" - Plan to Watch: {randomAnime.statistics.status.plan_to_watch}");

    sb.AppendLine("Main Picture:");
    sb.AppendLine($" - Medium: {randomAnime.main_picture.medium}");
    sb.AppendLine($" - Large: {randomAnime.main_picture.large}");

    sb.AppendLine(new string('=', 50));
    sb.AppendLine(new string('=', 50));


    Console.WriteLine(sb.ToString());

    //AllFilterAnime
    /*
     foreach (var animeItem in animesWithFilter)
    {
        Console.WriteLine("==========================================");
        Console.WriteLine($"ID: {animeItem.id}");
        Console.WriteLine($"Title: {animeItem.title}");
        Console.WriteLine($"Start Date: {animeItem.start_date}");
        Console.WriteLine($"End Date: {animeItem.end_date}");
        Console.WriteLine($"Mean Rating: {animeItem.mean}");
        Console.WriteLine($"Rank: {animeItem.rank}");
        Console.WriteLine($"Popularity: {animeItem.popularity}");
        Console.WriteLine($"Number of List Users: {animeItem.num_list_users}");
        Console.WriteLine($"Number of Scoring Users: {animeItem.num_scoring_users}");
        Console.WriteLine($"Created At: {animeItem.created_at}");
        Console.WriteLine($"Updated At: {animeItem.updated_at}");
        Console.WriteLine($"Media Type: {animeItem.media_type}");
        Console.WriteLine($"Status: {animeItem.status}");
        Console.WriteLine($"Number of Episodes: {animeItem.num_episodes}");
        Console.WriteLine($"Start Season: {animeItem.start_season.year} {animeItem.start_season.season}");
        Console.WriteLine($"Rating: {animeItem.rating}");

        Console.WriteLine("Genres:");
        foreach (var genre in animeItem.genres)
        {
            Console.WriteLine($"- {genre.name}");
        }

        Console.WriteLine("Statistics:");
        Console.WriteLine($"- Watching: {animeItem.statistics.status.watching}");
        Console.WriteLine($"- Completed: {animeItem.statistics.status.completed}");
        Console.WriteLine($"- On Hold: {animeItem.statistics.status.on_hold}");
        Console.WriteLine($"- Dropped: {animeItem.statistics.status.dropped}");
        Console.WriteLine($"- Plan to Watch: {animeItem.statistics.status.plan_to_watch}");

        Console.WriteLine("Main Picture:");
        Console.WriteLine($"- Medium: {animeItem.main_picture.medium}");
        Console.WriteLine($"- Large: {animeItem.main_picture.large}");

        Console.WriteLine("==========================================\n\n");
    }

     */

    Console.Write("\n\nOne more time? (y/n): ");
    answer = Console.ReadLine();

} while (!answerTypes.Contains(answer.ToLower()));


