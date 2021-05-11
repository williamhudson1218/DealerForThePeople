﻿using DealerForThePeople.Model;
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
        /// <summary>
        /// Orders the reviews by their score (highest first) and then Prints the first 3 into the conosle for user to see
        /// </summary>
        /// <param name="reviews">List of 'Review' objects</param>
        public static void PrintReviewsToConsole(List<Review> reviews)
        {
            reviews = reviews.OrderByDescending(x => x.Score).ToList();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(reviews[i].ToString());
            }
        }

        /// <summary>
        /// Takes in the html document appends to list of reviews.
        /// </summary>
        /// <param name="html">HtmlDocument from HtmlAgilityPack nuget package.</param>
        /// <param name="reviews">List of 'Review' objects to append to.</param>
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

        /// <summary>
        /// Takes in a single HtmlDocument, parses into the 'Review' class.
        /// </summary>
        /// <param name="reviewHtml">HtmlDocument reprenting a single review.</param>
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

        /// <summary>
        /// Takes in a string. Rating is calculated based on class name
        /// i.e. rating-50 = 5/5, rating-48 = 4.8 etc
        /// </summary>
        /// <param name="outerHtml">string representing the div tag in which the rating is contained</param>
        /// <returns>The integer value contained in the classname. i.e. 48, 50 etc</returns>
        public static int ParseRating(string outerHtml)
        {
            int indexEnd = outerHtml.IndexOf("pull-right") - 1;
            _ = int.TryParse(outerHtml.Substring(indexEnd - 2, 2), out int rating);
            return rating;
        }
    }
}
