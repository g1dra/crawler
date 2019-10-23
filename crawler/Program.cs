using System;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace crawler
{
    class Program
    {

        async static Task Main(string[] args)
        {
            //get page
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://liquipedia.net/dota2/Dota_Pro_Circuit/2018-19/Rankings");
            var pageContents = await response.Content.ReadAsStringAsync();

            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            //get team names
            var nameNodes = pageDocument.DocumentNode.SelectNodes("(//span[contains(@class,'team-template-text')])//a");
            List<string> teamNamesList = new List<string>();
            List<string> teamDpcList = new List<string>();

            //add team names to list
            foreach (var node in nameNodes)
            {
                teamNamesList.Add(node.Attributes["title"].Value);
            }

            //add team dpc point to list
            var valueNodes = pageDocument.DocumentNode.SelectNodes("(//table[contains(@class,'wikitable')])//td//b");
            for (int i = 0; i < valueNodes.Count; i++)
            {
                if (i%2!=0)
                {
                    teamDpcList.Add(valueNodes[i].InnerHtml);
                }
            }




            //merge to dictionary
            var dictionary = teamNamesList.Zip(teamDpcList, (k, v) => new { Key = k, Value = v })
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var d in dictionary)
            {
                Console.WriteLine($"key je : {d.Key}, value je: {d.Value}");
            }
        }
    }
}
