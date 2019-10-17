using System;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace crawler
{
    class Program
    {

        async static Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://liquipedia.net/dota2/Dota_Pro_Circuit/2018-19/Rankings");
            var pageContents = await response.Content.ReadAsStringAsync();

            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var nameNodes = pageDocument.DocumentNode.SelectNodes("(//span[contains(@class,'team-template-text')])//a");
            List<string> elements = new List<string>();

            foreach (var node in nameNodes)
            {
                elements.Add(node.Attributes["title"].Value);
            }

           

            var valueNodes = pageDocument.DocumentNode.SelectNodes("(//table[contains(@class,'wikitable')])//td//b");
            for (int i = 0; i < valueNodes.Count; i++)
            {
                if(i%2!=0)
                {
                    Console.WriteLine(valueNodes[i].InnerHtml);
                }
            }

            Console.ReadLine();

        }
    }
}
