using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Le001
{
    class Program
    {
        static string SavePath = "result.txt";
        static readonly HttpClient client = new HttpClient();
        private static readonly CancellationTokenSource cts = new CancellationTokenSource();

        static string Way = "https://jsonplaceholder.typicode.com/posts/";

        static void Main(string[] args)
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
            else
            {
                File.WriteAllText(SavePath, Environment.NewLine);
            }

            ExecuteAsync(); 
            
            Console.ReadKey();
        }

        static async void ExecuteAsync()
        {
            for ( int i = 4; i < 14; ++i )
            {
                BlogData CurrentData = await ExecuteOnceAsync(i);
                await SaviorAsync( CurrentData );

                ShouwarAsync( CurrentData );
            }

            Task.WaitAll();
            
            Console.WriteLine("To exit press any buuton");
        }
        
        static async Task<BlogData> ExecuteOnceAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{Way}{id}");                
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                BlogData NewData = JsonSerializer.Deserialize<BlogData>(responseBody);
                cts.CancelAfter(100);
                return NewData;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Cannot get data from URL");
            }
            return null;
        }
        static async Task SaviorAsync( BlogData someData )
        {
            try
            {
                await File.AppendAllTextAsync(SavePath, $"{someData.userId}\n");
                await File.AppendAllTextAsync(SavePath, $"{someData.id}\n");
                await File.AppendAllTextAsync(SavePath, $"{someData.title}\n");
                await File.AppendAllTextAsync(SavePath, $"{someData.body}\n\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot save file");
                throw;
            }            
        }

        static async void  ShouwarAsync( BlogData someData )
        {
            Console.WriteLine($"{someData.userId}");
            Console.WriteLine($"{someData.id}");
            Console.WriteLine($"{someData.title}");
            Console.WriteLine($"{someData.body}");
            Console.WriteLine();
        }
    }

    class BlogData
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
