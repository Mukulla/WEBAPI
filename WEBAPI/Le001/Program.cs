using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;


namespace Le001
{
    class Program
    {        
        static readonly HttpClient client = new HttpClient();
        private static readonly CancellationTokenSource cts = new CancellationTokenSource();

        static string Way = "https://jsonplaceholder.typicode.com/posts/";

        static void Main(string[] args)
        {            
            Execute();
            Console.Read();
        }

        static void Execute()
        {
            for ( int i = 4; i < 14; ++i )
            {
                ExecuteOnce(i);
                Console.WriteLine();
            }
        }
        
        static async void ExecuteOnce(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{Way}{id}");                
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);                
                cts.CancelAfter(100);
                return;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
