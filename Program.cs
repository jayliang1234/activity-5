using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Activity5
{

    class gender
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("probability")]
        public float Probability { get; set; }

    }


    class Program
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
                    Console.WriteLine("Enter any name. Press Enter without writing a name to quit the program.");
                    var name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://api.genderize.io/?name=" + name.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var name_ = JsonConvert.DeserializeObject<gender>(resultRead);

                    Console.WriteLine("___");
                    Console.WriteLine("Count #" + name_.Count);
                    Console.WriteLine("Name: " + name_.Name);
                    Console.WriteLine("Gender:" + name_.Gender);
                    Console.WriteLine("Propability: " + name_.Probability);
                    Console.WriteLine("\n---");
                }
                catch
                {
                    Console.WriteLine("ERROR, Please enter a valid Pokemon name!");
                }
            }
        }
    }
}
