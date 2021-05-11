using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DealerForThePeople.Model;
using HtmlAgilityPack;

namespace DealerForThePeople
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Review> reviews = new();

            for (int i = 0; i < 5; i++)
            {
                HtmlDocument htmlDoc = new();
                using (WebClient client = new())
                {
                    string htmlString = client.DownloadString($"https://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/page{i + 1}/?filter=#link");
                    htmlDoc.LoadHtml(htmlString);
                }
                Helpers.GetReviewsFromHtml(htmlDoc, reviews);
            }

            Helpers.PrintReviewsToConsole(reviews);

            Console.WriteLine("Press enter to close.");
            Console.ReadLine();
        }
    }
}
