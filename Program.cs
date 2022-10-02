using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace webAPIClient
{
    class Anime
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("director")]
        public string Director { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    class hao
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Anime ID. Press Enter without writing a name to quit the program. ");

                    var AnimeID = Console.ReadLine();

                    if (string.IsNullOrEmpty(AnimeID))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://ghibliapi.herokuapp.com/films/" + AnimeID.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var anime = JsonConvert.DeserializeObject<Anime>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Anime ID: " + anime.Id);
                    Console.WriteLine("Name: " + anime.Title);
                    Console.WriteLine("Picture: " + anime.Image);
                    Console.WriteLine("Director " + anime.Director);
                    Console.WriteLine("Description:\n " + anime.Description);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Please enter a valid Anime ID!");
                }
            }
        }
    }
}



