using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
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
        public string start_date { get; set; }
        public string end_date { get; set; }
        public double mean { get; set; }
        public int rank { get; set; }
        public int popularity { get; set; }
        public int num_list_users { get; set; }
        public int num_scoring_users { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string media_type { get; set; }
        public string status { get; set; }
        public List<Genre> genres { get; set; }
        public int num_episodes { get; set; }
        public StartSeason start_season { get; set; }
        public string rating { get; set; }
        public List<RelatedAnime> related_anime { get; set; }
        public Statistics statistics { get; set; }
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



    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    public class StartSeason
    {
        public int year { get; set; }
        public string season { get; set; }
    }

    public class RelatedAnime
    {
        public Node node { get; set; }
        public string relation_type { get; set; }
        public string relation_type_formatted { get; set; }
    }

    public class Status
    {
        public string watching { get; set; }
        public string completed { get; set; }
        public string on_hold { get; set; }
        public string dropped { get; set; }
        public string plan_to_watch { get; set; }
    }

    public class Statistics
    {
        public Status status { get; set; }
        public int num_list_users { get; set; }
    }

    public class AnimeDetail
    {
        public int id { get; set; }
        public string title { get; set; }
        public MainPicture main_picture { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public double mean { get; set; }
        public int rank { get; set; }
        public int popularity { get; set; }
        public int num_list_users { get; set; }
        public int num_scoring_users { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string media_type { get; set; }
        public string status { get; set; }
        public List<Genre> genres { get; set; }
        public int num_episodes { get; set; }
        public StartSeason start_season { get; set; }
        public string rating { get; set; }
//        public List<RelatedAnime> related_anime { get; set; }
        public Statistics statistics { get; set; }

        //public ListClasses ListClasses;


        //public void InitializeDate()
        //{
        //    Json.Provide(out ListClasses listClassesObject);
        //    ListClasses = listClassesObject;
        //}

        //public void newAnime(AnimeDetail animeDetail)
        //{
        //    ListClasses.data.Add(animeDetail);
        //    Json.WriteDown(ListClasses);
        //    Console.WriteLine("anime was added");
        //}

    }


    [Serializable]
    public class ListClasses
    {
        public List<AnimeDetail> data { get; set; }


        public ListClasses()
        {
            data = new List<AnimeDetail>();
        }
    }

}
