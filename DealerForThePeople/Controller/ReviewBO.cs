using DealerForThePeople.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DealerForThePeople.Controller
{
    public static class ReviewBO
    {
        public static List<HtmlDocument> GetReviews(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            List<HtmlDocument> docList = new();

            for (int i = 0; i < 5; i++)
            {
                HtmlDocument htmlDoc = new();
                using (WebClient client = new())
                {
                    if (url.Contains("page"))
                    {
                        int pageIndex = url.IndexOf("page");
                        StringBuilder sb = new StringBuilder(url);
                        sb.Remove(pageIndex, 5).Insert(pageIndex, $"page{i + 1}");
                        url = sb.ToString();
                    }
                    else
                        i = 4;

                    string htmlString = "";
                    try
                    {
                        htmlString = client.DownloadString(url);
                    }
                    catch 
                    {
                        //this means the url is bad
                        return docList;
                    };

                    if (string.IsNullOrWhiteSpace(htmlString))
                        continue;

                    htmlDoc.LoadHtml(htmlString);
                }
                docList.Add(htmlDoc);
            }
            return docList;
        }

        /// <summary>
        /// Orders the reviews by their score (highest first) and then Prints the first 3 into the conosle for user to see
        /// </summary>
        /// <param name="reviews">List of 'Review' objects</param>
        public static void PrintReviewsToConsole(List<Review> reviews)
        {
            reviews = reviews.OrderByDescending(x => x.Score).ToList();
            for (int i = 0; i < 3; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Username: {reviews[i].Username}");
                sb.Append(Environment.NewLine);
                sb.Append($"Date: {reviews[i].Date.ToString("MM/dd/yyyy")}");
                sb.Append(Environment.NewLine);
                sb.Append($"Rating: {reviews[i].Rating / 10}/5");
                sb.Append(Environment.NewLine);
                sb.Append($"Title: {reviews[i].Title}");
                sb.Append(Environment.NewLine);
                sb.Append($"Body: {reviews[i].Body}");
                sb.Append(Environment.NewLine);
                sb.Append($"Score: {reviews[i].Score}");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                sb.Append(Environment.NewLine);
                Console.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// Takes in the html document appends to list of reviews.
        /// </summary>
        /// <param name="html">HtmlDocument from HtmlAgilityPack nuget package.</param>
        public static List<Review> ParseReviews(List<HtmlDocument> docList)
        {
            List<Review> reviews = new();
            foreach (HtmlDocument html in docList)
            {
                HtmlNodeCollection reviewsHtml = html.DocumentNode.SelectNodes("//div[contains(@class, 'review-entry')]");

                foreach (HtmlNode review in reviewsHtml)
                {
                    HtmlDocument reviewHtml = new();
                    reviewHtml.LoadHtml(review.OuterHtml);
                    Review r = ParseReview(reviewHtml);
                    r.Score = ScoreBO.CalculateScore(r);
                    reviews.Add(r);
                }
            }
            return reviews;
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
