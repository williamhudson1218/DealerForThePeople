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
            Console.WriteLine("Fetching Reviews....");
            List<HtmlDocument> htmlDocList = ReviewLogic.GetReviews(SettingsLogic.GetURL());
            List<Review> reviews = ReviewLogic.ParseReviews(htmlDocList);
            ScoreLogic.CalculateScores(reviews);
            ReviewLogic.PrintReviewsToConsole(reviews);

            Console.WriteLine("Press enter to close.");
            Console.ReadLine();
        }
    }
}
