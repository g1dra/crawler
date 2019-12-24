using System;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace crawler
{
    class Program
    {

        async static Task Main(string[] args)
        {
            //get page
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://api.opendota.com/api/heroes");
            var content = result.Content;
            Console.WriteLine($"Result je {content}");
        }
    }
}
