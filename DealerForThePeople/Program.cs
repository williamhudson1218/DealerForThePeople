using System;
using System.Collections.Generic;
using DealerForThePeople.Controller;
using DealerForThePeople.Model;
using HtmlAgilityPack;

namespace DealerForThePeople
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<HtmlDocument> htmlDocList = ReviewBO.GetReviews(SettingsBO.GetURL());
            List<Review> reviews = ReviewBO.ParseReviews(htmlDocList);
            ReviewBO.PrintReviewsToConsole(reviews);

            Console.WriteLine("Press enter to close.");
            Console.ReadLine();
        }
    }
}
