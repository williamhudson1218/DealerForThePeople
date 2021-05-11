using DealerForThePeople.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerForThePeople
{
    public class Helpers
    {
        public static void PrintReviewsToConsole(List<Review> reviews)
        {
            reviews = reviews.OrderByDescending(x => x.Score).ToList();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(reviews[i].ToString());
            }
        }

        public static void GetReviewsFromHtml(HtmlDocument html, List<Review> reviews)
        {
            HtmlNodeCollection reviewsHtml = html.DocumentNode.SelectNodes("//div[contains(@class, 'review-entry')]");

            foreach (HtmlNode review in reviewsHtml)
            {
                HtmlDocument reviewHtml = new();
                reviewHtml.LoadHtml(review.OuterHtml);
                reviews.Add(ParseReview(reviewHtml));
            }
        }

        public static Review ParseReview(HtmlDocument reviewHtml)
        {
            try
            {
                return new Review()
                {
                    Username = reviewHtml.DocumentNode.SelectSingleNode("//span[contains(@class, 'font-18')]").InnerText.Replace("- ", ""),
                    Title = reviewHtml.DocumentNode.SelectSingleNode("//h3[contains(@class, 'no-format')]").InnerText,
                    Body = reviewHtml.DocumentNode.SelectSingleNode("//p[contains(@class, 'review-content')]").InnerText,
                    Rating = ParseRating(reviewHtml.DocumentNode.SelectSingleNode("//div[contains(@class, 'rating-static')]").OuterHtml),
                    Date = DateTime.Parse(reviewHtml.DocumentNode.SelectSingleNode("//div[contains(@class, 'review-date')]")?.ChildNodes[1].InnerText)
                };
            }
            catch
            {
                return null;
            }
        }

        public static int ParseRating(string outerHtml)
        {
            int indexEnd = outerHtml.IndexOf("pull-right") - 1;
            _ = int.TryParse(outerHtml.Substring(indexEnd - 2, 2), out int rating);
            return rating;
        }
    }
}
